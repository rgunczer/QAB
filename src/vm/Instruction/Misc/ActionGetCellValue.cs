using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionGetCellValue : ActionBase
    {
        public override EnumActionResult Execute()
        {
            try
            {                
                string key = string.Empty;

                int indexToFind = int.Parse(_vm.variables.Get(m_params[1]));

                ControlGrid grd = _vm.GetGrid();
                
                if (indexToFind >= grd.aeRows.Count)
                {
                    _vm.host.WriteLog("GetCellValue Requested index is greater [" + indexToFind + "] than number of rows is: " + grd.aeRows.Count);
                    return EnumActionResult.ERROR;
                }
                
                _vm.host.aeCurrent = grd.aeRows[indexToFind];

                _vm.host.UpdateMarker(_vm.host.aeCurrent.Current.BoundingRectangle);
                AutomationPattern.ScrollIntoView(_vm.host.aeCurrent);
                _vm.host.aeCurrent.SetFocus();
                
                key = _vm.variables.Get(m_params[3]);

                Dictionary<string, string> dic = grd.Rows[indexToFind];
                
                AutomationElement row = grd.aeRows[indexToFind];
                AutomationElementCollection cells = row.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text));
                
                int j = 0;
                foreach(string k in dic.Keys)
                {
                    if (k == key)
                    {
                        string value = cells[j].Current.Name;
                        _vm.variables.Update(m_params[5], value);

                        return EnumActionResult.OK;
                    }
                    ++j;
                }

                _vm.host.WriteLog("GetCellValue Column [" + key + "] NOT found");

                return EnumActionResult.ERROR;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
