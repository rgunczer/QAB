using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class ActionLabel : ActionFlow
    {
        public string GetLabel
        {
            get { return (m_params[1]); }
        }

        public override EnumActionResult Execute()
        {            
            return (EnumActionResult.OK);
        }
    }
}
