using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace VM
{
    class ActionUserInput : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string label = _vm.variables.Get(m_params[1]);;
            string text = _vm.variables.Get(m_params[3]);
            string retValue = string.Empty;

            if (_vm.host.ShowUserInputDialog(label, text, ref retValue) == DialogResult.OK)
            {
                _vm.variables.Update(m_params[3], retValue);
                return EnumActionResult.OK;
            }                        
            return EnumActionResult.ERROR;            
        }
    }
}