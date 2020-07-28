using SearchDataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchDataApp.Repository
{
    public interface IResultsRepository
    {
        IEnumerable<QueryResult> GetResults(string keyword);

        void InsertQueryResult(IList<QueryResult> results);

        void Save();
    }
}
