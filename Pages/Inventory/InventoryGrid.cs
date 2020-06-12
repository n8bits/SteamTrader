using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader.Pages.Inventory
{
    public class InventoryGrid : PageComponent
    {
        private IList<IWebElement> items => this.Context.FindElements(By.CssSelector("div.itemHolder")).Where(x => x.Displayed).ToList();

        public InventoryGrid(IWebDriver driver, ISearchContext context) : base(driver, context)
        {
        }

        public IList<InventoryItem> Items => this.items.Select(x => new InventoryItem(Driver, x)).ToList();
    }
}
