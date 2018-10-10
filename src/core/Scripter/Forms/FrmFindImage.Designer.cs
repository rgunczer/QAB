namespace Scripter
{
    partial class FrmFindImage
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
            this.cmdCapture = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.numericHeight = new System.Windows.Forms.NumericUpDown();
            this.numericWidth = new System.Windows.Forms.NumericUpDown();
            this.lblWidht = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblOffsetY = new System.Windows.Forms.Label();
            this.lblOffsetX = new System.Windows.Forms.Label();
            this.numericOffsetX = new System.Windows.Forms.NumericUpDown();
            this.numericOffsetY = new System.Windows.Forms.NumericUpDown();
            this.cmdRectColor = new System.Windows.Forms.Button();
            this.cmdUp = new System.Windows.Forms.Button();
            this.cmdRight = new System.Windows.Forms.Button();
            this.cmdLeft = new System.Windows.Forms.Button();
            this.cmdDown = new System.Windows.Forms.Button();
            this.lblZoom = new System.Windows.Forms.Label();
            this.numericZoom = new System.Windows.Forms.NumericUpDown();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.cmdShowLastSaved = new System.Windows.Forms.Button();
            this.cmdBackColor = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOffsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericZoom)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCapture
            // 
            this.cmdCapture.Location = new System.Drawing.Point(3, 3);
            this.cmdCapture.Name = "cmdCapture";
            this.cmdCapture.Size = new System.Drawing.Size(116, 59);
            this.cmdCapture.TabIndex = 1;
            this.cmdCapture.Text = "Capture Screen";
            this.cmdCapture.UseVisualStyleBackColor = true;
            this.cmdCapture.Click += new System.EventHandler(this.cmdCapture_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(579, 234);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.Resize += new System.EventHandler(this.pictureBox1_Resize);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(130, 68);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(150, 28);
            this.cmdSave.TabIndex = 8;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox2.Location = new System.Drawing.Point(426, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(76, 63);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // numericHeight
            // 
            this.numericHeight.Location = new System.Drawing.Point(337, 81);
            this.numericHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericHeight.Name = "numericHeight";
            this.numericHeight.Size = new System.Drawing.Size(75, 20);
            this.numericHeight.TabIndex = 13;
            this.numericHeight.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericHeight.ValueChanged += new System.EventHandler(this.numericHeight_ValueChanged);
            // 
            // numericWidth
            // 
            this.numericWidth.Location = new System.Drawing.Point(337, 54);
            this.numericWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericWidth.Name = "numericWidth";
            this.numericWidth.Size = new System.Drawing.Size(75, 20);
            this.numericWidth.TabIndex = 12;
            this.numericWidth.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericWidth.ValueChanged += new System.EventHandler(this.numericWidth_ValueChanged);
            // 
            // lblWidht
            // 
            this.lblWidht.Location = new System.Drawing.Point(297, 58);
            this.lblWidht.Name = "lblWidht";
            this.lblWidht.Size = new System.Drawing.Size(38, 13);
            this.lblWidht.TabIndex = 8;
            this.lblWidht.Text = "Width:";
            this.lblWidht.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHeight
            // 
            this.lblHeight.Location = new System.Drawing.Point(294, 85);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(41, 13);
            this.lblHeight.TabIndex = 9;
            this.lblHeight.Text = "Height:";
            this.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOffsetY
            // 
            this.lblOffsetY.Location = new System.Drawing.Point(287, 32);
            this.lblOffsetY.Name = "lblOffsetY";
            this.lblOffsetY.Size = new System.Drawing.Size(48, 13);
            this.lblOffsetY.TabIndex = 13;
            this.lblOffsetY.Text = "Offset Y:";
            this.lblOffsetY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOffsetX
            // 
            this.lblOffsetX.Location = new System.Drawing.Point(287, 4);
            this.lblOffsetX.Name = "lblOffsetX";
            this.lblOffsetX.Size = new System.Drawing.Size(48, 13);
            this.lblOffsetX.TabIndex = 12;
            this.lblOffsetX.Text = "Offset X:";
            this.lblOffsetX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericOffsetX
            // 
            this.numericOffsetX.Location = new System.Drawing.Point(337, 2);
            this.numericOffsetX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericOffsetX.Name = "numericOffsetX";
            this.numericOffsetX.Size = new System.Drawing.Size(75, 20);
            this.numericOffsetX.TabIndex = 10;
            this.numericOffsetX.ValueChanged += new System.EventHandler(this.numericOffsetX_ValueChanged);
            // 
            // numericOffsetY
            // 
            this.numericOffsetY.Location = new System.Drawing.Point(337, 28);
            this.numericOffsetY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericOffsetY.Name = "numericOffsetY";
            this.numericOffsetY.Size = new System.Drawing.Size(75, 20);
            this.numericOffsetY.TabIndex = 11;
            this.numericOffsetY.ValueChanged += new System.EventHandler(this.numericOffsetY_ValueChanged);
            // 
            // cmdRectColor
            // 
            this.cmdRectColor.Location = new System.Drawing.Point(3, 68);
            this.cmdRectColor.Name = "cmdRectColor";
            this.cmdRectColor.Size = new System.Drawing.Size(116, 28);
            this.cmdRectColor.TabIndex = 2;
            this.cmdRectColor.Text = "Rectangle Color";
            this.cmdRectColor.UseVisualStyleBackColor = true;
            this.cmdRectColor.Click += new System.EventHandler(this.cmdRectColor_Click);
            // 
            // cmdUp
            // 
            this.cmdUp.Location = new System.Drawing.Point(181, 3);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Size = new System.Drawing.Size(48, 28);
            this.cmdUp.TabIndex = 4;
            this.cmdUp.Text = "Up";
            this.cmdUp.UseVisualStyleBackColor = true;
            this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
            // 
            // cmdRight
            // 
            this.cmdRight.Location = new System.Drawing.Point(232, 34);
            this.cmdRight.Name = "cmdRight";
            this.cmdRight.Size = new System.Drawing.Size(48, 28);
            this.cmdRight.TabIndex = 7;
            this.cmdRight.Text = "Right";
            this.cmdRight.UseVisualStyleBackColor = true;
            this.cmdRight.Click += new System.EventHandler(this.cmdRight_Click);
            // 
            // cmdLeft
            // 
            this.cmdLeft.Location = new System.Drawing.Point(130, 34);
            this.cmdLeft.Name = "cmdLeft";
            this.cmdLeft.Size = new System.Drawing.Size(48, 28);
            this.cmdLeft.TabIndex = 5;
            this.cmdLeft.Text = "Left";
            this.cmdLeft.UseVisualStyleBackColor = true;
            this.cmdLeft.Click += new System.EventHandler(this.cmdLeft_Click);
            // 
            // cmdDown
            // 
            this.cmdDown.Location = new System.Drawing.Point(181, 34);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Size = new System.Drawing.Size(48, 28);
            this.cmdDown.TabIndex = 6;
            this.cmdDown.Text = "Down";
            this.cmdDown.UseVisualStyleBackColor = true;
            this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
            // 
            // lblZoom
            // 
            this.lblZoom.Location = new System.Drawing.Point(294, 112);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(41, 13);
            this.lblZoom.TabIndex = 20;
            this.lblZoom.Text = "Zoom:";
            this.lblZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericZoom
            // 
            this.numericZoom.Location = new System.Drawing.Point(337, 108);
            this.numericZoom.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericZoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericZoom.Name = "numericZoom";
            this.numericZoom.Size = new System.Drawing.Size(75, 20);
            this.numericZoom.TabIndex = 14;
            this.numericZoom.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericZoom.ValueChanged += new System.EventHandler(this.numericZoom_ValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(4, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox4);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblOffsetX);
            this.splitContainer1.Panel2.Controls.Add(this.cmdCapture);
            this.splitContainer1.Panel2.Controls.Add(this.cmdSave);
            this.splitContainer1.Panel2.Controls.Add(this.cmdShowLastSaved);
            this.splitContainer1.Panel2.Controls.Add(this.lblHeight);
            this.splitContainer1.Panel2.Controls.Add(this.numericOffsetY);
            this.splitContainer1.Panel2.Controls.Add(this.cmdBackColor);
            this.splitContainer1.Panel2.Controls.Add(this.cmdRight);
            this.splitContainer1.Panel2.Controls.Add(this.cmdLeft);
            this.splitContainer1.Panel2.Controls.Add(this.numericHeight);
            this.splitContainer1.Panel2.Controls.Add(this.lblWidht);
            this.splitContainer1.Panel2.Controls.Add(this.lblOffsetY);
            this.splitContainer1.Panel2.Controls.Add(this.cmdRectColor);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox2);
            this.splitContainer1.Panel2.Controls.Add(this.numericZoom);
            this.splitContainer1.Panel2.Controls.Add(this.cmdDown);
            this.splitContainer1.Panel2.Controls.Add(this.lblZoom);
            this.splitContainer1.Panel2.Controls.Add(this.cmdUp);
            this.splitContainer1.Panel2.Controls.Add(this.numericWidth);
            this.splitContainer1.Panel2.Controls.Add(this.numericOffsetX);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox3);
            this.splitContainer1.Size = new System.Drawing.Size(579, 372);
            this.splitContainer1.SplitterDistance = 234;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(547, 204);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(27, 25);
            this.pictureBox4.TabIndex = 2;
            this.pictureBox4.TabStop = false;
            // 
            // cmdShowLastSaved
            // 
            this.cmdShowLastSaved.Location = new System.Drawing.Point(130, 100);
            this.cmdShowLastSaved.Name = "cmdShowLastSaved";
            this.cmdShowLastSaved.Size = new System.Drawing.Size(150, 28);
            this.cmdShowLastSaved.TabIndex = 9;
            this.cmdShowLastSaved.Text = "Show Last Saved";
            this.cmdShowLastSaved.UseVisualStyleBackColor = true;
            this.cmdShowLastSaved.Click += new System.EventHandler(this.cmdShowLastSaved_Click);
            // 
            // cmdBackColor
            // 
            this.cmdBackColor.Location = new System.Drawing.Point(3, 100);
            this.cmdBackColor.Name = "cmdBackColor";
            this.cmdBackColor.Size = new System.Drawing.Size(116, 28);
            this.cmdBackColor.TabIndex = 3;
            this.cmdBackColor.Text = "Background Color";
            this.cmdBackColor.UseVisualStyleBackColor = true;
            this.cmdBackColor.Click += new System.EventHandler(this.cmdBackColor_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Salmon;
            this.pictureBox3.Location = new System.Drawing.Point(421, 1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(87, 74);
            this.pictureBox3.TabIndex = 21;
            this.pictureBox3.TabStop = false;
            // 
            // FrmFindImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 380);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmFindImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Find Image";
            this.Load += new System.EventHandler(this.FrmFindImage_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmFindImage_FormClosed);
            this.Resize += new System.EventHandler(this.FrmFindImage_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOffsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericZoom)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCapture;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.NumericUpDown numericHeight;
        private System.Windows.Forms.NumericUpDown numericWidth;
        private System.Windows.Forms.Label lblWidht;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblOffsetY;
        private System.Windows.Forms.Label lblOffsetX;
        private System.Windows.Forms.NumericUpDown numericOffsetX;
        private System.Windows.Forms.NumericUpDown numericOffsetY;
        private System.Windows.Forms.Button cmdRectColor;
        private System.Windows.Forms.Button cmdUp;
        private System.Windows.Forms.Button cmdRight;
        private System.Windows.Forms.Button cmdLeft;
        private System.Windows.Forms.Button cmdDown;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.NumericUpDown numericZoom;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button cmdBackColor;
        private System.Windows.Forms.Button cmdShowLastSaved;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}