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
		public void ClickOnFlightsLinks(IWebDriver driver, ExtentTest test)
		{
			driver.FindElement(By.LinkText("Flights")).Click();
			//selecting "one way" option
			driver.FindElement(By.XPath("//body//b//input[2]")).Click();
			//Selecting August from "On" drop down list
			driver.FindElement(By.XPath("//select[@name='fromMonth']//option[contains(text(),'August')]")).Click();
			driver.FindElement(By.Name("findFlights")).Click();
			String Noflights = driver.FindElement(By.XPath("/html[1]/body[1]/div[2]/table[1]/tbody[1]/tr[1]/td[2]/table[1]/tbody[1]/tr[4]/td[1]/table[1]/tbody[1]/tr[1]/td[2]/table[1]/tbody[1]/tr[1]/td[1]/p[1]/font[1]/b[1]/font[1]")).Text;

			if (Noflights.Contains("No Seats Avaialble"))
			{

				test.Log(Status.Pass, "No flights found with the selected search critera");
			}
			else
			{
				test.Log(Status.Info, "This step shows usage of log");
			}

		}

	}
}
