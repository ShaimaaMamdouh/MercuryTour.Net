using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using Xunit;

namespace MercuryTour.Net
{
	//Before Running this project the following folders should be found in C driver as following "C:\Reports\ScreenShots"
	//Just press start to Run the project :)
	public class TC1_FlightReservation
	{
		public static ExtentReports extent = new ExtentReports();
		public static ExtentTest test;
		public static ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter("C:\\Reports\\Report.html");
		public static string ScreenShotsPath= "C:\\Reports\\ScreenShots";

			public static void Main ()
			{
			
			extent.AttachReporter(htmlReporter);
			 test = extent.CreateTest("TC1_Flightreservation", "Status report for TC1_Flightreservation");

			LaunchBrowser chromeBrowser = new LaunchBrowser();
				chromeBrowser.Launch();
				chromeBrowser.TitleCheck();

			Login user = new Login();
			String ExcelFilepath = "C:\\Reports\\Users.xlsx";
			int SheetNumber = 1;
			user.OpenExcel(ExcelFilepath, SheetNumber);
			String username = user.ReadFromExcel(2, 1);
			String password= user.ReadFromExcel(2, 2);
			user.LoginCredentials(username, password);
			user.CloseExcel();
			user.CheckSucessLogin();

			FindFlight flight = new FindFlight();
			flight.ClickOnFlightsLinks();

			CloseBrowser browser = new CloseBrowser();
			browser.Close();

			// calling flush writes everything to the log file
			extent.Flush();
		}
		}
	}


