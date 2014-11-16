
using System;
using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RT
{
	public class RTTableViewController : UITableViewController
	{
		private readonly UINavigationController navControl;
		private readonly RTRepository repository = new RTRepository();

		private RTTableViewSource source; 

		public RTTableViewController (UINavigationController navControl) 
		{
			this.navControl = navControl;
		}


		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Register the TableView's data source
			source = new RTTableViewSource ();
			TableView = new UITableView(Rectangle.Empty) {Source = source};
			RefreshControl = new UIRefreshControl();
			RefreshControl.ValueChanged += RefreshControlOnValueChanged;
			source.OnRowSelect = OnRowSelect; 
		}

		private void OnRowSelect(int section, int row)
		{
			IMovie movie;
			switch (section) {
			case 0:
				movie = source.openingMovies [row];
				break; 
			case 1:
				movie = source.topBox [row];
				break;
			case 2:
				movie = source.inTheaters [row];
				break;
			default:
				Console.WriteLine ("Error on row select");
				break;
			}
			navControl.PushViewController(new RTMovieViewController(movie);

			//navigationController.PushViewController(new EntryViewController(entry.Title, entry.Url), true);
		}

		public async override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			await LoadMoviesAsync ();
		}

		private async void RefreshControlOnValueChanged(object sender, EventArgs eventArgs)
		{
			await LoadMoviesAsync();
			RefreshControl.EndRefreshing();
		}

		private async Task LoadMoviesAsync()
		{
			var topBox 		  = await repository.RetrieveTopBox();
			var inTheaters 	  = await repository.RetrieveInTheaters ();
			var openingMovies = await repository.RetrieveOpeningMovies ();


			source.openingMovies = openingMovies;
			source.topBox = topBox;
			source.inTheaters = inTheaters;

			TableView.ReloadData();
		}
	}
}

