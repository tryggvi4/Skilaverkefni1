
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using DM.MovieApi.MovieDb.Movies;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;

namespace MovieSearchB.Droid
{
    [Activity(Label = "MovieDetailsActivity")]
    public class MovieDetailsActivity : Activity
    {
        MovieInfo _movie;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.MovieDetail);

            var jsonStr = this.Intent.GetStringExtra("movie");
            this._movie = JsonConvert.DeserializeObject<MovieInfo>(jsonStr);

            //TODO: Setja upp layout og fylla það af this._movie upplýsingum

            var movieTitleText = this.FindViewById<TextView>(Resource.Id.textView1);
            var movieDateText = this.FindViewById<TextView>(Resource.Id.textView2);
            var moviePoster = this.FindViewById<ImageView>(Resource.Id.imageView1);
            var movieOverviewText = this.FindViewById<TextView>(Resource.Id.textView3);

            movieTitleText.Text = _movie.Title;
            this.FindViewById<TextView>(Resource.Id.textView2).Text = _movie.ReleaseDate.Year.ToString();
            Glide.With(this).Load("http://image.tmdb.org/t/p/w185/" + _movie.PosterPath).Apply(RequestOptions.FitCenterTransform()).Into(moviePoster);
            movieOverviewText.Text = _movie.Overview;


        }
    }
}
