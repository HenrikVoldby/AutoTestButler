using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace TivoliTests
{
    public class TivoliCommon
    {
        public IWebDriver Driver
        {
            get
            {
                return ScenarioContext.Current.Get<IWebDriver>(nameof(IWebDriver));
            }
        }
    }
}
