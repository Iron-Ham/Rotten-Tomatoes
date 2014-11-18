using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
namespace RT
{
	public partial class RTTableView : DialogViewController
	{
		public readonly UINavigationController navControl;
		public RTRepository repository { get; set; }
		public TopBoxRootObject topBox { get; set;} 
		public InTheatersRootObject inTheaters { get; set;} 
		public OpeningRootObject openingMovies { get; set;}

		public RTTableView (UINavigationController navControl) : base (UITableViewStyle.Plain, null)
		{
			this.navControl = navControl;
			Title = "Rotten Tomatoes";
			topBox = new TopBoxRootObject ();
			inTheaters = new InTheatersRootObject ();
			openingMovies = new OpeningRootObject ();
		}

		public async override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			await LoadMoviesAsync ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			RefreshControl = new UIRefreshControl ();
			RefreshControl.ValueChanged += RefreshControlOnValueChanged;
		}

		private async void RefreshControlOnValueChanged(object sender, EventArgs eventArgs)
		{
			await LoadMoviesAsync();
			RefreshControl.EndRefreshing();
		}

		private async Task LoadMoviesAsync()
		{
			var topBox 		     = await repository.RetrieveTopBox();
			var inTheaters 	     = await repository.RetrieveInTheaters ();
			var openingMovies    = await repository.RetrieveOpeningMovies ();

			this.openingMovies   = openingMovies;
			this.topBox          = topBox;
			this.inTheaters      = inTheaters;

			TableView.ReloadData();
		}

		public RootElement getUI() {
			var RootElement = new RootElement (Title);

			var oMovies = new Section("Opening Movies");
			foreach (IMovie i in openingMovies.movies) {
				var o = new RTMovieElement (i);
				oMovies.Add (o);
			}
			RootElement.Add (oMovies);

			var tMovies = new Section ("Top Box Office");
			foreach (IMovie i in topBox.movies) {
				var t = new RTMovieElement (i);
				tMovies.Add (t);
			}
			RootElement.Add (tMovies);

			var aMovies = new Section ("Also in Theaters");
			foreach (IMovie i in inTheaters.movies) {
				var m = new RTMovieElement (i);
				aMovies.Add (m);
			}
			RootElement.Add (aMovies);


			return RootElement;
		}
	}
}

