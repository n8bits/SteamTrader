using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SteamTrader.Extensions;
using SteamTrader.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader
{
    public class ItemPricingPage : SteamPage
    {
        
        private IList<IWebElement> highestBidPrice => Driver.FindElements(By.CssSelector("div#market_commodity_buyrequests .market_commodity_orders_header_promote:nth-of-type(2)")).ToList();

        private IList<IWebElement> lowestAskPrice => Driver.FindElements(By.CssSelector("div#market_commodity_forsale .market_commodity_orders_header_promote:nth-of-type(2)")).ToList();

        public ItemPricingPage(IWebDriver driver, string relativeUrl) : base(driver, relativeUrl)
        {
        }

        public Decimal HighestBidPrice
        {
            get
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

                try
                {
                    var priceElement = wait.Until(d => highestBidPrice.FirstOrDefault()?.GetAttribute("innerHTML"));
                    var price = Decimal.Parse(priceElement.Replace("$", ""));
                    //Driver.ExecuteJavascript(@"window.close();");
                    Driver.SwitchTo().Window(Driver.WindowHandles.First());
                    return price;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Driver.ExecuteJavascript(@"window.close();");
                    Driver.SwitchTo().Window(Driver.WindowHandles.First());
                    return -1;
                }
            }
        }

        public Decimal LowestAskPrice
        {
            get
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(4));

                try
                {
                    var priceElement = wait.Until(d => lowestAskPrice.FirstOrDefault()?.GetAttribute("innerHTML"));
                    var price = Decimal.Parse(priceElement.Replace("$", ""));
                    //Driver.ExecuteJavascript(@"window.close();");
                    Driver.SwitchTo().Window(Driver.WindowHandles.First());
                    return price;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Driver.ExecuteJavascript(@"window.close();");
                    Driver.SwitchTo().Window(Driver.WindowHandles.First());
                    return -1;
                }
            }
        }
    }
}
