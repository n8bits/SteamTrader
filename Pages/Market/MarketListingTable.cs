using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader.Pages.Market
{
    public class MarketListingTable : PageComponent
    {
        private IList<IWebElement> marketListingRows => Context.FindElements(By.CssSelector("div.market_listing_row"));

        private IWebElement currentPage => Context.FindElement(By.CssSelector(".market_paging_pagelink.active"));

        private IList<IWebElement> pageLinks => Context.FindElements(By.CssSelector(".market_paging_pagelink")).ToList();

        private IWebElement nextButton => Context.FindElement(By.CssSelector("#tabContentsMyActiveMarketListings_btn_next"));


        public MarketListingTable(IWebDriver driver, ISearchContext context) : base(driver, context)
        {
        }

        public IList<MarketListingRow> Listings => this.marketListingRows.Select(x => new MarketListingRow(Driver, x)).ToList();

        public int CurrentPage
        {
            get
            {
                return Int32.Parse(this.currentPage.GetAttribute("innerHTML"));
            }
        }

        public int TotalPages => this.pageLinks.Count();

        public void NextPage()
        {
            while(true)
            {
                try
                {
                    this.nextButton.Click();
                    break;
                }catch ( Exception e)
                {

                }
                
            }
            
        }

        public bool OnLastPage
        {
            get
            {
                return this.CurrentPage == this.TotalPages;
            }
        }
    }
}
