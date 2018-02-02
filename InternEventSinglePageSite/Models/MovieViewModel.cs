using System.Collections.Generic;
using System;

namespace InternEventSinglePageSite.Models
{
    public class MovieViewModel
    {
        public MovieViewModel()
        {
            ReturnId = "Home";
            SearchKey = String.Empty;
            SearchFormat = String.Empty;
        }

        public List<MovieResults> MovieResults { get; set; }
        public string SearchKey { get; set; }
        public string SearchFormat { get; set; }
        public string ReturnId { get; set; }
        public string ApiPath { get; set; }
        public string ApiResults { get; set; }
    }
}
