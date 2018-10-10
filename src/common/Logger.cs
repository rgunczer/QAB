using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Util
{
    public class Logger
    {
        private string m_path;
        private bool m_bTimeStamp;
        private bool m_bIsOK;
        private string m_version;
        private string m_name;
        private string m_level = "ALL";
        private string m_BigBrother = string.Empty;


        public string path
        {
            get { return m_path; }
        }

        public bool IsOK
        {
            get { return m_bIsOK; }
        }

        public string Level
        {
            get { return m_level;  }
            set { m_level = value; }
        }
        
        public void New()
        {
            try
            {
                using (StreamWriter SW = File.CreateText(path))
                {
                }               

                DateTime dt = DateTime.Now;

                string dateStr = UtilSys.GetDateTime();
                string head = "<" + m_name + " version=\"" + m_version + "\" date=\"" + dateStr + "\">";

                m_bIsOK = true;

                Write(head);     
            }
            catch
            {
                m_bIsOK = false;
            }
        }

        public void Clear()
        {
            New();
        }

        public Logger(string name, string version, string path, string bigbrother)
        {
            m_name = name;
            m_version = version;            
            m_path = path;
            m_BigBrother = bigbrother;

            m_bTimeStamp = false;

            New();
        }

        ~Logger()
        {
            try
            {                
                Write("<Exit Code=\"" + System.Environment.ExitCode.ToString()+ "\"/>");
                Write("</" + m_name + ">");                
            }
            catch
            {
                m_bIsOK = false;
            }
        }

        public void WriteException(Exception ex)
        {
            Write("<ERROR src=\"Exception\" message=\"" + ex.Message + "\">");
            Write(ex.StackTrace);
            Write("</ERROR>");
        }

        public void Write(string msg)
        {
            if (!m_bIsOK) // logger is not ready to write
                return;

            if (!string.IsNullOrEmpty(m_BigBrother))
                SharedMemory.Send(m_BigBrother, "PilotOutput|" + msg);

            using (StreamWriter sw = File.AppendText(m_path))
            {
                if (m_bTimeStamp)
                {
                    DateTime dt = DateTime.Now; // timestamp
                    sw.WriteLine("Time: " + dt.Hour + ":" + dt.Minute + ":" + dt.Second + " - " + msg);
                }
                else
                {
                    sw.WriteLine(msg);
                    //sw.WriteLine("<step>" + msg + "</step>");
                }
            }
        }
    }
}
