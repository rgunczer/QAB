using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;
using Util;


namespace VM
{
    public class ActionFindImage : ActionBase
    {
        private string m_pathToImage;
        private int m_Tolerance;

        private Bitmap bmpScreenshot;
        private Graphics gfxScreenshot;

        private Size imgSize;

        private Color[,] arrScreen = null;
        private Color[,] arrImage = null;

        private Color[] colors = new Color[2];
        private Point[] points = new Point[2];

        private Color findUpperLeft;

        private struct colorInfo
        {
            public Color color;
            public int count;
            public int x;
            public int y;            
        };

        private Dictionary<Color, colorInfo> mapToFind = new Dictionary<Color, colorInfo>();
        private Dictionary<Color, int> mapScreen = new Dictionary<Color, int>();

        private bool GetBitmapToFind()
        {
            Bitmap bmpToFind;
            m_pathToImage = _vm.variables.Get(m_params[1]);

            Directory.SetCurrentDirectory(_vm.host.StartupPath);

            m_pathToImage = Path.GetFullPath(m_pathToImage);
            _vm.host.WriteLog("fullpath: " + m_pathToImage);

            if (!File.Exists(m_pathToImage))
            {
                string msg = "Image File not found [" + m_pathToImage + "]";
                _vm.host.WriteLog(msg);                
                return false;
            }

            bmpToFind = (Bitmap)Bitmap.FromFile(m_pathToImage);            

            imgSize = new Size(bmpToFind.Width, bmpToFind.Height);
            arrImage = new Color[bmpToFind.Width, bmpToFind.Height];

            GetPixels(bmpToFind, arrImage);

            mapToFind.Clear();

            Color color;

            for (int y = 0; y < bmpToFind.Height; ++y)
            {
                for (int x = 0; x < bmpToFind.Width; ++x)
                {
                    color = arrImage[x, y];                    

                    if (mapToFind.ContainsKey(color))
                    {
                        colorInfo c = mapToFind[color];
                        ++c.count;
                        c.color = color;
                        c.x = x;
                        c.y = y;
                        mapToFind[color] = c;
                    }
                    else
                    {
                        colorInfo c = new colorInfo();
                        c.color = color;
                        c.count = 1;
                        c.x = x;
                        c.y = y;
                        mapToFind.Add(color, c);
                    }
                }
            }

            bmpToFind.Dispose();
            return true;
        }

        private void GetPixels(Bitmap bmp, Color[,] arr)
        {
            FastBitmap fastBmp = new FastBitmap(bmp);

            fastBmp.LockImage();            

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    arr[x, y] = fastBmp.GetPixel(x, y);
                }
            }
            fastBmp.UnlockImage();
        }
        
        private void GetScreenshot()
        {
            _vm.host.WriteLog("Getting Screenshot");

            bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format24bppRgb);
            gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

            arrScreen = new Color[Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height];

            GetPixels(bmpScreenshot, arrScreen);
