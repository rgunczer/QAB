namespace Controls
{
    partial class ctlScriptEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctlEditor = new Controls.Editor();
            this.SuspendLayout();
            // 
            // ctlEditor
            // 
            this.ctlEditor.AcceptsTab = true;
            this.ctlEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctlEditor.DetectUrls = false;
            this.ctlEditor.Dirty = true;
            this.ctlEditor.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlEditor.HideSelection = false;
            this.ctlEditor.Loading = false;
            this.ctlEditor.Location = new System.Drawing.Point(60, 0);
            this.ctlEditor.Name = "ctlEditor";
            this.ctlEditor.Size = new System.Drawing.Size(741, 448);
            this.ctlEditor.TabIndex = 0;
            this.ctlEditor.Text = "";
            this.ctlEditor.WordWrap = false;
            // 
            // ctlScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.ctlEditor);
            this.DoubleBuffered = true;
            this.Name = "ctlScriptEditor";
            this.Size = new System.Drawing.Size(801, 448);
            this.ResumeLayout(false);

        }

        #endregion

        public Editor ctlEditor;




    }
}
