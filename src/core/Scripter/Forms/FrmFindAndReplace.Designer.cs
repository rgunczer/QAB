namespace Scripter
{
    partial class FrmFindAndReplace
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
            this.cmdFind = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTextToFind = new System.Windows.Forms.ComboBox();
            this.chkMatchCase = new System.Windows.Forms.CheckBox();
            this.chkWholeWord = new System.Windows.Forms.CheckBox();
            this.cmdReplace = new System.Windows.Forms.Button();
            this.cmdReplaceAll = new System.Windows.Forms.Button();
            this.cboReplaceWith = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdExit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdFind
            // 
            this.cmdFind.Location = new System.Drawing.Point(12, 174);
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new System.Drawing.Size(90, 26);
            this.cmdFind.TabIndex = 4;
            this.cmdFind.Text = "Find Next";
            this.cmdFind.UseVisualStyleBackColor = true;
            this.cmdFind.Click += new System.EventHandler(this.cmdFind_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Find what:";
            // 
            // cboTextToFind
            // 
            this.cboTextToFind.FormattingEnabled = true;
            this.cboTextToFind.Location = new System.Drawing.Point(12, 23);
            this.cboTextToFind.Name = "cboTextToFind";
            this.cboTextToFind.Size = new System.Drawing.Size(325, 21);
            this.cboTextToFind.TabIndex = 0;
            // 
            // chkMatchCase
            // 
            this.chkMatchCase.AutoSize = true;
            this.chkMatchCase.Location = new System.Drawing.Point(6, 25);
            this.chkMatchCase.Name = "chkMatchCase";
            this.chkMatchCase.Size = new System.Drawing.Size(82, 17);
            this.chkMatchCase.TabIndex = 2;
            this.chkMatchCase.Text = "Match case";
            this.chkMatchCase.UseVisualStyleBackColor = true;
            // 
            // chkWholeWord
            // 
            this.chkWholeWord.AutoSize = true;
            this.chkWholeWord.Location = new System.Drawing.Point(6, 48);
            this.chkWholeWord.Name = "chkWholeWord";
            this.chkWholeWord.Size = new System.Drawing.Size(113, 17);
            this.chkWholeWord.TabIndex = 3;
            this.chkWholeWord.Text = "Match whole word";
            this.chkWholeWord.UseVisualStyleBackColor = true;
            // 
            // cmdReplace
            // 
            this.cmdReplace.Location = new System.Drawing.Point(106, 174);
            this.cmdReplace.Name = "cmdReplace";
            this.cmdReplace.Size = new System.Drawing.Size(90, 26);
            this.cmdReplace.TabIndex = 5;
            this.cmdReplace.Text = "Replace";
            this.cmdReplace.UseVisualStyleBackColor = true;
            this.cmdReplace.Click += new System.EventHandler(this.cmdReplace_Click);
            // 
            // cmdReplaceAll
            // 
            this.cmdReplaceAll.Location = new System.Drawing.Point(106, 206);
            this.cmdReplaceAll.Name = "cmdReplaceAll";
            this.cmdReplaceAll.Size = new System.Drawing.Size(90, 26);
            this.cmdReplaceAll.TabIndex = 6;
            this.cmdReplaceAll.Text = "Replace All";
            this.cmdReplaceAll.UseVisualStyleBackColor = true;
            this.cmdReplaceAll.Click += new System.EventHandler(this.cmdReplaceAll_Click);
            // 
            // cboReplaceWith
            // 
            this.cboReplaceWith.FormattingEnabled = true;
            this.cboReplaceWith.Location = new System.Drawing.Point(12, 66);
            this.cboReplaceWith.Name = "cboReplaceWith";
            this.cboReplaceWith.Size = new System.Drawing.Size(325, 21);
            this.cboReplaceWith.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Replace with:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkWholeWord);
            this.groupBox1.Controls.Add(this.chkMatchCase);
            this.groupBox1.Location = new System.Drawing.Point(12, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 75);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Find Options";
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Location = new System.Drawing.Point(247, 206);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(90, 26);
            this.cmdExit.TabIndex = 7;
            this.cmdExit.Text = "Close";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // FrmFindAndReplace
            // 
            this.AcceptButton = this.cmdFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(349, 240);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboReplaceWith);
            this.Controls.Add(this.cmdReplaceAll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdFind);
            this.Controls.Add(this.cmdReplace);
            this.Controls.Add(this.cboTextToFind);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmFindAndReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Find And Replace";
            this.Load += new System.EventHandler(this.FrmFindAndReplace_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTextToFind;
        private System.Windows.Forms.CheckBox chkMatchCase;
        private System.Windows.Forms.CheckBox chkWholeWord;
        private System.Windows.Forms.Button cmdReplace;
        private System.Windows.Forms.Button cmdReplaceAll;
        private System.Windows.Forms.ComboBox cboReplaceWith;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdExit;
    }
}