using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InternEventSinglePageSite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InternEventSinglePageSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(MovieViewModel mvm)
        {
            SampleApiController rClient = new SampleApiController();
            var results = new MovieViewModel();
            //string[] movieName = txtUri.Text.Split(null);
            //rClient.endPoint = rClient.endPoint + "Wonder+Woman";

            //rClient.endPoint = rClient.endPoint + "&page=1";
            if (mvm.SearchKey == null || mvm.SearchKey == String.Empty)
            {
                rClient.endPoint = "https://api.themoviedb.org/3/discover/movie?api_key=05875cd50919223ef7db595c5c0743c4&page=1";
                results.ReturnId = "Home";
            }
            else
            {
                results.ReturnId = "SampleApi";

                switch (mvm.SearchFormat)
                {
                    case "2":
                        rClient.endPoint = "http://api.themoviedb.org/3/search/movie?api_key=05875cd50919223ef7db595c5c0743c4&language=en-US&query=";
                        string[] movieName = mvm.SearchKey.Split(null);
                        int count = 0;
                        foreach (var item in movieName)
                        {
                            if (count == 0)
                            {
                                rClient.endPoint = rClient.endPoint + $"{item}";
                                count++;
                            }
                            else
                            {
                                rClient.endPoint = rClient.endPoint + $"+{item}";
                            }
                        }
                        break;
                    case "3":
                        rClient.endPoint = $"https://api.themoviedb.org/3/discover/movie?primary_release_year={mvm.SearchKey}&page=1&include_video=false&include_adult=false&language=en-US&api_key=05875cd50919223ef7db595c5c0743c4";
                        break;
                    case "0":
                    default:
                        break;

                }
                rClient.endPoint = rClient.endPoint + $"&page=1";
            }

            string strResponse = string.Empty;
            strResponse = rClient.makeRequest();

            results.apiPath = rClient.endPoint;
            results.apiResults = strResponse;

            List<MovieResults> list = new List<MovieResults>();
            try
            {
                Movie jPerson = JsonConvert.DeserializeObject<Movie>(strResponse);


                foreach (var item in jPerson.results)
                {
                    if (item.release_date != null && item.release_date != String.Empty)
                    {
                        string dateOfRelease = Convert.ToDateTime(item.release_date).ToShortDateString();
                        item.DOR = dateOfRelease;
                    }
                    item.poster_path = @"https://image.tmdb.org/t/p/w600_and_h900_bestv2" + item.poster_path;
                    list.Add(item);
                }

                results.MovieResults = reOrder(mvm.SearchFormat, list);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("We had a problem " + ex.Message.ToString());
            }

            
            return View(results);
        }

        private List<MovieResults> reOrder(string searchMethod, List<MovieResults> list)
        {
            switch (searchMethod)
            {
                case "2":
                    list = list.OrderByDescending(o => o.release_date).Take(10).ToList();
                    break;
                case "3":
                    list = list.OrderByDescending(o => o.vote_average).Take(10).ToList();
                    break;
                default:
                    list = list.OrderByDescending(o => o.release_date).Take(10).ToList();
                    break;
            }
            return list;
        }
        // GET: /<controller>/
        public IActionResult Instructions()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult SampleApi()
        {
            return View();
        }
    }
}
