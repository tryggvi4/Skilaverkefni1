using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using MovieDownload;

namespace MovieSearchB.iOS.Controllers
{
    public class MovieListController : UITableViewController
    {
        private readonly ApiSearchResponse<MovieInfo> _response;
        ImageDownloader _downloader;
        private IApiMovieRequest _movieApi;
        private MovieCredit[] _credits;

        public MovieListController(ApiSearchResponse<MovieInfo> response, ImageDownloader downloader, IApiMovieRequest movieApi, MovieCredit[] credits)
        {
            this._response = response;
            this._downloader = downloader;
            this._movieApi = movieApi;
            this._credits = credits;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Movie list";

            this.TableView.Source = new MovieListDataSource(this._response, _onSelectedMovie, _downloader, _movieApi, _credits);
        }

        private void _onSelectedMovie(int row)
        {
            this.NavigationController.PushViewController(new MovieDetailViewController(this._response.Results[row], _downloader, _credits[row]), true);
        }
    }
}
