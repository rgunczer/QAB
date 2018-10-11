using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace VM
{
    class ActionUserInputYesNo : ActionBase
    {
        public override EnumActionResult Execute()
        {
            DialogResult res = Util.UtilSys.MessageBoxQuestion(m_params[1]);
            
            if (res == DialogResult.Yes)                        
                _vm.variables.Update(m_params[3], "1");
            else
                _vm.variables.Update(m_params[3], "0");
            
            return EnumActionResult.OK;
        }
    }
}