using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace SeleniumGridExample
{
  public class WhenBrowsingTheInternet
  {
    private const bool ShouldUseSeleniumGrid = true;

    private IWebDriver _driver;

    [SetUp]
    public void SetUp()
    {
      _driver = BuildWebDriver();
      _driver.Manage().Window.Maximize();
    }

    [Test]
    public void IShouldBeOnGoogle()
    {
      _driver.Url = "http://www.google.co.uk";
      Assert.True(_driver.Url.Contains("google"));
    }

    [TearDown]
    public void TearDown()
    {
      _driver.Close();
    }

    private IWebDriver BuildWebDriver()
    {
      if (!ShouldUseSeleniumGrid) {
        return new FirefoxDriver();
      }

      var seleniumGridUrl = BuildSeleniumGridUrl();
      var browser = Environment.GetEnvironmentVariable("SeleniumGridBrowser");
      var version = Environment.GetEnvironmentVariable("SeleniumGridVersion");
      var capabilities = new DesiredCapabilities(browser, version, new Platform(PlatformType.Windows));
      var driver = new RemoteWebDriver(new Uri(seleniumGridUrl), capabilities, TimeSpan.FromSeconds(30));
      return driver;
    }

    private string BuildSeleniumGridUrl()
    {
      var host = Environment.GetEnvironmentVariable("SeleniumGridUrl") ?? "localhost";
      return $"http://{host}:4444/wd/hub";
    }
  }
}


