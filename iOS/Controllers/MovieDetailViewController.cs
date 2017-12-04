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
        MovieCredit _credit;

        private const double margin = 20;
        private const double spaceBefore = 70;
        private const double height = 50;
        private const double posterHeight = 148;
        private const double posterWidth = 100;
        private const double spaceBetween = 5;
        private const double actorHeight = 25;

        public MovieDetailViewController(MovieInfo res, ImageDownloader downloader, MovieCredit credit)
        {
            this._res = res;
            this._downloader = downloader;
            this._credit = credit;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = _res.Title;

            this.View.BackgroundColor = UIColor.White;

            UILabel promptLabel = new UILabel()
            {
                Frame = new CoreGraphics.CGRect(margin, spaceBefore, this.View.Bounds.Width - (margin * 2), height),
                Font = UIFont.FromName("Helvetica", 22f),
                TextColor = UIColor.FromRGB(64, 64, 64),
                Text = _res.Title
            };
            this.View.AddSubview(promptLabel);

            UILabel yearLabel = new UILabel()
            {
                Frame = new CoreGraphics.CGRect(margin, spaceBefore + 25, this.View.Bounds.Width - (margin * 2), height),
                Font = UIFont.FromName("Helvetica", 20f),
                TextColor = UIColor.FromRGB(64, 64, 64),
                Text = "(" + _res.ReleaseDate.Year + ")"
            };
            this.View.AddSubview(yearLabel);


            string combinedText = _res.Overview.Length.ToString() + " mins";
            if(_res.Genres.Count > 0){
                combinedText += " | ";
                var last = _res.Genres[_res.Genres.Count - 1];
                foreach (var genre in _res.Genres)
                {
                    if (genre.Equals(last))
                    {
                        combinedText += genre.Name;
                    }
                    else
                    {
                        combinedText += genre.Name + ", ";
                    }
                }
            }
            UILabel timeAndGenere = new UILabel()
            {
                Frame = new CoreGraphics.CGRect(margin, spaceBefore + height, this.View.Bounds.Width - (margin * 2), height),
                Text = combinedText
            };
            this.View.AddSubview(timeAndGenere);

            var b = _downloader.LocalPathForFilename(_res.PosterPath);
            UIImageView poster = new UIImageView()
            {
                Frame = new CoreGraphics.CGRect(margin, spaceBefore + (height * 2), posterWidth, posterHeight),
                Image = UIImage.FromFile(b)
            };
            this.View.AddSubview(poster);

            UILabel description = new UILabel()
            {
                Frame = new CoreGraphics.CGRect(margin + posterWidth + spaceBetween, spaceBefore + (height * 2), this.View.Bounds.Width - (margin*2 + (posterWidth)), posterHeight),
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 0,
                Text = _res.Overview
            };
            this.View.AddSubview(description);

            if(_credit.CastMembers.Count > 0){
                UILabel actors = new UILabel()
                {
                    Frame = new CoreGraphics.CGRect(margin, spaceBefore + (posterHeight + (height * 2)), this.View.Bounds.Width - (margin * 2), height),
                    Text = "Actors: "
                };
                this.View.AddSubview(actors);

                int extraHeight = 1;
                foreach (var actor in _credit.CastMembers)
                {
                    UILabel actorLabel = new UILabel()
                    {
                        Frame = new CoreGraphics.CGRect(margin, spaceBefore + (posterHeight + (height * 2)) + margin + (actorHeight * extraHeight), this.View.Bounds.Width - (margin * 2), actorHeight),
                        Text = actor.Name + " as " + actor.Character
                    };
                    this.View.AddSubview(actorLabel);
                    extraHeight++;
                }
            }
        }
    }
}
