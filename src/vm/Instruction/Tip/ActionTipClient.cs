using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace VM
{
    class ActionTipClient : ActionBase
    {        
        public override EnumActionResult Execute()
        {
            try
            {
                System.Windows.Point pt = new System.Windows.Point();
                pt.X = Convert.ToDouble(m_params[5]);
                pt.Y = Convert.ToDouble(m_params[7]);

                Point ptScreen = new Point();
                ptScreen.X = (int)pt.X;
                ptScreen.Y = (int)pt.Y;
                
                if ( false == _vm.host.InitTipDialog(ptScreen.X, ptScreen.Y, m_params[3], Convert.ToInt32(m_params[1]) ) )
                    return EnumActionResult.ERROR;

                _vm.host.ShowTipDialog();

                return EnumActionResult.OK;
            }
            catch (Exception ex)
            {
                _vm.host.WriteLog("ActionTipClient->" + ex.Message);
                return EnumActionResult.ERROR;             
            }
        }

    }
}