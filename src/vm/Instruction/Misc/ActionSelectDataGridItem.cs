using System;
using System.Collections.Generic;
using System.Text;
using Util;


namespace VM
{
    class ActionSelectDataGridItem : ActionBase
    {
        public override EnumActionResult Execute()
        {
            try
            {
                int indexToFind = int.Parse(_vm.variables.Get(m_params[1]));

                ControlGrid grd = _vm.GetGrid();

                _vm.host.aeCurrent = grd.aeRows[indexToFind];

                _vm.host.UpdateMarker(_vm.host.aeCurrent.Current.BoundingRectangle);                    
                AutomationPattern.ScrollIntoView(_vm.host.aeCurrent);
                _vm.host.aeCurrent.SetFocus();
                return (EnumActionResult.TEST_OK);                                                                                        
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
