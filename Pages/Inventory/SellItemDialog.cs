using OpenQA.Selenium;
using SteamTrader.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTrader.Pages.Inventory
{
    public class SellItemDialog : PageComponent
    {
        private IWebElement buyerPaysInput => Driver.FindElement(By.CssSelector("#market_sell_buyercurrency_input"));

        private IWebElement tosAccept => Driver.FindElement(By.CssSelector("#market_sell_dialog_accept_ssa"));

        private IWebElement acceptButton => Driver.FindElement(By.CssSelector("#market_sell_dialog_accept span"));

        private IWebElement sellConfirmOkButton => Driver.FindElement(By.CssSelector("#market_sell_dialog_ok"));

        private IWebElement additionalConfirmationNeededOKButton
            => Driver.FindElements(By.CssSelector(".newmodal .btn_grey_white_innerfade")).FirstOrDefault(x => x.Displayed);

        private IWebElement confirmationModal => Driver.FindElement(By.CssSelector(".newmodal[id~='market_sell_dialog']"));

        private IWebElement closeModalButton => Driver.FindElement(By.CssSelector(".newmodal_close"));

        public SellItemDialog(IWebDriver driver, ISearchContext context) : base(driver, context)
        {
        }

        public Decimal BuyerPays
        {
            get
            {
                return Decimal.Parse(this.buyerPaysInput.Text);
            }

            set
            {
                value = Decimal.Round(value, 2);
                this.buyerPaysInput.Clear();
                this.buyerPaysInput.SendKeys(value.ToString());
                Thread.Sleep(200);
            }
        }

        public void Sell(Decimal price)
        {
            this.BuyerPays = price;
            this.tosAccept.SetSelected(true);
            acceptButton.Click();
            Wait.Until(d => this.sellConfirmOkButton.Displayed);
            this.sellConfirmOkButton.Click();
            Thread.Sleep(1000);

            try
            {
                Wait.Until(d => additionalConfirmationNeededOKButton?.Displayed ?? false && (confirmationModal?.Displayed ?? false));
            }catch (Exception e)
            {
                CloseModalIfError();
                return;
            }
            
            //Driver.ExecuteJavascript("arguments[0].click()", acceptButton);

            additionalConfirmationNeededOKButton.Click();
            CloseModalIfError();
        }


        public void CloseModalIfError()
        {
            if (Error)
            {
                Close();
                Driver.Navigate().Refresh();
            }
        }

        public void Close()
        {
            this.closeModalButton.Click();

            Wait.Until(d => closeModalButton.Displayed != true);
        }

        public bool Error
        {
            get
            {
                return Driver.FindElements(By.CssSelector("#market_sell_dialog_error")).Any(x => x.Displayed);
            }
        }
    }
}
