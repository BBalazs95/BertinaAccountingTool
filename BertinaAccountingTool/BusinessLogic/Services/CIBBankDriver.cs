using BertinaAccountingTool.Model.CIBBankPOM;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Windows.Input;
using System.Windows;
using System.Runtime.InteropServices;

namespace BertinaAccountingTool.BusinessLogic.Services
{
    internal class CIBBankDriver
    {
        private WebDriver driver;

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const byte VK_RETURN = 0x0D; // Enter key
        private const byte VK_CONTROL = 0x11; // Ctrl key
        private const byte VK_V = 0x56;      // V key
        private const uint KEYEVENTF_KEYDOWN = 0x0000;
        private const uint KEYEVENTF_KEYUP = 0x0002;


        public CIBBankDriver()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Size = new System.Drawing.Size(1280, 720);
        }

        internal void Open()
        {
            //driver.Navigate().GoToUrl("https://internetbankdemo.com/cibbusinessonline");
            driver.Navigate().GoToUrl("https://businessonline.cib.hu/");

            var logingPage = new LoginPage(driver);

            logingPage.ClickLoginWithoutApp();

            //logingPage.SetVicaId("vicademo");
            logingPage.SetVicaId("MOVAIR:EW1WD6");

            logingPage.ClickLogin();

            logingPage.TryPressOK();
        }

        internal void Close()
        {
            driver.Quit();
        }

        internal void SetCompany(string name)
        {
            var header = new HeaderPage(driver);

            try
            {
                header.OpenCompanyDropDown();

                header.SelectCompany(name);

                WebHelper.WaitForFullPageLoad();

                header.TryPressOK();
            }
            catch (Exception)
            {
                MessageBox.Show($"{name} cég kiválasztása nem sikerült, kérlek válaszd ki a weboldalon utánna nyomj egy okét.");
            }
        }

        internal void UploadFile(string path)
        {
            var packageImportPage = new PackageImportPage(driver);
            try
            {
                packageImportPage.MenuButtonClick();
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba lépett fel a 'Csomag importálása' menüpontra lépéskor, kérlek lépj a 'Csomag importálása' menübe majd nyomj egy okét");
            }

            try
            {
                packageImportPage.PressBrowse();
                Thread.Sleep(1000);

                Clipboard.SetText(path);
                Thread.Sleep(100);

                SimulateCtrlV();
                Thread.Sleep(100);

                SimulateKeyPress(VK_RETURN);
                Thread.Sleep(100);
            }
            catch (Exception)
            {
                MessageBox.Show($"Hiba lépett fel a {path} fájl feltöltése kapcsán, kérlek tallózd be a fájlt utánna nyomj egy okét");
            }

            try
            {
                packageImportPage.SetGroupHUF();
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba lépet fel a forinutalás kiválasztásakor, kérlek válaszd ki a forintátutalást utánna nyomj egy okét");
            }

            try
            {
                packageImportPage.SetFormatCSV();
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba lépett fel a CSV formátum kiválasztásakor, kérlek válaszd ki a CSV formátumot utánna nyomj egy okét");
            }

            try
            {
                packageImportPage.SetEncodeUTF8();
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba lépett fel az UTF8 kódolás kiválasztásakor, kérlek válaszd ki az UTF8 kódolást utánna nyomj egy okét");
            }

            try
            {
                packageImportPage.ImportButtonClick();
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba lépett fel az Import gomb megnyomásakor, kérlek nyomj rá az import gombra utánna nyomj egy okét");
            }
        }
        private void SimulateCtrlV()
        {
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            keybd_event(VK_V, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            keybd_event(VK_V, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }

        private void SimulateKeyPress(byte virtualKeyCode)
        {
            keybd_event(virtualKeyCode, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            keybd_event(virtualKeyCode, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }
    }
}
