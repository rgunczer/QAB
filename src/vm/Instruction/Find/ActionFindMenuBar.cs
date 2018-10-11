using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindMenuBar : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string automationID = _vm.variables.Get(m_params[1]);

            AutomationElement ae = Finder.FindMenuBar(_vm.host.TargetWindow, automationID);

            if (null == ae)
            {
                _vm.PushError("MenuBar '" + automationID + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("MenuBar '" + automationID + "' found.");
            _vm.host.aeCurrent = ae;

            return EnumActionResult.OK;
        }
    }
}
