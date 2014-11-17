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
			PathToIPA = Path.Combine(dir, "RT", "bin", "iPhoneSimulator", "Debug", "RT.app");
		}


		[SetUp]
		public void SetUp()
		{
			_app = ConfigureApp.iOS.AppBundle(PathToIPA).StartApp();
		}

		[Test ()]
		public void TestCase ()
		{
		}
	}
}

