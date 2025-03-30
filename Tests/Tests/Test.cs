using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        private WebDriverWait wait = null!;
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
        [DataRow("chrome", "", "", "Username is required")]
        [DataRow("edge", "", "", "Username is required")]
        [Parallelizable(ParallelScope.Self)]
        public void TestLoginWithEmptyCredentials(string browser, string username, string password, string expectedMessage)
        {
            Logger.Info($"Executing TestLoginWithEmptyCredentials on {browser}");
            SetupDriver(browser);

            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClearInputs();
            loginPage.ClickLogin();

            string errorMessage = loginPage.GetErrorMessage();

            Logger.Info("Verifying error message for empty credentials");
            errorMessage.Should().NotBeNullOrWhiteSpace("Error message should be displayed when username and password are empty.");
            Logger.Info($"Error message is: {errorMessage}");

            errorMessage.Should().Contain(expectedMessage, $"Expected error message to contain '{expectedMessage}' but was '{errorMessage}'");
            Logger.Info($"Verified that error message contains: {expectedMessage}");
        }


        [TestMethod]
        [TestCategory("UC-2")]
        [TestCategory("Parallel")]
        [DataRow("chrome", "standard_user", "", "Password is required")]
        [DataRow("edge", "standard_user", "", "Password is required")]
        [Parallelizable(ParallelScope.Self)]
        public void TestLoginWithOnlyUsername(string browser, string username, string password, string expectedMessage)
        {
            Logger.Info($"Executing TestLoginWithOnlyUsername on {browser}");
            SetupDriver(browser);

            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClearInputs();
            loginPage.ClickLogin();

            string errorMessage = loginPage.GetErrorMessage();

            Logger.Info("Verifying error message for missing password");
            errorMessage.Should().NotBeNullOrWhiteSpace("Error message should be displayed when password is empty.");
            Logger.Info($"Error message is: {errorMessage}");

            errorMessage.Should().Contain(expectedMessage, $"Expected error message to contain '{expectedMessage}', but was '{errorMessage}'");
            Logger.Info($"Verified that error message contains: {expectedMessage}");
        }


        [TestMethod]
        [TestCategory("UC-3")]
        [TestCategory("Parallel")]
        [DataRow("chrome", "standard_user", "secret_sauce")]
        [DataRow("edge", "standard_user", "secret_sauce")]
        [Parallelizable(ParallelScope.Self)]
        public void TestLoginWithValidCredentials(string browser, string username, string password)
        {
            Logger.Info($"Executing TestLoginWithValidCredentials on {browser}");
            SetupDriver(browser);

            loginPage.Should().NotBeNull("LoginPage instance should be initialized.");
            driver.Should().NotBeNull("WebDriver should be initialized.");

            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClearInputs();
            loginPage.ClickLogin();

            wait.Until(d => d.Title.Contains("Swag Labs"));

            Logger.Info("Verifying the page title after successful login");
            driver.Title.Should().NotBeNullOrWhiteSpace("Page title should not be empty after login.");
            Logger.Info($"Page title is: {driver.Title}");

            driver.Title.Should().Be("Swag Labs", "User should be redirected to the Swag Labs page after successful login.");
            Logger.Info($"Verified that page title is 'Swag Labs'.");
        }


        [TestCleanup]
        public void Teardown()
        {
            Logger.Info("Closing browser");
            driver?.Quit();
            Logger.Info("Browser closed successfully");
        }

        private void SetupDriver(string browser)
        {
            Logger.Info($"Initializing driver for {browser}");
            try
            {
                IDriverFactory driverFactory = new DriverFactory(browser);
                driver = driverFactory.CreateDriver();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(LoginPage.Url);
                loginPage = new LoginPage(driver);
            }
            catch (WebDriverException ex)
            {
                Logger.Error($"Error initializing driver for {browser}: {ex.Message}");
                throw new AssertFailedException($"Failed to start {browser}");
            }
        }
    }
}
