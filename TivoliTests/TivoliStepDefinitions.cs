using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using TestsCommon;

namespace TivoliTests
{
    [Binding]
    public sealed class TivoliStepDefinitions : TivoliCommon
    {
        [AfterScenario]
        public void AfterScenario()
        {
            Driver.Quit();
        }

        [Then(@"I am on the '(.*)' page")]
        public void ThenIAmSentToThePage(string header)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));
            Assert.IsTrue(wait.Until((d) => d.PageSource.Contains($"<h2>{header}</h2>")));
        }

        [Given(@"I open the (.*) web site")]
        public void GivenIOpenTheWebsite(string siteName)
        {
            var landingPage = ConfigurationManager.AppSettings[siteName];
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl(landingPage);
        }

        [Then(@"The page contains (.*) '(.*)'")]
        public void ThenThePageContains(string textType, string text)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));

            if (textType == "substring")
                Assert.IsTrue(wait.Until((d) => d.PageSource.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > 0), Driver.PageSource);

            if (textType == "header")
                Assert.IsTrue(wait.Until((d) => d.PageSource.IndexOf($"<h1>{text}</h1>", StringComparison.InvariantCultureIgnoreCase) > 0), Driver.PageSource);
        }

        [Then(@"The '(.*)' textbox shows validation message '(.*)'")]
        public void ThenTheTextboxShowsValidationMessage(string elementId, string validationMessage)
        {
            var selector = GetElementSelector("textbox", "id", elementId);
            var actualMessage = Driver.GetValidationMessage(selector);
            Assert.AreEqual(validationMessage, actualMessage);
        }

        [When(@"I click the (.*) with (.*) '(.*)'"), Given(@"I click the (.*) with (.*) '(.*)'")]
        public void WhenIClickTheControl(string elementType, string elementSelector, string elementIdentifier)
        {
            var selector = GetElementSelector(elementType, elementSelector, elementIdentifier);
            Driver.ClickElement(selector);
        }

        [When(@"I (.*) '(.*)' in the '(.*)' (.*)"), Given(@"I (.*) '(.*)' in the '(.*)' (.*)")]
        public void WhenIChooseInTheCombobox(string action, string value, string elementId, string controlType)
        {
            var locator = By.Id(elementId);
            if (action == "choose") Driver.ChooseValue(locator, value);
            if (action == "click") Driver.ClickElement(locator);
            if (action == "enter") Driver.SendKeys(locator, value);
        }

        private static By GetElementSelector(string elementType, string elementSelector, string elementIdentifier)
        {
            if (elementType == "menu item")
                return By.XPath($"//span[text() = '{elementIdentifier}']");

            if (elementType == "button")
                return By.XPath($"//span[text() = '{elementIdentifier}']");

            if (elementSelector.ToLower() == "id") return By.Id(elementIdentifier);
            else if (elementSelector.ToLower() == "xpath") return By.XPath(elementIdentifier);
            else if (elementSelector.ToLower() == "text") return By.LinkText(elementIdentifier);
            else throw new ArgumentOutOfRangeException(nameof(elementSelector));
        }
    }
}
