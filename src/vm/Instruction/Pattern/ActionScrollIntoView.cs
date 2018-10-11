using System;
using System.Threading;


namespace VM
{
    class ActionScrollIntoView : ActionBase
    {
        public override EnumActionResult Execute()
        {
            try
            {
                AutomationPattern.ScrollIntoView(_vm.host.aeCurrent);
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
