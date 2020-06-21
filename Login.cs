using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MercuryTour.Net
{
	public class Login
	{
		readonly TC1_FlightReservation ReportTest = new TC1_FlightReservation();
		public void LoginCredentials(String userName, String Password)
		{
				//Wait untill submit button is displayed
				WebDriverWait wait = new WebDriverWait(LaunchBrowser.driver, TimeSpan.FromSeconds(5));
				IWebElement WaitForSubmitBtn = wait.Until((d) => d.FindElement(By.Name("submit")));

				LaunchBrowser.driver.FindElement(By.Name("userName")).SendKeys(userName);
				LaunchBrowser.driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]/table/tbody/tr[2]/td[3]/form/table/tbody/tr[4]/td/table/tbody/tr[3]/td[2]/input")).SendKeys(Password);
				LaunchBrowser.driver.FindElement(By.Name("submit")).Click();
		}
		
		public void CheckSucessLogin()
		{

			String sucessMsg = LaunchBrowser.driver.FindElement(By.TagName("h3")).Text;



			if (sucessMsg.Equals("Login Successfully"))
			{
				TC1_FlightReservation.test.Log(Status.Pass, "User Logged in successfully");
				// test with screenShot
				TC1_FlightReservation.test.Pass("ScreenShot", MediaEntityBuilder.CreateScreenCaptureFromPath("ScreenSHot.png").Build());
				TC1_FlightReservation.test.Pass("ScreenShot").AddScreenCaptureFromPath("ScreenSHot.png");
			}
			else
			{
				TC1_FlightReservation.test.Log(Status.Fail, "User can't Logged in successfully");
				// test with screenShot
				TC1_FlightReservation.test.Pass("ScreenShot", MediaEntityBuilder.CreateScreenCaptureFromPath("ScreenSHot.png").Build());
				TC1_FlightReservation.test.Pass("ScreenShot").AddScreenCaptureFromPath("ScreenSHot.png");
			}
		}
	}
}

