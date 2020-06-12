using OpenQA.Selenium;
using SteamTrader.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader.Pages.CaseSimulator
{
    public class CaseOptionsMenu : PageComponent
    {
        private IWebElement caseAutoOpenButton => Driver.FindElement(By.CssSelector("#case_auto input"));

        private IWebElement autoOpenClickableArea => Driver.FindElement(By.CssSelector("#case_auto"));

        public CaseOptionsMenu(IWebDriver driver, ISearchContext context) : base(driver, context)
        {
        }

        public bool AutoOpen
        {
            get
            {
                return this.caseAutoOpenButton.GetAttribute("checked") == "true";
            }

            set
            {
                if(this.caseAutoOpenButton.Checked() != value)
                {
                    Wait.Until(d => this.autoOpenClickableArea.AttemptClick());
                }
            }
        }
    }
}
