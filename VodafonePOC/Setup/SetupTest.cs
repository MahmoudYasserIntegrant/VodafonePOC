using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;


namespace VodafonePOC
{/// <summary>
/// This class to initiate the test base, initialize the driver to launch the test, and tear down
/// </summary>
    public class SetupTest
    {
       
        /********* Variables ***********/
        protected IWebDriver driver;
        protected static ExtentReports extent;
        protected static ExtentHtmlReporter htmlReporter;
        protected static ExtentTest test;
        protected ReadExcel reader = new ReadExcel();



        /********* Reporting ***********/
        public void ReportTest(String browserName)
        {       
          string reportPath = ReadExcel.GetCurrentPath() + "Reports\\TestRunReport.html";
            htmlReporter = new ExtentHtmlReporter(reportPath);
            GetInstance();
            extent.AttachReporter(htmlReporter);

            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.DocumentTitle = "Vodafone POC - Test Report";
            htmlReporter.Config.ReportName = "Vodafone POC - Test Report";
            extent.AddSystemInfo("System Under Test", "Vodafone Website");
            extent.AddSystemInfo("Environment", "Windows 10 - " + browserName);
            extent.AddSystemInfo("Owner", "Integrant inc - Mahmoud Yasser");
            test = extent.CreateTest("Test searching and sorting", "This test contains 3 test cases 1)Launching Vodafone website and assert its URL " +
                " 2)Navigate to Eshop page and search for a query(Data Driven) then assert that items count" +
                " 3)Sort items by price and assert the sorting");
          }
  
        public static ExtentReports GetInstance()
        {
            if (extent == null)
            {
                extent = new ExtentReports();
            }
            return extent;
        }

        /********* Initialize ***********/
        public void Setup(String browserInstance)
        {
            if (browserInstance.Equals("Chrome", StringComparison.InvariantCultureIgnoreCase))
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
            }
            else if (browserInstance.Equals("Firefox", StringComparison.InvariantCultureIgnoreCase))
            {
                driver = new FirefoxDriver();
                driver.Manage().Window.Maximize();
            }
            
        }

        /********* Wait Methods ***********/
        [Obsolete]
        public void WaitForUrlToConatin(String DesiredString)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.Until(ExpectedConditions.UrlContains(DesiredString));
        }
           
        [Obsolete]
        public void WaitForElementToClick(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }

        public void WaitForElementToBeVisible(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }

        [Obsolete]
        public void WaitForElementToClick(IWebElement e)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.Until(ExpectedConditions.ElementToBeClickable(e));
        }

        /********* TearDown ***********/

        [TearDown]
        public void End()
        {
            NUnit.Framework.Interfaces.TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;
            String message = TestContext.CurrentContext.Result.Message;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Log(Status.Fail, status + message);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                test.Log(Status.Pass, status + message);
            }
            else
            {
                test.Log(Status.Skip, status + message);

            }
        }
                
        [OneTimeTearDown]
        public void EndTest()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            extent.Flush();
           driver.Quit();

        }

    }
}
