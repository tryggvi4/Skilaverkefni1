using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views.InputMethods;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;

namespace MovieSearchB.Droid
{
    [Activity(Label = "Movie Search", MainLauncher = true, Theme = "@style/MyTheme")]
    public class MainActivity : FragmentActivity
    {
        MovieDbConnection database;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Data tengingin

            database = new MovieDbConnection();
            //IApiMovieRequest movieApi = MovieDbConnection.CreateMovieApi();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var fragments = new Fragment[]
            {
                new MovieInputFragment(database),
                new TopRatedFragment(),
            };

            var titles = CharSequence.ArrayFromStringArray(new[] { "Movie Search", "Top Rated" });

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);

            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "My Toolbar";

            //Button button = FindViewById<Button>(Resource.Id.myButton);

            //button.Click += delegate { button.Text = $"{count++} clicks!"; };
        }
    }
}