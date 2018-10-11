using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Util;


namespace VM
{
    public partial class FrmTip : Form
    {
        static private VirtualMachine _vm = null;
        public static VirtualMachine vm
        {
            set { _vm = value; }
        }

        static public Color m_ColorLeft = Color.Yellow;
        static public Color m_ColorRight = Color.White;
        static public Color m_ColorBorder = Color.Black;
        static public Color m_ColorFont = Color.Black;
        static public string m_FontFamily = "Courier New";
        static public int m_FontSize = 9;
        static public FontStyle m_FontStyle = FontStyle.Regular;
        static public int _BorderWidht = 1;

        private Pen m_pen = new Pen(m_ColorBorder, _BorderWidht);                
        private Point m_ptBox = new Point(0, 0);
        private Point m_ptTarget = new Point(0, 0);
        private Point m_ptSrc = new Point(0, 0);
        private string m_text = "<NO TEXT>";
        private int m_time = 0;
        private int m_timeQuit = 0;
        private Point m_ptTargetScreen;

        private StringFormat m_sf = null;
        private Font m_font = null;

        private Random m_rnd = null;

        public FrmTip()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            m_sf = new StringFormat(StringFormatFlags.DirectionRightToLeft);    
        }

        public bool Init(int x, int y, string text, int timeout)
        {
            try
            {
                Point pt = new Point();
                pt.X = x;
                pt.Y = y;
                m_text = text.Replace(@"\n", Environment.NewLine);
                m_timeQuit = timeout;
                m_time = 0;

                IntPtr handle = new IntPtr(_vm.host.TargetWindow.Current.NativeWindowHandle);

                Point point = new Point();
                point.X = (int)pt.X;
                point.Y = (int)pt.Y;
                NativeMethods.ClientToScreen(handle, ref point);
                                
                m_ptTargetScreen.X = point.X;
                m_ptTargetScreen.Y = point.Y;

                m_font = new Font(m_FontFamily, m_FontSize, m_FontStyle);

                m_rnd = new Random(DateTime.Now.Millisecond);
            }
            catch
            {
                return false;                
            }
            return true;            
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {            
            Graphics gfx = e.Graphics;
            SizeF sizef;
            
            if (m_text.Length < 8)            
                sizef = gfx.MeasureString("mmmmmmmm", m_font, Int32.MaxValue, m_sf);            
            else
                sizef = gfx.MeasureString(m_text, m_font, Int32.MaxValue, m_sf);            

            int width = (int)sizef.Width+20;
            int height = (int)sizef.Height+10;

            Width = width + 1;

            if (m_ptSrc.X == 0)
                m_ptSrc.X = 20 + m_rnd.Next(width - 80);

            m_ptTarget.Y = height + 40;

            Height = m_ptTarget.Y + 1;


            m_ptTarget.X = Width / 2;

            Point[] points = 
            {
                m_ptBox,

                new Point(m_ptBox.X+width, m_ptBox.Y),

                new Point(m_ptBox.X+width, m_ptBox.Y+height),
                
                new Point(m_ptBox.X+m_ptSrc.X+10, m_ptBox.Y+height),

                m_ptTarget,

                new Point(m_ptBox.X+m_ptSrc.X-10, m_ptBox.Y+height),
                
                new Point(m_ptBox.X, m_ptBox.Y+height),
                
                m_ptBox,
            };

            Left = m_ptTargetScreen.X - (Width / 2);
            Top = m_ptTargetScreen.Y - m_ptTarget.Y;

            Brush fb = new SolidBrush(Color.Black);
            LinearGradientBrush lGB;  // namespace System.Drawing.Drawing2D;            
            Color cW = Color.White;

            Rectangle rec = new Rectangle(1, 1, width - 1, height - 1);
            LinearGradientMode lGM = LinearGradientMode.Horizontal;
            lGB = new LinearGradientBrush(rec, m_ColorLeft, m_ColorRight, lGM);
            
            e.Graphics.FillPolygon(lGB, points);

            e.Graphics.DrawLines(m_pen, points);

            // Create font and brush.            
            SolidBrush drawBrush = new SolidBrush(m_ColorFont);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF( m_ptBox.X + 10.0F, m_ptBox.Y + 5.0F);

            // Draw string to screen.
            e.Graphics.DrawString(m_text, m_font, drawBrush, drawPoint);            
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            if (m_time > m_timeQuit)
                this.Close();

            m_time += tmr.Interval;
        }

        private void FrmTip_Load(object sender, EventArgs e)
        {
            Rectangle rc = Screen.PrimaryScreen.Bounds;

            Left = rc.Right;
            Top = rc.Height;

            Width = 0;
            Height = 0;
        }

    }
}