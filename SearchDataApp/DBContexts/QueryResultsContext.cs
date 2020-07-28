using Microsoft.EntityFrameworkCore;
using SearchDataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchDataApp.DBContexts
{
    
    public class QueryResultsContext : DbContext
    {
        public QueryResultsContext(DbContextOptions<QueryResultsContext> options) : base(options)
        {
        }
        public DbSet<QueryResult> QueryResults { get; set; }
        public DbSet<SearchEngine> SearchEngines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchEngine>().HasData(
                new SearchEngine
                {
                    Id = 1,
                    Name = "Google"
                },
                new SearchEngine
                {
                    Id = 2,
                    Name = "Bing"
                }
            );
        }

    }
}
