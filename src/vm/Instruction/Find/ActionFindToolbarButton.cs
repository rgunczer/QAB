using System;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Text;
using Util;


namespace VM
{
    class ActionFindToolbarButton : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string name = _vm.variables.Get(m_params[1]);
            AutomationElementCollection col = Finder.GetToolBarButtons(_vm.host.aeCurrent);
           
            foreach (AutomationElement elem in col)
            {
                if (name == elem.Current.Name)
                {
                    _vm.host.aeCurrent = elem;
                    _vm.host.WriteLog("ToolbarButton: '" + elem.Current.Name + "' found.");
                    return (EnumActionResult.OK);
                }
            }

            // workaround, sometimes this simply isn't enough to find that button inside a control            
            System.Windows.Point p = new System.Windows.Point();
            AutomationElement ae = null;

            for (int i = (int)_vm.host.aeCurrent.Current.BoundingRectangle.Left; i < (int)_vm.host.aeCurrent.Current.BoundingRectangle.Right ; i+=10)
            {                                                
                p.X = i;
                p.Y = _vm.host.aeCurrent.Current.BoundingRectangle.Top + 2;

                System.Windows.Point pos = p;                
                UtilAutomation.SetCursorPos(p);

                ae = AutomationElement.FromPoint(pos);

                UtilSys.Wait(250);

                if (null != ae)
                {
                    _vm.host.UpdateMarker(ae.Current.BoundingRectangle);                    

                    if (ae.Current.Name == name)
                    {
                        if (ae.Current.ControlType == ControlType.Button)
                        {
                            _vm.host.aeCurrent = ae;
                            _vm.host.WriteLog("ToolbarButton: '" + ae.Current.Name + "' found.");
                            return (EnumActionResult.OK);                            
                        }
                    }
                }
            }

            _vm.PushError("ToolbarButton: '" + name + "' NOT found.");
            return (EnumActionResult.ERROR);
        }
    }
}