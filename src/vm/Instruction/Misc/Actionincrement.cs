using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class Actionincrement : ActionBase
    {
        public override EnumActionResult Execute()
        {
            if (m_params[0].EndsWith("++"))
            {
                string var = m_params[0].Substring(0, m_params[0].Length - 2);
                int number = Convert.ToInt32(_vm.variables.Get(var));
                ++number;
                _vm.variables.Update(var, number.ToString());
                return EnumActionResult.OK;
            }

            if (m_params[0].EndsWith("--"))
            {
                string var = m_params[0].Substring(0, m_params[0].Length - 2);
                int number = Convert.ToInt32(_vm.variables.Get(var));
                --number;
                _vm.variables.Update(var, number.ToString());
                return EnumActionResult.OK;
            }

            return EnumActionResult.ERROR;            
        }

        public override string ToString()
        {
            return m_params[0];
        }
    }
}
