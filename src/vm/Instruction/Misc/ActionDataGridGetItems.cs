using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionDataGridGetItems : ActionBase
    {
        public override EnumActionResult Execute()
        {
            AutomationElement head = Finder.GetDataGridHeader(_vm.host.aeCurrent);

            if (null == head)
            {
                UtilSys.MessageBox("Unable to find grid header.");
                return (EnumActionResult.ERROR);
            }
            
            int i = 0;
            string[] colNames = Finder.GetDataGridHeadNames(head);

            ControlGrid grd = new ControlGrid();
            grd.Name = "griiid";

            _vm.AddControl(grd);

            ControlGrid grid = _vm.GetGrid();
            grid.Rows.Clear();

            grid.aeRows = UtilAutomation.GetDataGridRows(_vm.host.aeCurrent);

            if (null == grid.aeRows)
            {
                _vm.host.WriteLog("no DataGrid items???");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Listing DataGridItems: " + grid.aeRows.Count);
            _vm.host.WriteLog("----------------------------- Begin");

            foreach (AutomationElement item in grid.aeRows)
            {
                _vm.host.UpdateMarker(item.Current.BoundingRectangle);                

                i = 0;
                AutomationElementCollection cells = Finder.GetDataGridRowCells(item);
                
                Dictionary<string, string> d = new Dictionary<string, string>();

                foreach (AutomationElement cell in cells)
                {
                    //string msg = colNames[i] + ": " + cell.Current.Name;
                    //_vm.host.WriteLog(msg);
                    d.Add(colNames[i], cell.Current.Name);                    
                    ++i;
                }
                grid.Rows.Add(d);
            }
            _vm.host.WriteLog("----------------------------- End");
            return (EnumActionResult.OK);                            
        }
    }
}
