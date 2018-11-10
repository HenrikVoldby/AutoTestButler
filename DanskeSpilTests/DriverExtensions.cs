using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace DanskeSpilTests
{
    internal static class DriverExtensions
    {
        private const int defaultTimeout = 10;

        public static void ClickLink(this IWebDriver driver, By selector)
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

        public static void ClickButton(this IWebDriver driver, By selector)
        {
            var element = driver.WaitForElement(selector);
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

        public static IWebElement WaitForElement(this IWebDriver driver, By selector, bool waitForClickable = false, int seconds = defaultTimeout)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return waitForClickable ? 
                wait.Until(ExpectedConditions.ElementExists(selector)) : 
                wait.Until(ExpectedConditions.ElementToBeClickable(selector));
        }

        public static IWebElement FindImage(this IWebDriver driver, string srcSubstring)
        {
            return driver.FindElement(By.XPath($"//img[contains(@src, '{srcSubstring}')]"));
        }

        public static bool WaitForSubstring(this IWebDriver driver, string substring)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(defaultTimeout));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//*[contains(text(), '{substring}')]")));
            return true;
        }

        public static bool ContainsHeader(this IWebDriver driver, string header)
        {
            return driver.PageSource.Contains($"<h2>{header}</h2>");
        }
    }
}
