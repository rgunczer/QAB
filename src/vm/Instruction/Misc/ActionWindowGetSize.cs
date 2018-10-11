using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace VM
{
    class ActionWindowGetSize : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string varWidht = m_params[1];
            string varHeight = m_params[3];

            int widht = Convert.ToInt32(_vm.host.TargetWindow.Current.BoundingRectangle.Width);
            int height = Convert.ToInt32(_vm.host.TargetWindow.Current.BoundingRectangle.Height);
            
            _vm.variables.Update(varWidht, widht.ToString());
            _vm.variables.Update(varHeight, height.ToString());

            return EnumActionResult.OK;
        }
    }
}
