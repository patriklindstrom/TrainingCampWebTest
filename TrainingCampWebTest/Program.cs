using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
            System.Threading.Thread.Sleep(GetNewRandomNumber(0, 1) * 1000);
            IWebDriver driver = new FirefoxDriver();
            //driver = new InternetExplorerDriver(IE_DRIVER_PATH);   
            int noOfTestRunTest = 30;
            for (int i = 0; i < noOfTestRunTest; i++)
            {         
            TestHomePage(driver, BASE_URL);
            //Change to englisht
            TestHomePageLanguage(driver: driver, baseUrl: BASE_URL, language: "en",
                firstProjectMeetingText: "FIRST PROJECT MEETING");
            System.Threading.Thread.Sleep(GetNewRandomNumber(0, 1) * 1000);
            //Change to japanese
            TestHomePageLanguage(driver: driver, baseUrl: BASE_URL, language: "ja", firstProjectMeetingText: "最初のプロジェクト会議");            
            System.Threading.Thread.Sleep(GetNewRandomNumber(1, 3) * 1000);
            }
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
        // Method from the linked answer, copy-pasted here for completeness
        // added the relevant fields because the original code is an override
        public static Int32 GetNewRandomNumber(Int32 minValue, Int32 maxValue)
        {
            RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();
            byte[] uInt32Buffer = new byte[4];

            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;
            Int64 diff = maxValue - minValue;
            while (true)
            {
                _rng.GetBytes(uInt32Buffer);
                UInt32 rand = BitConverter.ToUInt32(uInt32Buffer, 0);

                Int64 max = (1 + (Int64)UInt32.MaxValue);
                Int64 remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (Int32)(minValue + (rand % diff));
                }
            }
        }
    }
}
