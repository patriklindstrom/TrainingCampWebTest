using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
            System.Console.WriteLine("MultithreadTest press Enter to Start");
            System.Console.ReadLine();
            
            Thread thread1 = new Thread(new ThreadStart(SeleniumTestChore));
            Thread thread2 = new Thread(new ThreadStart(SeleniumTestChore));
            Thread thread3 = new Thread(new ThreadStart(SeleniumTestChore));
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread1.Join();
            thread2.Join();
            thread3.Join();

            System.Console.WriteLine("Done press Enter to exit");
            System.Console.ReadLine();
        }

        public static void SeleniumTestChore()
        {
            IWebDriver driver;
            StringBuilder verificationErrors;
            string baseURL;

            driver = new FirefoxDriver();
            //driver = new InternetExplorerDriver(IE_DRIVER_PATH);          
            baseURL = "http://trainingcampsk.apphb.com/";
            verificationErrors = new StringBuilder();
            TestHomePage(driver, baseURL);
            //Change to englisht
            TestHomePageLanguage(driver: driver, baseUrl: baseURL, language: "en",
                firstProjectMeetingText: "FIRST PROJECT MEETING");
            //Change to japanese
            TestHomePageLanguage(driver: driver, baseUrl: baseURL, language: "ja", firstProjectMeetingText: "最初のプロジェクト会議");
        }

        private static void TestHomePageLanguage(IWebDriver driver, string baseUrl,string language,string firstProjectMeetingText)
        {
            driver.Navigate().GoToUrl(baseUrl + "/Home/index/" + language);
            var firstProjectMeetingValueOnPage = driver.FindElement(By.XPath("//div/header/h3")).Text;
            Debug.Assert(firstProjectMeetingValueOnPage == firstProjectMeetingText);
            System.Console.WriteLine("Language Test for " + language + " : " + firstProjectMeetingValueOnPage);
        }


        private static void TestHomePage(IWebDriver driver, string baseURL)
        {
            driver.Navigate().GoToUrl(baseURL + "/Home/index/en");
            driver.FindElement(By.Id("name")).Click();
            driver.FindElement(By.Id("name")).Clear();
            driver.FindElement(By.Id("name")).SendKeys("Patrik");
            driver.FindElement(By.Id("email")).Clear();
            driver.FindElement(By.Id("email")).SendKeys("patrik.lindstrom@home.com");
            driver.FindElement(By.Id("phone")).Clear();
            driver.FindElement(By.Id("phone")).SendKeys("+46070666666");
            driver.FindElement(By.Id("comments")).Clear();
            driver.FindElement(By.Id("comments")).SendKeys("I want to join this great camp");
            driver.FindElement(By.Id("submit")).Click();
        }
    }
}
