using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindTreeItem : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string name = _vm.variables.Get(m_params[1]);

            AutomationElement treeItem = UtilAutomation.FindTreeItem(_vm.host.aeCurrent, name);

            if (null == treeItem)
            {
                _vm.PushError("TreeItem '" + name + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("TreeItem '" + name + "' found.");
            _vm.host.aeCurrent = treeItem;
            return (EnumActionResult.OK);            
        }
    }
}
