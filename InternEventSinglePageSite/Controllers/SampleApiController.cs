using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using InternEventSinglePageSite.Models;

namespace InternEventSinglePageSite.Controllers
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    [Produces("application/json")]
    [Route("api/SampleApi")]
    public class SampleApiController : Controller
    {
        public string endPoint { get; set; }
        public HttpVerb httpMethod { get; set; }

        public SampleApiController()
        {
            endPoint = "https://api.themoviedb.org/3/search/movie?api_key=05875cd50919223ef7db595c5c0743c4&query=";
            httpMethod = HttpVerb.GET;
        }

        public string makeRequest()
        {
            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException("error code: " + response.StatusCode.ToString());
                    }
                    // Process the response stream
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            // read in ithe stream from responseStream
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                strResponseValue = reader.ReadToEnd();
                            } //End Stream Reader
                        }
                    } //End using response stream
                }// End Process Response
            }
            catch (Exception ex)
            {
                Console.WriteLine("We had a problem " + ex.Message.ToString());
            }

            return strResponseValue;
        }

        [HttpPost]
        public IActionResult SearchMovie(MovieViewModel movieViewModel)
        {
            return RedirectToAction("Index", "Home", movieViewModel);
        }
    }
}