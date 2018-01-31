using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternEventSinglePageSite.Models
{
    public class MovieViewModel
    {
        public List<MovieResults> MovieResults { get; set; }
        public string SearchKey { get; set; }
        public string SearchFormat { get; set; }
    }
}
