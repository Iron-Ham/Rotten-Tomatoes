﻿using System;
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

		public override float GetHeightForHeader(UITableView tableView, int section)
		{
			return 40;
		}

		public override UIView GetViewForHeader(UITableView tableView, int section)
		{
			var header = new UIView(new RectangleF(0, 0, 320, 40));
			header.BackgroundColor = UIColor.FromRGB(30, 124, 0);

			var headerLabel = new UILabel(new RectangleF(10, 0, 320, 40))
			{
				TextColor = UIColor.White
			};

			switch (section)
			{
			case 0: 
				headerLabel.Text = "Opening This Week";
				break;
			case 1: 
				headerLabel.Text = "Top Box Office";
				break;
			case 2: 
				headerLabel.Text = "Also In Theaters";
				break;
			default:
				headerLabel.Text = null;
				break;
			}
			header.Add(headerLabel);
			return header;
		}

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
			IMovie Movie = null; 
			switch (indexPath.Section) {
			case 0: 
				Movie = openingMovies.movies [indexPath.Row];
				break;
			case 1:
				Movie = topBox.movies [indexPath.Row];
				break;
			case 2:
				Movie = inTheaters.movies [indexPath.Row];
				break;
			}

			var cell = tableView.DequeueReusableCell (Movie.id) as RTTableViewCell;
			if (cell == null)
				cell = new RTTableViewCell (Movie.id);
			cell.UpdateCell (Movie);
			return cell; 
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 100f;
		}
	}
}
