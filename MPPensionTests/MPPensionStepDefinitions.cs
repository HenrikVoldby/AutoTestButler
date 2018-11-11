using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using TestsCommon;

namespace MPPensionTests
{
    [Binding]
    public sealed class MPPensionStepDefinitions : MPPensionCommon
    {
        [AfterScenario]
        public void AfterScenario()
        {
            Driver.Close();
            Driver.Quit();
        }

        [Then(@"I am on the '(.*)' page")]
        public void ThenIAmSentToThePage(string header)
        {
            Driver.ContainsHeader(header);
        }

        [Given(@"I open the (.*) web site")]
        public void GivenIOpenTheWebsite(string siteName)
        {
            var landingPage = ConfigurationManager.AppSettings[siteName];
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl(landingPage);
        }

        [Then(@"The page contains '(.*)'")]
        public void ThenThePageContainsAsdasdasd(string substring)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));
            Assert.IsTrue(wait.Until((d) => d.PageSource.Contains(substring)));
        }

        [Then(@"The page contains substring '(.*)'")]
        public void ThenThePageContainsSubstring(string substring)
        {
            Driver.PageSource.Contains(substring);
        }

        [When(@"I click the (.*) with (.*) selector '(.*)'"), Given(@"I click the (.*) with (.*) selector '(.*)'")]
        public void WhenIClickTheControl(string elementType, string elementSelector, string elementIdentifier)
        {
            Driver.ClickElement(GetElementSelector(elementSelector, elementIdentifier));
        }

        [When(@"I (.*) '(.*)' in the '(.*)' (.*)"), Given(@"I (.*) '(.*)' in the '(.*)' (.*)")]
        public void WhenIChooseInTheCombobox(string action, string value, string elementId, string controlType)
        {
            var locator = By.Id(elementId);
            if (action == "choose") Driver.ChooseValue(locator, value);
            if (action == "click") Driver.ClickElement(locator);
            if (action == "enter") Driver.SendKeys(locator, value);
        }

        private static By GetElementSelector(string elementSelector, string elementIdentifier)
        {
            if (elementSelector.ToLower() == "id") return By.Id(elementIdentifier);
            else if (elementSelector.ToLower() == "xpath") return By.XPath(elementIdentifier);
            else if (elementSelector.ToLower() == "text") return By.LinkText(elementIdentifier);
            else throw new ArgumentOutOfRangeException(nameof(elementSelector));
        }
    }
}
