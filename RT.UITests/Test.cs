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
			Func<AppQuery, AppQuery> topBoxOffice = e => e.Id ("topBox0");  
			Func<AppQuery, AppQuery> tableView = e => e.Id ("TableView");                     

			_app.ScrollDown (tableView);
			_app.WaitForElement (topBoxOffice);
			var cell = _app.Query (topBoxOffice).SingleOrDefault();
			var rating = cell.Label;
			Assert.Equals (rating, "Rotten");    
		}
	}
}

