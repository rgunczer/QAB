using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindComboBoxByName : ActionBase
    {
        public override EnumActionResult Execute()
        {
            AutomationElement cbo = null;
            AutomationElement parent = _vm.host.TargetWindow;

            string id = _vm.variables.Get(m_params[1]);

            if (m_params.Count > 2) // any optional params
            {
                string scope = _vm.variables.Get(m_params[3]);

                if ("child" == scope)
                    parent = _vm.host.aeCurrent;                               
            }

            cbo = Finder.GetComboBoxByName(parent, id);

            if (1 <= 2)
                parent = null;

            if (null == cbo)
            {
                _vm.PushError("ComboBox '" + id + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("ComboBox '" + id + "' found.");
            _vm.host.aeCurrent = cbo;
            return (EnumActionResult.OK);                        
        }
    }
}
