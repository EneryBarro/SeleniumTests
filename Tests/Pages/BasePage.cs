using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NLog;
using Tests.Utils;

namespace Tests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver driver;
        protected readonly IWaitStrategy waitStrategy;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected BasePage(IWebDriver driver, IWaitStrategy waitStrategy)
        {
            this.driver = driver;
            this.waitStrategy = waitStrategy;
        }

        protected IWebElement FindElement(By locator)
        {
            try
            {
                return waitStrategy.WaitForElement(driver, locator);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to find element with locator {locator}: {ex.Message}");
                throw;
            }
        }

        protected void Click(By locator)
        {
            Logger.Info($"Clicking element with locator {locator}");
            FindElement(locator).Click();
        }

        protected void EnterText(By locator, string text)
        {
            Logger.Info($"Entering text '{text}' into element with locator {locator}");
            var element = FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetText(By locator)
        {
            Logger.Info($"Getting text from element with locator {locator}");
            return FindElement(locator).Text;
        }
    }
}
