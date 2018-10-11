using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    public abstract class ControlBase
    {
        private string m_name;

        public string Name
        {
            get { return (m_name); }
            set { m_name = value; }
        }

        public abstract void Reset();
    }
}
