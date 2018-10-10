using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Util;
using System.IO;
using System.Drawing.Imaging;


namespace Scripter
{
    public partial class FrmFindImage : Form
    {
        private VScrollBar vScrollBar1;
        private HScrollBar hScrollBar1;

        private static Color m_colorRect = Color.Black;
        private static Color m_colorBack = Color.Brown;
        private string m_root = Application.StartupPath;

        private bool m_bMove = false;

        private int offX = 0;
        private int offY = 0;

        private string m_path = string.Empty;

        Pen m_penSelection = new Pen(m_colorRect);


        public FrmFindImage()
        {
            InitializeComponent();

            this.vScrollBar1 = new VScrollBar();
            this.hScrollBar1 = new HScrollBar();

            this.pictureBox1.Controls.AddRange(new Control[] { this.vScrollBar1, this.hScrollBar1 });

            AlignScrollBars();

            this.vScrollBar1.Scroll += new ScrollEventHandler(this.HandleScroll);
            this.hScrollBar1.Scroll += new ScrollEventHandler(this.HandleScroll);
        }

        private void AlignScrollBars()
        {
            this.hScrollBar1.Top = (this.pictureBox1.Top + this.pictureBox1.Height) - this.hScrollBar1.Height - 2;
            this.hScrollBar1.Left = this.pictureBox1.Left;
            this.hScrollBar1.Width = this.pictureBox1.Width - this.vScrollBar1.Width - 2;

            this.vScrollBar1.Left = (this.pictureBox1.Left + this.pictureBox1.Width) - this.vScrollBar1.Width - 2;
            this.vScrollBar1.Height = this.pictureBox1.Height - this.hScrollBar1.Height - 2;

            pictureBox4.Width = vScrollBar1.Width;
            pictureBox4.Height = hScrollBar1.Height;

            pictureBox4.Left = hScrollBar1.Left + hScrollBar1.Width + 3;
            pictureBox4.Top = vScrollBar1.Top + vScrollBar1.Height + 1;
            pictureBox4.Width-=2; 
        }

        private void HandleScroll(Object sender, ScrollEventArgs se)
        {
            OnDrawImage();
            DrawSelectionRectangle();
        }

        private void OnDrawImage()
        {
            if (null == pictureBox1.Image)
                return;
            
            using (Graphics g = pictureBox1.CreateGraphics())
            {                
                g.DrawImage(pictureBox1.Image,
                            new Rectangle(0, 0, pictureBox1.Right - vScrollBar1.Width,
                            pictureBox1.Bottom - hScrollBar1.Height),
                            new Rectangle(hScrollBar1.Value, vScrollBar1.Value,
                            pictureBox1.Right - vScrollBar1.Width,
                            pictureBox1.Bottom - hScrollBar1.Height),
                            GraphicsUnit.Pixel);             

                GetRegion();
            }
            pictureBox1.Update();
        }

        private void cmdCapture_Click(object sender, EventArgs e)
        {
            FormWindowState old = this.WindowState;
            this.WindowState = FormWindowState.Minimized;

            UtilSys.Wait(1500);

            pictureBox1.Image = Support.GetScreenshot();
            this.SetScrollBarValues();
            this.WindowState = old;

            Application.DoEvents();

            GetRegion();
            DrawSelectionRectangle();
        }
        
