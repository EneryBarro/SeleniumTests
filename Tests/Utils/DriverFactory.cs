using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Tests.Utils
{
    public interface IDriverFactory
    {
        IWebDriver CreateDriver();
    }

    public class BrowserConfiguration
    {
        public bool Headless { get; set; }
        public int ImplicitWait { get; set; }
        public int PageLoadTimeout { get; set; }
    }

    public class DriverFactory : IDriverFactory
    {
        private readonly string _browser;
        private readonly BrowserConfiguration _config;

        public DriverFactory(string browser)
        {
            _browser = browser.ToLower();
            _config = GetConfigurationForBrowser(_browser);
        }

        public IWebDriver CreateDriver()
        {
            IWebDriver driver;

            switch (_browser)
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    if (_config.Headless) chromeOptions.AddArgument("--headless");
                    driver = new ChromeDriver(chromeOptions);
                    break;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    if (_config.Headless) firefoxOptions.AddArgument("--headless");
                    driver = new FirefoxDriver(firefoxOptions);
                    break;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    if (_config.Headless) edgeOptions.AddArgument("--headless");
                    driver = new EdgeDriver(edgeOptions);
                    break;

                default:
                    throw new ArgumentException($"Unsupported browser: {_browser}");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_config.ImplicitWait);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_config.PageLoadTimeout);
            return driver;
        }

        private BrowserConfiguration GetConfigurationForBrowser(string browser)
        {
            return browser switch
            {
                "chrome" => new BrowserConfiguration { Headless = false, ImplicitWait = 5, PageLoadTimeout = 30 },
                "firefox" => new BrowserConfiguration { Headless = false, ImplicitWait = 3, PageLoadTimeout = 25 },
                "edge" => new BrowserConfiguration { Headless = false, ImplicitWait = 5, PageLoadTimeout = 30 },
                _ => throw new ArgumentException($"No configuration found for browser: {browser}")
            };
        }
    }
}
