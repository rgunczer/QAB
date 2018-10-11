using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class ActionWindowMove : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string posX = _vm.variables.Get(m_params[1]);
            string posY = _vm.variables.Get(m_params[3]);

            pos.X = Convert.ToDouble(posX);
            pos.Y = Convert.ToDouble(posY);

            UtilAutomation.WindowMove(_vm.host.TargetWindow, pos);
            return (EnumActionResult.OK);
        }
    }
}
