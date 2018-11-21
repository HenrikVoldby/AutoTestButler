using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestsCommon
{
    public static class DriverExtensions
    {
        private const int defaultWaitSeconds = 20;

        public static void ClickElement(this IWebDriver driver, By selector)
        {
            var element = driver.WaitUntilClickable(selector);

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

        public static void ContainsHeader(this IWebDriver driver, string header)
        {
            driver.PageSource.Contains($"<h2>{header}</h2>");
        }

        public static IWebElement FindImage(this IWebDriver driver, string srcSubstring)
        {
            return driver.FindElement(By.XPath($"//img[contains(@src, '{srcSubstring}')]"));
        }

        public static bool WaitForSubstring(this IWebDriver driver, string substring)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(defaultWaitSeconds));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//*[contains(text(), '{substring}')]")));
            return true;
        }

        public static string GetValidationMessage(this IWebDriver driver, By selector)
        {
            var element = driver.WaitUntilClickable(selector);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            return (string)executor.ExecuteScript("return arguments[0].validationMessage;", element);
        }

        public static string GetTooltipMessage(this IWebDriver driver, By selector)
        {
            var mouseAction = new Actions(driver);
            var element = driver.WaitUntilClickable(selector);
            mouseAction.MoveToElement(element).Perform();
            var toolTipElement = driver.WaitUntilExists(By.CssSelector(".UIInput_input"));
            return toolTipElement.Text;
        }

        public static void SendKeys(this IWebDriver driver, By selector, string value, bool clearText = true)
        {
            var element = driver.WaitForElement(ExpectedConditions.ElementIsVisible(selector));
            if (clearText) element.Clear();
            try
            {
                element.SendKeys(value + Keys.Tab);
            }
            catch (Exception)
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript($"arguments[0].value = '{value + Keys.Tab}';", element);
            }
        }

        public static IWebElement WaitUntilExists(this IWebDriver driver, By selector, int seconds = defaultWaitSeconds)
        {
            return driver.WaitForElement(ExpectedConditions.ElementExists(selector));
        }

        public static IWebElement WaitUntilClickable(this IWebDriver driver, By selector, int seconds = defaultWaitSeconds)
        {
            return driver.WaitForElement(ExpectedConditions.ElementToBeClickable(selector));
        }

        public static IWebElement WaitUntilVisible(this IWebDriver driver, By selector, int seconds = defaultWaitSeconds)
        {
            return driver.WaitForElement(ExpectedConditions.ElementIsVisible(selector));
        }

        private static IWebElement WaitForElement(this IWebDriver driver, Func<IWebDriver, IWebElement> expectedCondition, int waitSeconds = defaultWaitSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException), typeof(StaleElementReferenceException));
            return wait.Until(expectedCondition);
        }
    }
}
