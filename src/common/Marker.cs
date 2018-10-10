using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Util
{
    public class Marker
    {
        private bool m_bShown;
        private int m_iLineWidth;
        private Rectangle m_rcLocation;

        private Form leftForm;
        private Form topForm;
        private Form rightForm;
        private Form bottomForm;

        public Marker()
        {            
            m_bShown = false;
            m_iLineWidth = 3;
            leftForm = new Form();
            topForm = new Form();
            rightForm = new Form();
            bottomForm = new Form();
            Form[] forms = { leftForm, topForm, rightForm, bottomForm };

            foreach (Form form in forms)
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.ShowInTaskbar = false;
                form.TopMost = true;
                form.Visible = false;
                form.Left = 0;
                form.Top = 0;
                form.Width = 1;
                form.Height = 1;
                form.BackColor = Color.Red;

                // Make it a tool window so it doesn't show up with Alt+Tab.
                int style = NativeMethods.GetWindowLong(form.Handle, NativeMethods.GWL_EXSTYLE);
                NativeMethods.SetWindowLong(form.Handle, NativeMethods.GWL_EXSTYLE,(int)(style | NativeMethods.WS_EX_TOOLWINDOW));
            }
        }
        
        // Sets the visible state of the rectangle.             
        // The Layout method is called by using BeginInvoke, to prevent
        // cross-thread updates to the UI. This method can be called on
        // any form that belongs to the UI thread.        
        public bool Visible
        {
            set
            {
                if (m_bShown != value)
                {
                    m_bShown = value;
                    if (m_bShown)
                    {
                        MethodInvoker mi = new MethodInvoker(Layout);
                        leftForm.BeginInvoke(mi);
                        mi = new MethodInvoker(Show);
                        leftForm.BeginInvoke(mi);
                    }
                    else
                    {
                        MethodInvoker mi = new MethodInvoker(Hide);

                        try
                        {
                            leftForm.BeginInvoke(mi);
                        }
                        catch (Exception)
                        {
                                                        
                        }                        
                    }
                }
            }
        }

        public Rectangle Location
        {
            set
            {
                m_rcLocation = value;
                MethodInvoker mi = new MethodInvoker(Layout);
                leftForm.BeginInvoke(mi);
            }
        }

        void Show()
        {
            NativeMethods.ShowWindow(leftForm.Handle, NativeMethods.SW_SHOWNA);
            NativeMethods.ShowWindow(topForm.Handle, NativeMethods.SW_SHOWNA);
            NativeMethods.ShowWindow(rightForm.Handle, NativeMethods.SW_SHOWNA);
            NativeMethods.ShowWindow(bottomForm.Handle, NativeMethods.SW_SHOWNA);            
        }

        void Hide()
        {
            leftForm.Hide();
            topForm.Hide();
            rightForm.Hide();
            bottomForm.Hide();
        }
                
        // Sets the position and size of the four forms that make up the rectangle.
        //        
        // Use the Win32 SetWindowPosfunction so that SWP_NOACTIVATE can be set. 
        // This ensures that the windows are shown without receiving the focus.        
        private void Layout()
        {
            // Use SetWindowPos instead of changing the location via form properties: 
            // this allows us to also specify HWND_TOPMOST. 
            // Using Form.TopMost = true to do this has the side-effect
            // of activating the rectangle windows, causing them to gain the focus.
            
            // left
            NativeMethods.SetWindowPos(leftForm.Handle, NativeMethods.HWND_TOPMOST,
                             m_rcLocation.Left - m_iLineWidth, 
                             m_rcLocation.Top, 
                             m_iLineWidth, m_rcLocation.Height, 
                             NativeMethods.SWP_NOACTIVATE);
            // top
            NativeMethods.SetWindowPos(topForm.Handle, NativeMethods.HWND_TOPMOST,
                             m_rcLocation.Left - m_iLineWidth, 
                             m_rcLocation.Top - m_iLineWidth, 
                             m_rcLocation.Width + 2 * m_iLineWidth, 
                             m_iLineWidth, 
                             NativeMethods.SWP_NOACTIVATE);
            // right
            NativeMethods.SetWindowPos(rightForm.Handle, NativeMethods.HWND_TOPMOST,
                             m_rcLocation.Left + m_rcLocation.Width, 
                             m_rcLocation.Top, m_iLineWidth, 
                             m_rcLocation.Height, 
                             NativeMethods.SWP_NOACTIVATE);
            // bottom
            NativeMethods.SetWindowPos(bottomForm.Handle, NativeMethods.HWND_TOPMOST,
                             m_rcLocation.Left - m_iLineWidth, 
                             m_rcLocation.Top + m_rcLocation.Height, 
                             m_rcLocation.Width + 2 * m_iLineWidth, 
                             m_iLineWidth, 
                             NativeMethods.SWP_NOACTIVATE);
        }



    }  
}  