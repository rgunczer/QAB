using System;
using System.Collections.Generic;
using System.Text;
using Util;


namespace VM
{
    public enum EnumActionResult
    {
        NOT_EXECUTED = 0,
        OK = 1,
        ERROR = 2,
        STOPPED = 3,
        TIMEOUT = 4,
        TEST_FAILED = 5,
        TEST_OK = 6,
    };

    public abstract class ActionBase
    {
        protected static VirtualMachine _vm = null;  // everyone will know about the VM
        public static VirtualMachine vm
        {
            set { _vm = value; }
        }

        public ActionBase() 
        {
            m_type += this.GetType().ToString();
            m_type = m_type.Replace("VM.Action", "");
            m_raw = string.Empty;
            m_status = EnumActionResult.NOT_EXECUTED;
        }

        protected EnumActionResult m_status;
        protected string m_type;
        protected List<string> m_params = new List<string>();
        protected string m_comment;        

        protected string m_raw;
        protected int m_LineNumber = -1;
        protected string m_path = string.Empty;
                        
        public System.Windows.Point pos;

        public List<string> Params
        {
            get { return m_params; }
        }

        public EnumActionResult Result
        {
            get { return m_status; }
            set { m_status = value; }
        }

        public string Type
        {
            get { return m_type; }
        }

        public string RawCmdLine
        {
            get { return m_raw; }
            set { m_raw = value; }
        }

        public string Comment
        {
            get { return m_comment; }
            set { m_comment = value.Trim(); }
        }

        public int LineNumber
        {
            get { return m_LineNumber; }
            set { m_LineNumber = value; }
        }

        public string Path2Script
        {
            get { return m_path; }
            set { m_path = value; }
        }

        public string StatusString
        {
            get
            {
                switch (m_status)
                {
                    case EnumActionResult.NOT_EXECUTED:
                        return ("*");

                    case EnumActionResult.OK:
                        return ("OK");

                    case EnumActionResult.ERROR:
                        return ("ERR");

                    case EnumActionResult.STOPPED:
                        return ("STP");

                    case EnumActionResult.TIMEOUT:
                        return ("OUT");

                    case EnumActionResult.TEST_FAILED:
                        return ("FAIL");

                    case EnumActionResult.TEST_OK:
                        return ("OK");

                    default:
                        return ("UNK");
                }
            }
        }        

        public virtual void AddParams(List<string> tokens)
        {
            foreach(string item in tokens)
            {
                m_params.Add(item);
            }
        }

        public abstract EnumActionResult Execute();        
       
        public virtual string[] Dump()
        {
            string[] tmp = new string[6];

            try
            {
                tmp[0] = m_type;
                tmp[1] = this.ToString();
                tmp[2] = m_comment;
                tmp[3] = m_raw;
                tmp[4] = m_LineNumber.ToString();
                tmp[5] = m_path;

                return (tmp);
            }
            catch (Exception ex)
            {
                UtilSys.MessageBoxError(m_type + "->Dump: " + ex.Message);
                throw;
            }            
        }

        public override string ToString()
        {
            string tmp = string.Empty;

            for (int i = 1; i < m_params.Count; ++i)
            {
                tmp += " " + m_params[i];
            }
            return (tmp.Trim());
        }
    }
}