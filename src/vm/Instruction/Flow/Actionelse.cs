using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class Actionelse : ActionFlow
    {
        public override EnumActionResult Execute()
        {
            return EnumActionResult.OK;
        }
    }
}
