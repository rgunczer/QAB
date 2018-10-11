using System;
using System.Collections.Generic;
using System.Text;
using Util;
using System.Windows.Automation;


namespace VM
{
    class ActionSetCellValue : ActionBase
    {
        public override EnumActionResult Execute()
        {
            try
            {
                string key = string.Empty;

                int indexToFind = int.Parse(_vm.variables.Get(m_params[1]));

                ControlGrid grd = _vm.GetGrid();

                _vm.host.aeCurrent = grd.aeRows[indexToFind];

                _vm.host.UpdateMarker(_vm.host.aeCurrent.Current.BoundingRectangle);
                AutomationPattern.ScrollIntoView(_vm.host.aeCurrent);
                _vm.host.aeCurrent.SetFocus();


                AutomationElement row = grd.aeRows[indexToFind];
                AutomationElementCollection cells = row.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text));
                                
                key = _vm.variables.Get(m_params[3]);

                Dictionary<string, string> dic = grd.Rows[indexToFind];

                int j = 0;

                foreach(string k in dic.Keys)		
                {
                    if (k == key)
                    {
                        UtilAutomation.ClickOn(cells[j].Current.BoundingRectangle, true);
                        UtilSys.Wait(500);

                        string value = _vm.variables.Get(m_params[5]);
                        value += "+{ENTER}";
                        System.Windows.Forms.SendKeys.SendWait(value);
                                                
                        return EnumActionResult.OK;
                    }
                    ++j;
                }
                return EnumActionResult.ERROR;            
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
