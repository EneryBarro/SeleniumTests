using OpenQA.Selenium;

namespace Tests.Utils
{
    public interface IDriverFactory
    {
        IWebDriver CreateDriver();
    }

    public class DriverFactory : IDriverFactory
    {
        private readonly string _browser;

        public DriverFactory(string browser) => _browser = browser.ToLower();

        public IWebDriver CreateDriver()
        {
            try
            {
                return _browser switch
                {
                    "chrome" => new OpenQA.Selenium.Chrome.ChromeDriver(),
                    "edge" => new OpenQA.Selenium.Edge.EdgeDriver(),
                    _ => throw new ArgumentException($"Unsupported browser: {_browser}")
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to initialize WebDriver for {_browser}: {ex.Message}");
            }
        }
    }
}