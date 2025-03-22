using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using NLog;
using FluentAssertions;

namespace Tests
{
    public interface IDriverFactory
    {
        IWebDriver CreateDriver();
    }

    public class DriverFactory : IDriverFactory
    {
        private readonly string _browser;

        public DriverFactory(string browser)
        {
            _browser = browser.ToLower();
        }

        public IWebDriver CreateDriver()
        {
            return _browser switch
            {
                "chrome" => new ChromeDriver(),
                "edge" => new EdgeDriver(),
                _ => throw new ArgumentException($"Unsupported browser: {_browser}")
            };
        }
    }

    [TestClass]
    public class LoginTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
        private const string SauceDemoUrl = "https://www.saucedemo.com/";

        private static IEnumerable<object[]> LoginDataProvider()
        {
            yield return new object[] { "", "", "Username is required" };
            yield return new object[] { "standard_user", "", "Password is required" };
            yield return new object[] { "standard_user", "secret_sauce", "Swag Labs" };
        }

        [TestInitialize]
        public void Setup()
        {
            Logger.Info("Opening the browser");

            string browser = "chrome";
            IDriverFactory driverFactory = new DriverFactory(browser);
            driver = driverFactory.CreateDriver();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(SauceDemoUrl);
        }

        [TestMethod]
        [DynamicData(nameof(LoginDataProvider))]
        [TestCategory("ParallelExecution")]
        public void TestLogin(string username, string password, string expectedMessage)
        {
            Logger.Info($"Starting test with Username: {username}, Password: {password}");

            var usernameInput = driver.FindElement(By.XPath("//input[@id='user-name']"));
            var passwordInput = driver.FindElement(By.XPath("//input[@id='password']"));
            var loginButton = driver.FindElement(By.XPath("//input[@id='login-button']"));

            if (!string.IsNullOrEmpty(username))
            {
                usernameInput.SendKeys(username);
                Logger.Info($"Input Username: {username}");
            }

            if (!string.IsNullOrEmpty(password))
            {
                passwordInput.SendKeys(password);
                Logger.Info($"Input Password: {password}");
            }

            if (!string.IsNullOrEmpty(username))
            {
                usernameInput.Clear();
                Logger.Info("Username Clear");
            }

            if (!string.IsNullOrEmpty(password))
            {
                passwordInput.Clear();
                Logger.Info("Password Clear");
            }

            loginButton.Click();
            Logger.Info("Click Button");

            if (expectedMessage == "Swag Labs")
            {
                wait.Until(d => d.Title.Contains("Swag Labs"));
                driver.Title.Should().Be("Swag Labs");
                Logger.Info("Login successful, navigated to Swag Labs");
            }
            else
            {
                var errorMessage = driver.FindElement(By.XPath("//h3[@data-test='error']")).Text;
                errorMessage.Should().Contain(expectedMessage);
                Logger.Warn($"Login failed with expected error: {errorMessage}");
            }
        }

        [TestCleanup]
        public void Teardown()
        {
            Logger.Info("Closing browser");
            driver.Quit();
        }
    }
}
