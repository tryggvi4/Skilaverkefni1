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
using FragmentManager = Android.Support.V4.App.FragmentManager;

using DM.MovieApi.ApiRequest;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.IndustryProfessions;
using DM.MovieApi.MovieDb.Movies;



namespace MovieSearchB.Droid
{
    [Activity(Label = "Movie Search", Theme = "@style/MyTheme")]
    public class MovieInputFragment : Fragment
    {

        MovieDbConnection _database;

        public MovieInputFragment(MovieDbConnection database)
        {
            _database = database;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.MovieInput, container, false);

            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            // Get our button from the layout resource,
            // and attach an event to it
            var movieEditText = rootView.FindViewById<EditText>(Resource.Id.movieEditText);
            var searchButton = rootView.FindViewById<Button>(Resource.Id.searchButton);
            var searchTextView = rootView.FindViewById<TextView>(Resource.Id.movieNameView);
            var progressBar = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            progressBar.Visibility = Android.Views.ViewStates.Invisible;

            searchButton.Click += async (object sender, EventArgs e) =>
            {
                progressBar.Visibility = Android.Views.ViewStates.Visible;
                var manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                manager.HideSoftInputFromWindow(movieEditText.WindowToken, 0);

                MovieDbConnection movieApi = new MovieDbConnection();
                //FÆRA INNÍ 
                /*string apiKey = "6aa0a7127eac2b72e75b175229d1000d";
                string apiUrl = "http://api.themoviedb.org/3/";

                MovieDbFactory.RegisterSettings(apiKey, apiUrl);
                IApiMovieRequest _movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
                var response = await _movieApi.SearchByTitleAsync(movieEditText.Text);*/

                await _database.SetMovies(movieEditText.Text);

                progressBar.Visibility = Android.Views.ViewStates.Gone;
                //searchTextView.Text = database.GetMovies().Results[0].Title;
                var intent = new Intent(this.Context, typeof(MovieListActivity));
                //var response = _database.GetMovies();
                intent.PutExtra("movieList", JsonConvert.SerializeObject(_database.GetMovies()));
                //progressBar.Visibility = Android.Views.ViewStates.Gone;
                this.StartActivity(intent);


                /*FragmentManager fragmentManager = getSupportFragmentManager();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();*/


                // Create new fragment and transaction
                /*Fragment newFragment = new TopRatedFragment();
                FragmentTransaction transaction = getSupportFragmentManager().beginTransaction();

                // Replace whatever is in the fragment_container view with this fragment,
                // and add the transaction to the back stack
                transaction.replace(R.id.fragment_container, newFragment);
                transaction.addToBackStack(null);

                // Commit the transaction
                transaction.commit();*/
            };

            return rootView;
        }
    }
}