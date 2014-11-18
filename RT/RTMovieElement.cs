using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace RT
{
	public class RTMovieCellView: UIView {
		IMovie m;
		public RTMovieCellView(IMovie m) 
		{
			Update(m);
		}
		public void Update (IMovie m)
		{
			this.m = m;
			SetNeedsDisplay ();
		}
	}

	public class RTMovieCell :UITableViewCell {
		RTMovieCellView mcv; 
		UILabel MovieTitle, CriticScore, Cast, RatingAndLength, Date;
		public UIImageView Thumbnail, Freshness;
		public RTMovieCell(IMovie m, NSString identKey) : base (UITableViewCellStyle.Default, identKey)
		{
			Freshness = new UIImageView ();
			Thumbnail = new UIImageView ();
			mcv = new RTMovieCellView (m);
			ContentView.Add (mcv);
			switch (m.ratings.critics_rating) {
			case "Certified Fresh":
				Freshness.Image = UIImage.FromBundle ("CF_120x120.png");
				break;
			case "Rotten":
				Freshness.Image = UIImage.FromBundle ("rotten.png");
				break;
			case "Fresh":
				Freshness.Image = UIImage.FromBundle ("fresh.png");
				break;
			}
			ContentView.Add (Freshness);

			MovieTitle = new UILabel () {
				Text = m.title
			};
			MovieTitle.Lines = 2;
			MovieTitle.Font = UIFont.SystemFontOfSize (16f);
			ContentView.Add (MovieTitle);

			CriticScore = new UILabel () {
				Text = m.ratings.critics_score + "%"
			};
			CriticScore.Font = UIFont.SystemFontOfSize (14f);
			ContentView.Add (CriticScore);

			if (m.abridged_cast.Count > 0) {
				Cast = new UILabel() {
					Text = (m.abridged_cast.Count > 1) ? m.abridged_cast [0].name + ", " + m.abridged_cast [1].name : m.abridged_cast [0].name
				};
				Cast.Font = UIFont.SystemFontOfSize (14f);
				ContentView.Add(Cast);
			}

			RatingAndLength = new UILabel () {
				Font = UIFont.SystemFontOfSize (14f)
			};
			if (m.mpaa_rating != null && m.runtime != null) {
				RatingAndLength.Text = m.mpaa_rating + ", ";
				RatingAndLength.Text += m.runtime / 60 + " hr. " + m.runtime % 60 + " minutes";
				ContentView.Add (RatingAndLength);
			} else if (m.mpaa_rating != null) {
				RatingAndLength.Text = m.mpaa_rating + ", ";
				ContentView.Add (RatingAndLength);
			} else if (m.runtime != null) {
				RatingAndLength.Text += m.runtime / 60 + " hr. " + m.runtime % 60 + " minutes";
				ContentView.Add (RatingAndLength);
			}

			Date = new UILabel() {
				Text = m.release_dates.theater
			};
			Date.Font = UIFont.SystemFontOfSize (14f);
			ContentView.Add(Date);
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			CriticScore.Frame = new RectangleF (108, 56, 212, 25);
			RatingAndLength.Frame = new RectangleF (90, 41, 230, 25);
			Cast.Frame = new RectangleF (90, 28, 230, 25);
			Freshness.Frame = new RectangleF (90, 60, 15, 15);
			MovieTitle.Frame = new RectangleF (90, 5, 230, 25);
		}

		public void UpdateCell(IMovie newMovie)
		{
			mcv.Update (newMovie);
		}
	}


	public class RTMovieElement : Element, IElementSizing
	{
		public IMovie m;
		public RTRepository repository = new RTRepository ();
		public RTMovieElement (IMovie M) : base (null)
		{
			m = M; 
		}
		readonly float cellHeight = 100f;
		public float GetHeight (UITableView tableView, NSIndexPath indexPath)
		{
			return cellHeight;
		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (CellKey) as RTMovieCell;
			if (cell == null)
				cell = new RTMovieCell (m, CellKey);
			return cell;
		}
		public async override void Selected (DialogViewController dvc, UITableView tableView, NSIndexPath path)
		{
			var r = await repository.RetrieveMovieDetails (m.links.self + RTApiUrls.APIKey);
			var q = await repository.RetrieveReviews (m.links.reviews + RTApiUrls.APIKey);
			RTMovieView mv = new RTMovieView (r, q, dvc.NavigationController);
			var mdv = new DialogViewController (mv.getUI (), true);
			dvc.NavigationController.PushViewController (mdv, true);
		}

	}
}

