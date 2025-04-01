using Reqnroll;
using OpenQA.Selenium;
using FluentAssertions;
using Tests.Pages;
using Tests.Utils;

namespace Tests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private IWebDriver driver = null!;
        private LoginPage loginPage = null!;
        private IWaitStrategy waitStrategy = null!;

        [Given(@"I navigate to the login page with ""(.*)""")]
        public void GivenINavigateToTheLoginPage(string browser)
        {
            IDriverFactory driverFactory = new DriverFactory(browser);
            driver = driverFactory.CreateDriver();
            waitStrategy = new ExplicitWaitStrategy(10);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(LoginPage.Url);
            loginPage = new LoginPage(driver, waitStrategy);
        }

        [When(@"I enter username ""(.*)"" and password ""(.*)""")]
        public void WhenIEnterUsernameAndPassword(string username, string password)
        {
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
        }

        [When(@"I perform ""(.*)"" on input fields")]
        public void WhenIPerformActionOnInputFields(string action)
        {
            switch (action.ToLower())
            {
                case "clear both input fields":
                    loginPage.ClearInputs();
                    break;
                case "clear only password field":
                    loginPage.ClearPassword();
                    break;
                case "do not clear fields":
                    break;
                default:
                    throw new ArgumentException($"Unknown action: {action}");
            }
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            loginPage.ClickLogin();
        }

        [Then(@"I should see the error message ""(.*)""")]
        public void ThenIShouldSeeTheErrorMessage(string expectedMessage)
        {
            string errorMessage = loginPage.GetErrorMessage();
            errorMessage.Should().Contain(expectedMessage);
        }

        [Then(@"I should be redirected to the ""(.*)"" page")]
        public void ThenIShouldBeRedirectedToThePage(string expectedPageTitle)
        {
            waitStrategy.WaitForElement(driver, By.TagName("title"));
            driver.Title.Should().Be(expectedPageTitle);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }
    }
}
