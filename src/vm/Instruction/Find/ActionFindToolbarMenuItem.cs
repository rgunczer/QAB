using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    public class ActionFindToolbarMenuItem : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string name = _vm.variables.Get(m_params[1]);
            AutomationElementCollection col = Finder.GetToolBarMenuItems(_vm.host.aeCurrent);

            foreach (AutomationElement elem in col)
            {
                if (name == elem.Current.Name)
                {
                    _vm.host.aeCurrent = elem;
                    _vm.host.WriteLog("ToolbarMenuItem: '" + elem.Current.Name + "' found.");
                    return (EnumActionResult.OK);
                }
            }
            
            _vm.PushError("ToolbarMenuItem: '" + name + "' NOT found.");
            return EnumActionResult.ERROR;
        }


    }
}