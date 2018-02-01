﻿using System;
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
                switch (mvm.SearchFormat)
                {
                    case "2":
                        rClient.endPoint = "http://api.themoviedb.org/3/search/movie?api_key=05875cd50919223ef7db595c5c0743c4&language=en&query=";
                        string[] movieName = mvm.SearchKey.Split(null);
                        int count = 0;
                        foreach (var item in movieName)
                        {
                            if(count == 0)
                            {
                                rClient.endPoint = rClient.endPoint + $"{item}";
                            }
                            else
                            {
                                rClient.endPoint = rClient.endPoint + $"+{item}";
                            }
                        }
                        break;
                        //TODO
                        case "3":
                            rClient.endPoint = $"https://api.themoviedb.org/3/discover/movie?primary_release_year={mvm.SearchKey}&page=1&include_video=false&include_adult=false&language=en-US&api_key=05875cd50919223ef7db595c5c0743c4";
                            break;
                        case "0":
                        default: 
                        break;

                }
            }


            //"https://api.themoviedb.org/3/search/movie?api_key=05875cd50919223ef7db595c5c0743c4&query=Wonder+Woman&page=1";

            string strResponse = string.Empty;
            strResponse = rClient.makeRequest();
            List<MovieResults> list = new List<MovieResults>();
            try
            {
                Movie jPerson = JsonConvert.DeserializeObject<Movie>(strResponse);
                
             
                foreach (var item in jPerson.results)
                {
                    if(item.release_date != null && item.release_date != String.Empty){
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

        private List<MovieResults> reOrder (string searchMethod, List<MovieResults> list)
        {
            switch(searchMethod)
            {
                case "2":
                    list = list.OrderByDescending(o => o.release_date).Take(10).ToList();
                    break;
                case "3":
                    list = list.OrderByDescending(o => o.vote_average).Take(10).ToList();
                    break;
                default:
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
