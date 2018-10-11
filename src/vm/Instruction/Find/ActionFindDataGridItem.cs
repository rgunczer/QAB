using System;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Text;
using Util;


namespace VM
{
    class ActionFindDataGridItem : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string name = _vm.variables.Get(m_params[1]);

            AutomationElement elem = Finder.GetDataItemByName(_vm.host.aeCurrent, name);

            if (null == elem)            
                _vm.PushError("DataGridItem '" + name + "' NOT found.");            
            else
            {
                _vm.host.aeCurrent = elem;
                _vm.host.WriteLog("DataGridItem '" + name + "' found.");
                return (EnumActionResult.OK);
            }
            return (EnumActionResult.ERROR);
        }
    }
}
