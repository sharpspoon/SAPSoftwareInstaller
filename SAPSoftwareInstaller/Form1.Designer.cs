namespace SAPSoftwareInstaller
{
    partial class SAPSoftwareInstallerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SAPSoftwareInstallerForm));
            this.Install_ICM_Button = new System.Windows.Forms.Button();
            this.LogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.Install_Docker_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Install_ICM_Button
            // 
            this.Install_ICM_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Install_ICM_Button.Location = new System.Drawing.Point(12, 64);
            this.Install_ICM_Button.Name = "Install_ICM_Button";
            this.Install_ICM_Button.Size = new System.Drawing.Size(163, 46);
            this.Install_ICM_Button.TabIndex = 0;
            this.Install_ICM_Button.Text = "Install ICM";
            this.Install_ICM_Button.UseVisualStyleBackColor = true;
            this.Install_ICM_Button.Click += new System.EventHandler(this.Install_ICM_Button_Click);
            // 
            // LogRichTextBox
            // 
            this.LogRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogRichTextBox.Location = new System.Drawing.Point(181, 12);
            this.LogRichTextBox.Name = "LogRichTextBox";
            this.LogRichTextBox.ReadOnly = true;
            this.LogRichTextBox.Size = new System.Drawing.Size(968, 403);
            this.LogRichTextBox.TabIndex = 1;
            this.LogRichTextBox.Text = "";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.Location = new System.Drawing.Point(181, 421);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(968, 23);
            this.ProgressBar.TabIndex = 2;
            // 
            // Install_Docker_Button
            // 
            this.Install_Docker_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Install_Docker_Button.Location = new System.Drawing.Point(12, 12);
            this.Install_Docker_Button.Name = "Install_Docker_Button";
            this.Install_Docker_Button.Size = new System.Drawing.Size(163, 46);
            this.Install_Docker_Button.TabIndex = 3;
            this.Install_Docker_Button.Text = "Install Docker";
            this.Install_Docker_Button.UseVisualStyleBackColor = true;
            this.Install_Docker_Button.Click += new System.EventHandler(this.Install_Docker_Button_Click);
            // 
            // SAPSoftwareInstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 450);
            this.Controls.Add(this.Install_Docker_Button);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.LogRichTextBox);
            this.Controls.Add(this.Install_ICM_Button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SAPSoftwareInstallerForm";
            this.Text = "SAP Software Installer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Install_ICM_Button;
        private System.Windows.Forms.RichTextBox LogRichTextBox;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Button Install_Docker_Button;
    }
}

