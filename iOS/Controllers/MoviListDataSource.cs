using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieSearchB.iOS.Views;
using MovieDownload;

namespace MovieSearchB.iOS.Controllers
{
    public class MovieListDataSource : UITableViewSource
    {
        private readonly ApiSearchResponse<MovieInfo> _response;
        private ImageDownloader _downloader;
        private MovieCredit[] _credits;

        public readonly NSString MovieListCellId = new NSString("MovieListCell");
        private readonly Action<int> _onSelectedPerson;
        private IApiMovieRequest _movieApi;

        public MovieListDataSource(ApiSearchResponse<MovieInfo> response, Action<int> onSelectedPerson, ImageDownloader downloader, IApiMovieRequest movieApi, MovieCredit[] credits)
        {
            this._response = response;
            this._onSelectedPerson = onSelectedPerson;
            this._downloader = downloader;
            this._movieApi = movieApi;
            this._credits = credits;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (MovieCell)tableView.DequeueReusableCell((NSString)this.MovieListCellId);
            if (cell == null)
            {
                cell = new MovieCell(this.MovieListCellId, _downloader);  //UITableViewCell(UITableViewCellStyle.Default, this.MovieListCellId);
            }
            cell.UpdateCell(this._response.Results[indexPath.Row], this._credits[indexPath.Row]);  //TextLabel.Text = this._response.Results[indexPath.Row].Title;

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (this._response.Results == null)
            {
                return 0;
            }
            else
            {
                return this._response.Results.Count;
            }
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            this._onSelectedPerson(indexPath.Row);
        }
    }
}
