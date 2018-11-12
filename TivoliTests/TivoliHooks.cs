using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using TechTalk.SpecFlow;

namespace TivoliTests
{
    [Binding]
    public sealed class TivoliHooks
    {
        [BeforeScenario(Order = 1)]
        [Scope(Tag = "InternetExplorer")]
        public void BeforeInternetExplorerScenario()
        {
            ScenarioContext.Current.Set<IWebDriver>(new InternetExplorerDriver(), nameof(IWebDriver));
        }

        [BeforeScenario(Order = 2)]
        [Scope(Tag = "Chrome")]
        public void BeforeChromeScenario()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("headless");
            ScenarioContext.Current.Set<IWebDriver>(new ChromeDriver(options), nameof(IWebDriver));
        }

        [BeforeScenario(Order = 3)]
        [Scope(Tag = "InternetExplorer")]
        [Scope(Tag = "Chrome")]
        public void BeforeScenario()
        {
            if (!ScenarioContext.Current.ContainsKey(nameof(IWebDriver)))
                ScenarioContext.Current.Set<IWebDriver>(new InternetExplorerDriver(), nameof(IWebDriver));
        }
    }
}
