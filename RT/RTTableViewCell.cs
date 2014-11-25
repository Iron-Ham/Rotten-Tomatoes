
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RT
{
	//The cell for each movie in the initial screen
	public class RTTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("RTTableViewCell");
		public UILabel MovieTitle, CriticScore, abridgedCast, RatingAndLength, Date;
		public UIImageView Thumbnail, Freshness;
		IMovie m;
		//Constructor
		public RTTableViewCell (string id, IMovie movie) : base (UITableViewCellStyle.Default, id)
		{
			m = movie;
			Thumbnail = new UIImageView ();
			Freshness = new UIImageView ();
			NSUrl n = new NSUrl (m.posters.thumbnail);
			NSData k = NSData.FromUrl (n);
			Thumbnail.Image = new UIImage (k);
			MovieTitle = new UILabel () {
				Font = UIFont.SystemFontOfSize (12f),
				Lines = 2,
				LineBreakMode = UILineBreakMode.WordWrap
			};
			CriticScore = new UILabel () {
				Font = UIFont.SystemFontOfSize (11f)
			};
			abridgedCast = new UILabel () {
				Font = UIFont.SystemFontOfSize (11f)
			};
			RatingAndLength = new UILabel () {
				Font = UIFont.SystemFontOfSize (11f)
			};
			ContentView.Add (MovieTitle);
			ContentView.Add (CriticScore);
			ContentView.Add (abridgedCast);
			ContentView.Add (RatingAndLength);
			ContentView.Add (Freshness);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			CriticScore.Frame = new RectangleF (108, 61, 212, 25);
			RatingAndLength.Frame = new RectangleF (90, 46, 230, 25);
			abridgedCast.Frame = new RectangleF (90, 33, 230, 25);
			Freshness.Frame = new RectangleF (90, 65, 15, 15);
			MovieTitle.Frame = new RectangleF (90, 5, 230, 35);
		}

		//Updates all cell info.
		public void UpdateCell()
		{

			ImageView.Image = Thumbnail.Image;
			if (m.ratings.critics_score != null) {
				if (m.ratings.critics_score != -1)
					CriticScore.Text = m.ratings.critics_score + "%";
				Add (CriticScore);
			}

			switch (m.ratings.critics_rating) {
			case "Certified Fresh":
				UIImage CFimg = UIImage.FromBundle ("CF_120x120.png");
				Freshness.Image = CFimg;
				break;
			case "Rotten":
				UIImage RTimg = UIImage.FromBundle ("rotten.png");
				Freshness.Image = RTimg;
				break;
			case "Fresh":
				UIImage FTimg = UIImage.FromBundle ("fresh.png");
				Freshness.Image = FTimg;
				break;
			}

			if (m.abridged_cast.Count > 0) {
				abridgedCast.Text = (m.abridged_cast.Count > 1) ? m.abridged_cast [0].name + ", " + m.abridged_cast [1].name : m.abridged_cast [0].name;
				Add (abridgedCast);
			}

			if (m.mpaa_rating != null && m.runtime != null) {
				RatingAndLength.Text = m.mpaa_rating + ", ";
				RatingAndLength.Text += m.runtime / 60 + " hr. " + m.runtime % 60 + " minutes";
				Add (RatingAndLength);
			} else if (m.mpaa_rating != null) {
				RatingAndLength.Text = m.mpaa_rating;
				Add (RatingAndLength);
			} else if (m.runtime != null) {
				RatingAndLength.Text += m.runtime / 60 + " hr. " + m.runtime % 60 + " minutes";
				Add (RatingAndLength);
			}
			if (m.title != null) {
				MovieTitle.Text = m.title;
				Add (MovieTitle);
			}
		}
	}
}
