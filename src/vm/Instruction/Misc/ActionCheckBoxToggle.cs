using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionCheckBoxToggle : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string state = _vm.variables.Get(m_params[1]);

            switch (state)
	        {
		        case "On":
                    UtilAutomation.ToggleCheckBox(_vm.host.aeCurrent, ToggleState.On);
                break;

                case "Off":
                    UtilAutomation.ToggleCheckBox(_vm.host.aeCurrent, ToggleState.Off);
                break;

                case "Indeter":
                    UtilAutomation.ToggleCheckBox(_vm.host.aeCurrent, ToggleState.Indeterminate);
                break;

                default: // this should never happen -> parse
                    _vm.host.WriteLog("Unrecognized toggle value: '" + state + "'.");
                    return (EnumActionResult.ERROR);
	        }
            return (EnumActionResult.OK);
        }
    }
}
