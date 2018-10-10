using System;
using System.Collections.Generic;
using System.Text;
using System.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;


namespace Controls
{
    public partial class ctlScriptEditor : UserControl
    {
        public ctlScriptEditor()
        {            
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);

            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Center;                        

            Dock = DockStyle.Fill;

            InitializeComponent();
        }

        public TabPage page
        {
            set { ctlEditor.page = value; }
            get { return ctlEditor.page; }
        }

        public string Path
        {
            set { ctlEditor.m_path = value; }
            get { return ctlEditor.m_path; }
        }
        
        private Bitmap bmp = null;
        private Bitmap bmpBuffer = null;
                
        private StringFormat stringFormat = new StringFormat();
                        
        private int m_VertLinePosX = 56;

        private int m_BookmarkWidth = 15;

        private Graphics m_bmpGraphics = null;
        private Graphics m_bmpBufferGraphics = null;

        private Rectangle m_rc;
        private Rectangle m_rcNumber = new Rectangle();


        Color m_colorNumbers = Color.FromArgb(43, 145, 175);

        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);

                                           
        int GetFirstVisibleRow()
        {
            const int EM_GETFIRSTVISIBLELINE = 0xce;
            return SendMessage(ctlEditor.Handle, EM_GETFIRSTVISIBLELINE, 0, new IntPtr());            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);            

            SolidBrush brush = new SolidBrush(m_colorNumbers);

            m_bmpGraphics.DrawImage(bmpBuffer, 0, 0);
          
            int lineWithCaret = ctlEditor.GetLineFromCharIndex(ctlEditor.SelectionStart);

            Point p;
            Point pA = new Point();

            for (int curLine = GetFirstVisibleRow(); curLine < ctlEditor.Lines.Length; ++curLine)
            {               
                p = ctlEditor.GetPositionFromCharIndex(ctlEditor.GetFirstCharIndexFromLine(curLine));

                m_rcNumber.X = -1;
                m_rcNumber.Y = p.Y;
                m_rcNumber.Width = m_VertLinePosX;
                m_rcNumber.Height = ctlEditor.Font.Height;

                if (curLine == lineWithCaret)
                {
                    pA.X = m_BookmarkWidth;
                    pA.Y = p.Y;

                    Point[] points = 
                    {
                        pA,
                        new Point(pA.X+3, pA.Y + ctlEditor.Font.Height/2),
                        new Point(pA.X, pA.Y + ctlEditor.Font.Height),                            
                    };

                    m_bmpGraphics.DrawLines(Pens.DimGray, points);
                    m_bmpGraphics.DrawString(curLine.ToString(), ctlEditor.Font, Brushes.Black, m_rcNumber, stringFormat);
                }
                else
                {
                    m_bmpGraphics.DrawString(curLine.ToString(), ctlEditor.Font, brush, m_rcNumber, stringFormat);
                }

                if (ctlEditor.IsBookmarkInLine(curLine))
                {
                    m_bmpGraphics.FillEllipse(Brushes.Red, new Rectangle(0, p.Y + 1, (int)(ctlEditor.Font.Height * 0.95f), (int)(ctlEditor.Font.Height * 0.95f)));
                }

                p.Y += Font.Height;
                
                if (p.Y > m_rc.Bottom) 
                    break;
            }

            brush.Dispose();

            e.Graphics.DrawImage(bmp, 0, 0);                        
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //base.OnPaintBackground(pevent);
        }

        protected override void OnResize(EventArgs e)
        {           
            base.OnResize(e);

            if (null != ctlEditor)
            {                
                Rectangle rcEditor = ctlEditor.ClientRectangle;

                rcEditor.Height = ctlEditor.Size.Height;

                Rectangle rc = ClientRectangle;

                if (null != bmp)
                    bmp.Dispose();

                bmp = new Bitmap(rc.Width, rcEditor.Height);

                m_bmpGraphics = Graphics.FromImage(bmp);

                if (null != bmpBuffer)
                    bmpBuffer.Dispose();

                bmpBuffer = new Bitmap(rc.Width, rcEditor.Height);

                m_bmpBufferGraphics = Graphics.FromImage(bmpBuffer);

                rc.Width = m_VertLinePosX;
                             
                Rectangle rct = new Rectangle(rc.X + rc.Width, rc.Y, rc.Width + 3, rc.Height);

                using (SolidBrush brush = new SolidBrush(ctlEditor.BackColor))
                {
                    m_bmpBufferGraphics.FillRectangle(brush, rct);
                }

                m_bmpBufferGraphics.FillRectangle(Brushes.White, rc);

                using (Pen pen = new Pen(m_colorNumbers) { DashStyle = DashStyle.Dot })
                {
                    m_bmpBufferGraphics.DrawLine(pen, new Point(rc.Left + m_VertLinePosX, rc.Top), new Point(rc.Left + m_VertLinePosX, rc.Bottom));
                }

                m_rc = ClientRectangle;
                m_rc.Width = m_VertLinePosX;

                m_bmpBufferGraphics.FillRectangle(Brushes.Aqua, new Rectangle(0, 0, m_BookmarkWidth, ClientRectangle.Height));

            }

            Invalidate();
        }


    }
}