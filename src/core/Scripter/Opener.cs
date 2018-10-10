using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using Util;
using Parser;
using Controls;


namespace Scripter
{
    public static class Opener
    {
        public static readonly string FILE_TAG = "FILE";
        public static readonly string FUNCTION_TAG = "FUNCTION";
        public static readonly string VARIABLE_TAG = "VARIABLE";

        public static FrmScripter frm;
        public static MRU mru = null;
        public static string LastOpenedFolder = string.Empty;

        public static bool _bCompatibilty = true;

        private static bool _bTab = false;
        private static int _tabCount = 0;


        private static string[,] _patterns = 
        { 
        { "Start",                  "sys.start"     },
        { "Pilot \"Quit\"",         "sys.quit"      },
        { "Pilot \"UnCompact\"",    "sys.uncompac"  },
        { "Pilot \"Stop\"",         "sys.stop"      },
        { "Pilot \"Min\"",          "sys.min"       },
        { "Pilot \"Norm\"",         "sys.norm"      }, 
        { "Pilot \"Compact\"",      "sys.compact"   },
        { "Call",                   "call"          },
        { "Function",               "function"      },
        { "Log",                    "log.write"     },
        { "Log2File",               "log.path"      },
        { "For",                    "for"           },
        { "Return",                 "return"        },
        { "Loop",                   "loop"          },
        { "Next",                   "loop"          },
        { "next",                   "loop"          },
        { "MouseClickOn",           "mouse.ClickOn" },
        { "MouseDoubleClickOn",     "mouse.DoubleClickOn" },
        { "MouseRightClickOn",      "mouse.RightClickOn" },
        };

            



        public enum EnumOpener
        {
            Scripter,
            Notepad,
        }

        public static void OpenIn(EnumOpener opener)
        {
            if (null == frm.tvwImported.SelectedNode)
                return;

            string path = string.Empty; 

            if ((string)frm.tvwImported.SelectedNode.Tag != Opener.FILE_TAG)
            {
                if ((string)frm.tvwImported.SelectedNode.Tag == Opener.FUNCTION_TAG || (string)frm.tvwImported.SelectedNode.Tag == Opener.VARIABLE_TAG)
                {
                    TreeNode grp = frm.tvwImported.SelectedNode.Parent;
                    
                    if ((string)grp.Parent.Tag == Opener.FILE_TAG)                    
                        path = Path.Combine(Scripter.AppPath, grp.Parent.Text);                    
                    else
                        return;                                    
                }                
            }
            else
                path = Path.Combine(Scripter.AppPath, frm.tvwImported.SelectedNode.Text);
            
            if (File.Exists(path))
            {
                switch (opener)
                {
                    case EnumOpener.Scripter:
                        Open(path, null);
                        break;

                    case EnumOpener.Notepad:
                        Process.Start("notepad.exe", path);
                        break;
                }
            }
            else
                UtilSys.MessageBox("File '" + path + "' doesn't exist.");
        }

