using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace MPPensionTests
{
    public class MPPensionCommon
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
