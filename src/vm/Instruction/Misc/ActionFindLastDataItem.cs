using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;


namespace VM
{
    class ActionFindLastDataItem : ActionBase
    {
        public override EnumActionResult  Execute()
        {
            AutomationElementCollection col = _vm.host.aeCurrent.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem));

            if (null != col)
            {
                if (col.Count > 0)
                    _vm.host.aeCurrent = col[col.Count - 1];
            }
            else
                _vm.host.WriteLog("No items in DataGrid");

            return EnumActionResult.OK;
        }
    }
}