        public static void Open(string path, string filter)
        {
            if (string.IsNullOrEmpty(filter))
                filter = "QABOT Script Files (*.scp)|*.scp|QABOT Script Import Modules (*.sci)|*.sci|QABOT Data Files (*.dat)|*.dat|QABOT Project Files (*.qpf)|*.qpf|QABOT Template Files (*.tem)|*.tem|All Files (*.*)|*.*";

            if (string.IsNullOrEmpty(path))
            {
                path = UtilSys.OpenFileDialog("Open QABOT Script, Data or Template File", filter, LastOpenedFolder);
            }

            if (string.Empty == path)
                return;

            bool bOpened = false;

            // if the script is already opened in one of tabs 
            // check if it is modified if yes, ask to save it?
            int i = 0;
            Editor editor = null;
            foreach (TabPage page in frm.tabFiles.TabPages)
            {
                editor = (Editor)page.Controls[0].Controls[0];

                Debug.Assert(null != editor);

                if (editor.Path2File == path)
                {
                    bOpened = true;                                        
                    frm.tabFiles.SelectedIndex = i;
                    editor.Clear();
                    Application.DoEvents();
                    break;
                }
                ++i;
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string[] buffer = ScriptLoader.Load(frm, mru, path);

            if (null == buffer)
            {
                frm.UpdateMRUMenu();
                return;
            }

            LastOpenedFolder = Path.GetDirectoryName(path);

            FrmProgress frmP = new FrmProgress();

            frmP.Show();
            frmP.Top = frm.Top + frm.Height / 2;
            frmP.Left = (frm.Left + frm.Width / 2) - frmP.Width / 2;

            frmP.SetProgressMax(buffer.Length);

            if (!bOpened)
            {
                frm.CreateNewDocument(path);
                editor = frm.CurrentEditor;
            }

            editor.Dirty = false;
            editor.Loading = true;
            editor.SuspendLayout();

            bool IsCompatibilityChangeMade = false;

            string line = string.Empty;
            for (int j = 0; j < buffer.Length; ++j)
            {
                line = buffer[j];

                if (_bCompatibilty)
                {
                    line = CreateCompatibileLine(line);

                    if (line != buffer[j])
                    {
                        IsCompatibilityChangeMade = true;
                        Scripter.Output("Line(" + j.ToString() +") changed from -> to: '" + buffer[j] + "' -> '" + line + "'");
                    }
                }
                editor.AddLine(line);
                frmP.UpdateProgress();
                frmP.Text = "Progress... (" + j + "/" + buffer.Length + ")";
            }

            editor.ResumeLayout();
            editor.Loading = false;
            editor.HandleBlockComments();
            editor.InitUndoStack();
            frmP.Close();

            if (!bOpened)
            {
                frm.tabFiles.SelectedIndex = frm.tabFiles.TabPages.Count - 1;
                frm.tabFiles.TabPages[frm.tabFiles.SelectedIndex].Focus();
                frm.UpdateDebugButtons();
            }

            editor.Dirty = IsCompatibilityChangeMade;

            frm.UpdateMRUMenu();
            frm.OnReParse();
            editor.Focus();

            sw.Stop();

            TimeSpan span = sw.Elapsed;

            Scripter.Output("Load Time: " + span.Hours + ":" + span.Minutes + ":" + span.Seconds + ":" + span.Milliseconds);
        }

        private static string CreateCompatibileLine(string input)
        {
            _bTab = false;
            _tabCount = 0;

            if (input.StartsWith("\t"))
            {
                _bTab = true;

                foreach (char ch in input)
                {
                    if ('\t' == ch)
                        ++_tabCount;
                    else
                        break;
                }
            }

            string line = input.Trim();

            if (0 == line.Length) // empty line
                return input;

            if (line.StartsWith(@"//")) // comment line, do not waste cycles
                return input;

            // handle easy case (simple pattern replace, standalone keywords)
            for (int i = 0; i < _patterns.GetLength(0); ++i)
            {
                if (line.StartsWith(_patterns[i, 0]))
                {
                    string tmp = line.Replace(_patterns[i,0], _patterns[i,1]);

                    if (_bTab)
                        return new string('\t', _tabCount) + tmp;                    
                    else
                        return tmp;
                }
            }

            line = input;

            // Pilot "Speed", "1000" => sys.speed = 1000
            // Pilot "Wait", "1000"  => sys.wait = 1000

            // complex cases
            List<string> tokens = Tokenizer.Init(line);

            if (tokens.Count > 1)
            {
                if ("MouseClickClient" == tokens[0])
                {
                    tokens.RemoveAt(0);

                    tokens.Insert(0, " ");
                    tokens.Insert(0, "ClickClient");
                    tokens.Insert(0, ".");
                    tokens.Insert(0, "mouse");

                    return FormatLine(tokens);
                }

                if ("MouseClickScreen" == tokens[0])
                {
                    tokens.RemoveAt(0);

                    tokens.Insert(0, " ");
                    tokens.Insert(0, "ClickScreen");
                    tokens.Insert(0, ".");
                    tokens.Insert(0, "mouse");

                    return FormatLine(tokens);
                }

                if ("MouseDoubleClickClient" == tokens[0])                
                {
                    tokens.RemoveAt(0);

                    tokens.Insert(0, " ");
                    tokens.Insert(0, "DoubleClickClient");
                    tokens.Insert(0, ".");
                    tokens.Insert(0, "mouse");

                    return FormatLine(tokens);
                }

                if ("MouseDoubleClickScreen" == tokens[0])
                {
                    tokens.RemoveAt(0);

                    tokens.Insert(0, " ");
                    tokens.Insert(0, "DoubleClickScreen");
                    tokens.Insert(0, ".");
                    tokens.Insert(0, "mouse");

                    return FormatLine(tokens);
                }

                if ("Speed" == tokens[1])
                {
                    string speed = tokens[3];

                    tokens.RemoveAt(0); // Pilot
                    tokens.RemoveAt(0); // Speed
                    tokens.RemoveAt(0); // ,
                    tokens.RemoveAt(0); // 1000 

                    tokens.Insert(0, speed);
                    tokens.Insert(0, " = ");
                    tokens.Insert(0, "speed");
                    tokens.Insert(0, ".");
                    tokens.Insert(0, "sys");

                    return FormatLine(tokens);
                }

                if ("Wait" == tokens[1])
                {
                    string wait = tokens[3];

                    tokens.RemoveAt(0); // Pilot
                    tokens.RemoveAt(0); // Wait
                    tokens.RemoveAt(0); // ,
                    tokens.RemoveAt(0); // 1000 

                    tokens.Insert(0, wait);                    
                    tokens.Insert(0, " = ");
                    tokens.Insert(0, "wait");
                    tokens.Insert(0, ".");
                    tokens.Insert(0, "sys");

                    return FormatLine(tokens);
                }
            }
            return line;
        }

        private static string FormatLine(List<string> tokens)
        {
            if (_bTab)
            {
                for (int i = 0; i < _tabCount; ++i)
                    tokens.Insert(0, "\t");
            }

            string[] arr = tokens.ToArray();

            string line = string.Concat(arr);
            line = line.Replace(",", ", ");
            return line;
        }

    } 
}
