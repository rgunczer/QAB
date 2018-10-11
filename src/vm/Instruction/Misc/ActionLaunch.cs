using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Util;


namespace VM
{
    class ActionLaunch : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string path = _vm.variables.Get(m_params[1]);
            string args = string.Empty;

            if (m_params.Count > 2)
                args = _vm.variables.Get(m_params[3]);            

            if (StartApp(path, args))
                return EnumActionResult.OK;
            else
                return EnumActionResult.ERROR;
        }

        protected bool StartApp(string path, string cmdlineargs)
        {
            try
            {
                _vm.host.WriteLog("Starting app: '" + path + "', with cmd line args: '" + cmdlineargs + "'");
            
                string dir = Path.GetDirectoryName(path);

                if (dir.Length > 0)
                    Directory.SetCurrentDirectory(dir);

                System.Diagnostics.Process.Start(path, cmdlineargs);
            }
            catch (Exception ex)
            {
                string msg = "Error in ActionLaunch('" + path + "', '" + cmdlineargs + "'): " + ex.Message;

                _vm.host.WriteLog(msg);
                _vm.host.WriteLog(msg);

                return false;                
            }
            return true;
        }
    }
}
