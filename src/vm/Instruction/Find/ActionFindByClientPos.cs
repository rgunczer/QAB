using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindByClientPos : ActionBase
    {
        public override EnumActionResult Execute()
        {
            int x = 0;
            int y = 0;
            AutomationElement control = null;

            if (m_params.Count == 1) // no extra params, use mouse current pos
            {
                x = System.Windows.Forms.Cursor.Position.X;
                y = System.Windows.Forms.Cursor.Position.Y;
                                
                control = Finder.GetControlFromScreenPos(x, y);
            }
            else
            {
                x = Convert.ToInt32( _vm.variables.Get(m_params[1]));
                y = Convert.ToInt32( _vm.variables.Get(m_params[3]));

                control = Finder.GetControlFromClientPos(_vm.host.TargetWindow, x, y);
            }
            
            if (null == control)
            {
                _vm.PushError("Control at pos (" + x.ToString() + "," + y.ToString() + ") NOT found.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Control at pos (" + x.ToString() + ", " + y.ToString() + ") found.");
            _vm.host.WriteLog("Name: " + control.Current.Name);
            _vm.host.WriteLog("AutomationID: " + control.Current.AutomationId);
            _vm.host.WriteLog("--------------");

            _vm.host.aeCurrent = control;

            return EnumActionResult.OK;
        }

    }
}
