
using System;
using System.Drawing;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RT
{
	public class RTTableViewSource : UITableViewSource
	{
		public TopBoxRootObject topBox { get; set;} 
		public InTheatersRootObject inTheaters { get; set;} 
		public OpeningRootObject openingMovies { get; set;}
		public Action<int> OnRowSelect;
		private const string ValueCell = "Id";

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow(indexPath, true);

			if (OnRowSelect != null)
			{
				OnRowSelect(indexPath.Row);
			}
		}

		public RTTableViewSource ()
		{
			topBox = new TopBoxRootObject ();
			inTheaters = new InTheatersRootObject ();
			openingMovies = new OpeningRootObject ();
		}

		public override int NumberOfSections (UITableView tableView)
		{
			// TODO: return the actual number of sections
			return 3;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			// TODO: return the actual number of items in the section

			switch (section) {
			case 0:
				return openingMovies.movies.Count;
			case 1:
				return topBox.movies.Count;
			case 2:
				return inTheaters.movies.Count;
			default:
				return -1;
			}
		}

		public override string TitleForHeader (UITableView tableView, int section)
		{
			switch (section) {
			case 0:
				return "Opening This Week";
			case 1:
				return "Top Box Office";
			case 2:
				return "Also In Theaters";
			default:
				return "ERR000";
			}
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (RTTableViewCell.Key) as RTTableViewCell;
			if (cell == null)
				cell = new RTTableViewCell ();
			
			// TODO: populate the cell with the appropriate data based on the indexPath
			switch (indexPath.Section) {
			case 0: 
				//Opening This Week
				IMovie openingFilm = openingMovies.movies [indexPath.Row];
				cell.UpdateCell (openingFilm);

				return cell;
			case 1:
				IMovie topFilm = topBox.movies[indexPath.Row];
				cell.UpdateCell(topFilm);
				return cell;
			case 2:
				IMovie alsoRanFilm = inTheaters.movies[indexPath.Row];
				cell.UpdateCell(alsoRanFilm);
				return cell;
			default:
				return cell;
			}
		}
	}
}

