using OpenQA.Selenium;

namespace BertinaAccountingTool.Model.CIBBankPOM
{
    internal class HeaderPage : BasePage
    {
        private const string XpathToFindCompanyFromTheList = "/html/body/div[2]/div/div/div[4]/ul/li[1]/ul/li[.//*[contains(text(), '{0}')]]";

        private IWebElement CurrentCompany => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[4]/ul/li[1]"));

        public HeaderPage(IWebDriver driver) : base(driver)
        {
        }

        internal void OpenCompanyDropDown()
        {
            if (CurrentCompany.GetDomAttribute("data-is-click") == "true")
            {
                return;
            }

            CurrentCompany.Click();
        }

        internal void SelectCompany(string name)
        {
            if (driver.FindElement(By.XPath(string.Format(XpathToFindCompanyFromTheList, name))) is IWebElement nextCompany)
            {
                nextCompany.Click();
            }
            else
            {
                throw new Exception($"{name} company not found");
            }
        }
    }
}
