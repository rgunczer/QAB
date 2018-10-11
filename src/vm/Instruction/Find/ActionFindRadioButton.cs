using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindRadioButton : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string id = _vm.variables.Get(m_params[1]);
            AutomationElement radio = Finder.GetRadioButton(_vm.host.TargetWindow, id);

            if (null == radio)
            {
                _vm.host.WriteLog("RadioButton '" + id + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.PushError("RadioButton '" + id + "' found.");
            _vm.host.aeCurrent = radio;
            return (EnumActionResult.OK);
        }
    }
}