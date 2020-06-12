using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader.Pages
{
    public class SteamPage : WebPage
    {
        public static readonly string BaseUrl = @"https://steamcommunity.com/";

        private IWebElement modalOkButton => Driver.FindElements(By.CssSelector(".newmodal"))
            .FirstOrDefault(x => x.Displayed).FindElements(By.CssSelector("span"))
            .FirstOrDefault(x => x.Text == "OK");

        public SteamPage(IWebDriver driver, string relativeUrl) : base(driver, BaseUrl + relativeUrl)
        {

        }
    }
}
