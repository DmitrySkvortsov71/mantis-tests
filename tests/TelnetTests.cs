using NUnit.Framework;

namespace mantis_tests
{
  [TestFixture]
  public class TelnetTests : TestBase
  {
    [Test]
    public void TelnetTest()
    {
      var account = new AccountData()
      {
          Name = $"testuser_{GenerateRandomNumber(111, 1111)}",
          Password = $"{GenerateRandomNumber(111, 1111)}"
      };
      
      Assert.IsFalse(app.James.Verify(account));
      
      app.James.Add(account);
      Assert.IsTrue(app.James.Verify(account));
      
      app.James.Delete(account);
      Assert.IsFalse(app.James.Verify(account));
      
    }
    
  }
}