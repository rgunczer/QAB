using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;


namespace Util
{
    public static class SharedMemory
    {
        [Flags]
        private enum SendMessageTimeoutFlags : uint
        {
            SMTO_ABORTIFHUNG = 2,
            SMTO_BLOCK = 1,
            SMTO_NORMAL = 0,
            SMTO_NOTIMEOUTIFNOTHUNG = 8
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COPYDATASTRUCT
        {
        public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }

        private static COPYDATASTRUCT _dataStruct;

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern int SendMessageTimeout(IntPtr hwnd, int wMsg, int wParam, ref COPYDATASTRUCT lParam, SendMessageTimeoutFlags flags, uint timeout, out IntPtr result);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);


        private static COPYDATASTRUCT FillDataStruct(string msg)
        {            
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream();
            formatter.Serialize(serializationStream, msg);
            serializationStream.Flush();
            int length = (int)serializationStream.Length;
            byte[] buffer = new byte[length];
            serializationStream.Seek(0L, SeekOrigin.Begin);
            serializationStream.Read(buffer, 0, length);
            serializationStream.Close();
            IntPtr destination = Marshal.AllocCoTaskMem(length);
            Marshal.Copy(buffer, 0, destination, length);
            _dataStruct.cbData = length;
            _dataStruct.dwData = IntPtr.Zero;
            _dataStruct.lpData = destination;

            return _dataStruct;
        }

        public static void Send(string procName, string msg)
        {
            COPYDATASTRUCT lParam = FillDataStruct(msg);
            IntPtr zero = IntPtr.Zero;

            Process[] proc = Process.GetProcessesByName(procName);

            if (1 == proc.Length)
            {
                IntPtr ptrDest = proc[0].MainWindowHandle;
                int ret = SendMessageTimeout(ptrDest, NativeMethods.WM_COPYDATA, (int)IntPtr.Zero, ref lParam, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 0x3e8, out zero);
            }
            else if (proc.Length > 1)
            {
                // more than one process with the same name
            }
        }

        public static void SendToMFC(string procName, string str, IntPtr srcHandle)
        {
            Process[] proc = Process.GetProcessesByName(procName);

            if (1 == proc.Length)
            {
                IntPtr destHandle = proc[0].MainWindowHandle;

                COPYDATASTRUCT cds;

                cds.dwData = srcHandle;
                str = str + '\0';

                cds.cbData = str.Length + 1;
                cds.lpData = Marshal.AllocHGlobal(str.Length);
                cds.lpData = Marshal.StringToHGlobalAnsi(str);
                IntPtr iPtr = Marshal.AllocHGlobal(Marshal.SizeOf(cds));
                Marshal.StructureToPtr(cds, iPtr, true);

                // send to the MFC app
                SendMessage(destHandle, NativeMethods.WM_COPYDATA, IntPtr.Zero, iPtr);

                // Don't forget to free the allocated memory 
                Marshal.FreeHGlobal(cds.lpData);
                Marshal.FreeHGlobal(iPtr);
            }
        }

        /* MFC mod
        public static string Recv(ref Message msg)
        {            
            COPYDATASTRUCT dataStruct = (COPYDATASTRUCT)Marshal.PtrToStructure(msg.LParam, typeof(COPYDATASTRUCT));
                     
            byte[] bytes = new byte[dataStruct.cbData];            
            Marshal.Copy(dataStruct.lpData, bytes, 0, dataStruct.cbData);

            //char[] arr = Encoding.ASCII.GetChars(bytes); // for MFC
            char[] arr = Encoding.Unicode.GetChars(bytes);

            string rawmessage = new string(arr);

            return rawmessage;
        }
        */

        public static string Recv(ref Message msg)
        {
            // msg.LParam contains a pointer to the COPYDATASTRUCT struct
            COPYDATASTRUCT dataStruct = (COPYDATASTRUCT)Marshal.PtrToStructure(msg.LParam, typeof(COPYDATASTRUCT));

            // Create a byte array to hold the data
            byte[] bytes = new byte[dataStruct.cbData];

            // Make a copy of the original data referenced by the COPYDATASTRUCT struct
            Marshal.Copy(dataStruct.lpData, bytes, 0, dataStruct.cbData);

            // Deserialize the data back into a string
            string rawmessage = string.Empty;

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                BinaryFormatter b = new BinaryFormatter();
                rawmessage = (string)b.Deserialize(stream);
            }
            return rawmessage;
        }

    }
}
