using System;
using System.Collections.Generic;
using System.Text;
using Util;


namespace VM
{
    class Actionreport : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string type = m_params[0];
            string text = _vm.variables.Get(m_params[2]);
            string msg;

            msg = type + '|' + text;
            SharedMemory.Send("QABOT-Master", msg);

//<report.FunctionalityToTest text="Create new removable media" />
//<report.ExpectedResult text="New removable media can be created successfully" />
//<report.Comments text="-" />

            msg = "<" + type + " text=\"" + text + "\"/>";
            _vm.host.WriteLog(msg);

            return EnumActionResult.OK;
        }
        
        public override string ToString()
        {
            try
            {
                string[] arr = m_params[0].Split('.');                
                return arr[1] + " " + m_params[1] + " " + m_params[2];
            }
            catch
            {
                return base.ToString();
            }
        }

    }
}