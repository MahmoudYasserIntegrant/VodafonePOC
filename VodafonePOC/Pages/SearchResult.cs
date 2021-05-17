using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace VodafonePOC
{
    class SearchResult : SetupTest
    {
        /********* Variables ***********/
        String LabelCount;
        String RealCount;
        String PageURL;
        IList<IWebElement> items;
        IList<IWebElement> prices;
        List<int> pricesValues;
        IWebElement SortingSelector;
        SelectElement selector;

        /********* Constructor ***********/
        public SearchResult(IWebDriver driver)
        {
            this.driver = driver;
        }


        /********* Page Getters ***********/
        public String GetCountLabelValue()
        {
            // Thread.Sleep(3000);
            WaitForElementToBeVisible(By.CssSelector(reader.GetCountLabelLocator()));
            LabelCount = driver.FindElement(By.CssSelector(reader.GetCountLabelLocator())).Text;

         return LabelCount;
        }

        public String GetRealCountValue()
        {
            
           items = driver.FindElements(By.XPath(reader.GetRealCountLocator()));
            RealCount = items.Count.ToString();
            return RealCount;
        }

        [Obsolete]
        public String GetCurrentUrl()
        {
            PageURL = reader.GetResultPageURL();
            WaitForUrlToConatin(PageURL);
            return driver.Url;
        }
        public List<int> GetPrices()
        {
            prices = driver.FindElements(By.XPath(reader.GetPricesLocator()));
            pricesValues = new List<int>();

            foreach (IWebElement e in prices)
            {
                pricesValues.Add(Int32.Parse(Regex.Match(e.Text, @"\d+").Value));

            }
            return pricesValues;
       
        }
        public bool NoResultExist(By by)
        {

            if (driver.FindElements(by).Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /********* Page Actions ***********/
        public void SortItems(String order)
        {
            // select the drop down list
            SortingSelector = driver.FindElement(By.XPath(reader.GetSortLocator()));
            //create select element object 
            selector = new SelectElement(SortingSelector);
            if (order.Equals("Asc", StringComparison.InvariantCultureIgnoreCase))
            selector.SelectByValue("1");
            else if (order.Equals("Des", StringComparison.InvariantCultureIgnoreCase))
           selector.SelectByValue("2");

        }


    }
}
