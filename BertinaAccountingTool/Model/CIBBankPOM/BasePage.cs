using OpenQA.Selenium;

namespace BertinaAccountingTool.Model.CIBBankPOM
{
    internal abstract class BasePage
    {
        protected IWebDriver driver;

        protected BasePage(IWebDriver driver) {
            this.driver = driver;
        }
    }
}