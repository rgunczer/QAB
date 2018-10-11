using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindButton : ActionBase
    {
        public override EnumActionResult Execute()
        {            
            string searchType = _vm.variables.Get(m_params[1]);
            string value = _vm.variables.Get(m_params[3]);
            
            AutomationElement button = null;
            AutomationElement aeTarget = _vm.host.TargetWindow;

            switch (searchType)
            {
                case "ByID":
                    button = Finder.GetButtonByID(aeTarget, value);
                break;

                case "ByName":
                    button = Finder.GetButtonByName(aeTarget, value);
                break;

                default:
                    _vm.host.WriteLog(m_type + " Unrecognized Search Type: '" + searchType + "'");
                    return (EnumActionResult.ERROR);                                    
            }
            
            if (null == button)
            {
                _vm.PushError("Button '" + value + "' NOT found on '" + aeTarget.Current.Name + "'.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Button '" + value + "' found on '" + aeTarget.Current.Name + "'.");
            _vm.host.aeCurrent = button;
            
            return (EnumActionResult.OK);
        }
    }
}
