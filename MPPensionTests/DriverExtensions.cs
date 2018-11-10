using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MPPensionTests
{
    internal static class DriverExtensions
    {
        private const int defaultTimeout = 10;

        public static void ClickElement(this IWebDriver driver, By selector)
        {
            var element = driver.WaitForElement(selector, true);
            try
            {
                element.Click();
            }
            catch (Exception)
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].click();", element);
            }
        }

        public static void ChooseValue(this IWebDriver driver, By selector, string value)
        {
            var element = driver.FindElement(selector);
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(value);
        }

        public static void SendKeys(this IWebDriver driver, By selector, string value, bool clearText = true)
        {
            var element = driver.FindElement(selector);
            if (clearText) element.Clear();
            element.SendKeys(value + Keys.Tab);
        }

        public static IWebElement WaitForElement(this IWebDriver driver, By selector, bool waitForClickable = false)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(defaultTimeout));
            //return waitForClickable ? wait.Until(ExpectedConditions.ElementExists(selector)) : wait.Until(x => x.FindElement(selector));
            wait.Until(ExpectedConditions.ElementExists(selector));
            return wait.Until(ExpectedConditions.ElementToBeClickable(selector));
        }

        public static void ContainsHeader(this IWebDriver driver, string header)
        {
            driver.PageSource.Contains($"<h2>{header}</h2>");
        }
    }
}
