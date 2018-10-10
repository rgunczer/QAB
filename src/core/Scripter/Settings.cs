using System;
using System.Collections.Generic;
using System.Text;
using Util;
using System.Diagnostics;
using System.IO;


namespace Scripter
{
    public static class Settings
    {
        private static string _path = string.Empty;
        private static Dictionary<string, string> _dic = new Dictionary<string, string>();

        private static string[] m_Settings = 
        {
            "LaunchPilotMinimize = 1",
            "AutorunPilot = 0",
            "IntelliSense = 1",             
            "HotSlot0 = ",
            "HotSlot1 = ",
            "HotSlot2 = ",
            "HotSlot3 = ",
            "HotSlot4 = ",
            "HotSlot5 = ",
            "HotSlot6 = ",
            "HotSlot7 = ",
            "HotSlot8 = ",
            "HotSlot9 = ",
            "Template0 = ",
            "Template1 = ",
            "Template2 = ",
            "Template3 = ",
            "Template4 = ",
            "Template5 = ",
            "Template6 = ",
            "Template7 = ",
            "Template8 = ",
            "Template9 = ",
            "Compatibility = 1",
            "DefaultTemplateIndex = -1",
            "HighlightCurrentLine = 1",
            "EnableTooltip = 1",
            "ServerIP = ",
            "DownloadTo = ",
        };


        public static string Get(string key)
        {
            if (_dic.ContainsKey(key))
            {
                return _dic[key];
            }
            throw new System.Exception("Setting Key [" + key + "] NOT found.");
        }

        public static void Add(string key, string value)
        {
            if (_dic.ContainsKey(key))            
                _dic[key] = value;            
            else
                _dic.Add(key, value);
        }

        public static void Update(string key, string value)
        {
            if (_dic.ContainsKey(key))
                _dic[key] = value;
            else
                UtilSys.MessageBox("Unable to Update. Key [" + key + "] not present in settings");
        }

        private static void CreateDefaultSettingsFile(string path)
        {
            UtilSys.MessageBoxInfo("Creating Default Settings File");

            string dir = Path.GetDirectoryName(path);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (string line in m_Settings)
                {
                    sw.WriteLine(line);
                }
            }
        }

        public static bool Load(string path)
        {
            Debug.Assert(null != path);
            Debug.Assert(string.Empty != path);

            if (!File.Exists(path))
            {
                UtilSys.MessageBoxInfo("Settings Load: " + path);
                CreateDefaultSettingsFile(path);                

                UtilSys.Sleep(1000);

                return false;
            }
            
            _path = path;

            _dic.Clear();

            List<string> lst = UtilIO.ReadFile(_path);

            char[] sep = {' ', '=', ' '};
            string[] stringSeparators = new string[] { " = " };
        
            foreach (string line in lst)
            {
                string[] tmp = line.Split(stringSeparators, StringSplitOptions.None);
                _dic.Add(tmp[0], tmp[1]);
            }

            // at this point we have settings loaded
            // but there is a possibility that new setting keys are not in file, we have to check and fix that
            string key = string.Empty;
            string value = string.Empty;
            
            foreach(string keyLine in m_Settings)
            {   
                string[] tmp = keyLine.Split(stringSeparators, StringSplitOptions.None);

                key = tmp[0];
                value = tmp[1];

                if (!_dic.ContainsKey(key))
                    Add(key, value);                
            }
        
            Save();

            return true;
        }

        public static void Save()
        {
            try
            {
                using (StreamWriter sw = File.CreateText(_path))
                {
                    foreach (KeyValuePair<string, string> item in _dic)
                    {
                        sw.WriteLine(item.Key + " = " + item.Value);
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }            
        }
    }
}