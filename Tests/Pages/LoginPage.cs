using OpenQA.Selenium;
using NLog;
using Tests.Utils;

namespace Tests.Pages
{
    public class LoginPage : BasePage
    {
        private readonly By usernameField = By.XPath("//input[@id='user-name']");
        private readonly By passwordField = By.XPath("//input[@id='password']");
        private readonly By loginButton = By.XPath("//input[@id='login-button']");
        private readonly By errorMessage = By.XPath("//h3[@data-test='error']");
        public static readonly string Url = "https://www.saucedemo.com/";

        public LoginPage(IWebDriver driver, IWaitStrategy waitStrategy) 
               : base(driver, waitStrategy) { }

        public void EnterUsername(string username) => EnterText(usernameField, username);

        public void EnterPassword(string password) => EnterText(passwordField, password);

        public void ClickLogin() => Click(loginButton);

        public string GetErrorMessage() => GetText(errorMessage);

        public void ClearInputs()
        {
            Logger.Info("Clearing username and password fields");
            var usernameElement = driver.FindElement(usernameField);
            var passwordElement = driver.FindElement(passwordField);

            usernameElement.SendKeys(Keys.Control + "a");
            usernameElement.SendKeys(Keys.Delete);

            passwordElement.SendKeys(Keys.Control + "a");
            passwordElement.SendKeys(Keys.Delete);
        }

        public void ClearPassword()
        {
            Logger.Info("Clearing password field");
            var passwordElement = driver.FindElement(passwordField);

            passwordElement.SendKeys(Keys.Control + "a");
            passwordElement.SendKeys(Keys.Delete);
        }
    }
}
