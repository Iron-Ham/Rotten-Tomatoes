using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RT
{
	//A wrapper for a UIWebView
	public class ReviewView : UIViewController
	{
		private readonly string url; 
		public ReviewView (string title, string url)
		{
			Title = title; 
			this.url = url;
		}

		public override void ViewDidLoad ()
		{
			var webView = new UIWebView (View.Bounds);
			webView.LoadRequest (new NSUrlRequest (new NSUrl (url)));
			webView.ScalesPageToFit = true;
			View.AddSubview (webView);
		}
	}
}

