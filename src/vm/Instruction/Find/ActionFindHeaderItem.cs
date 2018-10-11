using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindHeaderItem : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string headerItemName = _vm.variables.Get(m_params[1]);

            AutomationElement headerItem = Finder.GetHeaderItem(_vm.host.TargetWindow, headerItemName);

            if (null == headerItem)
            {
                _vm.PushError("HeaderItem '" + headerItemName + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("HeaderItem '" + headerItemName + "' found.");
            _vm.host.aeCurrent = headerItem;
            return (EnumActionResult.OK);
        }
    }
}
