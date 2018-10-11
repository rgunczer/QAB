using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace VM
{
    class ActionCloseWindow : ActionBase
    {
        public override EnumActionResult Execute()
        {
            try
            {
                UtilAutomation.CloseWindow(_vm.host.TargetWindow);
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
