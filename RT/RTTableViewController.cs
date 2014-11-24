
using System;
using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace RT
{
	//HomeView
	//Table of 3 sections
	public class RTTableViewController : UITableViewController
	{
		private readonly UINavigationController navControl;
		private readonly RTRepository repository = new RTRepository();

		private RTTableViewSource source;
		//constructor
		public RTTableViewController (UINavigationController navControl)
		{
			this.navControl = navControl;
			var navigationBar = navControl.NavigationBar;
			navigationBar.BarTintColor = UIColor.FromRGB (245, 56, 82);
			navigationBar.TintColor = UIColor.FromRGB (92, 222, 51);
			navigationBar.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White });
			navigationBar.Translucent = false;
			Title = "Rotten Tomatoes";

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

		private async void OnRowSelect(int section, int row)
		{
			MovieRootObject r = new MovieRootObject ();
			ReviewRootObject q = new ReviewRootObject ();
			IMovie movie=null;
			switch (section) {
			case 0:
				movie = source.openingMovies.movies [row];
				break;
			case 1:
				movie = source.topBox.movies [row];
				break;
			case 2:
				movie = source.inTheaters.movies [row];
				break;
			default:
				Console.WriteLine ("Error on row select");
				break;
			}
			r = await repository.RetrieveMovieDetails (movie.links.self + RTApiUrls.APIKey);
			q = await repository.RetrieveReviews (movie.links.reviews + RTApiUrls.APIKey);
			if (r != null && q != null) {
				RTMovieView movieView = new RTMovieView (r, q, navControl);
				var movieDialog = new DialogViewController (movieView.getUI (), true);
				navControl.PushViewController (movieDialog, true);
			}
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
			var topBox						= await repository.RetrieveTopBox();
			var inTheaters				= await repository.RetrieveInTheaters ();
			var openingMovies			= await repository.RetrieveOpeningMovies ();

			source.openingMovies	= openingMovies;
			source.topBox					= topBox;
			source.inTheaters			= inTheaters;

			TableView.ReloadData();
		}
	}
}
