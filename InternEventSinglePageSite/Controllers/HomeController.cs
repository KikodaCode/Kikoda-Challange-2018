using InternEventSinglePageSite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InternEventSinglePageSite.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult MovieByTitle()
        {
            return Json(new MovieDataService().GetByTitle("Wonder Woman"));
        }

        public IActionResult Index(MovieViewModel mvm)
        {
            var client = new MovieDataService();
            var results = new MovieViewModel();
            var movie = new Movie();

            movie = client.SearchAllMovies();
            results.ReturnId = "Home";
            results.ApiResults = movie.ToJson();
            results.ApiPath = client.GetCurrentEndpoint();

            List <MovieResults> list = new List<MovieResults>();
            try
            {
                foreach (var item in movie.results)
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

        [HttpPost]
        public IActionResult SearchMovie(MovieViewModel movieViewModel)
        {
            return RedirectToAction("Index", "Home", movieViewModel);
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
