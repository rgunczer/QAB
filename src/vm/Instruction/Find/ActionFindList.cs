using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindList : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string lstID = _vm.variables.Get(m_params[1]);

            AutomationElement lst = Finder.GetList(_vm.host.TargetWindow, lstID);

            if (null == lst)
            {
                _vm.PushError("ListBox '" + lstID + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("ListBox '" + lstID + "' found.");
            _vm.host.aeCurrent = lst;
            return (EnumActionResult.OK);
        }
    }
}
