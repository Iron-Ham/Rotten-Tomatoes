
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RT
{
	public class RTTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("RTTableViewCell");
		public UILabel MovieTitle, CriticScore, abridgedCast, RatingAndLength, Date;
		public UIImageView Thumbnail, Freshness;
		public RTTableViewCell (string id) : base (UITableViewCellStyle.Default, id)
		{
			Thumbnail = new UIImageView ();
			Freshness = new UIImageView ();
			MovieTitle = new UILabel () {
				Font = UIFont.SystemFontOfSize (12f)
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
			Date = new UILabel () {
				Font = UIFont.SystemFontOfSize (11f)
			};
			ContentView.Add (MovieTitle);
			ContentView.Add (CriticScore);
			ContentView.Add (abridgedCast);
			ContentView.Add (RatingAndLength);
			ContentView.Add (Freshness);
			ContentView.Add (Date);

		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			CriticScore.Frame = new RectangleF (108, 56, 212, 25);
			RatingAndLength.Frame = new RectangleF (90, 41, 230, 25);
			abridgedCast.Frame = new RectangleF (90, 28, 230, 25);
			Freshness.Frame = new RectangleF (90, 60, 15, 15);
			MovieTitle.Frame = new RectangleF (90, 5, 230, 25);
		}

		public void UpdateCell(IMovie m)
		{
			if (m.ratings.critics_score != -1)
				CriticScore.Text = m.ratings.critics_score + "%";
			if (m.abridged_cast.Count > 0) {
				abridgedCast.Text = (m.abridged_cast.Count > 1) ? m.abridged_cast [0].name + ", " + m.abridged_cast [1].name : m.abridged_cast [0].name;
			}
			NSUrl n = new NSUrl (m.posters.thumbnail);
			NSData k = NSData.FromUrl (n);
			Thumbnail.Image = new UIImage (k);
			ImageView.Image = Thumbnail.Image;
			switch (m.ratings.critics_rating) {
			case "Certified Fresh":
				UIImage CFimg = UIImage.FromBundle ("CF_120x120.png");
				Freshness.Image = CFimg;
				break;
			case "Rotten":
				UIImage RTimg = UIImage.FromBundle("rotten.png");
				Freshness.Image = RTimg; 
				break;
			case "Fresh": 
				UIImage FTimg = UIImage.FromBundle("fresh.png");
				Freshness.Image = FTimg;
				break;
			}
			RatingAndLength.Text = m.mpaa_rating + ", ";
			Date.LineBreakMode = UILineBreakMode.WordWrap;
			Date.Lines = 0;
			MovieTitle.LineBreakMode = UILineBreakMode.TailTruncation;
			MovieTitle.Text = m.title;
			RatingAndLength.Text += m.runtime / 60 + " hr. " + m.runtime%60 + " minutes";
			Date.Text = m.release_dates.theater;
			Add (CriticScore);
			Add (abridgedCast);
			Add (RatingAndLength);
			Add (MovieTitle);
			Add (Date);
		}
	}
}

