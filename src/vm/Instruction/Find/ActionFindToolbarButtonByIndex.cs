using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindToolbarButtonByIndex : ActionBase
    {
        public override EnumActionResult Execute()
        {
            try 
	        {
                string strIndex = _vm.variables.Get(m_params[1]);
                int index = Convert.ToInt32(strIndex);

                AutomationElementCollection col = Finder.GetToolBarButtons(_vm.host.aeCurrent);
                                
                _vm.host.aeCurrent = col[index];
            
                _vm.host.WriteLog("ToolbarButton: '" + _vm.host.aeCurrent.Current.Name + "' found.");

                return (EnumActionResult.OK);
	        }
	        catch (Exception ex)
	        {
                _vm.PushError("FindToolbarButtonByIndex: " + ex.Message);
                return EnumActionResult.ERROR;		
	        }                                    
        }
        
    }
}