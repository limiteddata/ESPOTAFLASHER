namespace ESPOTAFLASHER
{
    partial class ESPOTAFLASHER
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.browseButton = new System.Windows.Forms.Button();
            this.infoText = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ipaddressText = new System.Windows.Forms.TextBox();
            this.firmwareLocationText = new System.Windows.Forms.TextBox();
            this.uploadButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(304, 43);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 0;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // infoText
            // 
            this.infoText.Location = new System.Drawing.Point(32, 166);
            this.infoText.Name = "infoText";
            this.infoText.Size = new System.Drawing.Size(347, 211);
            this.infoText.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(35, 129);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(344, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // ipaddressText
            // 
            this.ipaddressText.Location = new System.Drawing.Point(35, 92);
            this.ipaddressText.Name = "ipaddressText";
            this.ipaddressText.Size = new System.Drawing.Size(263, 20);
            this.ipaddressText.TabIndex = 3;
            // 
            // firmwareLocationText
            // 
            this.firmwareLocationText.Location = new System.Drawing.Point(35, 45);
            this.firmwareLocationText.Name = "firmwareLocationText";
            this.firmwareLocationText.Size = new System.Drawing.Size(263, 20);
            this.firmwareLocationText.TabIndex = 4;
            // 
            // uploadButton
            // 
            this.uploadButton.Location = new System.Drawing.Point(304, 89);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(75, 23);
            this.uploadButton.TabIndex = 5;
            this.uploadButton.Text = "Upload";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Firmware:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "IPaddress/Hostname:";
            // 
            // ESPOTAFLASHER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 404);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.firmwareLocationText);
            this.Controls.Add(this.ipaddressText);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.infoText);
            this.Controls.Add(this.browseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ESPOTAFLASHER";
            this.Text = "ESPOTAFLASHERv1.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label infoText;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox ipaddressText;
        private System.Windows.Forms.TextBox firmwareLocationText;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

