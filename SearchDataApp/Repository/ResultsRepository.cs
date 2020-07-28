using SearchDataApp.DBContexts;
using SearchDataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace SearchDataApp.Repository
{
    public class ResultsRepository : IResultsRepository
    {
        private readonly QueryResultsContext _dbContext;

        public ResultsRepository(QueryResultsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<QueryResult> GetResults(string keyword)
        {
            return _dbContext.QueryResults.Where<QueryResult>(x => x.Keyword == keyword);
        }


        public void InsertQueryResult(IList<QueryResult> results)
        {
            _dbContext.AddRange(results);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        
    }
}
