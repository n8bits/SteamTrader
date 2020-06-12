using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader.Pages
{
    public class ConvarsPage : WebPage
    {
        public static readonly string BaseUrl = @"https://convars.com/";

        public ConvarsPage(IWebDriver driver, string relativeUrl) : base(driver, BaseUrl + relativeUrl )
        {

        }
    }
}
