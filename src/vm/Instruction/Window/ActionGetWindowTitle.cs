using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class ActionGetWindowTitle : ActionBase
    {
        public override EnumActionResult Execute()
        {
            if (null == _vm.host.TargetWindow)
            {
                _vm.PushError("Target Window Not Set");
                return EnumActionResult.ERROR;
            }

            string name = _vm.host.TargetWindow.Current.Name;
            string varName = m_params[1];

            _vm.variables.Update(varName, name);
            
            return EnumActionResult.OK;
        }
    }
}
