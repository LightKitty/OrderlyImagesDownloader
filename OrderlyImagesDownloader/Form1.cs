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
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            buttonDownload.Enabled = false;
            try
            {
                FolderBrowserVistaDialog fbd = new FolderBrowserVistaDialog();
                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    outDirectory = fbd.DirectoryPath + "\\";
                    int startID = (int)numericUpDownStartID.Value;
                    int endId = (int)numericUpDownEndID.Value;
                    count = endId - startID + 1;
                    progressBarDownload.Maximum = count;
                    progress = 0;
                    Progress("Start download", 0);
                    int threadCount = (int)numericUpDownThreadCount.Value;
                    string urlTemplate = textBoxUrlTemplate.Text.Trim();
                    Download(urlTemplate, startID, endId, threadCount);
                }
            }
            catch(Exception ex)
            {
                WirteMessage(ex.Message);
            }
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
            string message = "Download complete";
            WirteMessage(message);
            MessageBox.Show(message);
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
                    Progress($"{fileName} downloaded");
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

        private void WirteMessage(string message)
        {
            textBoxMessage.AppendText(message + Environment.NewLine);
        }
    }
}
