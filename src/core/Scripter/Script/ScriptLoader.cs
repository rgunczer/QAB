using System;
using System.IO;
using System.Text;
using Util;


namespace Scripter
{
    public static class ScriptLoader
    {
        public static string[] Load(FrmScripter frmScp, MRU mru, string path)
        {
            if (!File.Exists(path))
            {
                UtilSys.MessageBox("File '" + path + "' does not exist.");
                mru.Remove(path);
                mru.Save();
                return null;
            }

            frmScp.Output("Loading file: " + path);

            string[] buffer = UtilIO.ReadFile2Array(path);

            mru.Add(path);
            mru.Save();

            return (buffer);
        }
    }
}