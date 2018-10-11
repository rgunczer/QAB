using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindCheckBox : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string id = _vm.variables.Get(m_params[1]);

            AutomationElement chk = Finder.GetCheckBox(_vm.host.TargetWindow, id);

            if (null == chk)
            {
                _vm.PushError("CheckBox '" + id + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("CheckBox '" + id + "' found.");
            _vm.host.aeCurrent = chk;
            return (EnumActionResult.OK);
        }
    }
}
