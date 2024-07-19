using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTests
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://demo.guru99.com/V1/index.php");
        }

        [Test]
        public void SuccessfulLogin()
        {
            driver.FindElement(By.Name("uid")).SendKeys("mngr581643");
            driver.FindElement(By.Name("password")).SendKeys("YnUvAga");
            driver.FindElement(By.Name("btnLogin")).Click();
            Assert.IsTrue(driver.Url.Contains("Managerhomepage")); // Adjust the URL check based on the actual successful login URL
        }

        [Test]
        public void FailedLogin()
        {
            driver.FindElement(By.Name("uid")).SendKeys("invalid_user");
            driver.FindElement(By.Name("password")).SendKeys("invalid_password");
            driver.FindElement(By.Name("btnLogin")).Click();
            var alert = driver.SwitchTo().Alert(); // Guru99 shows an alert on login failure
            Assert.IsTrue(alert.Text.Contains("User or Password is not valid"));
            alert.Accept(); // Close the alert
        }

        [Test]
        public void ErrorMessageDisplay()
        {
            driver.FindElement(By.Name("uid")).SendKeys("invalid_user");
            driver.FindElement(By.Name("password")).SendKeys("invalid_password");
            driver.FindElement(By.Name("btnLogin")).Click();
            var alert = driver.SwitchTo().Alert(); // Guru99 shows an alert on login failure
            Assert.IsTrue(alert.Text.Contains("User or Password is not valid"));
            alert.Accept(); // Close the alert
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose(); // Ensure proper disposal
            }
        }
    }
}
