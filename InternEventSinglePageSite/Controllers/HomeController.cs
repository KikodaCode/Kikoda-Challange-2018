using System;
using System.Collections.Generic;
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
        // GET: /<controller>/
        // public IActionResult Index()
        // {

        //     SampleApiController rClient = new SampleApiController();
        //     var results = new MovieViewModel();
        //     //string[] movieName = txtUri.Text.Split(null);
        //     //rClient.endPoint = rClient.endPoint + "Wonder+Woman";

        //     //rClient.endPoint = rClient.endPoint + "&page=1";
        //     rClient.endPoint = "http://api.themoviedb.org/3/search/movie?api_key=05875cd50919223ef7db595c5c0743c4&language=en&query=Matrix&year=1999&page=1";
        //     //"https://api.themoviedb.org/3/search/movie?api_key=05875cd50919223ef7db595c5c0743c4&query=Wonder+Woman&page=1";

        //     string strResponse = string.Empty;
        //     strResponse = rClient.makeRequest();

        //     try
        //     {
        //         Movie jPerson = JsonConvert.DeserializeObject<Movie>(strResponse);
        //         List<MovieResults> list = new List<MovieResults>();
        //         //debugOutput("Here are the titles\n");
        //         //debugOutput(jPerson.ToString());
        //         foreach (var item in jPerson.results)
        //         {
        //             string dateOfRelease = ((DateTime)item.release_date).ToShortDateString();
        //             //if (dateOfRelease.Substring(dateOfRelease.Length - 4) == "2017")
        //             //{
        //                 item.DOR = dateOfRelease;
        //                 list.Add(item);
        //                 //ViewBag.TITLE = item.title;
        //                 //ViewBag.VOTE_AVERAGE = item.vote_average;
        //                 //ViewBag.RELEASE_DATE = dateOfRelease;
        //                 //ViewBag.PLOT = item.overview;
        //                 //Console.WriteLine("Title: " + item.title);
        //                 //Console.WriteLine("Review: " + item.vote_average + "/10");
        //                 //Console.WriteLine("Release Date: " + dateOfRelease);
        //                 //Console.WriteLine("Plot: " + item.overview);
        //             //}
        //         }
        //         //debugOutput("Here is the Release Date " + jPerson);
        //         results.MovieResults = new List<MovieResults>(list);
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine("We had a problem " + ex.Message.ToString());
        //     }
        //     return View(results);
        // }

        public IActionResult Index(MovieViewModel mvm)
        {

            SampleApiController rClient = new SampleApiController();
            var results = new MovieViewModel();
            //string[] movieName = txtUri.Text.Split(null);
            //rClient.endPoint = rClient.endPoint + "Wonder+Woman";

            //rClient.endPoint = rClient.endPoint + "&page=1";
            if (mvm.SearchKey == null || mvm.SearchKey == String.Empty)
            {
                rClient.endPoint = "https://api.themoviedb.org/3/search/movie?api_key=05875cd50919223ef7db595c5c0743c4&query=Wonder+Woman&page=1";
            }
            else
            {
                rClient.endPoint = "http://api.themoviedb.org/3/search/movie?api_key=05875cd50919223ef7db595c5c0743c4&language=en&query=";
                string[] movieName = mvm.SearchKey.Split(null);
                foreach (var item in movieName)
                {
                    rClient.endPoint = rClient.endPoint + $"+{item}";
                }
                rClient.endPoint = rClient.endPoint + "&page=1";
            }


            //"https://api.themoviedb.org/3/search/movie?api_key=05875cd50919223ef7db595c5c0743c4&query=Wonder+Woman&page=1";

            string strResponse = string.Empty;
            strResponse = rClient.makeRequest();

            try
            {
                Movie jPerson = JsonConvert.DeserializeObject<Movie>(strResponse);
                List<MovieResults> list = new List<MovieResults>();
                //debugOutput("Here are the titles\n");
                //debugOutput(jPerson.ToString());
                foreach (var item in jPerson.results)
                {
                    string dateOfRelease = ((DateTime)item.release_date).ToShortDateString();
                    //if (dateOfRelease.Substring(dateOfRelease.Length - 4) == "2017")
                    //{
                    item.DOR = dateOfRelease;
                    list.Add(item);
                    //ViewBag.TITLE = item.title;
                    //ViewBag.VOTE_AVERAGE = item.vote_average;
                    //ViewBag.RELEASE_DATE = dateOfRelease;
                    //ViewBag.PLOT = item.overview;
                    //Console.WriteLine("Title: " + item.title);
                    //Console.WriteLine("Review: " + item.vote_average + "/10");
                    //Console.WriteLine("Release Date: " + dateOfRelease);
                    //Console.WriteLine("Plot: " + item.overview);
                    //}
                }
                //debugOutput("Here is the Release Date " + jPerson);
                results.MovieResults = new List<MovieResults>(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("We had a problem " + ex.Message.ToString());
            }
            return View(results);
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
