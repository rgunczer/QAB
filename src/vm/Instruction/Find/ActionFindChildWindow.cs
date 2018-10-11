using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindChildWindow : ActionBase
    {
        public override EnumActionResult Execute()
        {
            int count = 0;
            string searchType = _vm.variables.Get(m_params[1]);

            string name = "";

            if (m_params[3].Length > 0)
                name = _vm.variables.Get(m_params[3]);

            string tmp = _vm.variables.Get(m_params[5]);

            int maxAttempt = Convert.ToInt32(tmp);
            AutomationElement window = null;

            do
            {
                _vm.host.DoEvents();

                if (_vm.PlaybackStatus != VMStatus.IN_PLAYBACK)
                    return (EnumActionResult.STOPPED);

                _vm.host.WriteLog("Trying to find ChildWindow: '" + name + "' (" + maxAttempt + "/" + count + ")");

                switch (searchType)
                {
                    case "ByTitle":
                        window = Finder.GetChildWindowByTitle(_vm.host.TargetWindow, name);
                        break;

                    case "ByTitleLike":
                        window = Finder.GetChildWindowByTitleLike(_vm.host.TargetWindow, name);
                        break;

                    case "ByID":
                        window = Finder.GetChildWindowByID(_vm.host.TargetWindow, name);
                        break;

                    default:
                        _vm.host.WriteLog("Unrecognized search type: '" + searchType + "'");
                        return EnumActionResult.ERROR;
                }

                if (null != window)
                {
                    _vm.host.WriteLog("ChildWindow: '" + name  + "' found.");
                    _vm.host.TargetWindow = window;
                    return (EnumActionResult.OK);
                }
                UtilSys.Wait(1000);
                ++count;
            } while (count < maxAttempt);

            _vm.PushError("ChildWindow: '" + name + "' NOT found.");
            return (EnumActionResult.ERROR);
        }
    }
}
