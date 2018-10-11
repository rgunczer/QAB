using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class Actionbreak : ActionFlow
    {
        public override EnumActionResult Execute()
        {
            return EnumActionResult.OK;
        } 
    }
}
