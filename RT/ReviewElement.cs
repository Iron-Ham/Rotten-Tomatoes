using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;


namespace RT
{
	//Design pattern adapted from Miguel de Icaza's blog -- Tirania
	//The aux. UIView for ReviewCell
	public class ReviewCellView : UIView {
		Review R = new Review();
		public ReviewCellView(Review R){
			Update (R);
		}
		public void Update (Review R)
		{
			this.R = R; 
			SetNeedsDisplay ();
		}
	}

	//The cell which forms the basis of the Review element. 
	public class ReviewCell : UITableViewCell {
		ReviewCellView rcv; 
		UILabel publication, critic, quote; 
		UIImageView freshness = new UIImageView(); 


		public ReviewCell(Review R, NSString identKey) : base (UITableViewCellStyle.Default, identKey)
		{
			rcv = new ReviewCellView (R);
			freshness.Image = (R.freshness == "fresh") ? UIImage.FromBundle ("fresh.png") : UIImage.FromBundle ("rotten.png");
			critic = new UILabel () {
				Text = R.critic
			};
			critic.Font = UIFont.BoldSystemFontOfSize (16f);
			publication = new UILabel () {
				Text = R.publication
			};
			publication.Font = UIFont.BoldSystemFontOfSize (16f);
			quote = new UILabel () {
				Text = R.quote
			};
			quote.Lines = 3; 
			quote.Font = UIFont.SystemFontOfSize (14f);
			quote.LineBreakMode = UILineBreakMode.TailTruncation;
			ContentView.Add (rcv);
			ContentView.Add (freshness);
			ContentView.Add (critic);
			ContentView.Add (publication);
			ContentView.Add (quote);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			rcv.Frame = ContentView.Bounds;
			rcv.SetNeedsDisplay ();
			freshness.Frame = new RectangleF(5, 5, 15, 15);
			critic.Frame = new RectangleF(25, 5, 295, 20);
			publication.Frame = new RectangleF(25, 25, 315, 20);
			quote.Frame = new RectangleF(5, 35, 315, 70); 

		}

		public void Updatecell (Review newData)
		{
			rcv.Update (newData);
		}

	}

	//Element that holds a review cell
	public class ReviewElement : Element, IElementSizing
	{
		static NSString key = new NSString ("myReviewElement");
		public Review r; 

		public ReviewElement (Review R) : base (null)
		{
			r = R; 
		}
		readonly float cellHeight = 100f;
		public float GetHeight (UITableView tableView, NSIndexPath indexPath)
		{
			return cellHeight;
		}


		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (key) as ReviewCell;
			if (cell == null)
				cell = new ReviewCell (r, key);
			return cell;

		}
		// To detect when the user has tapped on the cell
		public override void Selected (DialogViewController dvc, UITableView tableView, NSIndexPath path) {
			dvc.NavigationController.PushViewController (new ReviewView (r.publication, r.links.review), true);
		}
	}
}

