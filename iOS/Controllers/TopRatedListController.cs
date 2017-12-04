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
        private IApiMovieRequest _movieApi;
        ImageDownloader downloader;
        ApiSearchResponse<MovieInfo> response;
        MovieCredit[] credits;


        public TopRatedListController(IApiMovieRequest movieApi)
        {
            _movieApi = movieApi;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 1);
        }

        public override async void ViewDidLoad()
        {
            StorageClient imageStorage = new StorageClient();
            downloader = new ImageDownloader(imageStorage);



            base.ViewDidLoad();
            this.Title = "Top Rated";

            UIActivityIndicatorView activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
            activitySpinner.Frame = new CoreGraphics.CGRect(this.View.Bounds.Width / 2, 0, 24, 24);
            activitySpinner.HidesWhenStopped = true;
            this.View.AddSubview(activitySpinner);

            activitySpinner.StartAnimating();
            response = await _movieApi.GetTopRatedAsync();
            credits = new MovieCredit[response.Results.Count];
            for (int i = 0; i < response.Results.Count; i++)
            {
                var credit = await _movieApi.GetCreditsAsync(response.Results[i].Id, "en");
                credits[i] = credit.Item;
            }


            var task = downloader.DownloadImagesFromResponces(response); //Download'ar öllum pósterum

            await task;

            activitySpinner.StopAnimating();
            this.TableView.ReloadData();

            this.TableView.Source = new MovieListDataSource(response, _onSelectedMovie, downloader, _movieApi, credits);
        }

        private void _onSelectedMovie(int row)
        {
            this.NavigationController.PushViewController(new MovieDetailViewController(response.Results[row], downloader, credits[row]), true);
        }

    }
}