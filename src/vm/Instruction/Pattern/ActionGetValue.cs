using System;
using System.Threading;
using Util;


namespace VM
{
    class ActionGetValue : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string varName = m_params[1];

            try
            {
                string value = AutomationPattern.GetValue(_vm.host.aeCurrent);
                _vm.variables.Update(varName, value);
            }
            catch (System.Exception ex)
            {
                _vm.host.WriteLog(ex.Message);
                return (EnumActionResult.ERROR);
            }
            return (EnumActionResult.OK);
        }
    }
}