        public void SetScrollBarValues()
        {
            if (null == pictureBox1.Image)
                return;
        
            this.vScrollBar1.Minimum = 0;
            this.hScrollBar1.Minimum = 0;
          
            if ((this.pictureBox1.Image.Size.Width - pictureBox1.ClientSize.Width) > 0)
            {
                this.hScrollBar1.Maximum =
                    this.pictureBox1.Image.Size.Width - pictureBox1.ClientSize.Width;
            }
    
            if (this.vScrollBar1.Visible)
            {
                this.hScrollBar1.Maximum += this.vScrollBar1.Width;
            }
            this.hScrollBar1.LargeChange = this.hScrollBar1.Maximum / 10;
            this.hScrollBar1.SmallChange = this.hScrollBar1.Maximum / 20;                      
            this.hScrollBar1.Maximum += this.hScrollBar1.LargeChange;
 
            if ((this.pictureBox1.Image.Size.Height - pictureBox1.ClientSize.Height) > 0)
            {
                this.vScrollBar1.Maximum =
                    this.pictureBox1.Image.Size.Height - pictureBox1.ClientSize.Height;
            }
        
            if (this.hScrollBar1.Visible)
            {
                this.vScrollBar1.Maximum += this.hScrollBar1.Height;
            }
            this.vScrollBar1.LargeChange = this.vScrollBar1.Maximum / 10;
            this.vScrollBar1.SmallChange = this.vScrollBar1.Maximum / 20;                      
            this.vScrollBar1.Maximum += this.vScrollBar1.LargeChange;


            this.hScrollBar1.Width = this.pictureBox1.Width - this.vScrollBar1.Width;
        }

        private void FrmFindImage_Resize(object sender, EventArgs e)
        {
            this.SetScrollBarValues();
            DrawSelectionRectangle();
        }

        private void FrmFindImage_Load(object sender, EventArgs e)
        {
            numericOffsetX.Value = 0;
            numericOffsetY.Value = 0;
            numericWidth.Value = 30;
            numericHeight.Value = 30;
        }

        private void DrawSelectionRectangle()
        {            
            Graphics g = pictureBox1.CreateGraphics();            
            g.DrawRectangle(m_penSelection, new Rectangle((int)numericOffsetX.Value, (int)numericOffsetY.Value, (int)numericWidth.Value, (int)numericHeight.Value));            
            g.Dispose();            
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (null == pictureBox2.Image)
                return;           

            SaveFileDialog sfn = new SaveFileDialog();
            sfn.Filter = "Bitmap Files (*.bmp)|*.bmp";
            sfn.Title = "Save Screen Play File";
            sfn.InitialDirectory = m_root;

            if (DialogResult.Cancel == sfn.ShowDialog())
                return;

            string path = sfn.FileName;

            int temp = (int)numericZoom.Value;
            numericZoom.Value = 1;

            pictureBox2.Image.Save(path, ImageFormat.Bmp);

            m_root = Path.GetDirectoryName(path);
            m_path = path;

            numericZoom.Value = temp;
        }
        
        private void GetRegion()
        {
            Graphics g = pictureBox1.CreateGraphics();

            Rectangle rc = new Rectangle((int)numericOffsetX.Value, (int)numericOffsetY.Value, (int)numericWidth.Value, (int)numericHeight.Value);

            Bitmap bmp = new Bitmap((int)numericWidth.Value, (int)numericHeight.Value);

            Graphics gBmp = Graphics.FromImage(bmp);

            Rectangle rcSrc = new Rectangle(hScrollBar1.Value + (int)numericOffsetX.Value,
                                            vScrollBar1.Value + (int)numericOffsetY.Value,
                                            (int)numericWidth.Value,
                                            (int)numericHeight.Value);

            gBmp.DrawImage(pictureBox1.Image, new Rectangle(0, 0, (int)numericWidth.Value, (int)numericHeight.Value), rcSrc, GraphicsUnit.Pixel);


            int zoom = (int)numericZoom.Value;

            Bitmap bmpZoomed = new Bitmap(bmp.Width * zoom, bmp.Height * zoom);

            Graphics gZoomed = Graphics.FromImage(bmpZoomed);
            gZoomed.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            gZoomed.DrawImage(bmp, new Rectangle(0, 0, bmpZoomed.Width, bmpZoomed.Height), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);

            gZoomed.Dispose();

            pictureBox2.Image = bmpZoomed;
            pictureBox2.Height = zoom * ( (int)numericHeight.Value -1);
            pictureBox2.Width = zoom * ( (int)numericWidth.Value - 1 );
            pictureBox2.Update();

            pictureBox3.Width = pictureBox2.Width + 10;
            pictureBox3.Height = pictureBox2.Height + 10;

            gBmp.Dispose();
        }

