namespace Scripter
{
    partial class FormServerFolderStructure
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
            this.tvw = new System.Windows.Forms.TreeView();
            this.cmdDownload = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.lblDownloadLocation = new System.Windows.Forms.Label();
            this.cmdBrowseFolder = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tvw
            // 
            this.tvw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvw.CheckBoxes = true;
            this.tvw.Location = new System.Drawing.Point(71, 34);
            this.tvw.Name = "tvw";
            this.tvw.Size = new System.Drawing.Size(391, 298);
            this.tvw.TabIndex = 0;
            this.tvw.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvw_AfterCheck);
            // 
            // cmdDownload
            // 
            this.cmdDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDownload.Location = new System.Drawing.Point(468, 62);
            this.cmdDownload.Name = "cmdDownload";
            this.cmdDownload.Size = new System.Drawing.Size(70, 44);
            this.cmdDownload.TabIndex = 1;
            this.cmdDownload.Text = "Download  Selected";
            this.cmdDownload.UseVisualStyleBackColor = true;
            this.cmdDownload.Click += new System.EventHandler(this.cmdDownload_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Location = new System.Drawing.Point(468, 34);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(70, 22);
            this.cmdRefresh.TabIndex = 2;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // lblDownloadLocation
            // 
            this.lblDownloadLocation.AutoSize = true;
            this.lblDownloadLocation.Location = new System.Drawing.Point(-1, 11);
            this.lblDownloadLocation.Name = "lblDownloadLocation";
            this.lblDownloadLocation.Size = new System.Drawing.Size(70, 13);
            this.lblDownloadLocation.TabIndex = 4;
            this.lblDownloadLocation.Text = "Download to:";
            // 
            // cmdBrowseFolder
            // 
            this.cmdBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseFolder.Location = new System.Drawing.Point(468, 6);
            this.cmdBrowseFolder.Name = "cmdBrowseFolder";
            this.cmdBrowseFolder.Size = new System.Drawing.Size(70, 22);
            this.cmdBrowseFolder.TabIndex = 5;
            this.cmdBrowseFolder.Text = "Browse";
            this.cmdBrowseFolder.UseVisualStyleBackColor = true;
            this.cmdBrowseFolder.Click += new System.EventHandler(this.cmdBrowseFolder_Click);
            // 
            // lblPath
            // 
            this.lblPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPath.Location = new System.Drawing.Point(71, 6);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(391, 22);
            this.lblPath.TabIndex = 7;
            this.lblPath.Text = "[not set]";
            this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormServerFolderStructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 335);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.cmdBrowseFolder);
            this.Controls.Add(this.lblDownloadLocation);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.cmdDownload);
            this.Controls.Add(this.tvw);
            this.Name = "FormServerFolderStructure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Browse and Download QAB-Server Files";
            this.Load += new System.EventHandler(this.FormServerFolderStructure_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TreeView tvw;
        private System.Windows.Forms.Button cmdDownload;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Label lblDownloadLocation;
        private System.Windows.Forms.Button cmdBrowseFolder;
        private System.Windows.Forms.Label lblPath;
    }
}