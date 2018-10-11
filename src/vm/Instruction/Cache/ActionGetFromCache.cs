using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class ActionGetFromCache : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string key = _vm.variables.Get(m_params[1]);

            if (_vm.Cache.ContainsKey(key))
            {
                _vm.host.aeCurrent = _vm.Cache[key];
                return (EnumActionResult.OK);
            }
            return (EnumActionResult.ERROR);
        }
    }
}
