using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;


namespace VM
{
    public class ControlUltraTree : ControlBase
    {
        private Dictionary<string, AutomationElement> m_Roots = new Dictionary<string, AutomationElement>();
        private Dictionary<string, AutomationElement> m_Children = new Dictionary<string, AutomationElement>();

        public Dictionary<string, AutomationElement> Roots
        {
            get { return m_Roots; }
        }

        public Dictionary<string, AutomationElement> Children
        {
            get { return m_Children; }
        }

        public override void Reset()
        {
            m_Roots.Clear();
            m_Children.Clear();
        }

    }
}
