using System;
using System.Collections.Generic;
using System.Text;
using Util;


namespace VM
{
    class ActionTestDocumentNotContain : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string forbiddenValue = _vm.variables.Get(m_params[1]);
            string curValue = UtilAutomation.GetText(_vm.host.aeCurrent);

            if (curValue.Contains(forbiddenValue))
            {
                _vm.host.WriteLog(curValue);
                _vm.host.WriteLog("Contain text: '" + forbiddenValue + "'");
                return (EnumActionResult.TEST_FAILED);
            }
            else
                return (EnumActionResult.TEST_OK);            
        }
    }
}
