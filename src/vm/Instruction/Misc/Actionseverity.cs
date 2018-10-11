using System;
using System.Collections.Generic;
using System.Text;

namespace VM
{
    public class Actionseverity : ActionBase
    {
        public override EnumActionResult Execute()
        {


            return EnumActionResult.OK;
        }

        public override string ToString()
        {
            string str = m_params[0];

            if (str.Contains("."))
            {
                string[] arr = str.Split('.');

                if (arr.Length == 2)
                    return arr[1];
                else
                    return str;
            }
            else
                return m_params[0];                          
        }

    }
}