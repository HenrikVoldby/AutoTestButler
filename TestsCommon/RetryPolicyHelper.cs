using OpenQA.Selenium;
using Polly;
using System;

namespace TestsCommon
{
    public class RetryPolicyHelper
    {
        private static int _defaultRetries = 5;
        private static TimeSpan _defaultSleepDuration = TimeSpan.FromSeconds(20);

        public static Policy ElementNotFoundRetryPolicy()
        {
            return Policy
                .Handle<WebDriverException>()
                .Or<StaleElementReferenceException>()
                .Or<NoSuchElementException>()                
                .WaitAndRetry(_defaultRetries, i => _defaultSleepDuration);
        }
    }
}
