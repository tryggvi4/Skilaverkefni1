using System;
using MonoTouch.Dialog;
using UIKit;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using MovieDownload;


namespace MovieSearchB.iOS.Controllers
{
    public class TopRatedListController : UITableViewController
    {
        //ÞESSI Á AÐ BÚA TIL SINN EIGINS _DOWNLOADER
        private IApiMovieRequest _movieApi;
        ApiSearchResponse<MovieInfo> _response;
        ImageDownloader _downloader;

        public TopRatedListController(IApiMovieRequest movieApi)
        {
            _movieApi = movieApi;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 1);
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Top Rated";

            _response = await _movieApi.GetTopRatedAsync();

            StorageClient imageStorage = new StorageClient();
            _downloader = new ImageDownloader(imageStorage);


            this.TableView.Source = new TopRatedListDataSource(_response, _onSelectedMovie, _downloader);
        }

        private void _onSelectedMovie(int row)
        {
            this.NavigationController.PushViewController(new MovieDetailViewController(_response.Results[row], _downloader), true);

            //TODO:  Sendir Alert. Á að opna details síðu fyrir myndina
            /*var okAlertController = UIAlertController.Create("Movie selected", this._response.Results[row].Title,
                UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this.PresentViewController(okAlertController, true, null);*/
        }

    }
}