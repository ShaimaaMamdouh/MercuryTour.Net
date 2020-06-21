using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercuryTour.Net
{
    class FindFlight
    {
        readonly TC1_FlightReservation ReportTest = new TC1_FlightReservation();
		public void ClickOnFlightsLinks()
		{
			LaunchBrowser.driver.FindElement(By.LinkText("Flights")).Click();
			//selecting "one way" option
			LaunchBrowser.driver.FindElement(By.XPath("//body//b//input[2]")).Click();
			//Selecting August from "On" drop down list
			LaunchBrowser.driver.FindElement(By.XPath("//select[@name='fromMonth']//option[contains(text(),'August')]")).Click();
			LaunchBrowser.driver.FindElement(By.Name("findFlights")).Click();
			String Noflights = LaunchBrowser.driver.FindElement(By.XPath("/html[1]/body[1]/div[2]/table[1]/tbody[1]/tr[1]/td[2]/table[1]/tbody[1]/tr[4]/td[1]/table[1]/tbody[1]/tr[1]/td[2]/table[1]/tbody[1]/tr[1]/td[1]/p[1]/font[1]/b[1]/font[1]")).Text;

			if (Noflights.Contains("No Seats Avaialble"))
			{

				TC1_FlightReservation.test.Log(Status.Pass, "No flights found with the selected search critera");
			}
			else
			{
				TC1_FlightReservation.test.Log(Status.Info, "This step shows usage of log");
			}

		}

	}
}
