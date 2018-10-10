using System;
using System.Collections.Generic;
using System.Text;


namespace Scripter
{
    class Command
    {
        private string m_name;
        private int m_index;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public int Index
        {
            get { return m_index; }
            set { m_index = value; }
        }

        public Command(string name, int index)
        {
            m_name = name;
            m_index = index;
        }

        public override string ToString()
        {
            return m_name;
        }
    }
}
