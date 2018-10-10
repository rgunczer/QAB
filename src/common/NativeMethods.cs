using System;
using System.Runtime.InteropServices;
using System.Drawing;

#pragma warning disable 0649

namespace Util
{	
    public static class NativeMethods
    {
        public const int WM_COPYDATA = 0x4A;

 

        public const int KEYEVENTF_KEYUP = 0x2;
        public const int KEYEVENTF_EXTENDEDKEY = 0x01;
        public const byte VK_TAB = 0x09;
        public const byte VK_LSHIFT = 0xA0;
        public const byte VK_LCONTROL = 0xA2;

        public class INPUTTYPE
        {
            public const int MOUSE = 0;
            public const int KEYBOARD = 1;
            public const int HARDWARE = 2;
        }

        public class MOUSEEVENTF
        {
            public const int MOVE = 0x0001; /* mouse move */
            public const int LEFTDOWN = 0x0002; /* left button down */
            public const int LEFTUP = 0x0004; /* left button up */
            public const int RIGHTDOWN = 0x0008; /* right button down */
            public const int RIGHTUP = 0x0010; /* right button up */
            public const int MIDDLEDOWN = 0x0020; /* middle button down */
            public const int MIDDLEUP = 0x0040; /* middle button up */
            public const int XDOWN = 0x0080; /* x button down */
            public const int XUP = 0x0100; /* x button down */
            public const int WHEEL = 0x0800; /* wheel button rolled */
            public const int VIRTUALDESK = 0x4000; /* map to entire virtual desktop */
            public const int ABSOLUTE = 0x8000; /* absolute move */
        }

        public struct INPUT
        {
            public int type;
            public MOUSEINPUT mi;
        }

        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            //public int mouseData;
            public int dwFlags;
            //public int time;
            //public int dwExtraInfo;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }


        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);                             

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32")]
        internal static extern int LockWindowUpdate(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        internal static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("User32.dll")]
        internal static extern uint SendInput(uint numberOfInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] input, int structSize);

        [DllImport("user32.dll")]
        internal static extern int ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        internal static extern bool ClientToScreen(IntPtr hwnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        internal static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hwndAfter, int x, int y, int width, int height, int flags);

        [DllImport("user32.dll")]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        internal static extern bool SetProcessDPIAware();

        internal const int GWL_EXSTYLE = -20;
        internal const int SW_SHOWNA = 8;
        internal const int WS_EX_TOOLWINDOW = 0x00000080;
        internal const int SWP_NOACTIVATE = 0x0010;
        internal static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        internal const UInt32 WM_SETREDRAW = 0x000B;


        public const int WM_USER = 0x400;
        public const int WM_PAINT = 0xF;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_CHAR = 0x102;

        public const int EM_GETSCROLLPOS = (WM_USER + 221);
        public const int EM_SETSCROLLPOS = (WM_USER + 222);


    }

}