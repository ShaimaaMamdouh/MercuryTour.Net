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
using System.Configuration;
using AventStack.ExtentReports.Configuration;
using NLog.Internal;
using System.Data.Common;
using FastExpressionCompiler.LightExpression;
using DryIoc;

namespace MercuryTour.Net
{
	public class Login
	{
		readonly _Application excel = new _Excel.Application();
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

		DbCommand command;
		//connection;
		DbProviderFactory factory;
		String connectionString;
		public void ConnectToDB()
		{
			String provider = System.Configuration.ConfigurationManager.AppSettings
		   ["provider"];
			connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
			factory = DbProviderFactories.GetFactory(provider);
		}
		String SelectedData;
		public String ReturnDataFromDB(int RecordID, string DBCoulmnName)
		{
			ConnectToDB();
			using (DbConnection connection = factory.CreateConnection())
			{
				try 
				{
					//Console.WriteLine("inside Connecting");
					//Console.ReadLine();
					connection.ConnectionString = connectionString;
					connection.Open();

					 command = factory.CreateCommand();
					command.Connection = connection;
					command.CommandText = "select * from Login where ID="+RecordID;
					using (DbDataReader datareader = command.ExecuteReader())
					{
						datareader.Read();
						SelectedData = datareader[DBCoulmnName].ToString();
						//Console.WriteLine(SelectedData);
						return SelectedData;

					}
				}
				catch(Exception e)
                {
					return SelectedData;
				}
			}
		}
		

		public void LoginCredentials(String userName, String Password, IWebDriver driver)
		{
				//Wait untill submit button is displayed
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
				IWebElement WaitForSubmitBtn = wait.Until((d) => d.FindElement(By.Name("submit")));

				driver.FindElement(By.Name("userName")).SendKeys(userName);
				driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]/table/tbody/tr[2]/td[3]/form/table/tbody/tr[4]/td/table/tbody/tr[3]/td[2]/input")).SendKeys(Password);
				driver.FindElement(By.Name("submit")).Click();
		}
		
		public void CheckSucessLogin(IWebDriver driver, ExtentTest test, String ScreenShotsPath, String TCNumber)
		{
			try
			{
				String sucessMsg = driver.FindElement(By.TagName("h3")).Text;
				if (sucessMsg.Equals("Login Successfully"))
				{
					test.Log(Status.Pass, "User Logged in successfully");
					//adding screenShot to the report
					ITakesScreenshot screenshotdriver = (ITakesScreenshot)driver;
					Screenshot screnshot = screenshotdriver.GetScreenshot();
					screnshot.SaveAsFile(ScreenShotsPath + "\\SucessLogin"+ TCNumber+".bmp", ScreenshotImageFormat.Bmp);
					test.AddScreenCaptureFromPath(ScreenShotsPath + "\\SucessLogin"+ TCNumber+".bmp");
				}
			}
			catch
			{
				test.Log(Status.Fail, "User can't Logged in successfully");				
				//adding screenShot to the report
				ITakesScreenshot screenshotdriver = (ITakesScreenshot)driver;
				Screenshot screnshot = screenshotdriver.GetScreenshot();
				screnshot.SaveAsFile(ScreenShotsPath + "\\FailureLogin"+ TCNumber+".bmp", ScreenshotImageFormat.Bmp);
				test.AddScreenCaptureFromPath(ScreenShotsPath + "\\FailureLogin"+ TCNumber+".bmp");
				driver.Close();
				driver.Quit();
				Environment.Exit(1);

			}
		}
	}
}
