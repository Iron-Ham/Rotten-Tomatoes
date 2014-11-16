
using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace RT
{
	public partial class RTMovieView : DialogViewController
	{
		public RTMovieView () : base (UITableViewStyle.Grouped, null)
		{

		}

		public RootElement getUI(IMovie movie){
			UIImage thumbnail = UIImage.LoadFromData (NSData.FromUrl (new NSUrl (movie.posters.thumbnail)));
			UIImage Rotten = UIImage.FromBundle ("rotten.png");
			UIImage Fresh = UIImage.FromBundle ("fresh.png");
			var Root_Element = new RootElement ("Movie Details");
			return Root_Element;
		}
	}
}
