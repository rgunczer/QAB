namespace Scripter
{
    partial class FrmHotList
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
            this.lbHotListItems = new System.Windows.Forms.ListBox();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdUp = new System.Windows.Forms.Button();
            this.cmdDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbHotListItems
            // 
            this.lbHotListItems.FormattingEnabled = true;
            this.lbHotListItems.Location = new System.Drawing.Point(12, 12);
            this.lbHotListItems.Name = "lbHotListItems";
            this.lbHotListItems.Size = new System.Drawing.Size(428, 251);
            this.lbHotListItems.TabIndex = 0;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(446, 124);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(111, 32);
            this.cmdDelete.TabIndex = 1;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdUp
            // 
            this.cmdUp.Location = new System.Drawing.Point(446, 12);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Size = new System.Drawing.Size(111, 32);
            this.cmdUp.TabIndex = 2;
            this.cmdUp.Text = "Move Up";
            this.cmdUp.UseVisualStyleBackColor = true;
            this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
            // 
            // cmdDown
            // 
            this.cmdDown.Location = new System.Drawing.Point(446, 50);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Size = new System.Drawing.Size(111, 32);
            this.cmdDown.TabIndex = 3;
            this.cmdDown.Text = "Move Down";
            this.cmdDown.UseVisualStyleBackColor = true;
            this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
            // 
            // FrmHotList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 272);
            this.Controls.Add(this.cmdDown);
            this.Controls.Add(this.cmdUp);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.lbHotListItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmHotList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Edit Hot List Items";
            this.Load += new System.EventHandler(this.FrmHotList_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmHotList_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbHotListItems;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdUp;
        private System.Windows.Forms.Button cmdDown;
    }
}