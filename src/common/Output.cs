using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System;


namespace Util
{
    public class Output
    {        
        private static TextBox _txt = null;
        private static string _title;

        public static void Set(TextBox txt, string title)
        {
            _txt = txt;
            _title = title;
            _txt.Text = _title;
        }

        public static void Clear()
        {
            _txt.Lines = null;
            _txt.Text = _title;
        }

        public delegate void dWrite(string msg);
        public static void Write(string msg)
        {
            if (null == _txt)
                return;

            msg = msg.Trim();

            if (0 == msg.Length)
                return;
            
            if (_txt.InvokeRequired)
            {
                _txt.Invoke(new dWrite(Write), new object[]{msg});
            }
            else
            {
                try
                {
                    _txt.Text += Environment.NewLine + msg;

                    int line = _txt.Lines.Length - 1;

                    if (line < 0)
                        line = 0;

                    _txt.SelectionStart = _txt.GetFirstCharIndexFromLine(line);                
                    _txt.ScrollToCaret();
                }
                catch
                {                    
                }
            }            
        }
    }

}
