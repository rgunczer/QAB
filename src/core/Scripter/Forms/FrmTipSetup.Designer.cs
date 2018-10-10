namespace Scripter
{
    partial class FrmTipSetup
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
            if(disposing && (components != null))
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
            this.btnInsertIntoScript = new System.Windows.Forms.Button();
            this.lblBorderWidth = new System.Windows.Forms.Label();
            this.numBorderWidth = new System.Windows.Forms.NumericUpDown();
            this.lstColors = new System.Windows.Forms.ListBox();
            this.txtFontFamily = new System.Windows.Forms.TextBox();
            this.btnFont = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numBorderWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInsertIntoScript
            // 
            this.btnInsertIntoScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInsertIntoScript.Location = new System.Drawing.Point(132, 296);
            this.btnInsertIntoScript.Name = "btnInsertIntoScript";
            this.btnInsertIntoScript.Size = new System.Drawing.Size(110, 29);
            this.btnInsertIntoScript.TabIndex = 22;
            this.btnInsertIntoScript.Text = "Insert into Script";
            this.btnInsertIntoScript.UseVisualStyleBackColor = true;
            this.btnInsertIntoScript.Click += new System.EventHandler(this.btnInsertIntoScript_Click);
            // 
            // lblBorderWidth
            // 
            this.lblBorderWidth.AutoSize = true;
            this.lblBorderWidth.Location = new System.Drawing.Point(6, 191);
            this.lblBorderWidth.Name = "lblBorderWidth";
            this.lblBorderWidth.Size = new System.Drawing.Size(72, 13);
            this.lblBorderWidth.TabIndex = 21;
            this.lblBorderWidth.Text = "Border Width:";
            // 
            // numBorderWidth
            // 
            this.numBorderWidth.Location = new System.Drawing.Point(79, 189);
            this.numBorderWidth.Name = "numBorderWidth";
            this.numBorderWidth.Size = new System.Drawing.Size(57, 20);
            this.numBorderWidth.TabIndex = 20;
            this.numBorderWidth.ValueChanged += new System.EventHandler(this.numBorderWidth_ValueChanged);
            // 
            // lstColors
            // 
            this.lstColors.FormattingEnabled = true;
            this.lstColors.IntegralHeight = false;
            this.lstColors.Location = new System.Drawing.Point(8, 92);
            this.lstColors.Name = "lstColors";
            this.lstColors.Size = new System.Drawing.Size(224, 93);
            this.lstColors.TabIndex = 19;
            // 
            // txtFontFamily
            // 
            this.txtFontFamily.Location = new System.Drawing.Point(8, 11);
            this.txtFontFamily.Multiline = true;
            this.txtFontFamily.Name = "txtFontFamily";
            this.txtFontFamily.Size = new System.Drawing.Size(224, 74);
            this.txtFontFamily.TabIndex = 18;
            // 
            // btnFont
            // 
            this.btnFont.Location = new System.Drawing.Point(238, 11);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(57, 50);
            this.btnFont.TabIndex = 17;
            this.btnFont.Text = "Select Font";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(238, 92);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(57, 50);
            this.btnColor.TabIndex = 15;
            this.btnColor.Text = "Select Color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRefresh.Location = new System.Drawing.Point(60, 296);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(65, 29);
            this.cmdRefresh.TabIndex = 23;
            this.cmdRefresh.Text = "Redraw";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // FrmTipSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 333);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.btnInsertIntoScript);
            this.Controls.Add(this.lblBorderWidth);
            this.Controls.Add(this.numBorderWidth);
            this.Controls.Add(this.lstColors);
            this.Controls.Add(this.txtFontFamily);
            this.Controls.Add(this.btnFont);
            this.Controls.Add(this.btnColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmTipSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Tip Setup";
            this.Load += new System.EventHandler(this.FrmTipSetup_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmTipSetup_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.numBorderWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInsertIntoScript;
        private System.Windows.Forms.Label lblBorderWidth;
        private System.Windows.Forms.NumericUpDown numBorderWidth;
        private System.Windows.Forms.ListBox lstColors;
        private System.Windows.Forms.TextBox txtFontFamily;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button cmdRefresh;
    }
}