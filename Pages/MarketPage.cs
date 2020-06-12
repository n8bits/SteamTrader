using OpenQA.Selenium;
using SteamTrader.Pages.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTrader.Pages
{
    public class MarketPage : SteamPage
    {
        private IWebElement listinPanel => Driver.FindElement(By.CssSelector("#myListings"));

        public MarketPage(IWebDriver driver) : base(driver, "market")
        {

        }

        public MarketListingTable MarketTable
        {
            get
            {
                return new MarketListingTable(Driver, this.listinPanel);
            }
        }

        public override void GoToPage()
        {
            base.GoToPage();
            Thread.Sleep(5000);
        }
    }
}
