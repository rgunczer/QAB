using System;
using System.Collections.Generic;
using System.Text;
using Util;


namespace VM
{
    class ActionTestEditBox : ActionBase
    {
        public override EnumActionResult Execute()
        {            
            string curValue = UtilAutomation.GetText(_vm.host.aeCurrent);
            string expValue = _vm.variables.Get(m_params[1]);

            string msg = "Test -> Current/Expected ('" + curValue + "'/'" + expValue + "')";

            if (curValue != expValue)
            {
                _vm.host.WriteLog(msg + " - Failed.");
                return (EnumActionResult.TEST_FAILED);
            }
            _vm.host.WriteLog(msg + " - Succeeded.");
            return (EnumActionResult.TEST_OK);
        }
    }
}
