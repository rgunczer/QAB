using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class ActionCache : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string name = _vm.variables.Get(m_params[1]);

            if (_vm.Cache.ContainsKey(name))
            {
                _vm.Cache[name] = _vm.host.aeCurrent;
                return (EnumActionResult.OK);
            }
            _vm.Cache.Add(name, _vm.host.aeCurrent);            
            return (EnumActionResult.OK);
        }
    }
}
