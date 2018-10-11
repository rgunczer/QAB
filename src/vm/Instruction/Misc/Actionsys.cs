using System;
using System.Collections.Generic;
using System.Text;
using Util;


namespace VM
{ 
    class Actionsys : ActionBase
    {
        public static string[] cmds =
        {
            "sys.wait",
            "sys.norm",
            "sys.compact",
            "sys.uncompact",
            "sys.quit",
            "sys.speed",
            "sys.start",
            "sys.stop",
            "sys.wait",
            "sys.min",            
        };

        public override EnumActionResult Execute()
        {
            string type = m_params[0];

            switch (type)
            {
                case "sys.quit":
                    _vm.PlaybackStatus = VMStatus.QUIT;
                break;    

                case "sys.speed":
                    _vm.Speed = Convert.ToInt32( _vm.variables.Get(m_params[2]) );
                break;

                case "sys.stop":
                    _vm.PlaybackStatus = VMStatus.STOPPED;
                break;

                case "sys.wait":
                    UtilSys.Wait( Convert.ToInt32( _vm.variables.Get(m_params[2])) );
                break;

                case "sys.min":
                    _vm.host.WindowMinimalize();
                break;

                case "sys.norm":
                    _vm.host.WindowNormal();
                break;

                case "sys.start":                                        
                break;

                case "sys.record":
                    _vm.host.RecordPlayback = Convert.ToBoolean(m_params[2]);
                break;

                case "sys.SaveScreenshotOnError":
                    _vm.host.SaveScreenshotOnError = Convert.ToBoolean(m_params[2]);                        
                break;

                default:
                    _vm.host.WriteLog("Unknown sys command: " + type);
                return (EnumActionResult.ERROR);                
            }
            return (EnumActionResult.OK);
        }

        public override string ToString()
        {
            string tmp = string.Empty;

            for (int i = 0; i < m_params.Count; ++i)
            {
                tmp += " " + m_params[i];
            }
            return (tmp.Trim());
        }

    }
}