using OpenQA.Selenium;
using SteamTrader.Extensions;
using SteamTrader.Pages.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTrader.Pages
{
    public class InventoryPage : SteamPage
    {
        private IWebElement inventory => Driver.FindElement(By.CssSelector("#inventories"));

        private IWebElement itemInfo => Driver.FindElements(By.CssSelector("div.inventory_page_right .inventory_iteminfo")).FirstOrDefault(x => x.Displayed);

        private IWebElement sellDialog => Driver.FindElement(By.CssSelector("#market_sell_dialog_item_name"));

        private IWebElement nextPage => Driver.FindElement(By.CssSelector("#pagebtn_next"));

        private IWebElement activeInventory => Driver.FindElement(By.CssSelector(".games_list_tab.active .games_list_tab_name"));

        

        public const int CSGO_ID = 730;

        public const int STEAM_INVENTORY_ID = 753;

        public InventoryPage(IWebDriver driver) : base(driver, "my/inventory/")
        {

        }

        public InventoryGrid Inventory => new InventoryGrid(Driver, inventory);

        public ItemInfoPanel ItemInfo => new ItemInfoPanel(Driver, this.itemInfo);

        public SellItemDialog SellItemDialog => new SellItemDialog(Driver, this.sellDialog);

        public string CurrentInventoryTab => activeInventory.GetAttribute("innerHTML");

        public bool CounterStrikeInventoryOpen => CurrentInventoryTab == "Counter-Strike: Global Offensive";

        public bool OpenCounterStrikeInventory()
        {
            try
            {
                Driver.ExecuteJavascript("ShowItemInventory(730)");
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public bool OpenInventory(int inventoryId)
        {
            var script = $"ShowItemInventory( {inventoryId} ); return true;";

            try
            {
                Driver.ExecuteJavascript(script);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void GoToPage()
        {
            base.GoToPage();
            Wait.Until(Driver => OpenCounterStrikeInventory());
            Wait.Until(Driver => this.Inventory.Items.Count > 1);
            Thread.Sleep(1000);
        }

        public void SkipPages(int pages)
        {
            for (int i = 0; i < pages; i++)
            {
                this.nextPage.Click();
                Thread.Sleep(799);
            }
        }
    }
}
