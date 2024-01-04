using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace mantis_tests
{
  public class ApplicationManager
  {
    public readonly string baseUrl;
    private IWebDriver driver;

    private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

    private ApplicationManager()
    {
      driver = new FirefoxDriver();
      baseUrl = "http://localhost/";

      Registration = new RegistrationHelper(this);
      Ftp = new FtpHelper(this);
    }

    public RegistrationHelper Registration { get; set; }
    public FtpHelper Ftp { get; set; }

    ~ApplicationManager()
    {
      driver.Quit();
    }

    public static ApplicationManager GetInstance()
    {
      if (!app.IsValueCreated)
      {
        app.Value = new ApplicationManager();
        app.Value.driver.Url = $"{app.Value.baseUrl}mantisbt-2.26.0/";
      }

      return app.Value;
    }

    public IWebDriver Driver => driver;
  }
}