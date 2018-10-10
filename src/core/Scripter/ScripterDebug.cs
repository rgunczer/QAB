using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace Scripter
{
    public static class ScripterDebug
    {
        public static FrmScripter frm;
        public static TextBox txt;
        
        public static void ShowTokensInCurrentLine(List<string> tokens, string line, int currentLine)
        {
            Scripter.ClearDebugOutput();            
            
            foreach (string item in tokens)
            {
                Scripter.DebugOutput(item);
            }            
        }

        public static void ShowCurrentLineNumber(int currentLine)
        {
            frm.status0.Text = "Ln : " + currentLine.ToString();
        }

        public static void ShowCurrentColumnNumber(int currentColumn)
        {
            frm.status1.Text = "Col : " + currentColumn.ToString();                            
        }

        public static void ShowCaretPosInCurrentLine(string line)
        {
            frm.status2.Text = "Current Line and Caret Pos: " + line;
        }

        public static void ShowCurrentPos(int index)
        {
            frm.status4.Text = "Pos: " + index;
        }
    }
}