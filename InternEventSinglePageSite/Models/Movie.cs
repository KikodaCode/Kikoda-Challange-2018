using InternEventSinglePageSite.Models;
using System.Collections.Generic;

namespace InternEventSinglePageSite
{
    public class Movie 
    {
        public List<MovieResults> results { get; set; }
    }

    public static class MovieExtensions
    {
        public static string ToJson(this Movie movie)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(movie);
        }
    }
}
