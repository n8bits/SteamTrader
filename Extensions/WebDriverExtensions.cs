using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SteamTrader.Pages;
using SteamTrader.Pages.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SteamTrader.Extensions
{
    public static class WebDriverExtensions
    {
        public static string ExecuteJavascript(this IWebDriver driver, string script)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(script)?.ToString();
        }

        public static string ExecuteJavascript(this IWebDriver driver, string script, object argument)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(script, argument)?.ToString();
        }

        public static string GetMarketUrl(this string itemName)
        {
            string url = "https://steamcommunity.com/market/listings/730/" + HttpUtility.UrlPathEncode(itemName);

            return url;
        }

        public static Decimal LookupBuyPrice(this IWebDriver driver, string url)
        {
            if (driver.WindowHandles.Count < 2)
            {
                driver.ExecuteJavascript("window.open()");
            }

            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Navigate().GoToUrl(url);
            url.Replace(SteamPage.BaseUrl, "");
            var pricingPage = new ItemPricingPage(driver, url);

            return pricingPage.HighestBidPrice;
        }

        public static Decimal LookupLowestSellPrice(this IWebDriver driver, string url)
        {
            if (driver.WindowHandles.Count < 2)
            {
                driver.ExecuteJavascript("window.open();");
            }
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Navigate().GoToUrl(url);
            url.Replace(SteamPage.BaseUrl, "");
            var pricingPage = new ItemPricingPage(driver, url);

            return pricingPage.LowestAskPrice;
        }
    }
}
