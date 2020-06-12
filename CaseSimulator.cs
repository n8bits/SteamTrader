using OpenQA.Selenium.Chrome;
using SteamTrader.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader
{
    public class CaseSimulator
    {
        public const double HighGradeDropProbability = 0.75d;

        public const double RemarkableDropProbability = 0.20d;

        public const double ExoticDropProbability = 0.05d;

        public static async Task OpenCases(string caseName)
        {
            String userProfile = @"E:\Code\VisualStudio\SteamTrader\ChromeProfile\";
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=" + userProfile);
            options.AddArgument("--profile-directory=SteamTrader");
            options.AddArgument("--disable-gpu");
            using (ChromeDriver driver = new ChromeDriver(options))
            {
                var simPage = new CaseSimulatorPage(driver);
                simPage.GoToPage();

                //Open specific case
                //var capsule = "Perfect World";
                //var selector = simPage.OpenCaseSelectionMenu();
                //selector.SelectCapsuleByIndex(13);

                var capsules = simPage.OpenCaseSelectionMenu().Capsules.Select(x => x.Name)
                    //.Where(x => x.ToLower().Contains("sticker 2")).ToList();
                    .ToList();

                foreach (var capsule in capsules)
                {
                    driver.Navigate().Refresh();
                    var selector = simPage.OpenCaseSelectionMenu();
                    selector.SelectCapsuleByIndex(capsules.IndexOf(capsule));

                    var caseCost = (simPage.CasePrice * 100m);
                    var revenue = simPage.CaseContents.CalculateExpectedReturn(true);
                    var profit = revenue - caseCost;
                    var outputText = $"{capsule} - Spent: {caseCost.ToString("C")}, Earned: {revenue.ToString("C")}, Total Profit: {profit.ToString("C")}";
                    Console.WriteLine(outputText);
                }


                //simPage.CaseOptionsMenu.AutoOpen = true;
                //simPage.OpenCases();
            }
        }
    }
}
