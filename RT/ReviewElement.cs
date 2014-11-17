using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.Foundation;


namespace RT
{
	public class ReviewElement : StyledMultilineElement, IElementSizing
	{
		Review r = new Review();
		public ReviewElement (string reviewText, Review R) : base (reviewText)
		{
			r = R; 
		}

		// To retrieve the UITableViewCell for your element
		// you would need to prepare the cell to be reused, in the
		// same way that UITableView expects reusable cells to work
		public override UITableViewCell GetCell (UITableView tv) {
			var cell = tv.DequeueReusableCell ("ViewCell") as UITableViewCell;
			if (cell == null)
				cell = new UITableViewCell ();
			return cell;
		}
		// To detect when the user has tapped on the cell
		public override void Selected (DialogViewController dvc, UITableView tableView, NSIndexPath path) {

		}
	}
}

