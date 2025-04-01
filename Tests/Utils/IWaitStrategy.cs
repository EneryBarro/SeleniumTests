using OpenQA.Selenium;

namespace Tests.Utils
{
    public interface IWaitStrategy
    {
        IWebElement WaitForElement(IWebDriver driver, By locator);
    }
}