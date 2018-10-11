using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;


namespace VM
{
    class ActionGetDataGridItemCount: ActionBase
    {
        public override EnumActionResult Execute()
        {
            string varName = m_params[1];

            AutomationElementCollection col = _vm.host.aeCurrent.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem));

            if (null != col)
                _vm.variables.Update(varName, col.Count.ToString());
            else
                _vm.variables.Update(varName, "0");

            return EnumActionResult.OK;
        }
    }
}
