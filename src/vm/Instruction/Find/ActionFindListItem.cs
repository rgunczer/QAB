using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindListItem : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string name = _vm.variables.Get(m_params[1]);

            AutomationElement lstItem = Finder.GetListItem(_vm.host.aeCurrent, name);

            if (null == lstItem)
            {
                _vm.PushError("ListBox Item '" + name + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("ListBox Item '" + name + "' found.");
            _vm.host.aeCurrent = lstItem;
            return (EnumActionResult.OK);
        }
    }
}