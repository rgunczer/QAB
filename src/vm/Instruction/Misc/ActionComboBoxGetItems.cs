using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionComboBoxGetItems : ActionBase
    {
        public override EnumActionResult Execute()
        {
            AutomationElementCollection col = UtilAutomation.GetComboBoxItems(_vm.host.aeCurrent);

            if (null != col)
            {
                _vm.host.WriteLog("Listing ComboBoxItems: " + col.Count);
                _vm.host.WriteLog("----------------------------- Begin");

                foreach (AutomationElement item in col)
                {
                    _vm.host.WriteLog(item.Current.Name);
                }
                _vm.host.WriteLog("----------------------------- End");
                return (EnumActionResult.OK);
            }
            else
            {
                _vm.host.WriteLog("No ComboBox items.");
            }
            return (EnumActionResult.ERROR);
        }  
    }
}
