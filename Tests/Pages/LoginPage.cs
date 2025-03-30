using OpenQA.Selenium;
using NLog;

namespace Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly By usernameField = By.XPath("//input[@id='user-name']");
        private readonly By passwordField = By.XPath("//input[@id='password']");
        private readonly By loginButton = By.XPath("//input[@id='login-button']");
        private readonly By errorMessage = By.XPath("//h3[@data-test='error']");
        public static readonly string Url = "https://www.saucedemo.com/";

        public LoginPage(IWebDriver driver) => this.driver = driver;

        public void EnterUsername(string username)
        {
            Logger.Info("Entering username");
            var element = driver.FindElement(usernameField);
            element.Clear();
            element.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            Logger.Info("Entering password");
            var element = driver.FindElement(passwordField);
            element.Clear();
            element.SendKeys(password);
        }

        public void ClickLogin()
        {
            Logger.Info("Clicking login button");
            driver.FindElement(loginButton).Click();
        }

        public string GetErrorMessage()
        {
            var errorText = driver.FindElement(errorMessage).Text;
            Logger.Warn($"Login failed: {errorText}");
            return errorText;
        }

        public void ClearInputs()
        {
            Logger.Info("Clearing input fields");
            driver.FindElement(usernameField).Clear();
            driver.FindElement(passwordField).Clear();
        }
    }
}
