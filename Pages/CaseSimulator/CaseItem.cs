using OpenQA.Selenium;
using SteamTrader.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader.Pages.CaseSimulator
{
    public class CaseItem : PageComponent
    {
        private  bool LookupPrices = true;

        private decimal quickSellPrice = -1;

        private IWebElement level => Context.FindElement(By.CssSelector(".case_item_descL"));

        private IWebElement price => Context.FindElement(By.CssSelector(".case_item_price1"));

        private IWebElement marketLink => Context.FindElement(By.CssSelector("a"));

        public CaseItem(IWebDriver driver, ISearchContext context) : base(driver, context)
        {
        }

        public string Level => this.level.GetAttribute("class").Last().ToString();

        public Decimal Price
        {
            get
            {
                return decimal.Parse(price.GetAttribute("innerHTML"), NumberStyles.Currency);

            }
        }

        public Decimal LookUpQuicksellPrice()
        {
            var url = this.marketLink.GetAttribute("href");
            if(Driver.WindowHandles.Count < 2)
            {
                this.Driver.ExecuteJavascript("window.open()");
            }
            this.Driver.SwitchTo().Window(Driver.WindowHandles[1]);
            
            this.Driver.Navigate().GoToUrl(url);
            var element = Wait.Until(d => d.FindElements(By.CssSelector(".market_listing_item_name")).FirstOrDefault());
            url = element.Text.GetMarketUrl();
            //Driver.ExecuteJavascript("window.close()");
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);

            this.quickSellPrice = Driver.LookupBuyPrice(url);

            return this.quickSellPrice;
        }
    }
}
