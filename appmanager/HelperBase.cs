using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
  public class HelperBase
  {
    protected IWebDriver driver;
    protected ApplicationManager manager;

    protected HelperBase(ApplicationManager manager)
    {
      this.manager = manager;
      driver = manager.Driver;
    }

    public void Type(By locator, string text)
    {
      if (text == null) return;

      driver.FindElement(locator).Click();
      driver.FindElement(locator).Clear();
      driver.FindElement(locator).SendKeys(text);
    }

    public bool IsElementPresent(By by)
    {
      try
      {
        driver.FindElement(by);
        return true;
      }
      catch (NoSuchElementException)
      {
        return false;
      }
    }

    public void WaitTillElementPresence(int secondsToWait, string selectorType, string selector)
    {
      switch (selectorType)
      {
        case "css":
          new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait))
              .Until(d => d.FindElements(By.CssSelector(selector)).Count > 0);
          break;
        case "xpath":
          new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait))
              .Until(d => d.FindElements(By.XPath(selector)).Count > 0);
          break;
        default: 
          System.Console.Write($"Not supported selector type {selectorType}. Should be css/xpath.");
          break;
      }
    }
  }
}