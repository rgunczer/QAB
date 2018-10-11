using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class Actionreturn : ActionFlow
    {
        public override EnumActionResult Execute()
        {
            return (EnumActionResult.OK);            
        }

        public override string ToString()
        {
            return ("");
        }
    }
}
