using System;
using NUnit.Framework;
using System.Reflection;
using System.IO;
using Xamarin.UITest.iOS;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;

namespace RT.UITests
{
	[TestFixture ()]
	public class Test
	{

		iOSApp _app;

		public string PathToIPA { get; set; }


		[TestFixtureSetUp]
		public void TestFixtureSetup()
		{
			string currentFile = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
			FileInfo fi = new FileInfo(currentFile);
			string dir = fi.Directory.Parent.Parent.Parent.FullName;
			PathToIPA = Path.Combine(dir, "RT", "bin", "iPhoneSimulator", "Debug", "RTiOS.app");
		}


		[SetUp]
		public void SetUp()
		{
			_app = ConfigureApp.iOS.AppBundle(PathToIPA).StartApp();
		}

		[Test ()]
		public void TestCase ()
		{
			Func<AppQuery, AppQuery> tableView = e => e.Class ("UITableView");
			Func<AppQuery, AppQuery> topBoxOffice = e => e.Id ("topBox0");
			TimeSpan p = new TimeSpan (10);
			_app.Repl ();
//		  _app.DragCoordinates (150, 400, 150, 50, p); Nope.
//		  _app.ScrollDown(); Bugged on the simulator. Xamarin Test Cloud is sim-only
//			_app.FlickCoordinates (150, 400, 150, 50); Nope
//			_app.WaitForElement (topBoxOffice); 
//			var cell = _app.Query (topBoxOffice).SingleOrDefault();
//			var rating = cell.Label;
//			var fresh = "Fresh";
//			}
		}
	}
}
