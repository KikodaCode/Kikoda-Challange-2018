using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InternEventSinglePageSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            
            SampleApiController rClient = new SampleApiController();
            //string[] movieName = txtUri.Text.Split(null);
            //rClient.endPoint = rClient.endPoint + "Wonder+Woman";
            
            //rClient.endPoint = rClient.endPoint + "&page=1";
            rClient.endPoint = "https://api.themoviedb.org/3/search/movie?api_key=05875cd50919223ef7db595c5c0743c4&query=Wonder+Woman&page=1";

            string strResponse = string.Empty;
            strResponse = rClient.makeRequest();

            try
            {
                Movie jPerson = JsonConvert.DeserializeObject<Movie>(strResponse);
                //debugOutput("Here are the titles\n");
                //debugOutput(jPerson.ToString());
                foreach (var item in jPerson.results)
                {
                    string dateOfRelease = ((DateTime)item.release_date).ToShortDateString();
                    if (dateOfRelease.Substring(dateOfRelease.Length - 4) == "2017")
                    {
                        ViewBag.TITLE = item.title;
                        ViewBag.VOTE_AVERAGE = item.vote_average;
                        ViewBag.RELEASE_DATE = dateOfRelease;
                        ViewBag.PLOT = item.overview;
                        //Console.WriteLine("Title: " + item.title);
                        //Console.WriteLine("Review: " + item.vote_average + "/10");
                        //Console.WriteLine("Release Date: " + dateOfRelease);
                        //Console.WriteLine("Plot: " + item.overview);
                    }
                }
                //debugOutput("Here is the Release Date " + jPerson);

            }
            catch (Exception ex)
            {
                Console.WriteLine("We had a problem " + ex.Message.ToString());
            }
            return View();
        }

        // GET: /<controller>/
        public IActionResult About()
        {
            return View();
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

        // GET: /<controller>/
        public IActionResult Contact()
        {
            return View();
        }
    }
}
