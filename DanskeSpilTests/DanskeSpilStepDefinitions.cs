using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using TestsCommon;

namespace DanskeSpilTests
{
    [Binding]
    public sealed class DanskeSpilStepDefinitions : DanskeSpilCommon
    {
        const int elementTimeout = 30;
        private enum GameContainers
        {
            Guldbarren,
            SmilISigte,

        }

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
            Driver.Navigate().GoToUrl(landingPage);
            Driver.Manage().Window.Maximize();
        }

        [Given(@"I wait for the game to finish")]
        public void GivenIWaitForTheGameToFinish()
        {
            Driver.WaitUntilExists(By.ClassName("bwi-modal-message"), 120);
        }

        [When(@"I wait until the game has loaded")]
        public void WhenIWaitUntilTheElementHasLoaded()
        {
            Driver.WaitUntilExists(By.Id("scratchAreaContainer"), 120);
        }

        [Then(@"The page contains (.*) '(.*)'")]
        public void ThenThePageContainsSubstring(string elementType, string substring)
        {
            if (elementType == "substring") Assert.IsTrue(Driver.WaitForSubstring(substring), $"Page source does not contain {substring}");
            else if (elementType == "image") Assert.IsNotNull(Driver.FindImage(substring));
            else if (elementType == "element") Assert.IsNotNull(Driver.WaitUntilExists(By.Id(substring), 20));
            else throw new ArgumentOutOfRangeException(nameof(elementType));
        }

        [Then(@"The '(.*)' game container is visible")]
        public void ThenTheGameContainerIVisible(string gameContainer)
        {
            IWebElement webElement = null;
            if (gameContainer == GameContainers.Guldbarren.ToString()) webElement = Driver.WaitUntilExists(By.Id("QKGULD-scratch-gamezone"));
            if (gameContainer == GameContainers.SmilISigte.ToString()) webElement = Driver.WaitUntilExists(By.Id("QKSS-scratch-gamezone"));

            Assert.IsTrue(webElement != null);
        }

        [When(@"I click the (.*) with (.*) selector '(.*)'"), Given(@"I click the (.*) with (.*) selector '(.*)'")]
        public void WhenIClickTheControl(string elementType, string elementSelector, string elementIdentifier)
        {
            if (elementType == "link")
                Driver.ClickElement(GetElementSelector(elementSelector, elementIdentifier));
            if (elementType == "button")
                Driver.ClickElement(GetElementSelector(elementSelector, elementIdentifier));
        }

        [When(@"I click the link with URL '(.*)'")]
        public void WhenIClickTheLinkWithURL(string url)
        {
            Driver.ClickElement(GetElementSelector("xpath", $"//a[@href=\"{url}\"]"));
        }

        [When(@"I (.*) '(.*)' in the '(.*)' (.*)"), Given(@"I (.*) '(.*)' in the '(.*)' (.*)")]
        public void WhenIChooseInTheCombobox(string action, string value, string elementId, string controlType)
        {
            if (action == "choose") Driver.ChooseValue(By.Id(elementId), value);
            if (action == "click") Driver.ClickElement(By.Id(elementId));
            if (action == "enter") Driver.SendKeys(By.Id(elementId), value);
        }

        private static By GetElementSelector(string elementSelector, string elementIdentifier)
        {
            if (elementSelector.ToLower() == "id") return By.Id(elementIdentifier);
            else if (elementSelector.ToLower() == "xpath") return By.XPath(elementIdentifier);
            else if (elementSelector.ToLower() == "class") return By.ClassName(elementIdentifier);
            else throw new ArgumentException(nameof(elementSelector));
        }
    }
}
