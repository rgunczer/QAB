using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;


namespace Util
{
    public static class Support
    {
        public static void JustCenterWindowOn(Form parent, Form frm)
        {
            int parentHalfWidth = parent.Width / 2;
            int dialogHalfWidth = frm.Width / 2;

            int parentHalfHeight = parent.Height / 2;
            int dialogHalfHeight = frm.Height / 2;

            frm.Left = (parent.Left + parentHalfWidth) - dialogHalfWidth;
            frm.Top = (parent.Top + parentHalfHeight) - dialogHalfHeight;            
        }

        public static void CenterWindowOn(Form parent, Form frm)
        {
            JustCenterWindowOn(parent, frm);
            frm.Show();
            frm.Activate();
        }

        public static Bitmap GetScreenshot()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            g.Dispose();
            return bmp;
        }


        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static bool ProcIsRunning(string procName)
        {
            Process[] proc = Process.GetProcessesByName(procName);

            return proc.Length > 0 ? true : false;
        }

        public static string SurrondItWith(string text, string ch)
        {
            return ch + text + ch;
        }

        public static bool IsDecimal(string value)
        {
            try
            {
                Convert.ToDouble(value);
                return true;
            } 
            catch 
            {
                return false;
            }
        }

        public static bool IsInteger(string value)
        {
            try
            {
                Convert.ToInt32(value);
                return true;
            } 
            catch 
            {
                return false;
            }
        }

        public static void ShowMessageNoTargetWindow()
        {
            UtilSys.MessageBox("TargetWindow not set, cannot perform action.");
        }

        public static void ShowMessageNoCurrentElement()
        {
            UtilSys.MessageBox("No Current Element, unable to execute action.");
        }

        public static string FormatTime4Me(TimeSpan timeSpan)
        {
            string hours = String.Format("{0:00}", timeSpan.Hours);
            string minutes = String.Format("{0:00}", timeSpan.Minutes);
            string seconds = String.Format("{0:00}", timeSpan.Seconds);

            return (hours + ":" + minutes + ":" + seconds);
        }

        // role of this function is to ensure that 
        // required folder structure under QABOT is created
        public static bool CreateFolderStructure(string path)
        {
            string dir = string.Empty;

            string[] dirs = { 
            "log", 
            "mru", 
            "projects", 
            "scripts", 
            "settings", 
            "screenshots", 
            "doc",            
            "templates",};

            string dirData = Path.Combine(path, "data");

            try
            {
                if(!Directory.Exists(dirData))
                {
                    Directory.CreateDirectory(dirData);
                }

                for(int i = 0; i < dirs.Length; ++i)
                {
                    dir = Path.Combine(dirData, dirs[i]);

                    if(!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                }
            }
            catch(Exception ex)
            {
                UtilSys.MessageBoxError(ex.Message);
                return false;
            }

            return true;
        }



    }
}
