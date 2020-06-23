using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercuryTour.Net
{
    class CloseBrowser
    {
        public void Close()
        {
            try
            {
                LaunchBrowser.driver.Close();
                TC1_FlightReservation.test.Log(Status.Pass, "Browser closed successfully");
            }
            catch (Exception)
            {
                TC1_FlightReservation.test.Log(Status.Fail, "Browser wasn't closed successfully :(");
            }
        }
    }
}
