using System;
using System.Collections.Generic;
using System.Text;
using Util;


namespace VM
{
    public class Actionfunction: ActionFlow
    {
        private string m_name;
        private List<string> m_parameters = new List<string>();        

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public List<string> Parameters
        {
            get { return m_parameters; }
        }

        public override EnumActionResult Execute()
        {
            return (EnumActionResult.OK);
        }

        public void AddParam(string name)
        {
            m_parameters.Add(name);         
        }

        public override string ToString()
        {
            string str = m_name + "(";

            foreach (string item in m_parameters)
	        {
		        str += item + ", ";
	        }

            if (str[str.Length-1] != '(' )
                str = str.Substring(0, str.Length - 2);                                

            str = str.Trim();
            str += ")";

            return ( str );
        }

    }
}