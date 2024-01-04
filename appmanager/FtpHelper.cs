using System.IO;
using System.Net.FtpClient;

namespace mantis_tests
{
  public class FtpHelper : HelperBase
  {

    private FtpClient client;
    private string name = "mantis";
    private string password = "mantis";

    public FtpHelper(ApplicationManager manager) : base(manager)
    {
      client = new FtpClient();
      client.Credentials = new System.Net.NetworkCredential(name, password);
      client.Host = "localhost";
      client.Port = 21;
      client.Connect();
    }

    public void BackupFile(string path)
    {
      var backupPath = $"{path}.bak";

      if (client.FileExists(backupPath)) return;
      client.Rename(path, backupPath);
    }

    public void RestoreBackupFile(string path)
    {
      var backupPath = $"{path}.bak";
      
      if (!client.FileExists(backupPath)) return;
      if (client.FileExists(path)) client.DeleteFile(path);
      
      client.Rename(backupPath, path);
      
    }

    public void Upload(string path, Stream localFile)
    {
      if (client.FileExists(path)) client.DeleteFile(path);

      using (Stream ftpStream = client.OpenWrite(path))
      {
        // 8 * 1024 => bugger size
        byte[] buffer = new byte[8 * 1024];

        var count = localFile.Read(buffer, 0, buffer.Length);  
        while (count > 0)  // we can read something into the buffer
        {
          ftpStream.Write(buffer, 0, count);
          count = localFile.Read(buffer, 0, buffer.Length);
        }
      } 
      
    }
  }
}