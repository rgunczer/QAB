using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindPane : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string id = _vm.variables.Get(m_params[1]);

            AutomationElement pne = Finder.GetPane(_vm.host.TargetWindow, id);

            if (null == pne)
            {
                _vm.PushError("Pane '" + id + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Pane '" + id + "' found.");
            _vm.host.aeCurrent = pne;
            return (EnumActionResult.OK);            
        }
    }
}
