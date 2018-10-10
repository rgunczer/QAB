using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace Controls
{
    public partial class ctlHeader : UserControl
    {
        private string m_text = string.Empty;
        private LinearGradientBrush m_BrushBackground = null;

        public Color m_colorBackLeft = Color.LightSkyBlue;
        public Color m_colorBackRight = Color.Blue;
        private Color m_colorFrame = Color.Gray;

        private StringFormat m_stringFormat = new StringFormat();
        private Rectangle m_rcBrush = new Rectangle();
        private Pen m_penFrame = null;
        private Font m_font = null;
        private int m_ize = 0;


        public ctlHeader()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);

            m_penFrame = new Pen(m_colorFrame);
            m_font = new Font("Arial", 9);

            m_stringFormat.Alignment = StringAlignment.Near;
            m_stringFormat.LineAlignment = StringAlignment.Center;
        }

        public void SetGradient(Color left, Color right)
        {
            m_colorBackLeft = left;
            m_colorBackRight = right;

            CreateBrush();
        }

        private void CreateBrush()
        {
            if (null != m_BrushBackground)
                m_BrushBackground.Dispose();

            m_rcBrush.X = 0;
            m_rcBrush.Y = 0;
            m_rcBrush.Width = Width;
            m_rcBrush.Height = Height;

            m_BrushBackground = new LinearGradientBrush(m_rcBrush, m_colorBackRight, m_colorBackLeft, 90, false);
            m_BrushBackground.SetSigmaBellShape(0.2f, 1.0f);            
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CreateBrush();            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            //g.FillRectangle(m_BrushBackground, m_rcBrush);

            Point[] points = { new Point(0,0), 
                               new Point(m_rcBrush.Width-m_ize,0), 
                               new Point(m_rcBrush.Width,m_rcBrush.Height), 
                               new Point(0,m_rcBrush.Height) };

            g.FillPolygon(m_BrushBackground, points);

            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);

            Region = new Region(path);

            Rectangle rc = m_rcBrush;
            --rc.Width;
            --rc.Height;

            Point[] pointss = { new Point(0, 0), 
                                new Point(rc.Width - m_ize, 0), 
                                new Point(rc.Width, rc.Height), 
                                new Point(0, rc.Height) };            

            //g.DrawRectangle(m_penFrame, rc);
            g.DrawPolygon(m_penFrame, pointss);

            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            g.DrawString(Text, m_font, Brushes.Black, ClientRectangle, m_stringFormat);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

    }
}
