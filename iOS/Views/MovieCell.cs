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
                Frame = new CGRect(0, 5, ImageHeight, ImageHeight),
            };

            this._headingLabel = new UILabel()
            {
                Frame = new CGRect(45, 5, this.ContentView.Bounds.Width - 60, 25),
                Font = UIFont.FromName("Helvetica", 20f),
                TextColor = UIColor.FromRGB(64, 64, 64),
                BackgroundColor = UIColor.Clear
            };

            this._subheadingLabel = new UILabel()
            {
                Frame = new CGRect(45, 25, 300, 20),
                Font = UIFont.FromName("HelveticaNeue-Bold", 12f),
                TextColor = UIColor.FromRGB(64, 64, 64),
                TextAlignment = UITextAlignment.Right,
                BackgroundColor = UIColor.Clear
            };

            this.ContentView.AddSubviews(new UIView[] { this._imageView, this._headingLabel, this._subheadingLabel});

            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        public void UpdateCell(MovieInfo info, MovieCredit credit)
        {
            var b = _downloader.LocalPathForFilename(info.PosterPath);
            this._imageView.Image = UIImage.FromFile(b);
            this._headingLabel.Text = info.Title + "(" + info.ReleaseDate.Year.ToString() + ")";
            if (credit.CastMembers.Count < 3){
                for (int i = 0; i < credit.CastMembers.Count; i++){
                    this._subheadingLabel.Text += credit.CastMembers[i].Name.ToString() + ", ";
                }
            }else{
                this._subheadingLabel.Text = credit.CastMembers[0].Name.ToString() + ", " + credit.CastMembers[1].Name.ToString() + ", "+ credit.CastMembers[2].Name.ToString();
            }
                
        }
    }
}
