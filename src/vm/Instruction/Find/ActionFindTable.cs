using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindTable : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string id = _vm.variables.Get(m_params[1]);

            AutomationElement table = Finder.GetTableByID(_vm.host.TargetWindow, id);

            if (null == table)
            {
                _vm.PushError("Table '" + id + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Table '" + id + "' found.");
            _vm.host.aeCurrent = table;
            return (EnumActionResult.OK);                        
        }
    }
}
