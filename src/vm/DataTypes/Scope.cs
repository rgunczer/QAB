using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    public class Scope
    {
        public Dictionary<string, Variable> parsedVariables = new Dictionary<string, Variable>();
        public Dictionary<string, Variable> parsedConstants = new Dictionary<string, Variable>();

        // vars
        private string m_name;
        private Dictionary<string, string> m_variables = null;
        private Dictionary<string, string> m_consts = null;

        // prop
        public string Name
        {
            get { return m_name; }
        }

        public Dictionary<string, string> Variables
        {
            get { return m_variables; }
        }

        public Dictionary<string, string> Consts
        {
            get { return m_consts; }
        }

        // ctor
        public Scope(string name)
        {
            m_name = name;
            m_variables = new Dictionary<string, string>();
            m_consts = new Dictionary<string, string>();
        }

        public Scope(string name, Dictionary<string, string> vars)
        {
            m_name = name;
            m_variables = vars;
            m_consts = new Dictionary<string, string>();
        }
    }
}