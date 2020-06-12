using OpenQA.Selenium;
using SteamTrader.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader.Pages.CaseSimulator
{
    public class CaseSelectButton : PageComponent
    {
        private IWebElement name => Context.FindElement(By.CssSelector(".case_selectname"));

        public CaseSelectButton(IWebDriver driver, ISearchContext context) : base(driver, context)
        {
        }

        public string Name => name.GetAttribute("innerHTML");


        public void Select()
        {
            Wait.Until(d => Context.ToWebElement().AttemptClick());
        }
    }
}
