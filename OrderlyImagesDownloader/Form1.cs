using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderlyImagesDownloader
{
    public partial class MainForm : Form
    {
        private string outDirectory;
        private int count;
        private int progress;

        public MainForm()
        {
            InitializeComponent();

            numericUpDownStartID.Maximum = int.MaxValue;
            numericUpDownEndID.Maximum = int.MaxValue;

            //https://www.cnblogs.com/farb/p/HttpRequestProblem.html
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            //progressBarDownload.Show();
            try
            {
                int startID = (int)numericUpDownStartID.Value;
                int endId = (int)numericUpDownEndID.Value;
                if (endId < startID)
                {
                    MessageBox.Show("结束ID不能小于起始ID！");
                }
                FolderBrowserVistaDialog fbd = new FolderBrowserVistaDialog();
                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    buttonDownload.Enabled = false;
                    string urlTemplate = textBoxUrlTemplate.Text.Trim();
                    string outFolder = Path.GetFileNameWithoutExtension(string.Format(urlTemplate, startID + "-" + endId));
                    outDirectory = fbd.DirectoryPath + "\\" + outFolder + "\\";
                    if (!Directory.Exists(outDirectory))
                    {
                        Directory.CreateDirectory(outDirectory);
                    }
                    count = endId - startID + 1;
                    ProgressReset(count);
                    WirteMessage("开始下载");
                    int threadCount = (int)numericUpDownThreadCount.Value;
                    Download(urlTemplate, startID, endId, threadCount);
                }
            }
            catch(Exception ex)
            {
                WirteMessage(ex.Message);
            }
        }

        private void ProgressReset(int maximum = 0)
        {
            progressBarDownload.Maximum = maximum;
            progress = 0;
        }

        private async void Download(string urlTemplate, int startID, int endId, int threadCount)
        {
            await Task.Run(() =>
            {
                Task[] tasks = new Task[count];
                TaskFactory taskFactory = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(threadCount));
                for (int i = 0; i < count; i++)
                {
                    int id = startID + i;
                    string url = string.Format(urlTemplate, id);
                    tasks[i] = taskFactory.StartNew((_url) =>
                    {
                        DownloadOne((string)_url);
                    }, url);
                }
                Task.WaitAll(tasks); //等待修复完成
            });
            buttonDownload.Enabled = true;
            string message = "全部下载完成";
            WirteMessage(message);
            MessageBox.Show(message);
            ProgressReset();
            //progressBarDownload.Hide();
        }

        private void DownloadOne(string url)
        {
            try
            {
                string fileName = Path.GetFileName(url);
                string extension = Path.GetExtension(url);
                string outPath = outDirectory + fileName;
                WebRequest imgRequest = WebRequest.Create(url);
                using (Image downImage = Image.FromStream(imgRequest.GetResponse().GetResponseStream()))
                {
                    downImage.Save(outPath, ImageHelper.GetImageFormat(extension));
                }
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    Progress($"{fileName} 下载成功");
                }));
            }
            catch(Exception ex)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    WirteMessage(ex.Message);
                }));
            }
        }

        private void Progress(string message, int count = 1)
        {
            if (count != 0)
            {
                progress += count;
                progressBarDownload.Value = progress;
            }
            if (!string.IsNullOrEmpty(message))
            {
                WirteMessage(message);
            }
        }

        private void WirteMessage(string message, bool newLine = true)
        {
            //message = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} | {message}";
            textBoxMessage.AppendText(newLine ? Environment.NewLine + message : message);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            WirteMessage("就绪", false);
        }
    }
}
