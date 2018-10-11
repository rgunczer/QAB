using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindToolbar : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string id = _vm.variables.Get(m_params[1]);
            AutomationElement toolBar = Finder.GetToolBarByID(_vm.host.TargetWindow, id);

            if (null == toolBar)
            {
                _vm.PushError("ToolBar '" + id + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("ToolBar '" + id + "' found.");
            _vm.host.aeCurrent = toolBar;

            return (EnumActionResult.OK);
        }
    }
}
