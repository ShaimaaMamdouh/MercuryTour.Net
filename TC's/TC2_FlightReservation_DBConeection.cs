using Amazon.CloudTrail.Model;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Data.SqlClient;
using Xunit;
using System.Data.Common;


namespace MercuryTour.Net
{
	//Before Running this project the following folders should be found in C driver as following "C:\Reports\ScreenShots"
	//Just press start to Run the project :)
	public class Tc2_FlightReservation_DBConeection
	{
		private static ExtentReports extent;
		private static ExtentTest test;
		private static ExtentHtmlReporter htmlReporterTC2;
		private static string ScreenShotsPath;
		private static IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			extent = new ExtentReports();
			htmlReporterTC2 = new ExtentHtmlReporter("C:\\Reports\\Report"+"TC2"+".html");
			ScreenShotsPath = "C:\\Reports\\ScreenShots";
			driver = new ChromeDriver();
			extent.AttachReporter(htmlReporterTC2);
			test = extent.CreateTest("TC2_Flightreservation using LoginData from SQL DB", "Status report for TC2_Flightreservation");

			LaunchBrowser chromeBrowser = new LaunchBrowser();

			chromeBrowser.Launch(driver);
			chromeBrowser.TitleCheck(driver, test);
		}
		[Test]
		public void TC2_FlightReservation002()
		{
			Login user = new Login();
			String username = "";
			String password = "";
			username = user.ReturnDataFromDB(1, "UserName");
			password = user.ReturnDataFromDB(1, "password");
			user.LoginCredentials(username, password, driver);
			user.CheckSucessLogin(driver, test, ScreenShotsPath,"TC2");

			FindFlight flight = new FindFlight();
			flight.ClickOnFlightsLinks(driver, test);

		}
		[TearDown]
		public void ClosingApp()
		{
			CloseBrowser browser = new CloseBrowser();
			browser.Close(driver, test);

			// calling flush writes everything to the log file
			extent.Flush();
		}
	}
}

