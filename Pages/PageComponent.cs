using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader.Pages
{
    /// <summary>
    /// Base class for all web page components and web pages. 
    /// </summary>
    public abstract class PageComponent
    {
        public PageComponent(IWebDriver driver, ISearchContext context)
        {
            this.Driver = driver;
            this.Context = context;
        }

        protected IWebDriver Driver { get; }

        public ISearchContext Context { get; }

        public WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(Driver, TimeSpan.FromSeconds(12));
            }
        }
    }
}
