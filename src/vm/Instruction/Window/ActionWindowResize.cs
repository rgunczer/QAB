using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class ActionWindowResize : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string width = _vm.variables.Get(m_params[1]);
            string height = _vm.variables.Get(m_params[3]);

            pos.X = Convert.ToDouble(width);
            pos.Y = Convert.ToDouble(height);

            UtilAutomation.WindowResize(_vm.host.TargetWindow, pos);
            return (EnumActionResult.OK);
        }
    }
}
