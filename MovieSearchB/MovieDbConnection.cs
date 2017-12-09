using System;
using DM.MovieApi;
using DM.MovieApi.ApiRequest;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.IndustryProfessions;
using DM.MovieApi.MovieDb.Movies;
using System.Threading.Tasks;



namespace MovieSearchB
{
    public class MovieDbConnection
    {
        static IApiMovieRequest movieApi;
        static ApiSearchResponse<MovieInfo> response;

        public MovieDbConnection()
        {
            string apiKey = "ffba741b9c12765812c7d80a7bae050d";
            string apiUrl = "http://api.themoviedb.org/3/";

            MovieDbFactory.RegisterSettings(apiKey, apiUrl);
            movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
        }

        public async Task SetMovies(string searchTerm)
        {
            response = await movieApi.SearchByTitleAsync(searchTerm); //Nær í allar upplýsingar tengdum myndunu
            return;
        }

        public ApiSearchResponse<MovieInfo> GetMovies()
        {
            return response;
        }

        public IApiMovieRequest GetMovieApi()
        {
            return movieApi;
        }
    }
}
