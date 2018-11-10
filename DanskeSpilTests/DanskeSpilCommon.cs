using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace DanskeSpilTests
{
    public class DanskeSpilCommon
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
