using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using Controls;


namespace Scripter
{
    public class Intellisense
    {
        public static ctlIS _is = null;        

        private Editor editor = null;

        private List<string> m_ListItems = new List<string>();

        public bool IsVisible
        {
            get { return _is.Visible; }
            set { _is.Visible = value; }
        }

        public Intellisense(Editor editor)
        {
            Debug.Assert(null != editor);
            this.editor = editor;
        }

        public void Clear()
        {
            Debug.Assert(null != _is);

            _is.ClearItems();
            m_ListItems.Clear();
        }

        public void AddItem(string item)
        {
            Debug.Assert(null != _is);

            _is.AddItem(item);
            m_ListItems.Add(item);
        }

        public void Up()
        {
            Debug.Assert(null != _is);
            _is.NavigateUp();
        }

        public void Down()
        {
            Debug.Assert(null != _is);
            _is.NavigateDown();
        }

        public int ItemCount
        {
            get { return _is.GetItemCount(); }
        }

        public string Selected
        {
            get
            {
                return _is.GetSelected();
            }
        }

        public void Show(Point pt)
        {
            _is.Location = pt;
            Show();
        }

        public void Show()
        {
            _is.Show();            
        }

        public void Hide()
        {
            _is.Hide();            
        }

        public void Update(string typed)
        {
            Debug.Assert(null != typed);

            bool bWasVisible = IsVisible;            
            
            if (0 == typed.Length)
            {
                Hide();
                return;
            }

            _is.ClearItems();
    
            foreach (string str in m_ListItems)
            {
                if (str.ToLower().StartsWith(typed.ToLower()))                
                    _is.AddItem(str);
            }
                                                
            if (0 == _is.GetItemCount())
            {
                Hide();
                return;
            }

            CalcWidth();

            CalcHeight();
            Show();

            if (!bWasVisible)
                Show(editor.IntelliSensePosition);
        }

        public void SetPos(Point pt)
        {
            _is.Top = pt.Y;
            _is.Left = pt.X;             
        }

        public void CalcWidth()
        {
            _is.CalcWidth();
        }

        public void CalcHeight()
        {
            _is.CalcHeight();
        }
    }
}