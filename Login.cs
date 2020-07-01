using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RazorEngine.Compilation.ImpromptuInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Windows;
using Amazon.SimpleEmail.Model;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MercuryTour.Net
{
	public class Login
	{
		String path;
		_Application excel = new _Excel.Application();
		Workbook wb;
		Worksheet ws;
		public void OpenExcel(String ExcelFilepath, int SheetNumber)
		{
			wb = excel.Workbooks.Open(ExcelFilepath);
			ws = excel.Worksheets[SheetNumber];
		}
		public string ReadFromExcel(int ExcelColumn, int ExcelRow)
		{
			if (ws.Cells[ExcelColumn, ExcelRow].Value2 != null)
			{
				return ws.Cells[ExcelColumn, ExcelRow].Value2;
			}
			else
			{
				return "Empty value";
			}
		}

		public void CloseExcel()
		{
			wb.Close();
			excel.Workbooks.Close();
			excel.Quit();
			Marshal.ReleaseComObject(ws);
			Marshal.ReleaseComObject(wb);
		}

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
			try
			{
				String sucessMsg = LaunchBrowser.driver.FindElement(By.TagName("h3")).Text;
				if (sucessMsg.Equals("Login Successfully"))
				{
					TC1_FlightReservation.test.Log(Status.Pass, "User Logged in successfully");
					//adding screenShot to the report
					ITakesScreenshot screenshotdriver = (ITakesScreenshot)LaunchBrowser.driver;
					Screenshot screnshot = screenshotdriver.GetScreenshot();
					screnshot.SaveAsFile(TC1_FlightReservation.ScreenShotsPath + "\\SucessLogin.bmp", ScreenshotImageFormat.Bmp);
					TC1_FlightReservation.test.AddScreenCaptureFromPath(TC1_FlightReservation.ScreenShotsPath + "\\SucessLogin.bmp");
				}
			}
			catch
			{
				TC1_FlightReservation.test.Log(Status.Fail, "User can't Logged in successfully");				
				//adding screenShot to the report
				ITakesScreenshot screenshotdriver = (ITakesScreenshot)LaunchBrowser.driver;
				Screenshot screnshot = screenshotdriver.GetScreenshot();
				screnshot.SaveAsFile(TC1_FlightReservation.ScreenShotsPath + "\\FailureLogin.bmp", ScreenshotImageFormat.Bmp);
				TC1_FlightReservation.test.AddScreenCaptureFromPath(TC1_FlightReservation.ScreenShotsPath + "\\FailureLogin.bmp");
				TC1_FlightReservation.extent.Flush();
				LaunchBrowser.driver.Close();
				LaunchBrowser.driver.Quit();
				Environment.Exit(1);

			}
		}
	}
}
