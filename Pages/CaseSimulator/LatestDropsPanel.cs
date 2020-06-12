using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader.Pages.CaseSimulator
{
    public class LatestDropsPanel : PageComponent
    {
        public IList<IWebElement> historyEntries => Context.FindElements(By.CssSelector(".drophistory"));

        public LatestDropsPanel(IWebDriver driver, ISearchContext context) : base(driver, context)
        {
        }
    }
}
