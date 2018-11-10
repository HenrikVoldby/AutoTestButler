using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;

namespace MPPensionTests
{
    [Binding]
    public sealed class MPPensionHooks
    {
        [BeforeScenario(Order = 1)]
        [Scope(Tag = "InternetExplorer")]
        public void BeforeInternetExplorerScenario()
        {
            ScenarioContext.Current.Set<IWebDriver>(new InternetExplorerDriver(@"C:\Work\Test\DanskeSpilTest\packages\Selenium.WebDriver.IEDriver.3.11.1\driver"), nameof(IWebDriver));
        }

        [BeforeScenario(Order = 2)]
        [Scope(Tag = "Chrome")]
        public void BeforeChromeScenario()
        {
            ScenarioContext.Current.Set<IWebDriver>(new ChromeDriver(@"C:\Work\Test\DanskeSpilTest\packages\Selenium.WebDriver.ChromeDriver.2.38.0\driver\win32"), nameof(IWebDriver));
        }

        [BeforeScenario(Order = 3)]
        [Scope(Tag = "InternetExplorer")]
        [Scope(Tag = "Chrome")]
        public void BeforeScenario()
        {
            if (!ScenarioContext.Current.ContainsKey(nameof(IWebDriver)))
                ScenarioContext.Current.Set<IWebDriver>(new InternetExplorerDriver(@"C:\Work\Test\DanskeSpilTest\packages\Selenium.WebDriver.IEDriver.3.11.1\driver"), nameof(IWebDriver));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.Keys.ToList().Contains(nameof(IWebDriver)))
                ScenarioContext.Current.Get<IWebDriver>(nameof(IWebDriver)).Quit();
        }
    }
}
