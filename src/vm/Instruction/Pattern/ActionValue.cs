using System;
using System.Threading;
using Util;


namespace VM
{
    class ActionValue : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string value = _vm.variables.Get(m_params[1]);

            try
            {
                AutomationPattern.Value(_vm.host.aeCurrent, value);
                Thread.Sleep(10);
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
