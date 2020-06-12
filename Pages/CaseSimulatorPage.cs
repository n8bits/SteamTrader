using OpenQA.Selenium;
using SteamTrader.Extensions;
using SteamTrader.Pages.CaseSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamTrader.Pages
{
    public class CaseSimulatorPage : ConvarsPage
    {
        private IWebElement chooseContainerButton => Driver.FindElement(By.CssSelector("#item_selectortext"));

        private IWebElement caseSelectionMenu => Driver.FindElement(By.CssSelector("#case_selectform"));

        private IWebElement caseOptionsMenu => Driver.FindElement(By.CssSelector("#case_expand"));

        private IWebElement opencasesButton => Driver.FindElement(By.CssSelector("#case_open"));

        private IWebElement latestDropsPanel => Driver.FindElement(By.CssSelector("#dropBlock"));

        private IWebElement caseContents => Driver.FindElement(By.Id("allitems"));

        public CaseSimulatorPage(IWebDriver driver) : base(driver, "case/en")
        {
        }

        public CaseContents CaseContents => new CaseContents(Driver, this.caseContents);

        public CaseSelector OpenCaseSelectionMenu()
        {
            Driver.ExecuteJavascript("cv_caseselect()");

            return new CaseSelector(Driver, caseSelectionMenu);
        }

        public CaseOptionsMenu CaseOptionsMenu => new CaseOptionsMenu(Driver, caseOptionsMenu);

        public decimal CasePrice
        {
            get
            {
                var priceText = Regex.Split(this.chooseContainerButton.Text, ": ").Last().Replace(" (+", " ")
                    .Replace(")", "");
                var prices = Regex.Split(priceText, " ");

                var cost = Decimal.Parse(prices[0], System.Globalization.NumberStyles.Currency);

                if(prices.Length > 1)
                {
                    var additionalCost = Decimal.Parse(prices[1], System.Globalization.NumberStyles.Currency);
                    cost += additionalCost;
                }

                return cost;
            }
        }


        public void OpenCases()
        {
            Wait.Until(d => this.opencasesButton.AttemptClick());
        }

        public LatestDropsPanel LatestDropsPanel => new LatestDropsPanel(Driver, this.latestDropsPanel);
    }
}
