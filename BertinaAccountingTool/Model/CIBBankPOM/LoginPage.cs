using BertinaAccountingTool.BusinessLogic.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace BertinaAccountingTool.Model.CIBBankPOM
{
    internal class LoginPage : HeaderPage
    {
        #region IWebElements

        private IWebElement LoginWithAppButton => driver.FindElement(By.XPath("//*[@id='modechooser']/div[2]/button[2]"));

        private IWebElement VicaIdTextBox => driver.FindElement(By.Id("vicauid"));

        private IWebElement LoginButton => driver.FindElement(By.Id("signinbutton"));

        #endregion

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void ClickLoginWithoutApp()
        {
            LoginWithAppButton.Click();
        }

        public void SetVicaId(string vicaId)
        {
            VicaIdTextBox.SendKeys(vicaId);
        }

        public void ClickLogin()
        {
            LoginButton.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/div[4]/ul/li[9]/a")));
        }
    }
}
