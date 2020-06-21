using AventStack.ExtentReports;
using DryIoc;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace MercuryTour.Net
{
    public class LaunchBrowser
    {
        public static IWebDriver driver = new ChromeDriver(); 

        public void Launch()
        { 
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Url= "http://demo.guru99.com/test/newtours/";
        }

    
        public void TitleCheck()
        {
            String expectedTitle = "Welcome: Mercury Tours";
            String actualTitle;
            actualTitle = driver.Title;
            if (actualTitle.Equals(expectedTitle))
            {
                TC1_FlightReservation.test.Log(Status.Pass,"expected Title was found successfully!");

            }
            else
            {
                TC1_FlightReservation.test.Log(Status.Fail, "expected Title wasn't found successfully :(");
            }
        }
    }
}
