using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;

using DM.MovieApi.ApiRequest;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.IndustryProfessions;
using DM.MovieApi.MovieDb.Movies;
using System.Threading.Tasks;

namespace MovieSearchB.Droid
{
    public class TopRatedFragment : Fragment
    {
        ApiSearchResponse<MovieInfo> responseTopRated;
        MovieCredit[] credits;
        IApiMovieRequest _movieApi;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.ListViewLayout, container, false);



            /*
             * BÍÐA MEÐ ÞETTA AÐEINS
             * 
             * // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var rootView = inflater.Inflate(Resource.Layout.ListViewLayout, container, false);
            //return base.OnCreateView(inflater, container, savedInstanceState);

            string apiKey = "6aa0a7127eac2b72e75b175229d1000d";
            string apiUrl = "http://api.themoviedb.org/3/";

            MovieDbFactory.RegisterSettings(apiKey, apiUrl);
            _movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

            var progressBar = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar4);
            progressBar.Visibility = Android.Views.ViewStates.Invisible;

            progressBar.Visibility = Android.Views.ViewStates.Visible;


            var t = Task.Run(async () => {
                responseTopRated = await _movieApi.GetTopRatedAsync();

                credits = new MovieCredit[responseTopRated.Results.Count];
                for (int i = 0; i < responseTopRated.Results.Count; i++)
                {
                    var credit = await _movieApi.GetCreditsAsync(responseTopRated.Results[i].Id, "en");
                    credits[i] = credit.Item;
                }
            });

            t.Wait();

            progressBar.Visibility = Android.Views.ViewStates.Gone;


            /*var intent = new Intent(this.Context, typeof(MovieListActivity));
            //var response = _database.GetMovies();
            intent.PutExtra("movieList", JsonConvert.SerializeObject(responseTopRated));
            //progressBar.Visibility = Android.Views.ViewStates.Gone;
            this.StartActivity(intent);

            return rootView;*/

        }
    }
}