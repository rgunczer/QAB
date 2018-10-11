using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindProcess : ActionBase
    {
        public override EnumActionResult Execute()
        {
            int MaxAttempt = 5;
            int Attempt = 0;

            string procName = _vm.variables.Get(m_params[1]);

            do
            {
                if (_vm.PlaybackStatus != VMStatus.IN_PLAYBACK)
                    return (EnumActionResult.STOPPED);

                _vm.host.WriteLog("Attempt to find process (" + Attempt + "/" + MaxAttempt + ") '" + procName + "'");
                _vm.host.DoEvents();

                Process[] proc = Process.GetProcessesByName(procName);

                if (1 == proc.Length)
                {
                    _vm.host.WriteLog("'" + procName + "' process found.");

                    _vm.host.TargetWindow = AutomationElement.FromHandle(proc[0].MainWindowHandle);

                    if (null != _vm.host.TargetWindow)
                        return (EnumActionResult.OK);

                    _vm.host.WriteLog("Error 'AutomationElement.FromHandle'");
                    return (EnumActionResult.ERROR);
                }
                UtilSys.Wait(3000);
                ++Attempt;            
            } while (Attempt <= MaxAttempt);

            _vm.PushError("Process '" + procName + "' NOT found.");
            return (EnumActionResult.ERROR);
        }
    }
}
