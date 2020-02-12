using OpenQA.Selenium;

using System;


namespace VodafonePOC
{
    class StartPage : SetupTest
    {
        /********* Variables ***********/
        String StartUrl;
        IWebElement eShopButton;

        /********* Constructor ***********/
        public StartPage (IWebDriver driver)
        {
            this.driver = driver;
        }

        /********* Page Actions ***********/
        public void LaunchWebsite()
        {
            reader.ReadData();
            StartUrl = reader.GetStartUrl();
            driver.Url = StartUrl;

        }

        [Obsolete]
        public void NavigateToEshop()
        {
            eShopButton = driver.FindElement(By.XPath(reader.GeteShopButtonLocator()));
            WaitForElementToClick(eShopButton);
            eShopButton.Click();

        }
    }
}
