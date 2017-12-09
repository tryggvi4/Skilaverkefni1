
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

using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;

using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;

namespace MovieSearchB.Droid
{
    public class MovieListAdapter : BaseAdapter<MovieInfo>
    {
        private readonly Activity _context;
        private readonly ApiSearchResponse<MovieInfo> _response;

        public MovieListAdapter(Activity context, ApiSearchResponse<MovieInfo> response)
        {
            this._context = context;
            this._response = response;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if(view == null)
            {
                view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);
            }
            var movie = this._response.Results[position];
            view.FindViewById<TextView>(Resource.Id.name).Text = movie.Title;
            view.FindViewById<TextView>(Resource.Id.year).Text = movie.ReleaseDate.Year.ToString();
            //Breyta þessu í glide pælinguna með myndir
            var imgTest = view.FindViewById<ImageView>(Resource.Id.picture);
            Glide.With(_context).Load("http://image.tmdb.org/t/p/w185/" + movie.PosterPath).Apply(RequestOptions.FitCenterTransform()).Into(imgTest);
            return view;
        }


        //Fill in cound here, currently 0
        public override int Count => this._response.Results.Count();

        public override MovieInfo this[int position] => this._response.Results[position];

        public override long GetItemId(int position)
        {
            return this._response.Results[position].Id;
        }
    }
}
