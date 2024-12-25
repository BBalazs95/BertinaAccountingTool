using BertinaAccountingTool.BusinessLogic.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BertinaAccountingTool.Model.CIBBankPOM
{
    internal class PackageImportPage : BasePage
    {
        private IWebElement SearchButton => driver.FindElement(By.Id("funfind_view"));
        private IWebElement CurrentPageButton => driver.FindElement(By.XPath("//*[@id='funfind_select']/div[2]/ul/li[.//*[text()='Csomag importálása']]"));
        private IWebElement GroupButton => driver.FindElement(By.XPath("//*[@id=\"group_view\"]/.."));
        private IWebElement HUFSendingButton => driver.FindElement(By.XPath("//*[@id=\"group_select\"]/div/ul/li[2]"));
        private IWebElement BrowseButton => driver.FindElement(By.XPath("//*[@id=\"file_view_group\"]/label/div"));
        private IWebElement FormatButton => driver.FindElement(By.XPath("//*[@id=\"format_view\"]/.."));
        private IWebElement CSVFromatButton => driver.FindElement(By.XPath("//*[@id=\"format_select\"]/div/ul/li[3]"));
        private IWebElement EncodeButton => driver.FindElement(By.XPath("//*[@id=\"impcharcode_view\"]/.."));
        private IWebElement UTF8Button => driver.FindElement(By.XPath("//*[@id=\"impcharcode_select\"]/div[2]/ul/li[3]"));
        private IWebElement ImportButton => driver.FindElement(By.XPath("//button[contains(text(),'Import')]"));

        public PackageImportPage(IWebDriver driver) : base(driver)
        {
        }

        public void MenuButtonClick()
        {
            WebHelper.WaitForFullPageLoad();

            SearchButton.Click();

            CurrentPageButton.Click();

            WebHelper.WaitForAjaxCall();

            Thread.Sleep(3000);
        }

        internal void PressBrowse()
        {
            BrowseButton.Click();
        }

        internal void SetGroupHUF()
        {
            GroupButton.Click();
            Thread.Sleep(1000);
            HUFSendingButton.Click();
            Thread.Sleep(1000);
        }

        internal void SetFormatCSV()
        {
            FormatButton.Click();
            Thread.Sleep(1000);
            CSVFromatButton.Click();
        }

        internal void SetEncodeUTF8()
        {
            EncodeButton.Click();
            Thread.Sleep(100);
            UTF8Button.Click();
            Thread.Sleep(1000);
        }

        internal void ImportButtonClick()
        {
            ImportButton.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/div[4]/ul/li[9]/a")));
        }
    }
}
