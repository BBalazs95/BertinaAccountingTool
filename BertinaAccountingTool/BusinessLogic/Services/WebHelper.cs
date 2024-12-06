using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace BertinaAccountingTool.BusinessLogic.Services
{
    internal static class WebHelper
    {
        private static IWebDriver? driver;

        public static IWebDriver? Driver
        {
            get => driver;
            set
            {
                driver = value;
                Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            }
        }

        public static WebDriverWait? Wait { get; set; }

        public static void WaitForAjaxCall()
        {
            try
            {
                Wait?.Until((Driver) => { return Driver.ExecuteJavaScript<bool>("return jQuery.active == 0"); });
            }
            catch (Exception e)
            {
                throw new Exception("Ajax Call didn't finished in time.", e);
            }
        }

        public static void ScrollTo(IWebElement element)
        {
            var scrollElementIntoMiddle = "var viewPortHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);"
                                        + "var elementTop = arguments[0].getBoundingClientRect().top;"
                                        + "window.scrollBy(0, elementTop-(viewPortHeight/2));";

            Driver?.ExecuteJavaScript(scrollElementIntoMiddle, element);
        }
    }
}
