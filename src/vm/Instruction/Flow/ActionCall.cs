using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    public class Actioncall : ActionFlow
    {
        private string m_name = null;

        private List<string> m_parameters = new List<string>();

        public string FunctionName
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

        public override string ToString()
        {            
            string str = m_name + "(";

            foreach (string item in m_parameters)
            {
                if (item.Length > 0)
                {
                    if ('@' == item[0])
                        str += item;
                    else
                        str += "'" + item + "'";
                }

                if (m_parameters.Count > 1)
                    str += ", ";
            }

            if (m_parameters.Count > 1)
                str = str.Substring(0, str.Length - 2);

            str += ")";

            return (str);
        }
    }
}
