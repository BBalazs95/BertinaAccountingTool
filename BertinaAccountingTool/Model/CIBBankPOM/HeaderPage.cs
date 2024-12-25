using OpenQA.Selenium;
using System.Drawing;

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
            if (driver.FindElement(By.XPath(string.Format(XpathToFindCompanyFromTheList, name.Substring(0,name.IndexOf(' ')).ToUpper()))) is IWebElement nextCompany)
            {
                nextCompany.Click();
            }
            else
            {
                throw new Exception($"{name} company not found");
            }
        }

        internal void TryPressOK()
        {
            try
            {
                var prevPos = driver.FindElement(By.XPath("//button[text()='Rendben']")).Location;
                Thread.Sleep(100);
                while (driver.FindElement(By.XPath("//button[text()='Rendben']")).Location !=prevPos)
                {
                    prevPos = driver.FindElement(By.XPath("//button[text()='Rendben']")).Location;
                    Thread.Sleep(100);
                }
                driver.FindElement(By.XPath("//button[text()='Rendben']")).Click();
            }
            catch (Exception)
            {
            }
        }
    }
}
