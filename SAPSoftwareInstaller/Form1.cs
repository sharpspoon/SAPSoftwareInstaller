using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Management.Automation;
using System.Diagnostics;

namespace SAPSoftwareInstaller
{
    public partial class SAPSoftwareInstallerForm : Form
    {
        public SAPSoftwareInstallerForm()
        {
            InitializeComponent();
        }

        string user = Environment.UserName;
        string userDir = @"C:\Users\" + Environment.UserName + @"\SAPSITemp";
        string dockerfile = "DockerDesktopInstaller.exe";
        string ProcessDir = Environment.CurrentDirectory;

        private void Install_ICM_Button_Click(object sender, EventArgs e)
        {
            LogRichTextBox.Clear();
            try
            {
                System.IO.Directory.CreateDirectory(userDir);
                LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + ">>>   Creating temp directory: " + userDir + " ...Done.");
            }
            catch
            {
                LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + ">>>   Creating temp directory: " + userDir + " ...Failed.");
                return;
            }
            
            try
            {
                LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + @">>>   Creating Directory: \SAPSoftwareInstaller\ICM");
                System.IO.Directory.CreateDirectory(ProcessDir + @"\SAPSoftwareInstaller\ICM");
            }
            catch
            {
                LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + @">>>   FAILURE - Creating Directory: \SAPSoftwareInstaller\ICM");
                return;
            }

            //LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + @">>>   Downloading Docker");
            //using (WebClient wc = new WebClient())
            //{
            //    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            //    wc.DownloadFileAsync(
            //        new System.Uri("https://wardr.net/sapsdi/docker/DockerDesktopInstaller.exe"),
            //        // Param2 = Path to save
            //        userDir + @"\" + dockerfile
            //    );
            //    wc.DownloadFileCompleted += DownloadCompleted;
            //}


            ExecuteAsAdmin(userDir + @"\" + dockerfile);//comment this out in prod
        }

        public void ExecuteAsAdmin(string fileName)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
            proc.WaitForExit();
            StartDockerAsAdmin(@"C:\Program Files\Docker\Docker\Docker Desktop.exe");
        }

        public void StartDockerAsAdmin(string fileName)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
            ProcessFolder();
        }

        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + ">>>   Downloading Docker....Done.");

            MessageBox.Show("The Docker installer will now launch. Please continue with default settings");
            ExecuteAsAdmin(userDir + @"\" + dockerfile);

            //string DOCKERINSTALLPath = userDir + @"\DOCKERINSTALL.cmd";
            //using (FileStream fs = new FileStream(DOCKERINSTALLPath, FileMode.OpenOrCreate))
            //{
            //    using (TextWriter tw = new StreamWriter(fs))
            //    {
            //        tw.WriteLine(@"DockerDesktopInstaller.exe");
            //    }
            //    System.Diagnostics.Process.Start(DOCKERINSTALLPath);
            //}
            //ProcessFolder(user, userDir, dockerfile);
        }
        private void ProcessFolder()
        {
            string RUNPath = ProcessDir + @"\SAPSoftwareInstaller\ICM\RUN.cmd";
            string ICMPath = ProcessDir + @"\SAPSoftwareInstaller\ICM\ICM.cmd";
            string SQLPath = ProcessDir + @"\SAPSoftwareInstaller\ICM\SQL.cmd";
            using (FileStream fs = new FileStream(ICMPath, FileMode.OpenOrCreate))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    tw.WriteLine("docker kill icmcontainer");
                    tw.WriteLine("docker container rm icmcontainer");
                    tw.WriteLine(@"docker run -p 80:8080 --net=icm-sql --add-host sqlcontainer:172.18.0.2 --name=icmcontainer icm:latest");
                }
            }
            using (FileStream fs = new FileStream(SQLPath, FileMode.OpenOrCreate))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    tw.WriteLine("docker kill sqlcontainer");
                    tw.WriteLine("docker container rm sqlcontainer");
                    tw.WriteLine(@"docker run -p 1433 --net=icm-sql --name sqlcontainer -v C:/temp/dbdump -d sql:latest");
                }
            }
            using (FileStream fs = new FileStream(RUNPath, FileMode.OpenOrCreate))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    tw.WriteLine(@"docker network create --driver=bridge icm-sql");
                    tw.WriteLine("start %~dp0SQL.cmd");
                    tw.WriteLine("start %~dp0ICM.cmd");
                }
                System.Diagnostics.Process.Start(RUNPath);
            }

            //LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + @">>>  inside of process file");
            //if (Directory.GetFiles(userDir, dockerfile).Length > 0)
            //{
            //    string[] files = Directory.GetFiles(userDir, dockerfile);

            //    foreach (var file in files)
            //    {
            //        LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + @">>>  inside of loop");
            //        var fileName = System.IO.Path.GetFileName(file);
            //        var fileNameWithPath = userDir + "\\" + fileName;
            //        LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + @">>>  inside of loop before switch");
            //        var argumentList = @"-quiet";
            //        LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + @">>>  inside of loop after switch path=" + fileNameWithPath);
            //        //Deploy application  
            //        DeployApplications(fileNameWithPath, fileName, argumentList);
            //        LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + @">>>  inside of loop before deploy call file name and path="+fileNameWithPath);
            //    }
            //    LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + ">>>   Installing Docker....Done.");
            //}
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }

        /////////////////
        //DEPLOYAPPS/////
        /////////////////
        public static void DeployApplications(string executableFilePath, string fileName, string argumentList)
        {
            PowerShell powerShell = null;
            Console.WriteLine("Deploying application..." + executableFilePath);
            try
            {
                using (powerShell = PowerShell.Create())
                {
                    powerShell.AddScript(@"$setup=Start-Process " + executableFilePath + @" -ArgumentList " + argumentList + " -Wait -PassThru");


                    Collection<PSObject> PSOutput = powerShell.Invoke(); foreach (PSObject outputItem in PSOutput)
                    {
                        if (outputItem != null)
                        {
                            Console.WriteLine(outputItem.BaseObject.GetType().FullName);
                            Console.WriteLine(outputItem.BaseObject.ToString() + "\n");
                        }
                    }

                    if (powerShell.Streams.Error.Count > 0)
                    {
                        string temp = powerShell.Streams.Error.First().ToString();
                        Console.WriteLine("Error: {0}", temp);
                    }
                    else
                        Console.WriteLine("Installation has completed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured: {0}", ex.InnerException);
                //throw;  
            }
            finally
            {
                if (powerShell != null)
                    powerShell.Dispose();
            }
        }
    }
}