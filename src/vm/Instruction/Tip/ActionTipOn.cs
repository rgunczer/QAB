using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace VM
{
    class ActionTipOn : ActionTipClient
    {        
        public override EnumActionResult Execute()
        {                        
            System.Windows.Point pt = new System.Windows.Point();
            pt.X = (int)_vm.host.aeCurrent.Current.BoundingRectangle.Left + ((int)_vm.host.aeCurrent.Current.BoundingRectangle.Width/2);
            pt.Y = (int)_vm.host.aeCurrent.Current.BoundingRectangle.Top + ((int)_vm.host.aeCurrent.Current.BoundingRectangle.Height / 2);

            Point ptScreen = UtilAutomation.Convert2Client(_vm.host.TargetWindow, pt);
                        
            if ( false == _vm.host.InitTipDialog(ptScreen.X, ptScreen.Y, m_params[3], Convert.ToInt32(m_params[1])))
                return EnumActionResult.ERROR;

            _vm.host.ShowTipDialog();           

            return EnumActionResult.OK;
        }

    }
}
