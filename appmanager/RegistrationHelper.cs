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
      Proceed();
    }

    private void Proceed()
    {
      driver.FindElement(By.CssSelector("a[href='login_page.php']")).Click();
    }

    private void OpenRegistrationForm()
    {
      driver.FindElement(By.CssSelector("a[href='signup_page.php']")).Click();
    }

    private void OpenMainPage()
    {
      manager.Driver.Url = $"{manager.baseUrl}mantisbt-2.26.0/";
    }

    private void FillRegistrationForm(AccountData account)
    {
      driver.FindElement(By.Name("username")).SendKeys(account.Name);
      driver.FindElement(By.Name("email")).SendKeys(account.Email);
       
    }

    private void SubmitRegistration()
    {
      driver.FindElement(By.CssSelector("input[type='submit']")).Click();
      WaitTillElementPresence(
          30, "xpath",
          "//*[contains(text(), 'Account registration processed.')]"
          );
    }
  }
}