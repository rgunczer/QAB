using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Util;
using System.IO;
using System.Diagnostics;
using Global;


namespace Scripter
{
    static class Scripter
    {
        private static HostScripter _host = null;

        public static HostScripter host
        {
            get { return _host; }
        }

        public static FrmScripter ScripterForm = null;
        public static Logger logger = null;

        public static string _pathSearchHistory = Path.Combine(Path.Combine(Path.Combine(Scripter.AppPath, "data"), "settings"), "QABOT-Scripter-Search-History.dat");
        public static string _pathReplaceHistory = Path.Combine(Path.Combine(Path.Combine(Scripter.AppPath, "data"), "settings"), "QABOT-Scripter-Replace-History.dat");
        public static string _pathServerIPHistory = Path.Combine(Path.Combine(Path.Combine(Scripter.AppPath, "data"), "settings"), "QABOT-Scripter-ServerIP-History.dat");

        public static string DEF_FILE_NAME = "Untitled";

        private static List<string> m_lstScriptsToLoad = new List<string>();

        public static List<string> ScriptsToLoad
        {
            get { return m_lstScriptsToLoad; }
        }

        public static string AppPath
        {
            get { return Application.StartupPath; }    
        }

        public static void Output(string text)
        {
            Debug.Assert(null != ScripterForm);
            ScripterForm.Output(text);
        }

        public static void DebugOutput(string text)
        {
            Debug.Assert(null != ScripterForm);
            ScripterForm.DebugOutput(text);
        }

        public static void ClearDebugOutput()
        {
            Debug.Assert(null != ScripterForm);
            ScripterForm.ClearDebugOutput();
        }

        private static bool HandleCommandLineArgs()
        {
            m_lstScriptsToLoad.Clear();

            string[] args = Environment.GetCommandLineArgs();

            if (1 == args.Length) // just the exe name
                return (true); // no args, no problem

            foreach (string str in args)
            {
                logger.Write("Command Line Param: " + str);
            }

            if (args[1] == "help" || args[1] == "?" || args[1] == "--help" || args[1] == "-help" || args[1] == "/help" || args[1] == "/?")
                return (ShowCommandLineUsage());

            for (int i = 1; i < args.Length; ++i)
            {
                string path = Path.Combine(Application.StartupPath, args[i]);

                if (File.Exists(path))                
                    m_lstScriptsToLoad.Add(path);                    
                else
                    return (ShowCommandLineUsage());
            }
            return true;
        }

        private static bool ShowCommandLineUsage()
        {
            string msg = "Command line usage:\r\n" +
                         "[script_file_name] -> load script.";

            UtilSys.MessageBox(msg);

            return (false);
        }

        public static void DoEvents()
        {
            Application.DoEvents();
        }

        public static void ClearSearchHistory()
        {                        
            SaveSearchHistory(new List<string>());
        }

        public static void ClearReplaceHistory()
        {
            SaveReplaceHistory(new List<string>());
        }

        public static List<string> LoadSearchHistory()
        {
            if (!File.Exists(_pathSearchHistory))
            {
                using (StreamWriter SW = File.CreateText(_pathSearchHistory))
                {

                }
            }

            List<string> history = UtilIO.ReadFile(_pathSearchHistory);

            return history;
        }

        public static List<string> LoadReplaceHistory()
        {
            if (!File.Exists(_pathReplaceHistory))
            {
                using (StreamWriter SW = File.CreateText(_pathReplaceHistory))
                {

                }
            }

            List<string> history = UtilIO.ReadFile(_pathReplaceHistory);

            return history;
        }


        public static void SaveSearchHistory(List<string> history)
        {
            using (StreamWriter sw = File.CreateText(_pathSearchHistory))
            {
                foreach (string item in history)
                {
                    sw.WriteLine(item);
                }
            }
        }

        public static void SaveReplaceHistory(List<string> history)
        {
            using (StreamWriter sw = File.CreateText(_pathReplaceHistory))
            {
                foreach (string item in history)
                {
                    sw.WriteLine(item);
                }
            }
        }

        public static void SaveServerIPHistory(List<string> history)
        {
            using (StreamWriter sw = File.CreateText(_pathServerIPHistory))
            {
                foreach (string item in history)
                {
                    sw.WriteLine(item);
                }
            }
        }

        public static List<string> LoadServerIPHistory()
        {
            if (!File.Exists(_pathServerIPHistory))
            {
                using (StreamWriter SW = File.CreateText(_pathServerIPHistory))
                {

                }
            }

            List<string> history = UtilIO.ReadFile(_pathServerIPHistory);

            return history;
        }

        public static void ServerOutput(string text)
        {
            Debug.Assert(null != ScripterForm);
            ScripterForm.ServerOutput(text);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Util.Support.CreateFolderStructure(Application.StartupPath))
            {
                UtilSys.MessageBoxError("Error Creating the QABOT Folder Structure.");
                System.Environment.Exit( -1 );
                Application.Exit();
                return;
            }

            //UtilSys.MessageBoxInfo("Startup: " + Application.StartupPath);

            string path = Path.Combine(Application.StartupPath, @"data\log\QABOT-Scripter.log");
            logger = new Logger(Globals.ProcessScripter, Application.ProductVersion, path, null);

            string[] args = Environment.GetCommandLineArgs();

            path = Path.Combine(Application.StartupPath, @"data\settings\QABOT-Scripter-Settings.dat");

            // try to load setting two times
            // if first time fails then, probably settings file or folder does not exist (will be created and filled with default data)
            // return value is false, then try to load the settings second time 
            if (!Settings.Load(path))
                Settings.Load(path);

            ScripterForm = new FrmScripter();

            _host = new HostScripter(ScripterForm);

            if (!HandleCommandLineArgs())
            {
                System.Environment.Exit( -1 );
                Application.Exit();
            }

            Application.Run(ScripterForm);
        }
    }
}
