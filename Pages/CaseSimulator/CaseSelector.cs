using OpenQA.Selenium;
using SteamTrader.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader.Pages.CaseSimulator
{
    public class CaseSelector : PageComponent
    {
        private IList<IWebElement> caseSelectionButtons => Context.FindElements(By.CssSelector("#case_select_dialog_switch div.case_selectbutton")).ToList();

        private IList<IWebElement> capsuleButtons => Context.FindElements(By.CssSelector("#case_select_caps div.case_selectbutton")).ToList();

        public CaseSelector(IWebDriver driver, ISearchContext context) : base(driver, context)
        {
        }

        public List<CaseSelectButton> CaseSelectionButtons => this.caseSelectionButtons.Select(x => new CaseSelectButton(Driver, x)).ToList();

        public List<CaseSelectButton> Capsules => this.capsuleButtons.Select(x => new CaseSelectButton(Driver, x)).ToList();

        public void SelectCapsuleByName(string name)
        {
            var index = this.Capsules.FindLastIndex(x => x.Name == name);
            Driver.ExecuteJavascript($"cv_caseselected({index},\"cap\")");
        }

        public void SelectCapsuleByIndex(int index)
        {
            Driver.ExecuteJavascript($"cv_caseselected({index},\"cap\")");
        }
    }
}
