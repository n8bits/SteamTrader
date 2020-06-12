using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamTrader.Pages.Market
{
    public class MarketListingRow : PageComponent
    {
        private IWebElement nameLink => Context.FindElement(By.CssSelector("a.market_listing_item_name_link"));

        private IWebElement buyerPrice => Context.FindElement(By.CssSelector("span[title='This is the price the buyer pays.']"));

        private IWebElement sellerGets => Context.FindElement(By.CssSelector("span[title='This is how much you will receive.']"));

        private IWebElement removeButton => Context.FindElement(By.CssSelector("span.item_market_action_button_contents"));

        private IWebElement RemoveAcceptButton => Driver.FindElement(By.CssSelector("#market_removelisting_dialog_accept"));

        public MarketListingRow(IWebDriver driver, ISearchContext context) : base(driver, context)
        {
        }

        public string Name => this.nameLink.Text;

        public Decimal BuyerPrice
        {
            get
            {
                var raw = this.buyerPrice.GetAttribute("innerHTML");
                raw = raw.Trim();
                raw = raw.Replace("$", "");
                return Decimal.Parse(raw);
            }
        }

        public void RemoveListing()
        {
            this.removeButton.Click();
            this.RemoveAcceptButton.Click();
            Wait.Until(d => d.FindElements(By.CssSelector("#market_removelisting_dialog")).All(X => X.Displayed != true));

        }
    }
}
