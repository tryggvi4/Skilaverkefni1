using Android.App;
using Android.OS;
using Newtonsoft.Json;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using Android.Content;
using Android.Widget;

namespace MovieSearchB.Droid
{
    [Activity(Label = "Movie List", Theme = "@style/MyTheme")]
    public class MovieListActivity : Activity
    {
        private ApiSearchResponse<MovieInfo> _responce;
        private ListView _listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here

            var jsonStr = this.Intent.GetStringExtra("movieList");
            this._responce = JsonConvert.DeserializeObject<ApiSearchResponse<MovieInfo>>(jsonStr);

            SetContentView(Resource.Layout.ListViewLayout);

            _listView = this.FindViewById<ListView>(Resource.Id.listview);

            this._listView.ItemClick += (sender, args) =>
            {
                this.ShowAlert(args.Position);
            };

            this._listView.Adapter = new MovieListAdapter(this, this._responce);
        }

        private void ShowAlert(int position)
        {
            var intent = new Intent(this, typeof(MovieDetailsActivity));
            intent.PutExtra("movie", JsonConvert.SerializeObject(this._responce.Results[position]));
            this.StartActivity(intent);
            /*
            var movie = this._responce.Results[position];
            var alertBuilder = new AlertDialog.Builder(this);
            alertBuilder.SetTitle("Movie selected");
            alertBuilder.SetMessage(movie.Title);
            alertBuilder.SetCancelable(true);
            alertBuilder.SetPositiveButton("OK", (e, args) => { });
            var dialog = alertBuilder.Create();
            dialog.Show();*/


        }
    }
}
