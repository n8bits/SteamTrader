using OpenQA.Selenium;
using SteamTrader.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTrader.Pages.Inventory
{
    public class ItemInfoPanel : PageComponent
    {
        private IWebElement marketInfo => Driver.FindElements(By.CssSelector(".item_market_actions div div:nth-of-type(2)")).FirstOrDefault(x => x.Displayed);

        private IWebElement sellButton => Driver.FindElements(By.CssSelector(".item_market_action_button_contents")).FirstOrDefault(x => x.Displayed);

        private IWebElement sellDialog => Driver.FindElement(By.CssSelector("#market_sell_dialog_item_name"));

        private IWebElement turnToGems => Driver.FindElements(By.CssSelector("#iteminfo0_item_scrap_link")).FirstOrDefault();

        private IWebElement modalOkButton => Driver.FindElements(By.CssSelector(".newmodal"))
            .FirstOrDefault(x => x.Displayed).FindElements(By.CssSelector("span"))
            .FirstOrDefault(x => x.Text == "OK");

        public ItemInfoPanel(IWebDriver driver, ISearchContext context) : base(driver, context)
        {
        }

        public bool Sellable => this.sellButton != null;

        public Decimal StartingPrice
        {
            get
            {
                if (this.marketInfo == null)
                    return -1;
               
                Decimal price = -1;

                try
                {
                    Wait.Until(d =>
                            {
                                if (!Sellable)
                                {
                                    price = 1000000;
                                    return true;
                                }


                                var startPrice = this.marketInfo?.GetAttribute("innerHTML").Replace("Starting at: $", "").Split("<br>".ToCharArray())[0];
                                return Decimal.TryParse(startPrice, out price);
                            });
                }
                catch (Exception)
                {
                    return -1;
                }
                
                return Decimal.Round(price, 2); ;
            }
        }

        public SellItemDialog Sell()
        {
            if (!Sellable)
                return null;
            Wait.Until(d => this.sellButton.AttemptClick());
            var dialog = new SellItemDialog(Driver, sellDialog);
            Wait.Until(d => sellDialog.Displayed);
            return new SellItemDialog(Driver, sellDialog);
        }

        public void TurnIntoGems()
        {
            try
            {
                this.turnToGems.Click();
                ConfirmAction();
            }
            catch
            {
                Console.WriteLine("Could not turn item into gems, skipping...");
            }

        }

        public void ConfirmAction()
        {
            Thread.Sleep(1000);
            this.modalOkButton.Click();
            Thread.Sleep(1000);
            this.modalOkButton.Click();
        }
    }
}
