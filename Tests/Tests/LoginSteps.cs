using Reqnroll;
using OpenQA.Selenium;
using FluentAssertions;
using Tests.Pages;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Tests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private IWebDriver driver = null!;
        private LoginPage loginPage = null!;
        private WebDriverWait wait = null!;

        [Given(@"I navigate to the login page")]
        public void GivenINavigateToTheLoginPage()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(LoginPage.Url);
            loginPage = new LoginPage(driver);
        }

        [When(@"I enter username ""(.*)"" and password ""(.*)""")]
        public void WhenIEnterUsernameAndPassword(string username, string password)
        {
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
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
            wait.Until(d => d.Title.Contains(expectedPageTitle));
            driver.Title.Should().Be(expectedPageTitle);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }
    }
}