        private void numericOffsetX_ValueChanged(object sender, EventArgs e)
        {
            OnDrawImage();            
            DrawSelectionRectangle();
        }

        private void numericOffsetY_ValueChanged(object sender, EventArgs e)
        {
            OnDrawImage();            
            DrawSelectionRectangle();
        }

        private void numericWidth_ValueChanged(object sender, EventArgs e)
        {
            OnDrawImage();            
            DrawSelectionRectangle();
        }

        private void numericHeight_ValueChanged(object sender, EventArgs e)
        {
            OnDrawImage();            
            DrawSelectionRectangle();
        }

        private void cmdRectColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog(); 

            if (DialogResult.Cancel != colorDialog.ShowDialog())
            { 
                m_colorRect = colorDialog.Color;
                m_penSelection = new Pen(m_colorRect);
                DrawSelectionRectangle();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle rc = new Rectangle((int)numericOffsetX.Value, (int)numericOffsetY.Value, (int)numericWidth.Value, (int)numericHeight.Value);

            if ((e.X > rc.Left && e.X < rc.Left + rc.Width) && (e.Y > rc.Top && e.Y < rc.Top + rc.Height))
            {                
                m_bMove = true;
                offX = e.X - rc.Left;
                offY = e.Y - rc.Top;
            }
            else
                m_bMove = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!m_bMove)
                return;

            if (null == pictureBox1.Image)
                return;

            if (e.Button == MouseButtons.Left) // left button is down
            {
                int tmp = 0;
                    
                tmp = e.X - offX;

                if (tmp < 0)
                    tmp = 0;

                if (tmp > (int)numericOffsetX.Maximum)
                    tmp = (int)numericOffsetX.Maximum;

                numericOffsetX.Value = tmp;

                tmp = e.Y - offY;

                if (tmp < 0)
                    tmp = 0;

                if (tmp > (int)numericOffsetY.Maximum)
                    tmp = (int)numericOffsetY.Maximum;

                numericOffsetY.Value = tmp;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            m_bMove = false;
        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            --numericOffsetY.Value;
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            ++numericOffsetY.Value;
        }

        private void cmdRight_Click(object sender, EventArgs e)
        {
            ++numericOffsetX.Value;
        }

        private void cmdLeft_Click(object sender, EventArgs e)
        {
            --numericOffsetX.Value;
        }

        private void numericZoom_ValueChanged(object sender, EventArgs e)
        {
            GetRegion();
        }

        private void cmdBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (DialogResult.Cancel != colorDialog.ShowDialog())
            {
                m_colorBack = colorDialog.Color;
                pictureBox3.BackColor = m_colorBack;
            }
        }

        private void OnShowLastSavedImage()
        {
            if (string.IsNullOrEmpty(m_path))
                return;

            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "Saved Image: [" + m_path + "]";
            frm.FormBorderStyle = FormBorderStyle.SizableToolWindow;

            PictureBox pbox = new PictureBox();
            pbox.SizeMode = PictureBoxSizeMode.AutoSize;
            pbox.BorderStyle = BorderStyle.FixedSingle;
            pbox.Load(m_path);

            frm.Controls.Add(pbox);

            pbox.Dock = DockStyle.Fill;

            frm.Size = pbox.Size;

            frm.ShowDialog();

            frm.Dispose();
        }

        private void cmdShowLastSaved_Click(object sender, EventArgs e)
        {
            OnShowLastSavedImage();
        }

        private void FrmFindImage_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_penSelection.Dispose();
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            AlignScrollBars();
            OnDrawImage();
        }




    }
}