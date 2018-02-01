using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InternEventSinglePageSite.Models
{
    public class MovieResults : Movie
    {
        
        public int vote_count { get; set; }
        public long id { get; set; }

        [DisplayName("Rating")]
        public long vote_average { get; set; }

        [DisplayName("Title")]
        public string title { get; set; }
        public string poster_path { get; set; }
        public bool adult { get; set; }

        [DisplayName("Overview")]
        public string overview { get; set; }

        [DisplayName("Release Date")]
        
        public string release_date { get; set; }

        public string DOR { get; set; }
    }
}
