using System;
using System.Collections.Generic;
using System.Text;
using Util;


namespace VM
{
    class ActionWindowVisualState : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string value = _vm.variables.Get(m_params[1]);
            
            switch (value)
            {
                case "Min":
                    UtilAutomation.SetWindowVisualState(_vm.host.TargetWindow, System.Windows.Automation.WindowVisualState.Minimized);
                break;

                case "Max":
                    UtilAutomation.SetWindowVisualState(_vm.host.TargetWindow, System.Windows.Automation.WindowVisualState.Maximized);
                break;

                case "Norm":
                    UtilAutomation.SetWindowVisualState(_vm.host.TargetWindow, System.Windows.Automation.WindowVisualState.Normal);
                break;

                default:
                    _vm.host.WriteLog("Unrecognized visaul state: ('" + value + "')");
                    return (EnumActionResult.ERROR);
            }
            return (EnumActionResult.OK);
        }
    }
}
