using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class ActionSetFocus : ActionBase
    {
        public override EnumActionResult Execute()
        {
            _vm.host.aeCurrent.SetFocus();
            return (EnumActionResult.OK);
        }
    }
}
