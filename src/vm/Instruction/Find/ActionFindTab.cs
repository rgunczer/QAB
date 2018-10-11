using System;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Text;
using Util;


namespace VM
{
    class ActionFindTab : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string tabID = _vm.variables.Get(m_params[1]);

            AutomationElement tab = Finder.GetTabByID(_vm.host.TargetWindow, tabID);

            if (null == tab)
            {
                _vm.PushError("Tab '" + tabID + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Tab '" + tabID + "' found.");
            _vm.host.aeCurrent = tab;
            return (EnumActionResult.OK);
        }
    }
}
