using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternEventSinglePageSite
{
    class Movie
    {
        public List<MovieResults> results { get; set; }
        public class MovieResults
        {
            public int vote_count { get; set; }
            public long id { get; set; }
            public long vote_average { get; set; }
            public string title { get; set; }
            public string poster_path { get; set; }
            public bool adult { get; set; }
            public string overview { get; set; }
            public DateTime release_date { get; set; }
        }

    }
}
