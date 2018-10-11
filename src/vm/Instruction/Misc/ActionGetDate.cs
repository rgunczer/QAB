using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class ActionGetDate : ActionBase
    {
        public override EnumActionResult Execute()
        {
            DateTime dt = DateTime.Now;

            string frmt = _vm.variables.Get(m_params[1]);

            string date = String.Format("{0:" + frmt + "}", dt);

            _vm.variables.Update(m_params[3], date);

            return EnumActionResult.OK;
        }
    }
}
