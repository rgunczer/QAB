using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionClickUltraTreeItemRoot : ActionBase
    {
        public override EnumActionResult Execute()
        {
            ControlUltraTree utree = (ControlUltraTree)_vm.GetUltraTree();

            if (null == utree)
            {
                _vm.host.WriteLog("No ultra tree object.");
                return (EnumActionResult.ERROR);
            }

            string key = _vm.variables.Get(m_params[1]);

            if (false == utree.Roots.ContainsKey(key))
            {
                _vm.host.WriteLog("Ultra Tree Roots doesn't contain key: '" + key + "'");
                return (EnumActionResult.ERROR);
            }

            AutomationElement treeItem = utree.Roots[key];
            _vm.host.UpdateMarker(treeItem.Current.BoundingRectangle);
            UtilAutomation.ClickOn(treeItem.Current.BoundingRectangle, true);

            return (EnumActionResult.OK);
        }
    }
}
