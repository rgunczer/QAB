using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindHyperlink : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string name = _vm.variables.Get(m_params[1]);

            AutomationElement hypLnk = Finder.GetHyperLink(_vm.host.TargetWindow, name);

            if (null == hypLnk)
            {
                _vm.PushError("Hyperlink '" + name + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Hyperlink '" + name + "' found.");
            _vm.host.aeCurrent = hypLnk;            

            return EnumActionResult.OK;
        }
    }
}
