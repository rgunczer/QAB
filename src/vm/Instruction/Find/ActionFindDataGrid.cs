using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindDataGrid : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string id = _vm.variables.Get(m_params[1]);
            AutomationElement grid = Finder.GetDataGrid(_vm.host.TargetWindow, id);

            if (null == grid)
            {
                _vm.PushError("Grid '" + id + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Grid '" + id + "' found.");
            _vm.host.aeCurrent = grid;
            return (EnumActionResult.OK);
        }
    }
}
