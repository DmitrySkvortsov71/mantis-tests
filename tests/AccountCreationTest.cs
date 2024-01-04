using System.IO;
using NUnit.Framework;

namespace mantis_tests
{
  
  
  [TestFixture]
  public class AccountCreationTests : TestBase
  {
    private const string ConfigRemotePath = "config_defaults_inc.php";
    private const string ConfigLocalPath = "config_defaults_inc.php";

    [OneTimeSetUp]
    public void SetupConfig()
    {
      app.Ftp.BackupFile(ConfigRemotePath);
      using (Stream localFile = File.Open(ConfigLocalPath, FileMode.Open)) 
        app.Ftp.Upload(ConfigRemotePath, localFile);
    }

    [OneTimeTearDown]
    public void RestoreConfig()
    {
      app.Ftp.RestoreBackupFile(ConfigRemotePath);
    }
    
    
    [Test]
    public void TestAccountRegistration()
    {
      var rand = GenerateRandomNumber(111, 1111);
      
      var account = new AccountData()
      {
          
          Name = $"testuser_{rand}",
          Password = "password",
          Email = $"testuser_{rand}@localhost.localdomain"
      };

      app.Registration.Register(account);
    }
    
  }
  
}