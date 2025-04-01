using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tests.Utils
{
    public class ExplicitWaitStrategy : IWaitStrategy
    {
        private readonly int timeoutSeconds;

        public ExplicitWaitStrategy(int timeoutSeconds = 10)
        {
            this.timeoutSeconds = timeoutSeconds;
        }

        public IWebElement WaitForElement(IWebDriver driver, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d => d.FindElement(locator));
        }
    }
}
