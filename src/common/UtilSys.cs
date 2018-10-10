using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;


namespace Util
{
    static class UtilSys
    {
        public static void TakeScreenShotJPG(string path)
        {
            try
            {
                string dir = Path.GetDirectoryName(path);

                if (!Directory.Exists(path))
                {
                    DirectoryInfo dirInfo = Directory.CreateDirectory(dir);
                }

                Bitmap bmpScreenshot;
                Graphics gfxScreenshot;

                bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                gfxScreenshot = Graphics.FromImage(bmpScreenshot);
                gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                bmpScreenshot.Save(path, ImageFormat.Jpeg);
                //bmpScreenshot.Save(path, ImageFormat.Bmp);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static void TakeScreenShot(string path)
        {
            try
            {
                string dir = Path.GetDirectoryName(path);

                if (!Directory.Exists(path))
                {
                    DirectoryInfo dirInfo = Directory.CreateDirectory(dir);
                }

                Bitmap bmpScreenshot;
                Graphics gfxScreenshot;

                bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                gfxScreenshot = Graphics.FromImage(bmpScreenshot);
                gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                //bmpScreenshot.Save(path, ImageFormat.Jpeg);
                bmpScreenshot.Save(path, ImageFormat.Bmp);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetDateTime()
        {
            return DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }

        public static string OpenFileDialog(string title, string filter, string initDir)
        {
            OpenFileDialog ofn = new OpenFileDialog();
            ofn.Title = title; //"Open QABOT Script File";
            ofn.Filter = filter; //"Screen Play Files (*.scp)|*.scp|All Files (*.*)|*.*";            
            ofn.InitialDirectory = initDir;

            if (ofn.ShowDialog() == DialogResult.Cancel)
                return string.Empty;

            return (ofn.FileName);
        }

        public static string OpenFileDialogQABOTexe(string initDir)
        {
            OpenFileDialog ofn = new OpenFileDialog();
            ofn.Filter = "QABOT - Pilot exe File (*.exe)|*.exe|Exe Files (*.*)|*.*";
            ofn.Title = "QABOT - Pilot File";
            ofn.InitialDirectory = initDir;

            if (ofn.ShowDialog() == DialogResult.Cancel)
                return string.Empty;

            return (ofn.FileName);
        }


        public static string SaveFileDialogProject(string initDir)
        {
            SaveFileDialog sfn = new SaveFileDialog();
            sfn.Filter = "QABOT Project Files (*.qpf)|*.qpf|All Files (*.*)|*.*";
            sfn.Title = "Save QABOT Project File";
            sfn.InitialDirectory = initDir;

            if (sfn.ShowDialog() == DialogResult.Cancel)
                return string.Empty;

            return (sfn.FileName);
        }

        public static string SaveFileDialogMonitor(string initDir)
        {
            SaveFileDialog sfn = new SaveFileDialog();
            sfn.Filter = "QABOT Monitor Files (*.qam)|*.qam|All Files (*.*)|*.*";
            sfn.Title = "Save QABOT Monitor File";
            sfn.InitialDirectory = initDir;

            if (sfn.ShowDialog() == DialogResult.Cancel)
                return string.Empty;

            return (sfn.FileName);
        }

        public static string SaveFileDialog(string initDir)
        {
            SaveFileDialog sfn = new SaveFileDialog();
            sfn.Filter = "Script Files (*.scp)|*.scp|All Files (*.*)|*.*";
            sfn.Title = "Save Screen Play File";
            sfn.InitialDirectory = initDir;

            if (sfn.ShowDialog() == DialogResult.Cancel)
                return string.Empty;

            return (sfn.FileName);
        }
        
        public static void Wait(int time2Wait) // do not hang the app (wa aren't using separate thread 4 playback)
        {
            int step = 10;
            int elapsed = 0;

            while (elapsed < time2Wait)
            {
                System.Threading.Thread.Sleep(step);
                Application.DoEvents();
                elapsed += step;
            }
        }

        public static void Sleep(int milisec)
        {
            System.Threading.Thread.Sleep(milisec);
        }

        public static DialogResult MessageBoxQuestion(string msg)
        {
            DialogResult res = System.Windows.Forms.MessageBox.Show(msg, Application.ProductName + " v" + Application.ProductVersion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return res;
        }

        public static void MessageBox(string msg)
        {
            System.Windows.Forms.MessageBox.Show(msg, Application.ProductName + " v" + Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void MessageBoxInfo(string msg)
        {
            System.Windows.Forms.MessageBox.Show(msg, Application.ProductName + " v" + Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MessageBoxError(string msg)
        {
            System.Windows.Forms.MessageBox.Show(msg, Application.ProductName + " v" + Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
