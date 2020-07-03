using Amazon.CloudTrail.Model;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace MercuryTour.Net
{
	//Before Running this project the following folders should be found in C driver as following "C:\Reports\ScreenShots"
	//Just press start to Run the project :)
	public class TC1_FlightReservation
	{
		private static ExtentReports extent;
		private static ExtentTest test;
		private static ExtentHtmlReporter htmlReporter;
		private static string ScreenShotsPath;
		private static IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			extent = new ExtentReports();
			htmlReporter = new ExtentHtmlReporter("C:\\Reports\\Report"+"TC1"+".html");
			ScreenShotsPath = "C:\\Reports\\ScreenShots";
			driver = new ChromeDriver();
			extent.AttachReporter(htmlReporter);
			test = extent.CreateTest("TC1_Flightreservation using LoginData From Excel sheet", "Status report for TC1_Flightreservation");

			LaunchBrowser chromeBrowser = new LaunchBrowser();

			chromeBrowser.Launch(driver);
			chromeBrowser.TitleCheck(driver, test);
		}

		[Test]
		public void TC1_FlightReservation001()
		{
			Login user = new Login();
			String ExcelFilepath = "C:\\Reports\\Users.xlsx";
			int SheetNumber = 1;
			user.OpenExcel(ExcelFilepath, SheetNumber);
			String username = user.ReadFromExcel(2, 1);
			String password = user.ReadFromExcel(2, 2);
			user.LoginCredentials(username, password,driver);
			user.CloseExcel();
			user.CheckSucessLogin(driver, test, ScreenShotsPath,"TC1");

			FindFlight flight = new FindFlight();
			flight.ClickOnFlightsLinks(driver,test);
		}

		[TearDown]
		public void ClosingApp()
		{
			CloseBrowser browser = new CloseBrowser();
			browser.Close(driver, test);

			// calling flush writes everything to the log file
			//extent.Flush();
		}
	}
}


