
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
	public partial class RTMovieView : DialogViewController
	{
		public MovieRootObject movieDetails { get; set; }
		public ReviewRootObject reviewList { get; set; }
		string title = null; 
		UINavigationController navControl;
		public RTMovieView (MovieRootObject r, ReviewRootObject q, UINavigationController navControl) : base (UITableViewStyle.Grouped, null)
		{
			movieDetails = r;
			reviewList = q;
			this.navControl = navControl;
			title = r.title; 
		}

		public RootElement getUI(){
			var DirectedBy = new Section ("Directed By"); 
			var directorString = "";
			for (int i = 0; i < movieDetails.abridged_directors.Count; i++) 
				directorString += (movieDetails.abridged_directors.Count - 1 == i) ? movieDetails.abridged_directors[i].name : movieDetails.abridged_directors[i].name + ", ";
			var directors = new StringElement (directorString);
			DirectedBy.Add (directors);

			var Synopsis = new Section ("Synopsis");
			var synopsisText = new MultilineElement (movieDetails.synopsis);
			Synopsis.Add (synopsisText);
			var RootElement = new RootElement (title);

			var Cast = new Section ("Cast"); 
			foreach (var actor in movieDetails.abridged_cast) {
				var a = new StringElement (actor.name);
				Cast.Add (a);
			}

			var MPAARating = new Section ("MPAA Rating"); 
			var mpaaRating = new StringElement(movieDetails.mpaa_rating);
			MPAARating.Add (mpaaRating);

			var movieRuntime = new Section ("Runtime");
			var runtime = new StringElement (string.Format ("{0} hr. {1} min.", movieDetails.runtime / 60, movieDetails.runtime % 60));
			movieRuntime.Add (runtime);

			var Genres = new Section ("Genre(s)"); 
			foreach (var genre in movieDetails.genres) {
				var g = new StringElement (genre);
				Genres.Add (g);
			}

			var ReleaseDate = new Section ("Release Date"); 
			var date = new StringElement (movieDetails.release_dates.theater);
			ReleaseDate.Add (date);

			var CriticReviews = new Section ("Critic Reviews");
			foreach (Review R in reviewList.reviews) {
				var reviewEl = new ReviewElement (R);
				CriticReviews.Add(reviewEl);
			}

			RootElement.Add (Cast);
			RootElement.Add (DirectedBy);
			RootElement.Add (MPAARating);
			RootElement.Add (movieRuntime);
			RootElement.Add (Genres);
			RootElement.Add (ReleaseDate);
			RootElement.Add (Synopsis);
			RootElement.Add(CriticReviews);

			return RootElement;
		}
	}

}
