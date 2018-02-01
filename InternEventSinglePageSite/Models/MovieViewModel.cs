using System.Collections.Generic;

namespace InternEventSinglePageSite.Models
{
    public class MovieViewModel
    {
        public MovieViewModel()
        {
            ReturnId = "Home";
        }

        public List<MovieResults> MovieResults { get; set; }
        public string SearchKey { get; set; }
        public string SearchFormat { get; set; }
        public string ReturnId { get; set; }
    }
}
