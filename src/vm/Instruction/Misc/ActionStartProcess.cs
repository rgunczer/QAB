using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace VM
{
    class ActionStartProcess
    {

        /*
                public static int ExecuteCommand(string Command, int Timeout)
                {
                    int ExitCode;
                    ProcessStartInfo ProcessInfo;
                    Process Process;

                    ProcessInfo = new ProcessStartInfo("cmd.exe", "/C " + Command);
                    ProcessInfo.CreateNoWindow = true;
                    ProcessInfo.UseShellExecute = false;
                    Process = Process.Start(ProcessInfo);
                    Process.WaitForExit(Timeout);
                    ExitCode = Process.ExitCode;
                    Process.Close();

                    return ExitCode;
                }
        */


        private void CmdPromptTest()
        {
            // Start cmd.exe and pass the "ipconfig /all" command to it  
            //ProcessStartInfo psiOpt = new ProcessStartInfo(@"cmd.exe", @"/C ipconfig /all");
            //ProcessStartInfo psiOpt = new ProcessStartInfo(@"cmd.exe", @"/C net start SCardSvr");
            //ProcessStartInfo psiOpt = new ProcessStartInfo(@"cmd.exe", @"/C net stop SCardSvr");
            ProcessStartInfo psiOpt = new ProcessStartInfo(@"cmd.exe", @"/C dir *.exe");


            // We don't want to show the Command Prompt window and we want the output redirected  
            psiOpt.WindowStyle = ProcessWindowStyle.Hidden;
            psiOpt.RedirectStandardOutput = true;
            psiOpt.UseShellExecute = false;
            psiOpt.CreateNoWindow = true;

            // Create the actual process object  
            Process procCommand = Process.Start(psiOpt);



            // Receives the output of the Command Prompt  
            StreamReader srIncoming = procCommand.StandardOutput;

            // Show the result  
            MessageBox.Show(srIncoming.ReadToEnd());

            // Close the process  
            procCommand.WaitForExit();
        }
    }
}
