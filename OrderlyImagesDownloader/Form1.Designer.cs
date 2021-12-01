namespace OrderlyImagesDownloader
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxUrlTemplate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.numericUpDownStartID = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEndID = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownThreadCount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadCount)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxUrlTemplate
            // 
            this.textBoxUrlTemplate.Location = new System.Drawing.Point(85, 12);
            this.textBoxUrlTemplate.Name = "textBoxUrlTemplate";
            this.textBoxUrlTemplate.Size = new System.Drawing.Size(287, 23);
            this.textBoxUrlTemplate.TabIndex = 0;
            this.textBoxUrlTemplate.Text = "{0}";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "URL模板：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "起始ID：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "结束ID：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "线程数：";
            // 
            // buttonDownload
            // 
            this.buttonDownload.Location = new System.Drawing.Point(297, 70);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(75, 23);
            this.buttonDownload.TabIndex = 8;
            this.buttonDownload.Text = "下载";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.Location = new System.Drawing.Point(12, 99);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(360, 23);
            this.progressBarDownload.TabIndex = 9;
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(12, 128);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ReadOnly = true;
            this.textBoxMessage.Size = new System.Drawing.Size(360, 121);
            this.textBoxMessage.TabIndex = 10;
            // 
            // numericUpDownStartID
            // 
            this.numericUpDownStartID.Location = new System.Drawing.Point(85, 42);
            this.numericUpDownStartID.Name = "numericUpDownStartID";
            this.numericUpDownStartID.Size = new System.Drawing.Size(79, 23);
            this.numericUpDownStartID.TabIndex = 11;
            // 
            // numericUpDownEndID
            // 
            this.numericUpDownEndID.Location = new System.Drawing.Point(293, 41);
            this.numericUpDownEndID.Name = "numericUpDownEndID";
            this.numericUpDownEndID.Size = new System.Drawing.Size(79, 23);
            this.numericUpDownEndID.TabIndex = 12;
            // 
            // numericUpDownThreadCount
            // 
            this.numericUpDownThreadCount.Location = new System.Drawing.Point(85, 70);
            this.numericUpDownThreadCount.Name = "numericUpDownThreadCount";
            this.numericUpDownThreadCount.Size = new System.Drawing.Size(79, 23);
            this.numericUpDownThreadCount.TabIndex = 13;
            this.numericUpDownThreadCount.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.numericUpDownThreadCount);
            this.Controls.Add(this.numericUpDownEndID);
            this.Controls.Add(this.numericUpDownStartID);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.progressBarDownload);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxUrlTemplate);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "有序图片下载";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUrlTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.ProgressBar progressBarDownload;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.NumericUpDown numericUpDownStartID;
        private System.Windows.Forms.NumericUpDown numericUpDownEndID;
        private System.Windows.Forms.NumericUpDown numericUpDownThreadCount;
    }
}

