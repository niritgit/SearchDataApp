using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SearchDataApp.Models;
using SearchDataApp.Repository;

namespace SearchDataApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryResultsController : ControllerBase
    {
        private readonly IResultsRepository _resultsRepository;
        private readonly IMemoryCache memoryCache;
        public QueryResultsController(IResultsRepository resultsRepository, IMemoryCache memoryCache)
        {
            _resultsRepository = resultsRepository;
            this.memoryCache = memoryCache;
        }

        //Search a keyword in Google and Bing sites
        //Extract 5 top results from each site and store the titles of the results in MS SQL database
        //1. If query results for keyword are in cache - get them from cache.
        //2.Else, get results from DB, and save results in cache.
        //3.Else, if query results for keyword are not in DB (and not in cache), 
        //run tasks to get results async from Google and Bing sites, store results in DB and in cache.
        [HttpGet("{keyword}", Name = "Get")]
        public IActionResult Get(string keyword)
        {
            IEnumerable<QueryResult> resultsList;
            string cacheKey = "results" + keyword.Trim().ToLower();
            resultsList =memoryCache.Get<IEnumerable<QueryResult>>(cacheKey);

            if (resultsList==null)//not in cache
            {
                resultsList = _resultsRepository.GetResults(keyword);//get results from DB
                if (resultsList== null || resultsList.Count()==0)//also not in DB 
                {
                    resultsList = Top10Search.RunTasks(keyword, _resultsRepository).Result;
                    memoryCache.Set(cacheKey, resultsList);//SAVE RESULT IN CACHE
                    return new OkObjectResult(resultsList);
                }
                else//in DB:
                {
                    memoryCache.Set(cacheKey, resultsList.ToList<QueryResult>());//SAVE RESULT IN CACHE
                    return new OkObjectResult(resultsList);
                }
                
            }
               
            return new OkObjectResult(resultsList);
        }

    }
}
