using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAPSoftwareInstaller
{
    public partial class SAPSoftwareInstallerForm : Form
    {
        public SAPSoftwareInstallerForm()
        {
            InitializeComponent();
        }

        private void Install_ICM_Button_Click(object sender, EventArgs e)
        {
            LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now+" ---Setting process dir");
            var ProcessDir = Environment.CurrentDirectory;
            try
            {
                LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + @" ---Creating Directory: \SAPSoftwareInstaller\ICM");
                System.IO.Directory.CreateDirectory(ProcessDir + @"\SAPSoftwareInstaller\ICM");
            }
            catch
            {
                LogRichTextBox.Text = LogRichTextBox.Text.Insert(0, Environment.NewLine + DateTime.Now + @" ---FAILURE - Creating Directory: \SAPSoftwareInstaller\ICM");
                return;
            }
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
        }
    }
}