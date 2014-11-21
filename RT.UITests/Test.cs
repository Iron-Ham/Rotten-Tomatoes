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
			_app.ScrollDown(); // is bugged in simulator. Only works on real devices -- I have no test device at the moment
		}

		[Test ()]
		public void TestCase ()
		{
			Func<AppQuery, AppQuery> topBoxOffice = e => e.Id ("topBox0");
			TimeSpan p = new TimeSpan (10);
			_app.DragCoordinates (150, 400, 150, 50, p);
			_app.WaitForElement (topBoxOffice);
			var cell = _app.Query (topBoxOffice).SingleOrDefault();
			var rating = cell.Label;
			var fresh = "Fresh";
			if (rating.Equals (fresh)) {
				Assert.Pass ();
			} else {
				Assert.Fail ();
			}
		}
	}
}

