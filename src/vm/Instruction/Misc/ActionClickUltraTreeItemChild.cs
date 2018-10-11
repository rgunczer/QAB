using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionClickUltraTreeItemChild : ActionBase
    {
        public override EnumActionResult Execute()
        {
            ControlUltraTree utree = _vm.GetUltraTree();

            if (null == utree)
            {
                _vm.host.WriteLog("No ultra tree object.");
                return (EnumActionResult.ERROR);
            }

            string key = _vm.variables.Get(m_params[1]);

            if (false == utree.Children.ContainsKey(key))
            {
                _vm.host.WriteLog("Ultra Tree Children doesn't contain key: '" + key + "'");
                return (EnumActionResult.ERROR);
            }

            AutomationElement treeItem = utree.Children[key];
            _vm.host.UpdateMarker(treeItem.Current.BoundingRectangle);
            UtilAutomation.ClickOn(treeItem.Current.BoundingRectangle, false);

            return (EnumActionResult.OK);
        }
    }
}
