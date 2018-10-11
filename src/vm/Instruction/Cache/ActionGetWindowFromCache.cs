using System;
using System.Collections.Generic;
using System.Text;

namespace VM
{
    class ActionGetWindowFromCache : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string key = _vm.variables.Get(m_params[1]);

            if (_vm.CacheWindow.ContainsKey(key))
            {
                _vm.host.aeCurrent = _vm.CacheWindow[key];
                return (EnumActionResult.OK);
            }
            return (EnumActionResult.ERROR);
        }
    }
}
