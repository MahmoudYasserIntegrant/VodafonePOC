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
        By SearchBarLocator;
        IWebElement SearchBarElement;

        /********* Constructor ***********/
        public EShop(IWebDriver driver)
        {
            this.driver = driver;
        }

        /********* Page Actions ***********/
        [Obsolete]
        public void Search(String searchQuery)
        {
            SearchBarLocator = By.XPath(reader.GetSearchLocator());
            Thread.Sleep(TimeSpan.FromSeconds(4));
            SearchBarElement = driver.FindElement(SearchBarLocator);
            SearchBarElement.SendKeys(searchQuery);
            SearchBarElement.SendKeys(Keys.Enter);
        }

    }
}
