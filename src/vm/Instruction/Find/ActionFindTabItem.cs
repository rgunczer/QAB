using System;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Text;
using Util;


namespace VM
{
    class ActionFindTabItem : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string name = _vm.variables.Get(m_params[1]);
            AutomationElement tabItem = Finder.GetTabItemByName(_vm.host.aeCurrent, name);

            if (null == tabItem)
            {
                _vm.PushError("TabItem '" + name + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("TabItem '" + name + "' found.");
            _vm.host.aeCurrent = tabItem;
            return (EnumActionResult.OK);
        }
    }
}
