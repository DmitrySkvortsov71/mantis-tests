using System;
using OpenQA.Selenium;

namespace mantis_tests
{
  public class RegistrationHelper : HelperBase
  {
    public RegistrationHelper(ApplicationManager manager) : base(manager)
    {
    }

    public void Register(AccountData account)
    {
      OpenMainPage();
      OpenRegistrationForm();
      FillRegistrationForm(account);
      SubmitRegistration();
    }

    private void OpenRegistrationForm()
    {
      driver.FindElement(By.CssSelector("a[href='signup_page.php']")).Click();
    }

    private void OpenMainPage()
    {
      manager.Driver.Url = $"{manager.baseUrl}mantis/";
    }

    private void FillRegistrationForm(AccountData account)
    {
      driver.FindElement(By.Name("username")).SendKeys(account.Name);
      driver.FindElement(By.Name("email")).SendKeys(account.Email);
       
    }

    private void SubmitRegistration()
    {
      driver.FindElement(By.CssSelector("input.button")).Click();
      WaitTillElementPresence(
          30, "xpath",
          "//*[contains(text(), 'Account registration processed.')]"
          );
    }
  }
}