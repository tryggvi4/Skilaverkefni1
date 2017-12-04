using System;
using MonoTouch.Dialog;
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
    public class MaHenda : UITableViewSource
    {
        private readonly ApiSearchResponse<MovieInfo> _response;
        private ImageDownloader _downloader;


        public readonly NSString TopRatedCellId = new NSString("TopRatedCell");
        private readonly Action<int> _onSelectedPerson;

        public MaHenda(ApiSearchResponse<MovieInfo> response, Action<int> onSelectedPerson, ImageDownloader downloader)
        {
            this._response = response;
            this._onSelectedPerson = onSelectedPerson;
            this._downloader = downloader;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (MovieCell)tableView.DequeueReusableCell((NSString)this.TopRatedCellId);
            if (cell == null)
            {
                cell = new MovieCell(this.TopRatedCellId, _downloader);  //UITableViewCell(UITableViewCellStyle.Default, this.MovieListCellId);
            }
            //cell.UpdateCell(this._response.Results[indexPath.Row]);  //TextLabel.Text = this._response.Results[indexPath.Row].Title;
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