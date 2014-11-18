
using System;
using System.Drawing;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RT
{
	//Data source for RTTableViewController
	public class RTTableViewSource : UITableViewSource
	{
		public TopBoxRootObject topBox { get; set;} 
		public InTheatersRootObject inTheaters { get; set;} 
		public OpeningRootObject openingMovies { get; set;}
		public Action<int, int> OnRowSelect;
		private const string ValueCell = "Id";
		private NSString ID = (NSString) "Id";


		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow(indexPath, true);
			if (OnRowSelect != null)
			{
				OnRowSelect(indexPath.Section, indexPath.Row);
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
			var cell = tableView.DequeueReusableCell (ValueCell) as RTTableViewCell;
			if (cell == null)
				cell = new RTTableViewCell (ID);
			switch (indexPath.Section) {
			case 0: 
				IMovie openingFilm = openingMovies.movies [indexPath.Row];
				cell.UpdateCell (openingFilm);
				return cell;
			case 1:
				IMovie topFilm = topBox.movies [indexPath.Row];
				cell.UpdateCell (topFilm);
				cell.AccessibilityIdentifier = "topBox" + indexPath.Row;
				cell.AccessibilityLabel = topFilm.ratings.critics_rating;
				return cell;
			case 2:
				IMovie alsoRanFilm = inTheaters.movies[indexPath.Row];
				cell.UpdateCell(alsoRanFilm);
				return cell;
			default:
				return cell;
			}
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 100f;
		}
	}
}