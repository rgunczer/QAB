using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindMenuItem : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string name = _vm.variables.Get(m_params[1]);
            AutomationElement menuItem = Finder.GetMenuItem(_vm.host.aeCurrent, name);

            if (null == menuItem)
            {
                _vm.PushError("MenuItem '" + name + "' NOT found.");
                return (EnumActionResult.ERROR);
            }
            
            _vm.host.WriteLog("MenuItem '" + name + "' found.");
            _vm.host.aeCurrent = menuItem;
            return (EnumActionResult.OK);            
        }
    }
}
