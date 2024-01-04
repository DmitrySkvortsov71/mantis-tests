using System;
using MinimalisticTelnet;

namespace mantis_tests
{
  public class JamesHelper : HelperBase
  {
    
    public JamesHelper(ApplicationManager manager) : base(manager) {}

    public void Add(AccountData account)
    {
      if (Verify(account)) return;
      
      var telnet = ConnectToJamesTelnet();
      
      // command to create account
      Console.Out.WriteLine(telnet.Read());
      telnet.WriteLine($"adduser {account.Name} {account.Password}");
      Console.Out.WriteLine(telnet.Read());
      
    }

    public void Delete(AccountData account)
    {
      if (! Verify(account)) return;
      
      var telnet = ConnectToJamesTelnet();

      // command to delete account
      Console.Out.WriteLine(telnet.Read());
      telnet.WriteLine($"deluser {account.Name}");
      Console.Out.WriteLine(telnet.Read());
    }
    
    public bool Verify(AccountData account)
    {
      var telnet = ConnectToJamesTelnet();
      
      // command to verify account
      Console.Out.WriteLine(telnet.Read());
      telnet.WriteLine($"verify {account.Name}");

      var result = telnet.Read();
      Console.Out.WriteLine(result);

      return !result.Contains("does not exist");
    }
    
    private static TelnetConnection ConnectToJamesTelnet()
    {
      // open telnet session
      var telnet = new TelnetConnection("localhost", 4555);
      
      // username
      System.Console.Out.WriteLine(telnet.Read());
      telnet.WriteLine("root");
      
      // password
      System.Console.Out.WriteLine(telnet.Read());
      telnet.WriteLine("root");
      
      return telnet;
    }
    
  }
}