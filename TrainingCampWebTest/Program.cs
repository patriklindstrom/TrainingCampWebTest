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
        public const string BASE_URL = "http://trainingcampsk.apphb.com/";
        static void Main(string[] args)
        {
            System.Console.WriteLine("MultithreadTest press Enter to Start");
            System.Console.ReadLine();
            
            int noOfConcurrentTest = 3;
            var arrThreads = new List<Thread>();
            for (int j = 0; j < noOfConcurrentTest; j++)
            {
                arrThreads.Add(new Thread(new ThreadStart(SeleniumTestChore)));
            }
            foreach (var arrThread in arrThreads)
            {
                arrThread.Start();
            }
            foreach (var arrThread in arrThreads)
            {
                arrThread.Join();
            }
            System.Console.WriteLine("Done press Enter to exit");
            System.Console.ReadLine();
        }

        public static void SeleniumTestChore()
        {
            IWebDriver driver = new FirefoxDriver();
            //driver = new InternetExplorerDriver(IE_DRIVER_PATH);          
            TestHomePage(driver, BASE_URL);
            //Change to englisht
            TestHomePageLanguage(driver: driver, baseUrl: BASE_URL, language: "en",
                firstProjectMeetingText: "FIRST PROJECT MEETING");
            //Change to japanese
            TestHomePageLanguage(driver: driver, baseUrl: BASE_URL, language: "ja", firstProjectMeetingText: "最初のプロジェクト会議");
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
