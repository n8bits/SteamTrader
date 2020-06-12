using OpenQA.Selenium;
using SteamTrader.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SteamTrader.Pages.Inventory
{
    public class InventoryItem : PageComponent
    {
        private static string LastOpenedItem = null;

        private IWebElement sellDialog => Driver.FindElement(By.CssSelector("#market_sell_dialog_item_name"));

        private IWebElement identifier => Context.FindElement(By.CssSelector("div.item"));

        private IWebElement itemInfoPanel => Driver.FindElements(By.CssSelector("div.inventory_page_right .inventory_iteminfo"))
            .FirstOrDefault(x => x.Displayed);

        private IWebElement nextPage => Driver.FindElement(By.CssSelector("#pagebtn_next"));

        public InventoryItem(IWebDriver driver, ISearchContext context) : base(driver, context)
        {
        }

        public string Id => this.identifier.GetAttribute("id");

        public string Name
        {
            get
            {
                var name = "";

                Wait.Until(d =>
                {
                    try
                    {
                        name = Driver.ExecuteJavascript("return arguments[0].rgItem.description.market_name", (IWebElement)Context);
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                });


                return name;
            }
        }

        public string MarketUrl
        {
            get
            {
                string url = "https://steamcommunity.com/market/listings/730/" + HttpUtility.UrlPathEncode(this.Name);

                return url;
            }
        }

        public Decimal Price { get; private set; }

        public ItemInfoPanel ItemInfoPanel
        {
            get
            {
                Thread.Sleep(100);
                Select();
                var itemInfo = new ItemInfoPanel(Driver, this.itemInfoPanel);
                return itemInfo;
            }
        }

        public ItemInfoPanel Select()
        {
            CloseModals();
            while(((IWebElement)Context).Displayed!=true)
            {
                this.nextPage.Click();
                Thread.Sleep(700);
            }
            ItemInfoPanel itemInfo;

            if (InventoryItem.LastOpenedItem != this.Id)
            {
                ((IWebElement)Context).Click();
                itemInfo = new ItemInfoPanel(Driver, this.itemInfoPanel);//this.Price = itemInfo.StartingPrice;

                while (this.itemInfoPanel == null)
                {
                    Thread.Sleep(200);
                    ((IWebElement)Context).Click();
                }
                Thread.Sleep(100);
                InventoryItem.LastOpenedItem = this.Id;
            }
            else
            {
                itemInfo = new ItemInfoPanel(Driver, this.itemInfoPanel);
            }

            return itemInfo;
        }

        public void CloseModals()
        {
            var modalButtons = Driver.FindElements(By.CssSelector(".newmodal_close")).Where(x => x.Displayed).ToList();

            foreach(var button in modalButtons)
            {
                button.Click();
            }

            Wait.Until(d => d.FindElements(By.CssSelector("[class*='newmodal']")).All(x => x.Displayed != true));
        }

        public SellItemDialog SellItemDialog => new SellItemDialog(Driver, this.sellDialog);
    }
}
