using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Util;


namespace Scripter
{
    static class TemplateManager
    {
        private static FrmScripter _ScripterForm = null;
        private static int _defaultTemplateIndex = -1;
        private static string DefTemplateIndex = "DefaultTemplateIndex";

        private static string[] _Templates = 
        {
            "Empty slot 0",
            "Empty slot 1",
            "Empty slot 2",
            "Empty slot 3",
            "Empty slot 4",
            "Empty slot 5",
            "Empty slot 6",
            "Empty slot 7",
            "Empty slot 8",
            "Empty slot 9",
        };

        private static string[] _defaultScript = 
        { 
            "//====================================",
            "// QABOT - Pilot file vX.XX",
            "//====================================",
            "",
            "// imports",
            "",
            "",
            "// variables",
            "",
            "",
            "// functions",
            "",
            "",
            "// entry point",
            "sys.start",
            "",
        };


        public static FrmScripter frm
        {
            set { _ScripterForm = value; }
        }

        public static int DefaultIndex
        {
            set 
            { 
                _defaultTemplateIndex = value;

                Settings.Update(DefTemplateIndex, _defaultTemplateIndex.ToString());
                Settings.Save();                
                Load();
            }
        }


        public static void Load()
        {
            Debug.Assert(null != _ScripterForm);

            int index = Convert.ToInt32(Settings.Get(DefTemplateIndex));

            ToolStripMenuItem mnuLoad = (ToolStripMenuItem)_ScripterForm.mnuTemplates.DropDown.Items[0];
            ToolStripMenuItem mnuDefaults = (ToolStripMenuItem)_ScripterForm.mnuTemplates.DropDown.Items[1];

            for (int j = 0; j < _Templates.Length; ++j)
            {
                string value = Settings.Get("Template" + j);

                mnuLoad.DropDown.Items[j].Enabled = false;
                mnuDefaults.DropDown.Items[j].Enabled = false;

                if (value.Length > 0)
                {
                    _Templates[j] = value;

                    ToolStripMenuItem mnuL = (ToolStripMenuItem)mnuLoad.DropDown.Items[j];
                    mnuL.Checked = false;

                    ToolStripMenuItem mnuD = (ToolStripMenuItem)mnuDefaults.DropDown.Items[j];
                    mnuD.Checked = false;

                    if (j == index)
                    {
                        _defaultTemplateIndex = index;
                        mnuL.Checked = true;
                        mnuD.Checked = true;    
                    }

                    mnuLoad.DropDown.Items[j].Enabled = true;
                    mnuDefaults.DropDown.Items[j].Enabled = true;
                }
                                                
                mnuLoad.DropDown.Items[j].Text = _Templates[j];
                mnuDefaults.DropDown.Items[j].Text = _Templates[j];
                _ScripterForm.ctxMenuAddToTemplates.DropDown.Items[j].Text = _Templates[j];                
            }

            ToolStripMenuItem mnu = (ToolStripMenuItem)mnuDefaults.DropDown.Items[mnuDefaults.DropDown.Items.Count-1];

            mnu.Checked = index == -1 ? true : false;                                        
        }

        public static void Save(int index)
        {
            string userName = string.Empty;
            string key = "Template" + index.ToString();
            string filename = key + ".tem";
            string path = Path.Combine(Path.Combine(Path.Combine(Scripter.AppPath, "data"), "templates"), filename);

            if (InputBox.ShowDialog("QABOT-Scripter Template", "Template name:", ref userName) != DialogResult.OK)
                return;

            if (string.Empty == userName || 0 == userName.Length)
                return;

            try
            {
                StreamWriter sw = File.CreateText(path);

                foreach (string line in _ScripterForm.CurrentEditor.Lines)
                {
                    sw.WriteLine(line);
                }
                sw.Close();

                Settings.Add(key, userName);
                Settings.Save();
                Load();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
                        
        public static void LoadItem(int index)
        {
            _ScripterForm.CreateNewDocument(Scripter.DEF_FILE_NAME);

            string filename = "Template" + index + ".tem";
            string path = Path.Combine(Path.Combine(Path.Combine(Scripter.AppPath, "data"), "templates"), filename);

            if (!File.Exists(path))
            {
                UtilSys.MessageBox("File '" + path + "' does not exist.");
                return;
            }

            string[] buffer = UtilIO.ReadFile2Array(path);

            _ScripterForm.CurrentEditor.Loading = true;

            foreach (string line in buffer)
            {
                _ScripterForm.CurrentEditor.AddLine(line);                
            }
            _ScripterForm.CurrentEditor.Loading = false;
            _ScripterForm.CurrentEditor.Dirty = false;
            _ScripterForm.CurrentEditor.InitUndoStack();
            Scripter.DoEvents();
            _ScripterForm.OnReParse();
            Scripter.DoEvents();
            _ScripterForm.CurrentEditor.Focus();
        }

        public static void NewScript()
        {            
            if (-1 != _defaultTemplateIndex)
                LoadItem(_defaultTemplateIndex);
            else
            {
                _defaultScript[1] = _defaultScript[1].Replace("vX.XX", "v" + Application.ProductVersion);

                _ScripterForm.CreateNewDocument(Scripter.DEF_FILE_NAME);

                _ScripterForm.CurrentEditor.Loading = true;

                foreach (string s in _defaultScript)
                {
                    _ScripterForm.CurrentEditor.AddLine(s);
                }
                _ScripterForm.CurrentEditor.Dirty = false;
                _ScripterForm.CurrentEditor.InitUndoStack();
                Scripter.DoEvents();
                _ScripterForm.OnReParse();
                Scripter.DoEvents();
                _ScripterForm.CurrentEditor.Focus();
            }

            _ScripterForm.CurrentEditor.Loading = false;
        }


    }
}
