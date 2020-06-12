using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTrader.Extensions
{
    public static class WebElementExtensions
    {
        public static void SetSelected(this IWebElement element, bool selected)
        {
            while (element.Selected != selected)
            {
                element.Click();
            }
        }

        public static bool Checked(this IWebElement element)
        {
            return element.GetAttribute("checked") == "true";
        }

        

        public static void TryClick(this IWebElement element, IWebDriver driver, int retries = 5)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));

            for(int i=0; i< retries; i++)
            {
                try
                {
                    element.Click();
                }catch (Exception e)
                {
                    Console.WriteLine("Could not click on attempt " + i);
                    Thread.Sleep(1000);
                }
            }
        }

        public static bool AttemptClick(this IWebElement element)
        {
            try
            {
                element.Click();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static IWebElement ToWebElement(this ISearchContext context)
        {
            return ((IWebElement)context);
        }
    }
}
