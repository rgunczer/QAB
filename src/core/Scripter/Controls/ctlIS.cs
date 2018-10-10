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
    public partial class ctlIS : UserControl
    {
        private static int minWidht = 263;
        private static int maxWidth = minWidht + 100;
        private static int MaxItemCount = 12;
        private static int subHeight = 20;

        private static Font font = new Font("Arial Black", 10, FontStyle.Italic);
        StringFormat format = StringFormat.GenericTypographic;


        public ctlIS()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        public string _text = "Intellisense";

        public Color color
        {
            set 
            { 
                lb.BackColor = value;
                BackColor = Color.Gray;
            }
            get { return lb.BackColor; }
        }

        public void ClearItems()
        {
            lb.Items.Clear();        
        }

        public void AddItem(string item)
        {
            lb.Items.Add(item);
        }

        public void NavigateUp()
        {
            if (lb.SelectedIndex < lb.Items.Count - 1)
                ++lb.SelectedIndex;
        }

        public void NavigateDown()
        {
            if (lb.SelectedIndex > 0)
                --lb.SelectedIndex;
        }

        public int GetItemCount()
        {
            return lb.Items.Count;
        }

        public string GetSelected()
        {
            if (lb.SelectedItems.Count == 0)
                return string.Empty;

            return lb.SelectedItem.ToString();
        }

        public void CalcWidth()
        {
            Size maxSize = new Size(0, 0);
            Size size = maxSize;            

            foreach(string str in lb.Items)
            {
                size = TextRenderer.MeasureText(str, lb.Font);

                if (size.Width > maxSize.Width)
                    maxSize = size;
            }
            Width = maxSize.Width;            

            if (Width < minWidht)
                Width = minWidht;

            if (Width > maxWidth)
                Width = maxWidth;
        }
        
        public void CalcHeight()
        {
            if (lb.Items.Count > 0)
                lb.SelectedIndex = 0;

            Height = (lb.Items.Count * lb.ItemHeight) + subHeight + 2;

            if (Height > ((MaxItemCount * lb.ItemHeight) + subHeight))
                Height = (lb.ItemHeight * MaxItemCount) + subHeight;
            
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = new Rectangle(lb.Left, (lb.Top + lb.Height), lb.Width, subHeight);

            using ( LinearGradientBrush brGradient = new LinearGradientBrush(rect, Color.Black, lb.BackColor, 0, false) )
            {
                Graphics g = e.Graphics;
                
                g.FillRectangle(brGradient, rect);
                        
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.DrawString(_text, font, Brushes.White, new PointF(1.0f, ClientRectangle.Height - 21));
            }            
        }


    }
}