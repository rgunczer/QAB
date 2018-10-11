using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;


namespace VM
{
    class ActionComboBoxMakeSelection : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string name = _vm.variables.Get(m_params[1]);

            AutomationElementCollection col = UtilAutomation.GetComboBoxItems(_vm.host.aeCurrent);

            foreach (AutomationElement elem in col)
            {
                if (name == elem.Current.Name)
                {
                    UtilAutomation.MakeSelectionItem(elem);
                    return (EnumActionResult.OK);
                }
            }
            return (EnumActionResult.OK);
        }
    }
}
