using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;


namespace VM
{
    public class ControlGrid : ControlBase
    {
        private AutomationElementCollection m_aeRows = null;

        private List<Dictionary<string, string>> m_Rows = new List<Dictionary<string, string>>();

        public AutomationElementCollection aeRows
        {
            get { return m_aeRows; }
            set { m_aeRows = value; }
        }

        public List<Dictionary<string, string>> Rows
        {
            get { return m_Rows; }
        }

        public override void Reset()
        {
            m_Rows.Clear();
        }
    }
}
