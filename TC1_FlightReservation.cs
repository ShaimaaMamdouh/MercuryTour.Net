using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using Xunit;

namespace MercuryTour.Net
{
	//Just press start to Run the project :)
    public class TC1_FlightReservation
	{
		public static ExtentReports extent = new ExtentReports();
		public static ExtentTest test;
		public static ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter("C:\\Reports\\Report.html");

			public static void Main (String[]args)
			{
			
			extent.AttachReporter(htmlReporter);
			 test = extent.CreateTest("TC1_Flightreservation", "Status report for TC1_Flightreservation");

			LaunchBrowser chromeBrowser = new LaunchBrowser();
				chromeBrowser.Launch();
				chromeBrowser.TitleCheck();

			Login user = new Login();
			user.LoginCredentials("Mercury", "Mercury");
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


