using System;
using DM.MovieApi;
using DM.MovieApi.ApiRequest;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.IndustryProfessions;
using DM.MovieApi.MovieDb.Movies;



namespace MovieSearchB
{
    public class MovieDbConnection
    {
        public MovieDbConnection()
        {
            string apiKey = "6aa0a7127eac2b72e75b175229d1000d";
            string apiUrl = "http://api.themoviedb.org/3/";

            MovieDbFactory.RegisterSettings(apiKey, apiUrl);
        }
    }
}
