using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SearchDataApp.Models
{
    public class QueryResult
    {
        public int Id { set; get; }
        [Required]
        public String Keyword { set; get; }
        [Required]
        public int SearchEngineId { set; get; }
        [Required]
        public String Title { set; get; }
        public DateTime EnteredDate { set; get; }
    }
}
