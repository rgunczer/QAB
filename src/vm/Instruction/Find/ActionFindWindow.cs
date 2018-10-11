using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Util;


namespace VM
{
    class ActionFindWindow : ActionBase
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        private const string DoNotLaunch = "<DoNotLaunch>";
        private string m_windowTitle = null;
        private string m_path2exe = null;

        
        public override EnumActionResult Execute()
        {
            m_windowTitle = _vm.variables.Get( m_params[1] );

            if (4 == m_params.Count)
                m_path2exe = _vm.variables.Get( m_params[3] );
            else
                m_path2exe = DoNotLaunch;

            if (!StartAppFindWindow())
                return (EnumActionResult.ERROR);

            return (EnumActionResult.OK);
        }

        protected virtual AutomationElement GetWindow(string title)
        {
            AutomationElement wnd = null;
            wnd = Finder.GetTopLevelWindow(title);
            return (wnd);
        }

        protected void StartApp(string path)
        {
            _vm.host.WriteLog("Starting app: '" + path + "'");
            
            string dir = Path.GetDirectoryName(path);

            if (dir.Length > 0)
                Directory.SetCurrentDirectory(dir);

            string curDir = Directory.GetCurrentDirectory();

            _vm.host.WriteLog("Current Dir: " + curDir);

            System.Diagnostics.Process.Start(path);
        }

        protected void SetBotVars(AutomationElement wnd)
        {
            Debug.Assert(null != wnd);

            BringWindowToTop(new IntPtr(wnd.Current.NativeWindowHandle) );

            _vm.host.TargetWindow = wnd;
            _vm.host.TargetWindowTitle = wnd.Current.Name;

            _vm.host.TargetProcessID = _vm.host.TargetWindow.Current.ProcessId;
            _vm.host.TargetWindowTitle = _vm.host.TargetWindow.Current.Name;

            _vm.host.WriteLog("Window: '" + _vm.host.TargetWindowTitle + "' found.");
        }

        protected AutomationElement FindWindow(int MaxAttempt, int waitInterval, string title)
        {
            AutomationElement wnd = null;
            int Attempt = 0;

            while (Attempt <= MaxAttempt)
            {
                if (_vm.PlaybackStatus != VMStatus.IN_PLAYBACK)
                    return (null);

                _vm.host.WriteLog("Attempt to find window (" + Attempt + "/" + MaxAttempt + ") '" + title + "'");

                wnd = GetWindow(title);

                if (null != wnd)
                    return (wnd);

                UtilSys.Wait(waitInterval);
                _vm.host.WriteLog("Waiting: " + waitInterval);
                waitInterval += 500;
                ++Attempt;
            }
            return (wnd);
        }

        protected bool StartAppFindWindow()
        {
            _vm.host.WriteLog("Attempt to find window: '" + m_windowTitle + "'");

            // try first, fastest way
            AutomationElement wnd = GetWindow(m_windowTitle);
            
            if (null != wnd)
            {
                SetBotVars(wnd);
                return (true);
            }

            // try second, launch way
            if (DoNotLaunch != m_path2exe)            
                StartApp(m_path2exe);            

            // try third, slow way
            wnd = FindWindow(10, 500, m_windowTitle);

            if (null != wnd)
            {
                SetBotVars(wnd);
                return (true);
            }

            _vm.PushError("Unable to find window '" + m_windowTitle + "'");
            return (false);
        }
    }
}