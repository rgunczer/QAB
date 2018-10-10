namespace Scripter
{
    partial class FrmSQLConnection
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
            this.cmdListDatabases = new System.Windows.Forms.Button();
            this.lblServer = new System.Windows.Forms.Label();
            this.lbDatabases = new System.Windows.Forms.ListBox();
            this.lblDatabases = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.chkIntegradedSecurity = new System.Windows.Forms.CheckBox();
            this.txtSQLServer = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdListDatabases
            // 
            this.cmdListDatabases.Location = new System.Drawing.Point(397, 9);
            this.cmdListDatabases.Name = "cmdListDatabases";
            this.cmdListDatabases.Size = new System.Drawing.Size(90, 21);
            this.cmdListDatabases.TabIndex = 2;
            this.cmdListDatabases.Text = "List Databases";
            this.cmdListDatabases.UseVisualStyleBackColor = true;
            this.cmdListDatabases.Click += new System.EventHandler(this.cmdListDatabases_Click);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(57, 13);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(41, 13);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server:";
            // 
            // lbDatabases
            // 
            this.lbDatabases.FormattingEnabled = true;
            this.lbDatabases.Location = new System.Drawing.Point(99, 37);
            this.lbDatabases.Name = "lbDatabases";
            this.lbDatabases.Size = new System.Drawing.Size(388, 134);
            this.lbDatabases.TabIndex = 4;
            this.lbDatabases.SelectedIndexChanged += new System.EventHandler(this.lbDatabases_SelectedIndexChanged);
            // 
            // lblDatabases
            // 
            this.lblDatabases.Location = new System.Drawing.Point(7, 37);
            this.lblDatabases.Name = "lblDatabases";
            this.lblDatabases.Size = new System.Drawing.Size(91, 13);
            this.lblDatabases.TabIndex = 3;
            this.lblDatabases.Text = "Databases:";
            this.lblDatabases.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Connection String:";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(99, 258);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(388, 35);
            this.txtConnectionString.TabIndex = 7;
            // 
            // chkIntegradedSecurity
            // 
            this.chkIntegradedSecurity.AutoSize = true;
            this.chkIntegradedSecurity.Location = new System.Drawing.Point(99, 175);
            this.chkIntegradedSecurity.Name = "chkIntegradedSecurity";
            this.chkIntegradedSecurity.Size = new System.Drawing.Size(115, 17);
            this.chkIntegradedSecurity.TabIndex = 5;
            this.chkIntegradedSecurity.Text = "Integrated Security";
            this.chkIntegradedSecurity.UseVisualStyleBackColor = true;
            this.chkIntegradedSecurity.CheckedChanged += new System.EventHandler(this.chkIntegradedSecurity_CheckedChanged);
            // 
            // txtSQLServer
            // 
            this.txtSQLServer.Location = new System.Drawing.Point(99, 9);
            this.txtSQLServer.Name = "txtSQLServer";
            this.txtSQLServer.Size = new System.Drawing.Size(292, 20);
            this.txtSQLServer.TabIndex = 1;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(35, 199);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(63, 13);
            this.lblUserName.TabIndex = 8;
            this.lblUserName.Text = "User Name:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(99, 196);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(213, 20);
            this.txtUserName.TabIndex = 9;
            this.txtUserName.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(42, 225);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(99, 222);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(213, 20);
            this.txtPassword.TabIndex = 11;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
            // 
            // FrmSQLConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 305);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.txtSQLServer);
            this.Controls.Add(this.chkIntegradedSecurity);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDatabases);
            this.Controls.Add(this.lbDatabases);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.cmdListDatabases);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmSQLConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SQL Connection";
            this.Load += new System.EventHandler(this.FrmSQLConnection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdListDatabases;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.ListBox lbDatabases;
        private System.Windows.Forms.Label lblDatabases;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.CheckBox chkIntegradedSecurity;
        private System.Windows.Forms.TextBox txtSQLServer;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
    }
}