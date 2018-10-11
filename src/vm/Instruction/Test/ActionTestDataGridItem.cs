using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionTestDataGridItem : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string column = _vm.variables.Get(m_params[1]);
            string value = _vm.variables.Get(m_params[3]);

            string[] columns = column.Split('|');
            string[] values = value.Split('|');
            bool[] founds = new bool[columns.Length];
            
            ControlGrid grd = _vm.GetGrid();
            List<Dictionary<string, string>> rows = grd.Rows;

            int i = 0;

            foreach (Dictionary<string, string> item in rows)
            {
                for (int j = 0; j < founds.Length; ++j)
                    founds[j] = false;

                int colIndex = 0;

                foreach (string col in columns)
                {
                    if (!item.ContainsKey(col))
                    {
                        _vm.host.WriteLog("Column '" + col + "' NOT found.");
                        return EnumActionResult.ERROR;
                    }

                    if (values[colIndex] == item[col])
                    {
                        _vm.host.WriteLog("Item '" + item[col] + "' found in column '" + columns[colIndex] + "'");
                        founds[colIndex] = true;
                    }
                    ++colIndex;
                }
                
                bool found = false;
                
                for (int k = 0; k < founds.Length; ++k)                
                    found = founds[k];                

                if (found)
                {
                    _vm.host.UpdateMarker(grd.aeRows[i].Current.BoundingRectangle);
                    _vm.host.aeCurrent = grd.aeRows[i];
                    AutomationPattern.ScrollIntoView(_vm.host.aeCurrent);
                    _vm.host.aeCurrent.SetFocus();                    
                    return (EnumActionResult.TEST_OK);
                }
                ++i;
            }
            _vm.host.WriteLog("Item NOT found.");
            return (EnumActionResult.TEST_FAILED);            
        }     
    }
}
