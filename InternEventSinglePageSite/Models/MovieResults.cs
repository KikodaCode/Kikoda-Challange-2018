using System;
using System.ComponentModel.DataAnnotations;

namespace InternEventSinglePageSite.Models
{
    public class MovieResults : Movie
    {
        public int vote_count { get; set; }
        public long id { get; set; }
        public long vote_average { get; set; }
        public string title { get; set; }
        public string poster_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }

        [DataType(DataType.Date)]
        public DateTime release_date { get; set; }
        public string DOR { get; set; }
    }
}
