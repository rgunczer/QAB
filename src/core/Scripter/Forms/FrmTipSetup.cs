using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Util;
using System.Drawing.Drawing2D;


namespace Scripter
{
    public partial class FrmTipSetup : Form
    {
        private Font m_TipFont = new Font("Arial", 14);
        public static FrmScripter parent;

        private Color m_ColorFont = Color.Black;
        private Color m_colorBorder = Color.Black;
        private Color m_colorLeft = Color.Yellow;
        private Color m_colorRight = Color.White;

        public FrmTipSetup()
        {
            InitializeComponent();
        }

        private void FrmTipSetup_Load(object sender, EventArgs e)
        {
            numBorderWidth.Value = 2;
            
            lstColors.Items.Add("TipSetup \"BorderWidth\", \"" + numBorderWidth.Value.ToString() + "\"");
            lstColors.Items.Add("TipSetup \"BorderColor\", \"0\", \"0\", \"0\"");
            lstColors.Items.Add("TipSetup \"LeftColor\", \"0\", \"128\", \"255\"");
            lstColors.Items.Add("TipSetup \"RightColor\", \"255\", \"255\", \"255\"");
            
            txtFontFamily.Text = "TipSetup \"FontFamily\", \"" + m_TipFont.Name + "\"" + Environment.NewLine;
            txtFontFamily.Text += "TipSetup \"FontSize\", \"" + m_TipFont.Size + "\"" + Environment.NewLine;
            txtFontFamily.Text += "TipSetup \"FontStyle\", \"" + m_TipFont.Style.ToString() + "\"" + Environment.NewLine;
            txtFontFamily.Text += "TipSetup \"FontColor\", \"" + m_ColorFont.R + "\", \"" + m_ColorFont.G + "\", \"" + m_ColorFont.B + "\"";

        }

        private void btnColor_Click(object sender, EventArgs e)
        {              
            int index = lstColors.SelectedIndex;

            if (index < 1)
            {
                UtilSys.MessageBox("Select an item from a list.");
                return;
            }

            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() != DialogResult.Cancel)
            {
                string colorCode = "\"" + colorDialog.Color.R + "\", \"" + colorDialog.Color.G + "\", \"" + colorDialog.Color.B + "\"";

                string tmp = (string)lstColors.Items[index];

                int pos = tmp.IndexOf(',') + 2;

                tmp = tmp.Substring(0, pos) + colorCode;

                lstColors.Items[index] = tmp;

                switch (index)
                {
                    case 1:
                        m_colorBorder = colorDialog.Color;
                    break;

                    case 2:
                        m_colorLeft = colorDialog.Color;
                    break;

                    case 3:
                        m_colorRight = colorDialog.Color;
                    break;
                }

                DrawTip(true);
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog dlgFont = new FontDialog();
            dlgFont.ShowColor = true;

            if (null != m_TipFont)
            {
                dlgFont.Font = m_TipFont;
                dlgFont.Color = m_ColorFont;
            }

            if (dlgFont.ShowDialog() != DialogResult.Cancel)
            {
                txtFontFamily.Text = "TipSetup \"FontFamily\", \"" + dlgFont.Font.Name + "\"" + Environment.NewLine;
                txtFontFamily.Text += "TipSetup \"FontSize\", \"" + dlgFont.Font.Size + "\"" + Environment.NewLine;
                txtFontFamily.Text += "TipSetup \"FontStyle\", \"" + dlgFont.Font.Style.ToString() + "\"" + Environment.NewLine;
                txtFontFamily.Text += "TipSetup \"FontColor\", \"" + dlgFont.Color.R + "\", \"" + dlgFont.Color.G + "\", \"" + dlgFont.Color.B + "\"";

                m_TipFont = dlgFont.Font;

                m_TipFont = dlgFont.Font;
                m_ColorFont = dlgFont.Color;

                DrawTip(true);
            }
        }

        private void numBorderWidth_ValueChanged(object sender, EventArgs e)
        {
            if (lstColors.Items.Count > 0)
                lstColors.Items[0] = "TipSetup \"BorderWidth\", \"" + numBorderWidth.Value.ToString() + "\"";

            DrawTip(true);
        }

        private void btnInsertIntoScript_Click(object sender, EventArgs e)
        {
            string tmp = string.Empty;
            tmp = txtFontFamily.Text + Environment.NewLine;

            foreach(string str in lstColors.Items)
            {
                tmp += str + Environment.NewLine;
            }
            
            Clipboard.Clear();
            Clipboard.SetText(tmp);

            parent.CurrentEditor.MyPaste();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            DrawTip(true);
        }

        void DrawTip(bool invalidate)
        {
            if (invalidate)
                this.Invalidate();

            Rectangle rect = new Rectangle(10, ClientRectangle.Height - 115, ClientRectangle.Width - 20, 70);

            using (Graphics g = this.CreateGraphics())
            {
                using (LinearGradientBrush brGradient = new LinearGradientBrush(rect, m_colorLeft, m_colorRight, 0, false))
                {
                    g.FillRectangle(brGradient, rect);

                    if (numBorderWidth.Value != 0)
                    {
                        using (Pen pen = new Pen(m_colorBorder, (float)numBorderWidth.Value))
                        {
                            g.DrawRectangle(pen, new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1));
                        }
                    }

                    using (SolidBrush brush = new SolidBrush(m_ColorFont))
                    {
                        StringFormat sf = new StringFormat();

                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center; 

                        g.DrawString("Sample Text", m_TipFont, brush, rect, sf);
                    }
                }
            }
        }

        private void FrmTipSetup_Paint(object sender, PaintEventArgs e)
        {
            DrawTip(false);
        }
 
    
    }
}