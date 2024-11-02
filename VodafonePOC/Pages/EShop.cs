using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace VodafonePOC
{
    [TestClass, Order(1)]
    class EShop : SetupTest
    {
        /********* Variables ***********/
        By searchBarLocator;
        By rejectCookiesLocator;
        IWebElement searchBarElement;
        IWebElement rejectCookies;


        /********* Constructor ***********/
        public EShop(IWebDriver driver)
        {
            this.driver = driver;
        }

        /********* Page Actions ***********/
        [Obsolete]
        public void Search(String searchQuery)
        {
            searchBarLocator = By.XPath(reader.GetSearchLocator());
            //Thread.Sleep(TimeSpan.FromSeconds(4));
            searchBarElement = driver.FindElement(searchBarLocator);
            searchBarElement.SendKeys(searchQuery);
            searchBarElement.SendKeys(Keys.Enter);
        }

        public void RejectCookies()
        {
            rejectCookiesLocator = By.XPath(reader.GetRejectCookies());
            Thread.Sleep(TimeSpan.FromSeconds(4));
            rejectCookies.Click();
        }

    }
}
