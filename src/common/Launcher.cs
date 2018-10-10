using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;


namespace Util
{
    public static class Launcher
    {        
        public static void Explorer()
        {
            string path = Path.Combine(Application.StartupPath, "QABOT-Explorer.exe");

            if (File.Exists(path))
                System.Diagnostics.Process.Start(path);
            else
                UtilSys.MessageBox("'QABOT-Explorer.exe' does not exist on '" + path + "'");
        }

        public static void Master()
        {
            string path = Path.Combine(Application.StartupPath, "QABOT-Master.exe");

            if (File.Exists(path))
                System.Diagnostics.Process.Start(path);
            else
                UtilSys.MessageBox("'QABOT-Master.exe' does not exist on '" + path + "'");
        }
        
        public static void Pilot(Form hostForm, string path2Script, string path2Data, bool minimizeHostApp, bool autorun)
        {
            string path2Pilot = Path.Combine(Application.StartupPath, "QABOT-Pilot.exe");            

            // no pilot.exe
            if (!File.Exists(path2Pilot))
            {
                UtilSys.MessageBox("'QABOT-Pilot.exe' does not exist on '" + path2Pilot + "' path.");
                return;
            }

            // no script file
            if (!File.Exists(path2Script))
            {
                System.Diagnostics.Process.Start(path2Pilot);
            }
            else // at this point pilot and script file exists now let's check if there is a data file
            {
                string cmdLineArgs = Support.SurrondItWith(path2Script, "\"");

                if (File.Exists(path2Data))
                    cmdLineArgs += " " + Support.SurrondItWith(path2Data, "\"");

                if (autorun)
                    cmdLineArgs += " run";

                System.Diagnostics.Process.Start(path2Pilot, cmdLineArgs);
            }

            if (minimizeHostApp)
                hostForm.WindowState = FormWindowState.Minimized;
        }



    }
}