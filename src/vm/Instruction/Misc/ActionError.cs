using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class ActionError : ActionBase
    {
        public override EnumActionResult Execute()
        {
            return EnumActionResult.ERROR;
        }
    }
}
