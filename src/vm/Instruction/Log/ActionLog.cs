using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;


namespace VM
{
    class Actionlog : ActionBase
    {
        public override EnumActionResult Execute()
        {            
            switch (m_params[0])
            {
                case "log.write":
                {
                    string text = EvaulateParams();
                    return LogWrite(text);
                }

                case "log.path":
                {
                    string path = EvaulateParams();
                    return LogPath(path);
                }

                case "log.level":
                {
                    string level = EvaulateParams();
                    return SetLogLevel(level);
                }

                case "log.clear":
                {
                    _vm.host.ClearLogFile();
                    return EnumActionResult.OK;
                }                

                default:
                {
                    _vm.ActionErrorMessage = "Unknown Command: [" + m_params[0] + "]";
                    return EnumActionResult.ERROR;
                }
            }
        }

        private string EvaulateParams()
        {
            string value = string.Empty;
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i < m_params.Count; ++i)
            {
                if ("+" == m_params[i])
                    continue;                
#if DEBUG
                string token = m_params[i];
                string tmp = _vm.variables.Get(token);

                sb.Append(tmp);
#else
                sb.Append(_vm.variables.Get(m_params[i]));
#endif
            }
            value = sb.ToString();
            return value;
        }

        private EnumActionResult LogWrite(string text)
        {
            _vm.host.WriteLog(text);
            return EnumActionResult.OK;
        }

        private EnumActionResult LogPath(string path)
        {
            if (_vm.host.SetLogFile(path, _vm.host.BigBrother))
                return EnumActionResult.OK;
            else
                return EnumActionResult.ERROR;
        }

        private EnumActionResult SetLogLevel(string level)
        {
            _vm.host.SetLogLevel(level);
            return EnumActionResult.OK;
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