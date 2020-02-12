using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace VodafonePOC
{
    /// <summary>
    ///This class drives the test scenario, it's about launching Vodafone website, validate its URL, search for a word,
    ///verify search result count and verify sorting functionality ascending and descending
    /// </summary>
    
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    class TestScenario : SetupTest
    {
        /********* Variables ***********/
        StartPage start;
        EShop shop;
        SearchResult result;
        By NoResultLocator;
        String ResultPageURL;
        String SearchQuery;
        String SortingOrder;
        private readonly string browser;

        /********* Constructor ***********/
        public TestScenario(string browser)
        {
            this.browser = browser;
        }

        /********* TestCases ***********/
        [Test, Order(1)]
        [Obsolete]
        public void VerifyGenericUrl()
        {
            ReportTest(browser);
            Setup(browser);
            test.Log(Status.Info, browser + " Browser launched successfully");
            start = new StartPage(driver);
            start.LaunchWebsite();
            test.Log(Status.Info, "Navigated to Vodafone Website");
            start.NavigateToEshop();
            test.Log(Status.Info, "Navigated to Eshop page");
            shop = new EShop(driver);
            SearchQuery = reader.GetSearchQuery();
            shop.Search(SearchQuery);
            test.Log(Status.Info, "Search query("+SearchQuery+") is sent to Search-bar input");
            result = new SearchResult(driver);
            ResultPageURL = reader.GetResultPageURL();
            Assert.IsTrue(result.GetCurrentUrl().Contains(ResultPageURL));
            test.Log(Status.Info, "Verify the generic URL for Search Result page");
        }
        
        [Test, Order(2)]
        public void VerifyResultCount()
        {
            Thread.Sleep(TimeSpan.FromSeconds(4));
            NoResultLocator = By.XPath(reader.GetNoResultLocator());
            if (result.NoResultExist(NoResultLocator))
            {
                Assert.Ignore(" No search result for your query");
            }
            else
            {
                string labelCount = result.GetCountLabelValue();
                string realCount = result.GetRealCountValue();
                Assert.AreEqual(labelCount, realCount);
                test.Log(Status.Info, "Verify the count displayed in label("+labelCount+") equals the real count("+realCount+") of search result items");

            }
        }
      
        [Test, Order(3)]
        public void VerifyPriceSorting()
        {
            SortingOrder = reader.GetSortingOrder();
            if (!(SortingOrder.Equals("Asc", StringComparison.InvariantCultureIgnoreCase)
                || SortingOrder.Equals("Des", StringComparison.InvariantCultureIgnoreCase)))
            {
                Assert.Ignore(" No Valid Sorting Order");
            }
            List<int> tempSortedList = new List<int>();
            tempSortedList = result.GetPrices();
            test.Log(Status.Info, "List of prices is fetched");
            
            if ((SortingOrder.Equals("Asc", StringComparison.InvariantCultureIgnoreCase)))
            {
                test.Log(Status.Info, "Sort Prices Ascending");
                tempSortedList.Sort((a, b) => a.CompareTo(b)); // Apply ascending sort to the list
            }
            else if ((SortingOrder.Equals("Des", StringComparison.InvariantCultureIgnoreCase)))
            {
                test.Log(Status.Info, "Sort Prices Descending");
                tempSortedList.Sort((a, b) => b.CompareTo(a)); // Apply Descending sort to the list
            }
           
            result.SortItems(SortingOrder);
            test.Log(Status.Info, "Sort items by Price in the web app");

            List<int> sortedFromTest = new List<int>();
            sortedFromTest = result.GetPrices();
            test.Log(Status.Info, "Sorted list of price is fetched from web app");
            Assert.True(sortedFromTest.SequenceEqual(tempSortedList));
            test.Log(Status.Info, "Verify that Items are sorted");

        }
    }
}
