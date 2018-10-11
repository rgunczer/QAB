using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindMenu : ActionBase
    {
        public override EnumActionResult Execute()
        {
            int MaxAttempt = 5;
            int Attempt = 0;

            string id = _vm.variables.Get(m_params[1]);

            do
            {
                if (_vm.PlaybackStatus != VMStatus.IN_PLAYBACK)
                    return (EnumActionResult.STOPPED);

                AutomationElement menu = Finder.GetMenu(_vm.host.TargetWindow, id);

                if (null == menu)
                    _vm.PushError("Menu '" + id + "' NOT found.");
                else
                {
                    _vm.host.WriteLog("Menu '" + id + "' found.");
                    _vm.host.aeCurrent = menu;
                    return (EnumActionResult.OK);
                }
                UtilSys.Wait(500);
                _vm.host.DoEvents();

                ++Attempt;
            } while (Attempt <= MaxAttempt);

            return (EnumActionResult.ERROR);
        }
    }
}
