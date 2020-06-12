using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteamTrader;

namespace SteamTrader.Pages.CaseSimulator
{
    public class CaseContents : PageComponent
    {
        
        public IList<IWebElement> items => Context.FindElements(By.CssSelector(".case_item"));

        public CaseContents(IWebDriver driver, ISearchContext context) : base(driver, context)
        {
        }

        public IList<CaseItem> Items => this.items.Select(x => new CaseItem(Driver, x)).ToList();

        public IList<CaseItem> HighGradeItems => this.Items.Where(i => i.Level == "2").ToList();

        public IList<CaseItem> RemarkableItems => this.Items.Where(i => i.Level == "3").ToList();

        public IList<CaseItem> ExoticItems => this.Items.Where(i => i.Level == "4").ToList();

        public IList<CaseItem> ExtraordinaryItems => this.Items.Where(i => i.Level == "5").ToList();

        public Decimal CalculateExpectedReturn(bool lookup)
        {
            var hgAverage = HighGradeItems.Sum(x => (lookup ? x.LookUpQuicksellPrice() : x.Price)) / HighGradeItems.Count;
            var remarkableAverage = RemarkableItems.Any() ? 
                RemarkableItems.Sum(x => (lookup ? x.LookUpQuicksellPrice() : x.Price)) / RemarkableItems.Count : 0;
            var exoticAverage = ExoticItems.Any() ? ExoticItems.Sum(x => (lookup ? x.LookUpQuicksellPrice() : x.Price)) / ExoticItems.Count : 0;

            var highGradeReturn = remarkableAverage == 0 ? 100m * hgAverage : 75m *  hgAverage;
            var remarkableReturn = (exoticAverage == 0 ? 25m : 20m) * remarkableAverage;
            var exoticReturn = 5m * exoticAverage;

            Console.WriteLine($"\nHigh Grade Return: {highGradeReturn.ToString("C")}, Remarkable Return: {remarkableReturn.ToString("C")}, Exotic Return: {exoticReturn.ToString("C")}");
            return highGradeReturn + remarkableReturn + exoticReturn;
        }
    }
}
