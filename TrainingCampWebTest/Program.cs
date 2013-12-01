using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
namespace TrainingCampWebTest
{
    class Program
    {
        private const string IE_DRIVER_PATH = @"D:\Users\Patrik\Documents\GitHub\TrainingCampWebTest\";
        static void Main(string[] args)
        {        
         IWebDriver driver;
         StringBuilder verificationErrors;
         string baseURL;
        
           // driver = new FirefoxDriver();
            
             driver = new InternetExplorerDriver(IE_DRIVER_PATH);
            
            baseURL = "http://trainingcampsk.apphb.com/";
            verificationErrors = new StringBuilder();
            driver.Navigate().GoToUrl(baseURL + "/Home/index/en");           
            driver.FindElement(By.Id("name")).Click();
            
        }
    }
}
