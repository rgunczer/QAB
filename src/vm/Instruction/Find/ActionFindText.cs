using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindText : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string name = _vm.variables.Get(m_params[1]);

            AutomationElement txt = Finder.GetText(_vm.host.TargetWindow, name);
        
            if (null == txt)
            {
                _vm.PushError("Text '" + name + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Text '" + name + "' found.");
            _vm.host.aeCurrent = txt;
            
            return EnumActionResult.OK;
        }
    }
}
