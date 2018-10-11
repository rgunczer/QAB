using System;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindTree : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string id = _vm.variables.Get(m_params[1]);
            AutomationElement tree = UtilAutomation.FindTree(_vm.host.TargetWindow, id);

            if (null == tree)
            {
                _vm.PushError("Tree '" + id + "' NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Tree '" + id + "' found.");
            _vm.host.aeCurrent = tree;
            return (EnumActionResult.OK);
        }
    }
}
