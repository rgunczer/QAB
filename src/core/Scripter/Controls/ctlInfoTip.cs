using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Scripter
{
    public partial class ctlInfoTip : UserControl
    {
        private static Font font = new Font("Arial Black", 10, FontStyle.Italic);

        public string text = "";
        public string typeText = "";

        public Color back = Color.White;

        public ctlInfoTip()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        public void Display()
        {
            Invalidate();
            Show();
        }

        public void Dismiss()
        {
            Hide();            
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            SizeF sf = g.MeasureString(text, Parent.Font);

            Width = Convert.ToInt32(sf.Width) + 4;
            Height = Convert.ToInt32(sf.Height) + 26;
            
            Rectangle rect = ClientRectangle;

            g.FillRectangle(/*Brushes.LightGoldenrodYellow*/ new SolidBrush(back), rect);

            Rectangle rc = new Rectangle(0, Height-20, Width, 20);

            Brush brGradient = new LinearGradientBrush(rc, Color.Black, back, 0, false);
            g.FillRectangle(brGradient, rc);
           
            //HatchBrush brHatch = new HatchBrush(HatchStyle.OutlinedDiamond, lb.BackColor, Color.Transparent);
            //g.FillRectangle(brHatch, rect);
                      
            g.DrawString(text, Parent.Font, Brushes.Black, new PointF(2.0f, 3.0f));

            g.DrawRectangle(Pens.Gray, new Rectangle(rect.X, rect.Y, rect.Width-1, rect.Height-1));

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(typeText, font, Brushes.White, new PointF(2.0f, ClientRectangle.Height - 21));

            //brHatch.Dispose();
            brGradient.Dispose();
        }


    }
}
