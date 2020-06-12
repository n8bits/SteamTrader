using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader.Pages
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WebPage : PageComponent
    {
        public WebPage(IWebDriver driver, string url) :
            base(driver, driver)
        {
            this.Url = url;
        }

        internal string Url { get; }

        // Allow for overriding in case a page needs additional navigation or wait logic
        public virtual void GoToPage()
        {
            Driver.Navigate().GoToUrl(this.Url);
        }
    }

}
