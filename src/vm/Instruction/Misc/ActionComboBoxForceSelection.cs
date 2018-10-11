using System;
using System.Collections.Generic;
using System.Text;
using Util;


namespace VM
{
    class ActionComboBoxForceSelection : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string target = _vm.variables.Get(m_params[1]);
                        
            System.Windows.Forms.SendKeys.SendWait("{END}");
            UtilSys.Sleep(500);
            _vm.host.WriteLog("END - " + _vm.host.aeCurrent.Current.Name);

            string end = _vm.host.aeCurrent.Current.Name;

            System.Windows.Forms.SendKeys.SendWait("{HOME}");
            UtilSys.Sleep(500);
            _vm.host.WriteLog("HOME - " + _vm.host.aeCurrent.Current.Name);

            _vm.host.WriteLog("LOOP - BEGIN");

            while (_vm.host.aeCurrent.Current.Name != target)
            {
                _vm.host.WriteLog(_vm.host.aeCurrent.Current.Name);

                if (_vm.host.aeCurrent.Current.Name == end)
                    return (EnumActionResult.ERROR);

                System.Windows.Forms.SendKeys.SendWait("{DOWN}");
                UtilSys.Sleep(1000);
            }

            _vm.host.WriteLog("LOOP - END");

            return (EnumActionResult.OK);
        }
    }
}
