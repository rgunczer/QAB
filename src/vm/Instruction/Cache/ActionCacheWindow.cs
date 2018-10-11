using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class ActionCacheWindow : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string name = _vm.variables.Get(m_params[1]);

            if (_vm.CacheWindow.ContainsKey(name))
            {
                _vm.CacheWindow[name] = _vm.host.TargetWindow;
                return (EnumActionResult.OK);
            }
            _vm.CacheWindow.Add(name, _vm.host.TargetWindow);
            return (EnumActionResult.OK);                                    
        }
    }
}
