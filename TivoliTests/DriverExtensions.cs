using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TivoliTests
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

        public static string GetValidationMessage(this IWebDriver driver, By selector)
        {
            var element = driver.WaitForElement(selector, true);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            return (string)executor.ExecuteScript("return arguments[0].validationMessage;", element);
        }

        public static void SendKeys(this IWebDriver driver, By selector, string value, bool clearText = true)
        {
            var element = driver.FindElement(selector);
            if (clearText) element.Clear();
            element.SendKeys(value + Keys.Tab);
        }

        public static IWebElement WaitUntilVisible(this IWebDriver driver, By selector)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(defaultTimeout));
            return wait.Until(ExpectedConditions.ElementIsVisible(selector));
        }



        public static IWebElement WaitForElement(this IWebDriver driver, By selector, bool waitForClickable = false)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(defaultTimeout));
            wait.Until(ExpectedConditions.ElementExists(selector));
            return wait.Until(ExpectedConditions.ElementToBeClickable(selector));
        }
    }
}
