using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Tests
{
    public class PastebinTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private const string PastebinUrl = "https://pastebin.com/";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Task1()
        {
            driver.Navigate().GoToUrl(PastebinUrl);

            var pasteTextArea = wait.Until(d => d.FindElement(By.Id("postform-text")));
            pasteTextArea.Click();
            pasteTextArea.SendKeys("Hello from WebDriver");

            var expirationDropdown = wait.Until(d => d.FindElement(By.Id("select2-postform-expiration-container")));
            expirationDropdown.Click();

            var expirationOption = wait.Until(d => d.FindElement(By.XPath("//li[contains(text(),'10 Minutes')]")));
            expirationOption.Click();

            var pasteTitle = driver.FindElement(By.Id("postform-name"));
            pasteTitle.SendKeys("helloweb");

            var submitButton = driver.FindElement(By.XPath("//button[text()='Create New Paste']"));
            submitButton.Click();
        }

        [Test]
        public void Task2()
        {
            driver.Navigate().GoToUrl(PastebinUrl);

            var pasteTextArea = wait.Until(d => d.FindElement(By.Id("postform-text")));
            pasteTextArea.SendKeys("git config --global user.name \"New Sheriff in Town\"\n" +
                                  "git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")\n" +
                                  "git push origin master --force");

            var syntaxDropdown = wait.Until(d => d.FindElement(By.Id("select2-postform-format-container")));
            syntaxDropdown.Click();
            var syntaxOption = wait.Until(d => d.FindElement(By.XPath("//li[contains(text(),'Bash')]")));
            syntaxOption.Click();

            var expirationDropdown = driver.FindElement(By.Id("select2-postform-expiration-container"));
            expirationDropdown.Click();
            var expirationOption = driver.FindElement(By.XPath("//li[contains(text(),'10 Minutes')]"));
            expirationOption.Click();

            var pasteTitle = driver.FindElement(By.Id("postform-name"));
            pasteTitle.SendKeys("how to gain dominance among developers");

            var submitButton = driver.FindElement(By.XPath("//button[text()='Create New Paste']"));
            submitButton.Click();
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}
