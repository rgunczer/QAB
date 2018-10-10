using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Controls
{
    public class ctlTab : TabControl
    {
        public delegate bool PreRemoveTab(int indx);

        private static StringFormat _stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        private static Font _fnt = null;

        public ctlTab()
        {
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.DoubleBuffer |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.SupportsTransparentBackColor, true);

            if (null == _fnt)
                _fnt = new Font(Font.Name, Font.Size, FontStyle.Bold);
        }

        public PreRemoveTab PreRemoveTabPage;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            //base.OnMouseClick(e);

            Point p = e.Location;
            for (int i = 0; i < TabCount; i++)
            {
                Rectangle rc = GetTabRect(i);

                int a = 10;
                int x = rc.Left + rc.Width - (a + 4);
                int y = rc.Top + (rc.Height / 2) - (a / 2);

                Rectangle rcClose = new Rectangle(x, y, a, a);

                if (rcClose.Contains(p))
                {
                    CloseTab(i);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //base.OnMouseMove(e);

            Graphics g = this.CreateGraphics();

            Point p = e.Location;
            for (int i = 0; i < TabCount; ++i)
            {
                Rectangle rc = GetTabRect(i);

                int a = 10;
                int x = rc.Left + rc.Width - (a + 4);
                int y = rc.Top + (rc.Height / 2) - (a / 2);

                Rectangle rcClose = new Rectangle(x, y, a, a);

                if (rcClose.Contains(p))
                    DrawX(g, rc, true);
                else
                    DrawX(g, rc, false);
            }
            g.Dispose();
        }

        private void CloseTab(int i)
        {
            if (null != PreRemoveTabPage)
            {
                bool closeIt = PreRemoveTabPage(i);

                if (!closeIt)
                    return;
            }
            TabPages.Remove(TabPages[i]);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            Rectangle TabControlArea = this.ClientRectangle;
            Rectangle TabArea = this.DisplayRectangle;

            // fill client area
            //Brush br = new SolidBrush(Color.Red); //(SystemColors.Control);
            //g.FillRectangle(Brushes.Indigo, TabControlArea);
            //br.Dispose();
            
            int nDelta = SystemInformation.Border3DSize.Width;
            TabArea.Inflate(nDelta, nDelta);
            g.DrawRectangle(Pens.Gray, TabArea);

            for (int i = 0; i < this.TabCount; i++)
                DrawTab(g, this.TabPages[i], i);

            if (-1 != this.SelectedIndex)
                DrawTab(g, this.TabPages[this.SelectedIndex], this.SelectedIndex);
        }

        internal void DrawTab(Graphics g, TabPage tabPage, int nIndex)
        {
            Rectangle rc = this.GetTabRect(nIndex);
            RectangleF tabTextArea = (RectangleF)this.GetTabRect(nIndex);

            bool bSelected = (this.SelectedIndex == nIndex);

            Rectangle rect = rc;

            GraphicsPath gp = new GraphicsPath();

            int num = 2;

            Point[] points = 
            {
                new Point(rc.Left, rc.Top+rc.Height),
                new Point(rc.Left, rc.Top+num),
                new Point(rc.Left+num, rc.Top),
                new Point(rc.Left+rc.Width-num, rc.Top),
                new Point(rc.Left+rc.Width, rc.Top+num),
                new Point(rc.Left+rc.Width, rc.Top+rc.Height),
            };

            gp.AddPolygon(points);

            if (bSelected)
            {
                g.FillPath(Brushes.White, gp);
                g.DrawPath(Pens.DimGray, gp);

                g.DrawLine(Pens.White, rc.Left + 1, rc.Top + rc.Height, rc.Left + rc.Width - 1, rc.Top + rc.Height);
                g.DrawLine(Pens.White, rc.Left + 1, rc.Top + rc.Height + 1, rc.Left + rc.Width - 1, rc.Top + rc.Height + 1);

                g.DrawString(tabPage.Text, _fnt, Brushes.Black, tabTextArea, _stringFormat);

                DrawX(g, rc, false);
            }
            else
            {
                using (LinearGradientBrush brBack = new LinearGradientBrush(rc, /*Color.FromKnownColor(KnownColor.Control)*/ Color.Aqua, Color.LightSkyBlue, 90, false))
                {
                    g.FillPath(brBack, gp);
                    g.DrawPath(Pens.Gray, gp);
                }

                g.DrawString(tabPage.Text, Font, Brushes.Black, tabTextArea, _stringFormat);

                DrawX(g, rc, false);
            }
        }

        private void DrawX(Graphics g, Rectangle rc, bool hover)
        {
            int a = 12;
            int x = rc.Left + rc.Width - (a + 4);
            int y = rc.Top + (rc.Height / 2) - (a / 2);
            ++y;

            if (hover)
            {
                using (Pen pen = new Pen(Color.Red, 2.0f))
                {
                    g.DrawLine(pen, new Point(x + 1, y + 1), new Point(x + a - 2, y + a - 2));
                    g.DrawLine(pen, new Point(x + a - 2, y + 1), new Point(x + 1, y + a - 2));
                }
            }
            else
            {
                using (Pen pen = new Pen(Color.LightGray, 2.0f))
                {
                    g.DrawLine(pen, new Point(x + 1, y + 1), new Point(x + a - 2, y + a - 2));
                    g.DrawLine(pen, new Point(x + a - 2, y + 1), new Point(x + 1, y + a - 2));
                }
            }
        }


    }
}