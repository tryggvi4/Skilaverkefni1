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

        //Fyrirlestur
        public override void ViewDidLoad()
        {
            ApiSearchResponse<MovieInfo> response = null;

            StorageClient imageStorage = new StorageClient();
            ImageDownloader downloader = new ImageDownloader(imageStorage);

            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.White;

            UILabel promptLabel = PromptLabel();
            UITextField nameField = NameField();
            UILabel greetingLabel = GreetingLabel();
            UIButton greetingButton = GreetingButton(response, nameField, greetingLabel);
            UIButton navigateButton = NavigationButton(nameField, response, downloader);

            this.View.AddSubviews(new UIView[] { promptLabel, nameField, greetingLabel, greetingButton, navigateButton });
        }

        private UIButton NavigationButton(UITextField nameField, ApiSearchResponse<MovieInfo> response, ImageDownloader downloader)
        {
            var navigateButton = UIButton.FromType(UIButtonType.RoundedRect);
            navigateButton.Frame = new CoreGraphics.CGRect(margin, spaceBetween * 5, this.View.Bounds.Width - (margin * 2), 50);
            navigateButton.SetTitle("See Movie list", UIControlState.Normal);

            navigateButton.TouchUpInside += async (sender, args) =>
            {
                nameField.ResignFirstResponder();
                //Færa þetta yfir í portable. 
                response = await _movieApi.SearchByTitleAsync(nameField.Text); //Nær í allar upplýsingar tengdum myndunum
                //Portable getur unnið með gögnin en verður að sækja myndirnar inní ios projectinu
                var task = downloader.DownloadImagesFromResponces(response); //Download'ar öllum pósterum
                await task;

                this.NavigationController.PushViewController(new MovieListController(response, downloader), true);
            };
            return navigateButton;
        }

        private UILabel GreetingLabel()
        {
            return new UILabel()
            {
                Frame = new CoreGraphics.CGRect(margin, spaceBetween * 4, this.View.Bounds.Width - (margin * 2), 50),
                Text = ""
            };
        }

        private UIButton GreetingButton(ApiSearchResponse<MovieInfo> response, UITextField nameField, UILabel greetingLabel)
        {
            var greetingButton = UIButton.FromType(UIButtonType.RoundedRect);
            greetingButton.Frame = new CoreGraphics.CGRect(margin, spaceBetween * 3, this.View.Bounds.Width - (margin * 2), 50);
            greetingButton.SetTitle("Leita", UIControlState.Normal);
            greetingButton.TouchUpInside += async (sender, args) =>
            {
                nameField.ResignFirstResponder(); //Losa lyklaborðið til að það fari
                response = await _movieApi.SearchByTitleAsync(nameField.Text);
                greetingLabel.Text = response.Results[0].Title;
            };
            return greetingButton;
        }

        private UITextField NameField()
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
                Text = "Enter your name: "
            };
        }
    }
}
