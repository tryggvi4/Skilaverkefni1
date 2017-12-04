using System;
using System.Collections.Generic;
using MonoTouch.Dialog;
using UIKit;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieDownload;
using System.Threading.Tasks;


namespace MovieSearchB.iOS.Controllers
{
    public class MovieSearchViewController : UIViewController
    {
        private const double margin = 20;
        private const double spaceBetween = 70;
        private const double height = 50;

        private IApiMovieRequest _movieApi;

        public MovieSearchViewController(IApiMovieRequest movieApi){
            _movieApi = movieApi;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
        }

        public override void ViewDidLoad()
        {
            
            ApiSearchResponse<MovieInfo> response = null;

            StorageClient imageStorage = new StorageClient();
            ImageDownloader downloader = new ImageDownloader(imageStorage);

            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.White;

            UILabel promptLabel = PromptLabel();
            UITextField movieField = MovieField();

            UIActivityIndicatorView activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
            activitySpinner.Frame = new CoreGraphics.CGRect(this.View.Bounds.Width/2, this.View.Bounds.Height/2, 24, 24);
            activitySpinner.HidesWhenStopped = true;

            UIButton navigateButton = NavigationButton(movieField, response, downloader, activitySpinner);

            this.View.AddSubviews(new UIView[] { promptLabel, movieField, navigateButton, activitySpinner });
        }

        private UIButton NavigationButton(UITextField nameField, ApiSearchResponse<MovieInfo> response, ImageDownloader downloader, UIActivityIndicatorView activitySpinner)
        {
            var navigateButton = UIButton.FromType(UIButtonType.RoundedRect);
            navigateButton.Frame = new CoreGraphics.CGRect(margin, spaceBetween * 3, this.View.Bounds.Width - (margin * 2), 50);
            navigateButton.SetTitle("See Movie list", UIControlState.Normal);

            navigateButton.TouchUpInside += async (sender, args) =>
            {
                activitySpinner.StartAnimating();
                nameField.ResignFirstResponder();
                response = await _movieApi.SearchByTitleAsync(nameField.Text); //Nær í allar upplýsingar tengdum myndunu
                MovieCredit[] credits = new MovieCredit[response.Results.Count];
                for (int i = 0; i < response.Results.Count; i++){
                    var credit = await _movieApi.GetCreditsAsync(response.Results[i].Id, "en");
                    credits[i] = credit.Item;
                }

                var task = downloader.DownloadImagesFromResponces(response); //Download'ar öllum pósterum

                await task;

                activitySpinner.StopAnimating();
                this.NavigationController.PushViewController(new MovieListController(response, downloader, _movieApi, credits), true);
            };
            return navigateButton;
        }

        private UITextField MovieField()
        {
            return new UITextField()
            {
                Frame = new CoreGraphics.CGRect(margin, spaceBetween * 2, this.View.Bounds.Width - (margin * 2), 50),
                BorderStyle = UITextBorderStyle.RoundedRect
            };
        }

        private UILabel PromptLabel()
        {
            return new UILabel()
            {
                Frame = new CoreGraphics.CGRect(margin, spaceBetween, this.View.Bounds.Width - (margin * 2), 50),
                Text = "Search: "
            };
        }
    }
}
