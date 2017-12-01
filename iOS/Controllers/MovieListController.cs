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

        public MovieListController(ApiSearchResponse<MovieInfo> response, ImageDownloader downloader)
        {
            this._response = response;
            this._downloader = downloader;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Name list";

            this.TableView.Source = new MovieListDataSource(this._response, _onSelectedMovie, _downloader);
        }

        private void _onSelectedMovie(int row)
        {
            this.NavigationController.PushViewController(new MovieDetailViewController(this._response.Results[row], _downloader), true);

            //TODO:  Sendir Alert. Á að opna details síðu fyrir myndina
            /*var okAlertController = UIAlertController.Create("Movie selected", this._response.Results[row].Title,
                UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this.PresentViewController(okAlertController, true, null);*/
        }
    }
}
