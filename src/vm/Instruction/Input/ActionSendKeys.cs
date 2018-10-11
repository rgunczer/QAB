using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace VM
{
    class ActionSendKeys : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string value = _vm.variables.Get(m_params[1]);

            System.Windows.Forms.SendKeys.SendWait(value);

            return (EnumActionResult.OK);
        }        
    }
}
