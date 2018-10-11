using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    public class ActionFindCustom : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string searchType = _vm.variables.Get(m_params[1]);
            string value = _vm.variables.Get(m_params[3]);

            AutomationElement custom = null;
            AutomationElement aeTarget = _vm.host.TargetWindow;

            switch (searchType)
            {
                case "ByID":
                    custom = Finder.GetCustomByID(aeTarget, value);
                    break;

                case "ByName":
                    custom = Finder.GetCustomByName(aeTarget, value);
                    break;

                default:
                    _vm.host.WriteLog(m_type + " Unrecognized Search Type: '" + searchType + "'");
                    return (EnumActionResult.ERROR);
            }

            if (null == custom)
            {
                _vm.PushError("Custom '" + value + "' NOT found on '" + aeTarget.Current.Name + "'.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Custom '" + value + "' found on '" + aeTarget.Current.Name + "'.");
            _vm.host.aeCurrent = custom;
            
            return EnumActionResult.OK;
        }


    }
}