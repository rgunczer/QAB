using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    public abstract class ActionFlowIterator : ActionFlow
    {
        protected bool m_done = false;
        protected string m_varName = string.Empty;
        protected bool m_initialized = false;
        
        public bool IsDone
        {
            get { return m_done; }
        }
        
        public void Done()
        {
            m_done = true;
            m_initialized = false;

            if (string.Empty != m_varName)
                _vm.variables.Remove(m_varName);        
        }

    }
}
