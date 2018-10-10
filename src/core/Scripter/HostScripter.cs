using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Automation;
using System.Timers;


using VM;
using Util;
using Global;


namespace Scripter
{
    public class HostScripter : IHost
    {
        private FrmScripter _frm = null;
        private bool m_bRecordPlayback = false;
        private string m_bigBrother = Globals.ProcessMaster;
        public static System.Windows.Point m_LeftClickPos;
        private static SqlConnection _conn = null;
        private static SqlDataReader _reader = null;
        private AutomationElement m_aeWindow = null;
        private string m_TargetWindowTitle = string.Empty;
        private int m_iTargetProcessID = -1;
        private AutomationElement m_aeCurrent = null;
        private bool m_bIsRegion = false;
        private static Marker _marker = new Marker();
        private static System.Timers.Timer _markerTimer = new System.Timers.Timer();
        private bool m_bShowMarker = true;
        private System.Windows.Rect m_imageRegion;

        // ctor
        public HostScripter(FrmScripter frm)
        {
            _frm = frm;

            _markerTimer.Elapsed += new ElapsedEventHandler(OnMarkerTimerTick);
            _markerTimer.Enabled = false;
            _markerTimer.AutoReset = false;
            _markerTimer.Interval = 5500;
        }

        private void OnMarkerTimerTick(object sender, EventArgs e)
        {
            _marker.Visible = false;
        }


        public void Compact(bool value)
        {
            //_frm.Compact(value);            
        }

        public void WindowMinimalize()
        {
            _frm.WindowState = FormWindowState.Minimized;
        }

        public void WindowNormal()
        {
            _frm.WindowState = FormWindowState.Normal;
        }

        public void ReleaseSQL()
        {
            if (null != reader)
                reader.Dispose();

            if (null != conn)
                conn.Dispose();
        }

        public void WriteLog(string text)
        {
            Scripter.ScripterForm.WriteVMLog(text);
        }

        public void ShowInfo()
        {

        }

        public void SetTitle(string title)
        {

        }
        
        public void Exit()
        {

        }

        public void ClearLogFile()
        {

        }

        public bool SetLogFile(string path, string bigBrother)
        {
            return true;
        }


        public void SetLogLevel(string level)
        {

        }

        public void MarkCurrentLine(int numberIP, int lineNumber, Color color, string path)
        {
            _frm.MarkCurrentLine(lineNumber, path);
        }

        public void UpdateMarker(System.Windows.Rect rc)
        {
            if (!m_bShowMarker)
                return;

            _marker.Visible = false;
            _marker.Location = new Rectangle((int)rc.Left, (int)rc.Top, (int)rc.Width, (int)rc.Height);
            _marker.Visible = true;
            _markerTimer.Start();
        }

        public void HideMarker()
        {
            _marker.Visible = false;
            //Pilot.DoEvents();
        }



        // prop

        public void ReleaseSQLStuff()
        {
            try
            {
                if (reader != null)
                    reader.Dispose();

                if (conn != null)
                    conn.Dispose();
            }
            catch (Exception ex)
            {
                WriteLog("HostPilot->ReleaseSQLStuff: " + ex.Message);
            }
        }

        public void DoEvents()
        {
            Scripter.DoEvents();
        }

        public SqlConnection conn
        {
            get { return HostScripter._conn; }
            set { HostScripter._conn = value; }
        }

        public SqlDataReader reader
        {
            get { return HostScripter._reader; }
            set { HostScripter._reader = value; }
        }


        public bool IsAutorun
        {
            get { return false; }
        }

        public bool IsRegion
        {
            get { return m_bIsRegion; }
            set { m_bIsRegion = value; }
        }

        public bool ShowMarker
        {
            get { return m_bShowMarker; }
            set { m_bShowMarker = value; }
        }

        public AutomationElement TargetWindow
        {
            get
            {
                if (null == m_aeWindow)
                    throw new Exception("Target Window not set.\nUse FindWindow or FindWindowLike to find the target window.");

                return m_aeWindow;
            }
            set
            {
                m_aeWindow = value;

                if (null != m_aeWindow)
                {
                    UpdateMarker(m_aeWindow.Current.BoundingRectangle);
                    m_bIsRegion = false;
                }
            }
        }

        public string TargetWindowTitle
        {
            get { return m_TargetWindowTitle; }
            set { m_TargetWindowTitle = value; }
        }

        public int TargetProcessID
        {
            get { return m_iTargetProcessID; }
            set { m_iTargetProcessID = value; }
        }

        public bool RecordPlayback
        {
            get { return m_bRecordPlayback; }
            set { m_bRecordPlayback = value; }
        }

        public AutomationElement aeCurrent
        {
            get
            {
                if (null == m_aeCurrent)
                    WriteLog("Error:: CurrentElement is null!");

                return m_aeCurrent;
            }

            set
            {
                m_aeCurrent = value;
                m_bIsRegion = false;

                if (null != m_aeCurrent)
                {
                    UpdateMarker(m_aeCurrent.Current.BoundingRectangle);
                }
            }
        }

        public System.Windows.Rect ImageRegion
        {
            get { return m_imageRegion; }
            set
            {
                m_imageRegion = value;

                System.Windows.Point pt = new System.Windows.Point((double)m_imageRegion.Left, (double)m_imageRegion.Top);
                System.Windows.Size size = new System.Windows.Size((double)m_imageRegion.Width, (double)m_imageRegion.Height);

                System.Windows.Rect rc = new System.Windows.Rect(pt, size);

                UpdateMarker(rc);
                m_bIsRegion = true;
            }
        }

        public string StartupPath
        {
            get { return Scripter.AppPath; }
        }

        public string BigBrother
        {
            get { return m_bigBrother; }
            set { m_bigBrother = value; }
        }


        public string _ret
        {
            get { return /*Pilot._ret*/ string.Empty; }
            set { /*Pilot._ret = value;*/ }
        }

        public System.Windows.Point LeftClickPos
        {
            get { return m_LeftClickPos; }
            set { m_LeftClickPos = value; }
        }

        public bool SaveScreenshotOnError
        {
            get { return false; }
            set { }
        }

        public void ShowTipDialog()
        {
            //Pilot.TipForm.ShowDialog();
        }

        public void UpdateResult(int pos, string value)
        {
            //_frm.ScriptViewUpdateResult(pos, value);
        }

        public bool InitTipDialog(int x, int y, string text, int timeout)
        {
            return false; //Pilot.TipForm.Init(x, y, text, timeout);
        }

        public DialogResult ShowUserInputDialog(string label, string text, ref string retValue)
        {
            FrmUserInput frm = new FrmUserInput();

            frm.lbl = label;
            frm.txt = text;

            DialogResult ret = frm.ShowDialog();

            if (ret == DialogResult.OK)
                retValue = frm.txt;

            return ret;
        }


    }
}