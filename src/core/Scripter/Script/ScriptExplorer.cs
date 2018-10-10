using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Util;
using Controls;


namespace Scripter
{
    class ScriptExplorer
    {
        private const string keyword_import = "#import";
        private const string keyword_function = "function";
        private const string keyword_return = "return";
        private const string keyword_var = "var";
        private const string keyword_const = "const";

        private static List<TreeNode> _nodesFiles = new List<TreeNode>();

        private static TreeNode _nodeFile = null;
        private static TreeNode _nodeVariables = null;
        private static TreeNode _nodeFunctions = null;

        private static Editor _editor = null; 
        private static FrmScripter _frm = null;

        private const string separator = "----------------------------------------------------------------------------------------";

        private static void CreateDefaultTreeNodes()
        {
            _frm.tvwImported.Nodes.Clear();
            _frm.NodeSearchResults = _frm.tvwImported.Nodes.Add("Search Result(s)");
            _frm.NodeSearchResults.Nodes.Add("[empty]");
            _frm.NodeSearchResults.ExpandAll();

            _frm.tvwImported.Nodes.Add(separator);
            _nodesFiles.Clear();
        }

        private static void CreateTreeNodes(TreeView tree, string fileName)
        {
            _nodeFile = tree.Nodes.Add(fileName);
            _nodeFile.Tag = "FILE";            
            _nodeFile.BackColor = Color.LightSkyBlue;

            _nodesFiles.Add(_nodeFile);

            _nodeVariables = _nodeFile.Nodes.Add("Variables [0]");           
            _nodeFunctions = _nodeFile.Nodes.Add("Functions [0]");            
        }

        private static void ParseLines(List<string> lst)
        {
            bool bInFunction = false;
            string line = string.Empty;

            TreeNode parent = null;

            //foreach(string rawline in lst)
            for (int i = 0; i < lst.Count; ++i)                                        
            {
                //line = rawline.Trim();
                line = lst[i].Trim();

                if (line.Length > keyword_var.Length)
                {
                    if (line.StartsWith(keyword_var))
                    {
                        string s = line.Substring(keyword_var.Length).Trim();
                        _editor.Variables.Add(s);
        
                        TreeNode nde = _nodeVariables.Nodes.Add(s);                        
                        nde.Tag = "VARIABLE";
                        
                        continue;
                    }
                }

                if (line.Length > keyword_const.Length)
                {
                    if (line.StartsWith(keyword_const))
                    {
                        string s = line.Substring(keyword_const.Length).Trim();
                        _editor.Variables.Add(s);

                        TreeNode nde = _nodeVariables.Nodes.Add(s);
                        nde.Tag = "VARIABLE";

                        continue;
                    }
                }

                if (line.Length > keyword_function.Length)
                {
                    string ss = line.Substring(0, keyword_function.Length).ToLower();

                    if (ss == keyword_function)
                    {
                        string fn = line.Substring(keyword_function.Length + 1);

                        parent = _nodeFunctions.Nodes.Add(fn);                        
                        parent.Tag = "FUNCTION";
                        _editor.Functions.Add(fn);
                        bInFunction = true;
                        continue;
                    }
                }

                if (bInFunction)
                {
                    if (line.Length >= keyword_return.Length)
                    {
                        string ss = line.Substring(0, keyword_return.Length).ToLower();

                        if (ss == keyword_return)
                        {
                            bool bLastReturnInFunction = true;

                            // check that the return is the last return                            
                            for (int j = i+1; j < lst.Count; ++j)
                            {
                                string ln = lst[j].Trim();

                                if (0 == ln.Length)
                                    continue;

                                if (ln.StartsWith("function") || ln.StartsWith("sys.start") || ln.StartsWith("//"))
                                    break;
                                else
                                {
                                    bLastReturnInFunction = false;
                                    break;
                                }
                            }

                            if (bLastReturnInFunction)
                            {
                                bInFunction = false;
                                parent = null; 
                                continue;
                            }
                        }
                    }
                }

                if (null != parent && bInFunction)
                {
                    string tmp = line.Trim();

                    if (tmp.Length > 0) // && tmp != keyword_return)
                    {
                        TreeNode nde = parent.Nodes.Add(tmp);                        
                    }
                }
            }

            _nodeVariables.Text = "Variables [" + _nodeVariables.Nodes.Count + "]";            
            _nodeVariables.Expand();

            _nodeFunctions.Text = "Functions [" + _nodeFunctions.Nodes.Count + "]";            
            _nodeFunctions.Expand();       
        }

        public static bool Parse(FrmScripter frmScp, Editor editor)
        {
            Debug.Assert(null != frmScp);
            Debug.Assert(null != editor);

            _frm = frmScp;        
            _editor = editor;
                       
            _editor.Variables.Clear();
            _editor.Functions.Clear();
            
            CreateDefaultTreeNodes();
            CreateTreeNodes(_frm.tvwImported, _editor.Path2File);
          
            List<string> lst = new List<string>(_editor.Lines);

            ParseLines(lst);            

            foreach(string rawline in editor.Lines)
            {
                string line = rawline.Trim();

                if (line.StartsWith(keyword_import))
                {
                    string str = line.Replace(keyword_import, "");
                    str = str.Trim();
                    str = str.Substring(1, str.Length - 2);

                    CreateTreeNodes(frmScp.tvwImported, str);

                    lst = UtilIO.ReadFile(Path.Combine(Application.StartupPath, str));

                    ParseLines(lst);
                }
            }

            foreach(TreeNode node in _nodesFiles)
            {
                node.Expand();
            }

            _frm.tvwImported.SelectedNode = _frm.tvwImported.Nodes[0];

            return true;
        }
        
    }    
}