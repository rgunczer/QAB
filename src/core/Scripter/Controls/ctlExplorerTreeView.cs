using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace Controls
{
    public class ctlExplorerTreeView : TreeView
    {
        private const int WM_LBUTTONCLK = 0x202;
        private const int WM_LBUTTONDBLCLK = 0x203;
        

        private bool m_bDoubleClickOnTree = false;

        public ctlExplorerTreeView() : base()
        {
            

        }
                                      
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {                                   
            if (m_bDoubleClickOnTree)
                e.Cancel = true;

            m_bDoubleClickOnTree = false;

            base.OnBeforeExpand(e);
        }

        protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
        {                                    
            if (m_bDoubleClickOnTree)
                e.Cancel = true;

            m_bDoubleClickOnTree = false;

            base.OnBeforeCollapse(e);
        }

        protected override void WndProc(ref Message m)
        {
            switch(m.Msg)
            {
                case WM_LBUTTONCLK:
                    m_bDoubleClickOnTree = false;
                break;

                case WM_LBUTTONDBLCLK:                    
                    m_bDoubleClickOnTree = true;                    
                break;
            }
            base.WndProc (ref m);
        }

    }
}
