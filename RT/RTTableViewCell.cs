
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RT
{
	public class RTTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("RTTableViewCell");
		public UILabel MovieTitle, CriticScore, abridgedCast, MPAARating, Length, Date;
		public UIImageView Thumbnail, Freshness; 
		public RTTableViewCell () : base (UITableViewCellStyle.Default, Key)
		{
			Thumbnail = new UIImageView ();
			Freshness = new UIImageView ();
			MovieTitle = new UILabel () {
				Font = UIFont.SystemFontOfSize (14f)
			};
			CriticScore = new UILabel () {
				Font = UIFont.SystemFontOfSize (10f)
			};
			abridgedCast = new UILabel () {
				Font = UIFont.SystemFontOfSize (10f)
			};
			MPAARating = new UILabel () {
				Font = UIFont.SystemFontOfSize (10f)
			};
			Length = new UILabel () {
				Font = UIFont.SystemFontOfSize (10f)
			};
			Date = new UILabel () {
				Font = UIFont.SystemFontOfSize (10f)
			};
			ContentView.Add (MovieTitle);
			ContentView.Add (CriticScore);
			ContentView.Add (abridgedCast);
			ContentView.Add (MPAARating);
			ContentView.Add (Length);
			ContentView.Add (Date);

		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			CriticScore.Frame = new RectangleF (98, 25,25, 25);
			MPAARating.Frame = new RectangleF (80, 50,50, 25);
			abridgedCast.Frame = new RectangleF (80, 37,300, 25);
			Length.Frame=new RectangleF(115,50 ,100, 25);
			Date.Frame=new RectangleF(80, 65,150, 25);;
			Freshness.Frame = new RectangleF(80, 29, 15, 15);
			MovieTitle.Frame=new RectangleF(80, 10, 300, 25);
		}
		public void UpdateCell(IMovie m) 
		{
			CriticScore.Text = m.ratings.critics_score + "%";
			foreach(AbridgedCast a in abridgedCast) {
				abridgedCast.Text += a.name; 
			}
			NSUrl n = new NSUrl (m.posters.thumbnail);
			NSData k = NSData.FromUrl (n);
			Thumbnail.Image = new UIImage (k);
			ImageView.Image = Thumbnail.Image;
			abridgedCast.Text = m.mpaa_rating;
			MPAARating.Text = m.mpaa_rating;
			CriticScore.LineBreakMode = UILineBreakMode.WordWrap;
			CriticScore.Lines = 0;
			abridgedCast.LineBreakMode = UILineBreakMode.WordWrap;
			abridgedCast.Lines = 0;
			Length.LineBreakMode = UILineBreakMode.WordWrap;
			Length.Lines = 0;
			Date.LineBreakMode = UILineBreakMode.WordWrap;
			Date.Lines = 0;
			MPAARating.LineBreakMode = UILineBreakMode.WordWrap;
			MPAARating.Lines = 0;
			MovieTitle.LineBreakMode = UILineBreakMode.WordWrap;
			MovieTitle.Lines = 0;
			MovieTitle.Text = m.title;
			Length.Text = "Length: " + m.runtime / 60 + " hrs, " + m.runtime%60 + " minutes";
			Date.Text = m.release_dates.theater;
			Add (CriticScore);
			Add (abridgedCast);
			Add (MPAARating);
			Add (MovieTitle);
			Add (Length);
			Add (Date);

			
		}
	}
}

