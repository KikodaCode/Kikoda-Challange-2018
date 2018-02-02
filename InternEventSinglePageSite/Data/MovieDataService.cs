using InternEventSinglePageSite;
using InternEventSinglePageSite.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

public class MovieDataService 
{
    const string SUPER_SECRET_API_KEY = "05875cd50919223ef7db595c5c0743c4";   //Second API Key = 303f7d3d44a509f75edcda6cb42a3b26
    private string _endPoint;

    public MovieDataService()
    {
        _endPoint = "";
    }

    public Movie GetByTitle(string title)
    {
        _endPoint = $"https://api.themoviedb.org/3/search/movie?api_key={SUPER_SECRET_API_KEY}&language=en&query={WebUtility.UrlEncode(title)}";
        var json = FetchData(_endPoint);
        var moviesByTitle = Newtonsoft.Json.JsonConvert.DeserializeObject<Movie>(json);

        return moviesByTitle;
    }

    public Movie SearchAllMovies()
    {
        _endPoint = "https://api.themoviedb.org/3/discover/movie?api_key=05875cd50919223ef7db595c5c0743c4&page=1";
        var json = FetchData(_endPoint);
        return Newtonsoft.Json.JsonConvert.DeserializeObject<Movie>(json);
    }

    private string FetchData(string endpoint)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);

        request.Method = "GET";
        try
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("error code: " + response.StatusCode.ToString());
                }
                // Process the response stream
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }

                    return string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Houston! We got a Problem! " + ex.Message.ToString());
        }
        
        return string.Empty;
    }

    public string GetCurrentEndpoint() => _endPoint;
}