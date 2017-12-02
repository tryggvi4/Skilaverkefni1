using System;
using MonoTouch.Dialog;
using UIKit;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieSearchB.iOS.Views;
using MovieDownload;

namespace MovieSearchB.iOS.Controllers
{
    public class MovieDetailViewController : UIViewController
    {

        private readonly MovieInfo _res;
        ImageDownloader _downloader;

        private const double margin = 20;
        private const double spaceBetween = 70;
        private const double height = 50;

        public MovieDetailViewController(MovieInfo res, ImageDownloader downloader)
        {
            this._res = res;
            this._downloader = downloader;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = _res.Title;

            this.View.BackgroundColor = UIColor.White;

            UILabel promptLabel = new UILabel()
            {
                Frame = new CoreGraphics.CGRect(margin, spaceBetween, this.View.Bounds.Width - (margin * 2), 50),
                Text = _res.Title + "(" + _res.ReleaseDate.Year + ")"
            };
            this.View.AddSubview(promptLabel);

            UILabel timeAndGenere = new UILabel()
            {
                Frame = new CoreGraphics.CGRect(margin, spaceBetween, this.View.Bounds.Width - (margin * 2), 50),
                Text = _res.Overview.Length.ToString() + " | " + _res.Genres
            };
            this.View.AddSubview(timeAndGenere);

            var b = _downloader.LocalPathForFilename(_res.PosterPath);
            UIImageView poster = new UIImageView()
            {
                Frame = new CoreGraphics.CGRect(margin, spaceBetween, this.View.Bounds.Width - (margin * 2), 50),
                Image = UIImage.FromFile(b)
            };

            this.View.AddSubview(poster);

        }
    }
}