/*
            mapScreen.Clear();

            Color cl;

            for (int i = 0; i < bmpScreenshot.Width; ++i)
            {
                for (int j = 0; j < bmpScreenshot.Height; ++j)
                {
                    cl = bmpScreenshot.GetPixel(i, j);

                    if (mapScreen.ContainsKey(cl))
                    {
                        ++mapScreen[cl];
                    }
                    else
                        mapScreen.Add(cl, 1);

                    arrScreen[i, j] = cl;
                }
            }
            _vm.host.WriteLog(bmpScreenshot.PixelFormat.ToString());
*/
        }
                
        private void PickColorsAndDeltas(ref Color prev)
        {
            int count = 100000;
            int index = 0;
            colorInfo minColor;
            minColor.color = Color.Red;
            minColor.x = -1;
            minColor.y = -1;
            List<colorInfo> list = new List<colorInfo>();

            foreach (colorInfo ci in mapToFind.Values)
            {
                list.Add(ci);
            }
            
            for (int i = 0; i < list.Count; ++i)
            {
                colorInfo ci = list[i];                

                if (ci.count <= count)
                {
                    index = i;
                    count = ci.count;
                    minColor = ci;
                }

                list[i] = ci;
            }

            colors[0] = minColor.color;
            points[0] = new Point(minColor.x, minColor.y);

            list.RemoveAt(index);

            count = 100000;

            for (int i = 0; i < list.Count; ++i)
            {
                colorInfo ci = list[i];

                if (ci.count <= count)
                {
                    index = i;
                    count = ci.count;
                    minColor = ci;
                }

                list[i] = ci;
            }

            colors[1] = minColor.color;
            points[1] = new Point(minColor.x, minColor.y);
        }

        private bool MatchImage(int scrX, int scrY)
        {
            try
            {
            Color img;
            Color screen;
            int xs = scrX;
            int ys = scrY;
            int relax = m_Tolerance;

            for (int y = 0; y < imgSize.Height; ++y)
            {
                xs = scrX;
                for (int x = 0; x < imgSize.Width; ++x)
                {
                    img = arrImage[x, y];
                    screen = arrScreen[xs, ys];

                    if (img.R != screen.R || img.G != screen.G || img.B != screen.B)
                    {
                        --relax;

                        if (relax <= 0)
                        {
                            return false;                            
                        }
                    }
                    ++xs;
                }
                ++ys;
            }
            return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool TryFindImage()
        {
            _vm.host.WriteLog("Trying to find Image on screen.");

            findUpperLeft = arrImage[0, 0];
            PickColorsAndDeltas(ref findUpperLeft);

            int height = bmpScreenshot.Height - imgSize.Height;
            int width = bmpScreenshot.Width - imgSize.Width;

            Color screen0;
            Color screen1;

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    if (findUpperLeft.R == arrScreen[x, y].R && findUpperLeft.G == arrScreen[x, y].G && findUpperLeft.B == arrScreen[x, y].B)
                    {
                        screen0 = arrScreen[x + points[0].X, y + points[0].Y];
                        screen1 = arrScreen[x + points[1].X, y + points[1].Y];

                        if (colors[0].R == screen0.R && colors[0].G == screen0.G && colors[0].B == screen0.B &&
                            colors[1].R == screen1.R && colors[1].G == screen1.G && colors[1].B == screen1.B)
                        {
                            if (MatchImage(x, y))
                            {
                                _vm.host.WriteLog("Image [" + m_pathToImage + "] found on Screen");
                                _vm.host.ImageRegion = new System.Windows.Rect(new System.Windows.Point((double)x, (double)y), new System.Windows.Size(imgSize.Width, imgSize.Height));
                                return true;
                            }
                        }
                    }
                }
            }
            _vm.host.WriteLog("Image [" + m_pathToImage + "] NOT found on Screen");
            return false;
        }


        public override EnumActionResult Execute()
        {
            int numberOfPixels = 0;
            
            int matchPercentage = 100;
            
            m_Tolerance = 0;

            if (!GetBitmapToFind())
                return EnumActionResult.ERROR;
            
            if (m_params.Count >= 4)
            {
                string relaxStr = _vm.variables.Get(m_params[3]);
                matchPercentage = Convert.ToInt32(relaxStr);
                numberOfPixels = imgSize.Width * imgSize.Height;
                m_Tolerance = numberOfPixels - ((numberOfPixels / 100) * matchPercentage);
            }
            
            int numOfScreens = Screen.AllScreens.Length;

            if (numOfScreens > 1)
            {
                string msg = "Number of Screens: " + numOfScreens + "\r\n" +
                             "WARNING: Find Image Supports only finding images on Primary Screen.";
                _vm.host.WriteLog(msg);
            }

            for (int i = 1; i < 11; ++i)
            {
                if (_vm.PlaybackStatus != VMStatus.IN_PLAYBACK)
                    return EnumActionResult.STOPPED;

                _vm.host.WriteLog("FindImage: " + i.ToString() + "/10");

                GetScreenshot();

                if ( TryFindImage() )
                    return EnumActionResult.OK;

                UtilSys.Wait(2000);
                _vm.host.WriteLog("- - - - - - - - - -");
            }
            
            _vm.PushError("Image NOT found on Screen");
            return EnumActionResult.ERROR;
        }


    }
}