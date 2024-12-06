using BertinaAccountingTool.Model.CIBBankPOM;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BertinaAccountingTool.BusinessLogic.Services
{
    internal class CIBBankDriver
    {
        private WebDriver driver;

        public CIBBankDriver()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Size = new System.Drawing.Size(1280, 720);
        }

        internal void Open()
        {
            driver.Navigate().GoToUrl("https://internetbankdemo.com/cibbusinessonline");

            var logingPage = new LoginPage(driver);

            logingPage.ClickLoginWithoutApp();

            logingPage.SetVicaId("vicademo");

            logingPage.ClickLogin();
        }

        internal void Close()
        {
            driver.Quit();
        }

        internal void SetCompany(string name)
        {
            var header = new HeaderPage(driver);

            header.OpenCompanyDropDown();

            header.SelectCompany(name);
        }

        internal void UploadFile(string path)
        {
            var packageImportPage = new PackageImportPage(driver);

            packageImportPage.MenuButtonClick();

            packageImportPage.SetFilePath(path);

            packageImportPage.SetGroupHUF();

            packageImportPage.SetFormatCSV();

            packageImportPage.SetEncodeUTF8();

            packageImportPage.ImportButtonClick();
        }
    }
}
