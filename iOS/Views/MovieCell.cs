using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using MovieDownload;

namespace MovieSearchB.iOS.Views
{
    public class MovieCell : UITableViewCell
    {
        private const double ImageHeight = 33;
        private readonly UIImageView _imageView;
        private readonly UILabel _headingLabel;
        private readonly UILabel _subheadingLabel;
        ImageDownloader _downloader;

        public MovieCell(NSString cellId, ImageDownloader downloader) : base(UITableViewCellStyle.Default, cellId)
        {
            this._downloader = downloader;

            this.SelectionStyle = UITableViewCellSelectionStyle.Gray;

            this._imageView = new UIImageView()
            {
                Frame = new CGRect(this.ContentView.Bounds.Width - 60, 5, ImageHeight, ImageHeight),
            };

            this._headingLabel = new UILabel()
            {
                Frame = new CGRect(5, 5, this.ContentView.Bounds.Width - 60, 25),
                Font = UIFont.FromName("Cochin-BoldItalic", 22f),
                TextColor = UIColor.FromRGB(127, 51, 0),
                BackgroundColor = UIColor.Clear
            };

            this._subheadingLabel = new UILabel()
            {
                Frame = new CGRect(100, 25, 100, 20),
                Font = UIFont.FromName("AmericanTypewriter", 12f),
                TextColor = UIColor.FromRGB(38, 127, 0),
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.Clear
            };

            this.ContentView.AddSubviews(new UIView[] { this._imageView, this._headingLabel, this._subheadingLabel});

            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        public void UpdateCell(MovieInfo info)
        {
            //ToDo - Skipta um gögnin
            var b = _downloader.LocalPathForFilename(info.PosterPath);
            this._imageView.Image = UIImage.FromFile(b);
            this._headingLabel.Text = info.Title;
            this._subheadingLabel.Text = info.ReleaseDate.ToString();
        }
    }
}
