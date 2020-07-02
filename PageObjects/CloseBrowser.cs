using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercuryTour.Net
{
    class CloseBrowser
    {
        public void Close(IWebDriver driver,ExtentTest test)
        {
            try
            {
                driver.Close();
                test.Log(Status.Pass, "Browser closed successfully");
            }
            catch (Exception)
            {
                test.Log(Status.Fail, "Browser wasn't closed successfully :(");
            }
        }
    }
}
