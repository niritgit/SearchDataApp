using HtmlAgilityPack;
using SearchDataApp.Models;
using SearchDataApp.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchDataApp
{
    public class Top10Search
    {
        private static readonly object padlock = new object();
        enum EngineType { Google=1, Bing};

        //run tasks to get 10 top results async from Google and Bing sites, store results in DB
        public static async Task<IEnumerable<QueryResult>> RunTasks(string kewWord, IResultsRepository resultsRepository)
        {
            IList<QueryResult> results = new List<QueryResult>();
            Parallel.Invoke(
                () =>
                {
                    SearchByEngine(EngineType.Google,"http://google.com/search?q=",kewWord, "//h3[@class='LC20lb DKV0Md']", results);

                },
                () =>
                {
                    SearchByEngine(EngineType.Bing,"http://bing.com/search?q=",kewWord, "//li[@class='b_algo']", results);

                }
                );
            if(results.Count>0)
            {
                resultsRepository.InsertQueryResult(results);
            }
            return results;

        }
        //get top 5 results async from Google/Bing site
        static async Task SearchByEngine(EngineType engineType, string engineUrl,string kewWord, string xpath, IList<QueryResult> results)
        {
            string SearchResults = engineUrl + kewWord;

            StringBuilder sb = new StringBuilder();
            byte[] ResultsBuffer = new byte[8192];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SearchResults);
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;
            do
            {
                count = resStream.Read(ResultsBuffer, 0, ResultsBuffer.Length);
                if (count != 0)
                {
                    tempString = Encoding.UTF8.GetString(ResultsBuffer, 0, count);
                    sb.Append(tempString);
                }
            }

            while (count > 0);
            string sbb = sb.ToString();

            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();

            html.LoadHtml(sbb);

            var selectNodes = html.DocumentNode.SelectNodes(xpath);
            HtmlNode doc = html.DocumentNode;

            int i = 0;
            foreach (var node in selectNodes)
            {
                if (i == 5)
                    break;
                try
                {
                    lock (padlock)
                    {
                        string text = engineType == EngineType.Google ? node.InnerText : node.ChildNodes[0].ChildNodes[0].InnerText;
                        results.Add(new QueryResult()
                        {
                            Keyword = kewWord,
                            Title = System.Web.HttpUtility.UrlDecode(text),
                            EnteredDate = DateTime.Now,
                            SearchEngineId = (int)engineType
                        });
                    }
                }
                catch(Exception ex)
                {
                }
                
                i++;
            }
        }

    }
}
