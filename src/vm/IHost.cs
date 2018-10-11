using System;
using System.Text;
using System.Drawing;
using System.Windows.Automation;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace VM
{
    public interface IHost
    {        
        void WindowMinimalize();
        void WindowNormal();        
        void ReleaseSQL();
        void WriteLog(string text);
        void Exit();
        void SetTitle(string str);
        void ClearLogFile();
        bool SetLogFile(string path, string bigBrother);
        void SetLogLevel(string level);
        void MarkCurrentLine(int numberIP, int lineNumber, Color color, string path);
        void UpdateResult(int pos, string value);
        void UpdateMarker(System.Windows.Rect rc);
        void HideMarker();
        bool InitTipDialog(int x, int y, string text, int timeout);
        void ShowTipDialog();

        DialogResult ShowUserInputDialog(string label, string text, ref string retValue);


        // prop
        bool IsAutorun { get; }
        AutomationElement TargetWindow { get; set; }
        AutomationElement aeCurrent { get; set; }
        string TargetWindowTitle { get; set; }
        int TargetProcessID { get; set; }
        bool IsRegion { get; set; }        
        System.Windows.Rect ImageRegion { get; set; }
        
        System.Windows.Point LeftClickPos { get; set; }
        bool SaveScreenshotOnError { get; set; }
        bool RecordPlayback { get; set; }
        void ReleaseSQLStuff();
        void DoEvents();

        SqlConnection conn { get; set; }
        SqlDataReader reader { get; set; }

        string StartupPath { get; }
        string _ret { get; set; }
        string BigBrother { get; set; }

        

    }
}