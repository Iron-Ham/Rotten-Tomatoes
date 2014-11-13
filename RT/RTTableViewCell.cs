
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
		public RTTableViewCell () : base (UITableViewCellStyle.Default, Key)
		{
			Thumbnail = new UIImageView ();
			Freshness = new UIImageView ();
			MovieTitle = new UILabel () {
				Font = UIFont.SystemFontOfSize (12f)
			};
			CriticScore = new UILabel () {
				Font = UIFont.SystemFontOfSize (8f)
			};
			abridgedCast = new UILabel () {
				Font = UIFont.SystemFontOfSize (8f)
			};
			RatingAndLength = new UILabel () {
				Font = UIFont.SystemFontOfSize (8f)
			};
			Date = new UILabel () {
				Font = UIFont.SystemFontOfSize (8f)
			};
			ContentView.Add (MovieTitle);
			ContentView.Add (CriticScore);
			ContentView.Add (abridgedCast);
			ContentView.Add (RatingAndLength);
			ContentView.Add (Date);

		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			CriticScore.Frame = new RectangleF (98, 25,25, 25);
			RatingAndLength.Frame = new RectangleF (70, 25 ,320, 12);
			abridgedCast.Frame = new RectangleF (70, 15,320, 12);
			Date.Frame=new RectangleF(80, 65,150, 25);;
			Freshness.Frame = new RectangleF(80, 29, 15, 15);
			MovieTitle.Frame=new RectangleF(70, 5, 320, 12);
		}
		public void UpdateCell(IMovie m) 
		{
			CriticScore.Text = m.ratings.critics_score + "%";
			Console.WriteLine (CriticScore.Text + "\n");
			for (int i = 0; i < 3; i++) { 
				if (i != 2)
					abridgedCast.Text += m.abridged_cast[i].name + ", ";
				if (i == 2)
					abridgedCast.Text += m.abridged_cast [i].name;
			}
			NSUrl n = new NSUrl (m.posters.thumbnail);
			NSData k = NSData.FromUrl (n);

			//Handle Error: No internet connection. 
			Thumbnail.Image = new UIImage (k);
			ImageView.Image = Thumbnail.Image;

			RatingAndLength.Text = m.mpaa_rating + ", ";
			Date.LineBreakMode = UILineBreakMode.WordWrap;
			Date.Lines = 0;
			MovieTitle.LineBreakMode = UILineBreakMode.WordWrap;
			MovieTitle.Lines = 0;
			MovieTitle.Text = m.title;
			RatingAndLength.Text += m.runtime / 60 + " hrs, " + m.runtime%60 + " minutes";
			Date.Text = m.release_dates.theater;
			Add (CriticScore);
			Add (abridgedCast);
			Add (RatingAndLength);
			Add (MovieTitle);
			Add (Date);

			
		}
	}
}

