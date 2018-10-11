using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class ActionGoto : ActionFlow
    {        
        public string jumpCount // yes, it's a string!
        {
            get { return m_params[3]; }
        }
       
        public string label
        {
            get { return m_params[1]; }
        }

        public override EnumActionResult Execute()
        {
            return (EnumActionResult.OK);
        }

        public override string ToString()
        {
            if ("0" == m_params[3])
                return ("'" + m_params[1] + "', FOREVER");
            else
                return ("'" + m_params[1] + "', " + m_params[3] + " times.");
        }

    }
}
