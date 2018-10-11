using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using Util;


namespace VM
{
    class ActionSetDate : ActionBase
    {

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            [MarshalAs(UnmanagedType.U2)]
            public short Year;
            [MarshalAs(UnmanagedType.U2)]
            public short Month;
            [MarshalAs(UnmanagedType.U2)]
            public short DayOfWeek;
            [MarshalAs(UnmanagedType.U2)]
            public short Day;
            [MarshalAs(UnmanagedType.U2)]
            public short Hour;
            [MarshalAs(UnmanagedType.U2)]
            public short Minute;
            [MarshalAs(UnmanagedType.U2)]
            public short Second;
            [MarshalAs(UnmanagedType.U2)]
            public short Milliseconds;

            public SYSTEMTIME(DateTime dt)
            {
                dt = dt.ToUniversalTime();  // SetSystemTime expects the SYSTEMTIME in UTC
                Year = (short)dt.Year;
                Month = (short)dt.Month;
                DayOfWeek = (short)dt.DayOfWeek;
                Day = (short)dt.Day;
                Hour = (short)dt.Hour;
                Minute = (short)dt.Minute;
                Second = (short)dt.Second;
                Milliseconds = (short)dt.Millisecond;
            }
        }

        [DllImport("user32")]
        static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, ref SYSTEMTIME st);

               

        public override EnumActionResult Execute()
        {
            AutomationElement ae = _vm.host.aeCurrent;

            DateTime value = new DateTime(2010, 05, 10);
            SYSTEMTIME st = new SYSTEMTIME(value);
            IntPtr pnt = Marshal.AllocCoTaskMem(Marshal.SizeOf(st));
            Marshal.StructureToPtr(st, pnt, true);
                       
            try
            {
                int DTM_SETSYSTEMTIME = 0x1002;

                IntPtr hWnd = (IntPtr)ae.Current.NativeWindowHandle;

                int ret = NativeMethods.SendMessage(hWnd, DTM_SETSYSTEMTIME, 0, pnt);


                if (ret == 0)
                    Util.UtilSys.MessageBoxError("Action SetDate Error");

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Marshal.FreeCoTaskMem(pnt);
            }

            return EnumActionResult.OK;            
        }

    }
}
