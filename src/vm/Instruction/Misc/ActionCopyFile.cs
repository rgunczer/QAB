using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Util;


namespace VM
{
    class ActionCopyFile : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string src = _vm.variables.Get(m_params[1]);
            string dst = _vm.variables.Get(m_params[3]);

            try
            {
                File.Copy(src, dst, true);
            }
            catch (Exception ex)
            {
                _vm.host.WriteLog(ex.Message);
                throw;
            }            
            return (EnumActionResult.OK);
        }
    }
}
