using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SearchDataApp.Models
{
    public class SearchEngine
    {
        public int Id { set; get; }
        [Required]
        public String Name { set; get; }
    }
}
