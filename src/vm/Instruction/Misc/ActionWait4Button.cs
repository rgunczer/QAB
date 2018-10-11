using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionWait4Button : ActionBase
    {
        public override EnumActionResult Execute()
        {
            AutomationElement button = null;
            int count = 0;
            string button2Find = _vm.variables.Get(m_params[3]);
            int maxAttempt = Convert.ToInt32( _vm.variables.Get(m_params[5]) );
            bool bFindByID = false;
            string str = _vm.variables.Get(m_params[1]);

            switch (str)
            {
                case "ByID":
                    bFindByID = true;
                break;

                case "ByName":
                    bFindByID = false;
                break;

                default:
                    UtilSys.MessageBoxError("ActionWait4Button::Execute -> Unrecognized SearchBy Type [" + str + "]");
                    return EnumActionResult.ERROR;
            }

            do
            {
                _vm.host.DoEvents();

                if (VMStatus.IN_PLAYBACK != _vm.PlaybackStatus)
                    return (EnumActionResult.STOPPED);

                _vm.host.WriteLog("TargetWindowTitle: " + _vm.host.TargetWindowTitle);

                _vm.host.TargetWindow = Finder.GetTopLevelWindow(_vm.host.TargetWindowTitle);

                if (null != _vm.host.TargetWindow)
                {
                    if (bFindByID)
                        button = Finder.GetButtonByID(_vm.host.TargetWindow, button2Find);
                    else
                        button = Finder.GetButtonByName(_vm.host.TargetWindow, button2Find);

                    _vm.host.WriteLog("Try to find button: '" + button2Find + "'");

                    if (null != button)
                    {
                        _vm.host.WriteLog("Button '" + button2Find + "' found. (" + maxAttempt + "/" + count + ")");
                        _vm.host.aeCurrent = button;
                        return (EnumActionResult.OK);
                    }
                    else
                    {
                        _vm.host.WriteLog("Button '" + button2Find + "' NOT found. (" + maxAttempt + "/" + count + ")");
                    }
                    ++count;
                }
                UtilSys.Wait(1000);
                
            } while (count < maxAttempt);

            return (EnumActionResult.TIMEOUT); // bad things happen here!                     
        }
    }
}
