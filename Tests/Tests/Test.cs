using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using NLog;
using FluentAssertions;
using Tests.Pages;
using Tests.Utils;
using NUnit.Framework;

namespace Tests.Tests
{
    [TestClass]
    [Parallelizable(ParallelScope.All)]
    public class LoginTests
    {
        private IWebDriver driver = null!;
        private IWaitStrategy waitStrategy = null!;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private LoginPage loginPage = null!;

        [TestInitialize]
        public void Setup()
        {
            Logger.Info("Setting up WebDriver");
        }

        [TestMethod]
        [TestCategory("UC-1")]
        [TestCategory("Parallel")]
        [DataRow("chrome", "standard_user", "secret_sauce", "Username is required")]
        [DataRow("edge", "standard_user", "secret_sauce", "Username is required")]
        [DataRow("firefox", "standard_user", "secret_sauce", "Username is required")]
        [Parallelizable(ParallelScope.Self)]
        public void TestLoginWithEmptyCredentials(string browser, string username, string password, string expectedMessage)
        {
            Logger.Info($"Executing TestLoginWithEmptyCredentials on {browser}");
            SetupDriver(browser);

            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClearInputs();
            loginPage.ClickLogin();

            var errorMessage = loginPage.GetErrorMessage();
            Logger.Info($"Verifying error message: {errorMessage}");
            errorMessage.Should().Contain(expectedMessage);
        }

        [TestMethod]
        [TestCategory("UC-2")]
        [TestCategory("Parallel")]
        [DataRow("chrome", "standard_user", "secret_sauce", "Password is required")]
        [DataRow("edge", "standard_user", "secret_sauce", "Password is required")]
        [DataRow("firefox", "standard_user", "secret_sauce", "Password is required")]
        [Parallelizable(ParallelScope.Self)]
        public void TestLoginWithOnlyUsername(string browser, string username, string password, string expectedMessage)
        {
            Logger.Info($"Executing TestLoginWithOnlyUsername on {browser}");
            SetupDriver(browser);

            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClearPassword();
            loginPage.ClickLogin();

            var errorMessage = loginPage.GetErrorMessage();
            Logger.Info($"Verifying error message: {errorMessage}");
            errorMessage.Should().Contain(expectedMessage);
        }

        [TestMethod]
        [TestCategory("UC-3")]
        [TestCategory("Parallel")]
        [DataRow("chrome", "standard_user", "secret_sauce")]
        [DataRow("edge", "standard_user", "secret_sauce")]
        [DataRow("firefox", "standard_user", "secret_sauce")]
        [Parallelizable(ParallelScope.Self)]
        public void TestLoginWithValidCredentials(string browser, string username, string password)
        {
            Logger.Info($"Executing TestLoginWithValidCredentials on {browser}");
            SetupDriver(browser);

            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClickLogin();

            waitStrategy.WaitForElement(driver, By.TagName("title"));
            driver.Title.Should().Be("Swag Labs");
            Logger.Info("Successfully navigated to Swag Labs page.");
        }

        [TestCleanup]
        public void Teardown()
        {
            Logger.Info("Closing browser");
            driver?.Quit();
        }

        private void SetupDriver(string browser)
        {
            Logger.Info($"Initializing driver for {browser}");
            try
            {
                IDriverFactory driverFactory = new DriverFactory(browser);
                driver = driverFactory.CreateDriver();
                waitStrategy = new ExplicitWaitStrategy(10);
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(LoginPage.Url);
                loginPage = new LoginPage(driver, waitStrategy);
            }
            catch (WebDriverException ex)
            {
                Logger.Error($"Error initializing driver for {browser}: {ex.Message}");
                throw new AssertFailedException($"Failed to start {browser}");
            }
        }
    }
}
