using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Util;


namespace VM
{
    class ActionScreenshot : ActionBase
    {
        public override EnumActionResult Execute()
        { 
            string path = _vm.variables.Get(m_params[1]);

            UtilSys.TakeScreenShot(path);
            return EnumActionResult.OK;
        }    
    }
}
