using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindHeader : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string headerID = _vm.variables.Get(m_params[1]);

            AutomationElement head = Finder.GetHeader(_vm.host.TargetWindow, headerID);

            if (null == head)
            {
                _vm.host.WriteLog("Header '" + headerID + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Header '" + headerID + "' found.");
            _vm.host.aeCurrent = head;
            return (EnumActionResult.OK);
        }
    }
}
