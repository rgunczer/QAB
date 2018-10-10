using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Util;
using Controls;
using Global;
using VM;
using Parser;


namespace Scripter
{
    public partial class FrmScripter : Form
    {
        private VirtualMachine m_vm = null; 

        private bool m_bEnableTooltip;

        public bool EnableTooptip
        {
            get { return m_bEnableTooltip; }
        }

        Form m_frmImage = new Form();


        [Flags()]
        private enum TCHITTESTFLAGS
        {
            TCHT_NOWHERE = 1,
            TCHT_ONITEMICON = 2,
            TCHT_ONITEMLABEL = 4,
            TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
        }

        private const int TCM_HITTEST = 0x130D;

        [StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct TCHITTESTINFO
        {
            public Point pt;
            public TCHITTESTFLAGS flags;
            public TCHITTESTINFO(int x, int y)
            {
                pt = new Point(x, y);
                flags = TCHITTESTFLAGS.TCHT_ONITEM;
            }
        }

        private string m_lastWord;
        private bool m_bUpdating = false;

        private const int SB_VERT = 0x1;

        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, ref TCHITTESTINFO lParam);

        
        const int NUM_OF_MRUS = 6;


        private TreeNode m_nodeSearchResults = null;

        public TreeNode NodeSearchResults
        {
            get { return m_nodeSearchResults; }
            set { m_nodeSearchResults = value; }            
        }

        private TreeNode m_nodeThisScript = null;

        public TreeNode NodeThisScript
        {
            get { return m_nodeThisScript; }
            set { m_nodeThisScript = value; }
        }
        
        public Editor CurrentEditor
        {
            get
            {
                txtPathToCurrentFile.Text = string.Empty;

                if (-1 == tabFiles.SelectedIndex)                                   
                    return null;                

                TabPage page = tabFiles.TabPages[tabFiles.SelectedIndex];
                
                foreach (Control ctl in page.Controls[0].Controls)
                {
                    if (ctl is Editor)
                    {
                        Editor editor = (Editor)ctl;
                        txtPathToCurrentFile.Text = editor.m_path;
                        return editor;
                    }
                }                
                return null;
            }
        }

        public static FrmTipSetup m_frmTipSetup = null;

        string m_CurCommand = string.Empty;

        public ctlIS m_ctlIS = null;
        private ctlInfoTip m_ctlInfo = null;

        private List<Command> m_list = new List<Command>();

        // Forms
        private FrmFindAndReplace m_frmFindAndReplace = null;
        private FrmFindImage m_frmFindImage = null;
        private FrmSQLConnection m_frmSQLConnection = null;
        private FrmHotList m_frmHotList = null;
        private FormServerConfigure m_frmServerConfigurator = null;
        private FormServerFolderStructure m_frmServerFolderStructure = null;
        

        public ctlInfoTip tip
        {
            get { return m_ctlInfo; }
        }


        public FrmScripter()
        {
            InitializeComponent();
        }

        public void UpdateMRUMenu()
        {
            Opener.mru.Load();

            const int from = 7;
            int i = from;
            foreach (string str in Opener.mru.lst)
            {
                mnuFile.DropDown.Items[i].Text = str;
                mnuFile.DropDown.Items[i].Visible = true;
                ++i;
            }

            for (int j = i; j < from + NUM_OF_MRUS; ++j)
            {
                mnuFile.DropDown.Items[j].Visible = false;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {            
            Visible = false;

            Text = "QABOT-Scripter " + Application.ProductVersion;
            
            Editor.frm = this;            
            ScripterDebug.frm = this;            
            Opener.frm = this;
            TemplateManager.frm = this;
            FrmTipSetup.parent = this;

            ctlHeader1.SetGradient(Color.Cyan, Color.LightSkyBlue);
            ctlHeader2.SetGradient(Color.Cyan, Color.LightSkyBlue);
            ctlHeader3.SetGradient(Color.Cyan, Color.LightSkyBlue);

            ctlHeader2.Text = "Script Explorer";
            ctlHeader2.Invalidate();
                                    
            Opener.mru = new MRU(Path.Combine(Application.StartupPath, @"data\mru\QABOT-Scripter.mru"), NUM_OF_MRUS);
            Opener.LastOpenedFolder = Application.StartupPath;

            OnClearOutput();
            OnClearDebug();

            LoadHotList();
            TemplateManager.Load();

            // intellisense
            m_ctlIS = new ctlIS();
            
            m_ctlIS.Width = 300;
            m_ctlIS.Height = 150;
            m_ctlIS.Left = 400;
            m_ctlIS.Top = 300;
            m_ctlIS.Visible = false;

            Controls.Add(m_ctlIS);

            m_ctlIS.BringToFront();

            // tooltip
            m_ctlInfo = new ctlInfoTip();
            m_ctlInfo.Left = 0;
            m_ctlInfo.Top = 0;
            m_ctlInfo.Visible = false;

            Controls.Add(m_ctlInfo);

            m_ctlInfo.BringToFront();

            Intellisense._is = m_ctlIS;           

            cboExternalData.Items.Add("No Data File");
            cboExternalData.SelectedIndex = 0;            

            Rectangle rc = Screen.PrimaryScreen.Bounds;
            Left = rc.Right / 2 - Width / 2;
            Top = (rc.Height - Height) - 32;

            Show();
            Application.DoEvents();
           
            UpdateMRUMenu();

            for (int i=0; i<Language.commands.Length; ++i)
            {
                m_list.Add(new Command(Language.commands[i], i));
            }

            foreach (Command item in m_list)
            {
                lstCommands.Items.Add(item);
            }
                        
            ctlHeader1.Text = "Commands [" + lstCommands.Items.Count + "]";
            ctlHeader1.Invalidate();

            LoadHelpOrder();

            // reflect settings on the main form
            mnuMinimizeWhenPilotIsLaunched.Checked = Settings.Get("LaunchPilotMinimize") == "1" ? true : false;
            mnuSettingsCompatibility.Checked = Settings.Get("Compatibility") == "1" ? true : false;
            mnuEnableTooltip.Checked = Settings.Get("EnableTooltip") == "1" ? true : false;
            mnuAutorun.Checked = Settings.Get("AutorunPilot") == "1" ? true : false;
            DisplayActiveServerIP();

            m_bEnableTooltip = mnuEnableTooltip.Checked;
            Opener._bCompatibilty = mnuSettingsCompatibility.Checked;

            UpdateCompatibilityStatus();

            TemplateManager.NewScript();

            tabFiles.PreRemoveTabPage = CanCloseDocTab;

            foreach (string file in Scripter.ScriptsToLoad)
            {
                Opener.Open(file, null);
            }            
        }

        public void DisplayActiveServerIP()
        {
            toolServerConfigureAccess.Text = Settings.Get("ServerIP");
        }

        private void OnClearDebug()
        {
            txtDebug.Clear();
            txtDebug.Text = "QABOT - Scripter Debug:";
        }

        private void OnClearOutput()
        {
            txtVMOutput.Clear();
            txtVMOutput.Text = "QABOT - Scripter Output:";
        }

        public string LookUpProjectExplorer(string word, ref string path)
        {
            string value = string.Empty;

            string findWhat = word.ToLower();

            if (word.StartsWith("@")) // variable
            {
                findWhat += " =";
            }

            for (int i = 1; i < tvwImported.Nodes.Count; ++i)
            {
                TreeNode fileNode = tvwImported.Nodes[i];

                foreach (TreeNode group in fileNode.Nodes) // variables, functions
                {
                    foreach (TreeNode node in group.Nodes) // the meat
                    {
                        string nodeText = node.Text.ToLower();

                        if (nodeText.StartsWith(findWhat))
                        {
                            path = fileNode.Text;

                            return node.Text;
                        }
                    }
                }
            }
            return string.Empty;
        }


        private string LookUpImportModule(string word)
        {
            string nodeText;
            string findWhat = word.ToLower();

            if (word.StartsWith("@"))
                findWhat += " ="; // variable            
            else
                findWhat += "("; // function
                                                                                 
            for (int i = 1; i < tvwImported.Nodes.Count; ++i)
            {
                TreeNode fileNode = tvwImported.Nodes[i];                    

                foreach (TreeNode group in fileNode.Nodes) // variables, functions
                {
                    foreach (TreeNode node in group.Nodes)
                    {
                        nodeText = node.Text.ToLower();

                        if (nodeText.StartsWith(findWhat))
                        {
                            return "Value: " + node.Text + Environment.NewLine + "Module: " + fileNode.Text;
                        }
                    }
                }
            }        
            return string.Empty;
        }

        public void HoverHelp(string word)
        {
            if (m_lastWord == word)
                return;

            m_lastWord = word;

            ShowHelpOnCurrentCommand(word);

            int x = Control.MousePosition.X;
            int y = Control.MousePosition.Y;

            // convert to client
            Point cpos = PointToClient(new Point(x, y));
            cpos.X += 2;
            cpos.Y += 9;

            if (word.EndsWith(".bmp\""))
            {
                string path = word.Substring(1, word.Length - 2);

                path = Path.GetFullPath(path);

                Output("fullpath: " + path);

                if (File.Exists(path))
                {
                    Output("ImageTip path found: " + path);

                    if (null != pic.Image)
                        pic.Image.Dispose();

                    using (Image img = Image.FromFile(path))
                    {
                        if (null != pic.Image)
                        {
                            pic.Image.Dispose();
                            pic.Image = null;
                        }

                        pic.Image = new Bitmap(img);
                        pic.Visible = true;
                        pic.Size = img.Size;
                    }

                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.Show();
                    pic.Location = new Point(cpos.X - pic.Width, cpos.Y - pic.Height - CurrentEditor.Font.Height * 2);

                    picShadow.BackColor = Color.Black;
                    picShadow.Size = pic.Size;

                    picShadow.Show();
                    picShadow.Location = new Point(cpos.X + 4 - pic.Width, cpos.Y + 4 - pic.Height - CurrentEditor.Font.Height * 2);
                    picShadow.BringToFront();

                    pic.BringToFront();                    
                }
                else
                    Output("ImageTip path NOT found: " + path);

                return;
            }

            string text = LookUpImportModule(word);

            if (text == string.Empty)
            {
                HideInfoTip();
                return;
            }

            if (word.StartsWith("@"))
            {
                m_ctlInfo.typeText = "Variable";
                m_ctlInfo.back = Color.DarkOrange;
            }
            else
            {
                m_ctlInfo.typeText = "Function";
                m_ctlInfo.back = Color.DarkSalmon;
            }
            
            m_ctlInfo.Location = cpos;
            m_ctlInfo.text = text;
            m_ctlInfo.Display();

            Output("displaying " + word);
        }

        public void HideInfoTip()
        {            
            m_ctlInfo.Dismiss();
            m_frmImage.Hide();

            if (null != pic.Image)
            {
                pic.Image.Dispose();
                pic.Image = null;
            }
            pic.Hide();
            picShadow.Hide();
            m_lastWord = string.Empty;
        }


        private void UpdateCompatibilityStatus()
        {
            if (mnuSettingsCompatibility.Checked)
                status3.Text = "Compatibility: On";
            else
                status3.Text = "Compatibility: Off";
        }

        private void LoadHelpOrder()
        {
            HelpItem.SetOrder("210");
            UpdateHelpOrderMenu();
        }

        private void UpdateHelpOrderMenu()
        {
            string order = HelpItem.GetOrder();

            for (int i = 0; i < mnuConfigHelpOrder.DropDown.Items.Count; ++i)
            {
                string tag = (string)mnuConfigHelpOrder.DropDown.Items[i].Tag;

                if (tag != null)
                {
                    ToolStripMenuItem item = (ToolStripMenuItem)mnuConfigHelpOrder.DropDown.Items[i];
                    if (tag == order)
                        item.Checked = true;
                    else
                        item.Checked = false;
                }
            }

            HelpItem hlpItem = Help.Get(m_CurCommand);

            if (null != hlpItem)
                ShowCommandDesc(hlpItem.ToString());
        }

        private void ShowCommandDesc(string strTextToAdd)
        {
            rtbInfo.Clear();
            rtbInfo.AppendText(strTextToAdd);
            string strRTF = rtbInfo.Rtf;
            rtbInfo.Clear();

            string colors = @"{\colortbl;\red128\green0\blue0;\red0\green128\blue0;\red0\green0\blue255;\red128\green128\blue128;}";
            int iRTFLoc = strRTF.IndexOf("\\rtf");
            int iInsertLoc = strRTF.IndexOf('{', iRTFLoc);
                        
            if (iInsertLoc == -1) iInsertLoc = strRTF.IndexOf('}', iRTFLoc) - 1;
            
            strRTF = strRTF.Insert(iInsertLoc, colors);
            strRTF = strRTF.Replace(@"\\", @"\");
            rtbInfo.Rtf = strRTF;
        }

        private void lstCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            Command cmd = (Command)lstCommands.SelectedItem;
            HelpItem hlpItem = Help.Get(cmd.Name);

            m_CurCommand = cmd.Name;

            if (null == hlpItem)
                ShowCommandDesc("<help not available>");
            else
                ShowCommandDesc(hlpItem.ToString());
        }

        public void Output(string text)
        {
            txtVMOutput.Text += Environment.NewLine + text;

            txtVMOutput.SelectionStart = txtVMOutput.Text.Length;
            txtVMOutput.ScrollToCaret();
        }

        public void DebugOutput(string text)
        {
            txtDebug.Text += Environment.NewLine + text;

            txtDebug.SelectionStart = txtDebug.Text.Length;
            txtDebug.ScrollToCaret();
        }

        public void ClearDebugOutput()
        {
            OnClearDebug();
        }

        public int GetActiveTabIndex()
        {
            return tabDetails.SelectedIndex;
        }

        public void SetActiveTabIndex(int index)
        {
            tabDetails.SelectedIndex = index;
        }
        
        public void ShowHelpOnCurrentCommand(string command)
        {
            int index = lstCommands.FindString(command);

            if (-1 != index)
                lstCommands.SelectedIndex = index;
        }      

        public void OnReParse()
        {
            if (null == CurrentEditor)
                return;

            txtPathToCurrentFile.Text = CurrentEditor.Path2File;
            ScriptExplorer.Parse(this, CurrentEditor);
        }



        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Save();
            
            for (int i = 0; i < tabFiles.TabCount; i++)
            {
                tabFiles.SelectedIndex = i;

                Editor editor = CurrentEditor;

                if (null != editor)
                {
                    e.Cancel = !editor.CanClose();

                    if (e.Cancel)
                        return;
                }                
            }

            if (m_bUpdating)
                System.Diagnostics.Process.Start("selfupdater.exe", Globals.exeScripter + " NEW_" + Globals.exeScripter);
        }

        private void lstCommands_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (null == CurrentEditor)
                return;

            if (null == lstCommands.SelectedItem)
                return;

            Command cmd = (Command)lstCommands.SelectedItem;
            CurrentEditor.Insert(cmd.Name, Color.Blue);
        }
        
        private void launchQABOTExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "QABOT-Explorer.exe");

            if (File.Exists(path))
                System.Diagnostics.Process.Start(path);
            else
                MessageBox.Show("'QABOT-Explorer.exe' does not exist on '" + path + "'", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            TemplateManager.NewScript();
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            Opener.Open(string.Empty, null);
        }

        private void mnuLaunchPilot_Click(object sender, EventArgs e)
        {
            OnLaunchPilot();
        }

        private void OnLaunchPilot()
        {
            if (null == CurrentEditor)
                return;

            if (CurrentEditor.Dirty)
            {
                DialogResult res = UtilSys.MessageBoxQuestion("Script not saved. Do you want to Launch the Pilot?");

                if (DialogResult.No == res)
                    return;
            }            

            if (CurrentEditor.CanLaunch)
            {
                string path2Data = string.Empty;

                if (-1 != cboExternalData.SelectedIndex && 0 != cboExternalData.SelectedIndex)
                {
                    path2Data = (string)cboExternalData.Items[cboExternalData.SelectedIndex];
                }
                Launcher.Pilot(this, CurrentEditor.Path2File, path2Data, Settings.Get("LaunchPilotMinimize") == "1" ? true : false, Settings.Get("AutorunPilot") == "1" ? true : false);
            }
            else
                UtilSys.MessageBox("QABOT-Pilot could be not launched." + Environment.NewLine + "Insert [sys.start] to mark script entry point.");
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            OnSave();
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }

        private void mnuFileMru0_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuFileMru0.Text, null);
        }

        private void mnuFileMru1_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuFileMru1.Text, null);
        }

        private void mnuFileMru2_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuFileMru2.Text, null);
        }

        private void mnuFileMru3_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuFileMru3.Text, null);
        }

        private void mnuFileMru4_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuFileMru4.Text, null);
        }

        private void mnuFileMru5_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuFileMru5.Text, null);
        }

        private void mnuExParDesc_Click(object sender, EventArgs e)
        {
            HelpItem.SetOrder(mnuExParDesc.Tag.ToString());
            UpdateHelpOrderMenu();
        }

        private void mnuExDescPar_Click(object sender, EventArgs e)
        {
            HelpItem.SetOrder(mnuExDescPar.Tag.ToString());
            UpdateHelpOrderMenu();
        }

        private void mnuDescExPar_Click(object sender, EventArgs e)
        {
            HelpItem.SetOrder(mnuDescExPar.Tag.ToString());
            UpdateHelpOrderMenu();
        }

        private void mnuDescParEx_Click(object sender, EventArgs e)
        {
            HelpItem.SetOrder(mnuDescParEx.Tag.ToString());
            UpdateHelpOrderMenu();
        }

        private void mnuParExDesc_Click(object sender, EventArgs e)
        {
            HelpItem.SetOrder(mnuParExDesc.Tag.ToString());
            UpdateHelpOrderMenu();
        }

        private void mnuParDescEx_Click(object sender, EventArgs e)
        {
            HelpItem.SetOrder(mnuParDescEx.Tag.ToString());
            UpdateHelpOrderMenu();
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            CurrentEditor.SaveAs();
        }

        private void cmdSetExternalDataFileLocation_Click(object sender, EventArgs e)
        {
            string path = UtilSys.OpenFileDialog("Open QABOT Data File",
                                                 "QABOT Data Files (*.dat)|*.dat|All Files (*.*)|*.*",
                                                 Application.StartupPath/*m_LastOpenedFolderData*/);

            if (string.Empty == path)
                return;

            cboExternalData.Items.Add(path);
            cboExternalData.SelectedIndex = cboExternalData.Items.Count - 1;
        }


        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            OnSave();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            CurrentEditor.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            CurrentEditor.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            CurrentEditor.MyPaste();
        }

        private void frmScripter_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control)
                return;

            switch (e.KeyCode)
            {
                case Keys.F:
                    if (null != CurrentEditor)
                        ShowFindDialog();
                    e.SuppressKeyPress = true;
                break;

                case Keys.P:
                    OnLaunchPilot();
                    e.SuppressKeyPress = true;
                break;

                case Keys.E:
                    Launcher.Explorer();
                    e.SuppressKeyPress = true;
                break;

                case Keys.O:
                    Opener.Open(string.Empty, null);
                    e.SuppressKeyPress = true;
                break;

                case Keys.S:
                    OnSave();                        
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                break;

                case Keys.M:
                    Launcher.Master();
                    e.SuppressKeyPress = true;
                break;

                case Keys.N:
                    TemplateManager.NewScript();
                    e.SuppressKeyPress = true;
                break;
            }
        }
        
        private void mnuComboDataFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowExternalData((string)cboExternalData.Items[cboExternalData.SelectedIndex]);
        }

        private void ShowExternalData(string path)
        {
            lstData.Items.Clear();

            if (path == "No Data File")
            {
                ctlHeader3.Text = "External Data [0]";
                ctlHeader3.Invalidate();
            }
            else
            {                
                if (File.Exists(path))
                {
                    List<string> lst = UtilIO.ReadFile(path);

                    foreach (string str in lst)
                    {
                        lstData.Items.Add(str);
                    }
                    ctlHeader3.Text = "External Data [" + lstData.Items.Count + "]";
                    ctlHeader3.Invalidate();
                }
                else
                    UtilSys.MessageBox("Unable to load '" + path + "' file.");
            }
        }

        private void mnuMinimizeWhenPilotIsLaunched_Click(object sender, EventArgs e)
        {
            mnuMinimizeWhenPilotIsLaunched.Checked = !mnuMinimizeWhenPilotIsLaunched.Checked;
            
            if (mnuMinimizeWhenPilotIsLaunched.Checked)
                Settings.Update("LaunchPilotMinimize", "1");
            else
                Settings.Update("LaunchPilotMinimize", "0");
        }

        private void mnuUndo_Click(object sender, EventArgs e)
        {
            if (null != CurrentEditor)
                CurrentEditor.MyUndo();
        }

        private void ctxMenuDocClose_Click(object sender, EventArgs e)
        {
            OnCloseDocTab();
        }

        private void OnCloseDocTab()
        {
            if (CurrentEditor.CanClose())
            {
                TabPage page = CurrentEditor.page;
                tabFiles.TabPages.Remove(page);
            }
                         
            if (0 == tabFiles.TabPages.Count)
                tvwImported.Nodes.Clear();
        }

        private bool CanCloseDocTab(int index)
        {
            TabPage page = tabFiles.TabPages[index];
            tabFiles.SelectedIndex = index;
            page.Select();

            if (CurrentEditor.CanClose())
            {
                if (1 == tabFiles.TabPages.Count)
                    tvwImported.Nodes.Clear();

                return true;
            }
            else
                return false;
        }

        private void tabFiles_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TCHITTESTINFO HTI = new TCHITTESTINFO(e.X, e.Y);
                TabPage hotTab = tabFiles.TabPages[SendMessage(tabFiles.Handle, TCM_HITTEST, IntPtr.Zero, ref HTI)];
                tabFiles.ContextMenuStrip = hotTab.ContextMenuStrip;

                int index = tabFiles.TabPages.IndexOf(hotTab);

                tabFiles.SelectedIndex = index;
                hotTab.Select();                
            }
        }

        private void tabFiles_MouseUp(object sender, MouseEventArgs e)
        {
            tabFiles.ContextMenuStrip = null;
        }

        private void ctxMenuDocReParse_Click(object sender, EventArgs e)
        {
            OnReParse();
        }

        private void tabFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindAndReplace.SearchIndex = 0;
            txtPathToCurrentFile.Text = string.Empty;

            if (-1 == tabFiles.SelectedIndex)
                return;            

            if (null == CurrentEditor)
                return;

            OnReParse();
         
            UpdateDebugButtons();
        }

        public void UpdateDebugButtons()
        {
            bool value = !CurrentEditor.Path2File.EndsWith("sci");
            
            toolDebug.Enabled = value;
            toolPilot.Enabled = value;
            mnuDebugStart.Enabled = value;                    
        }

        private void ctxMenuDocSave_Click(object sender, EventArgs e)
        {
            OnSave();
        }

        private void mnuFileSaveAll_Click(object sender, EventArgs e)
        {
            OnSaveAll();
        }

        private void OnSave()
        {
            if (null == CurrentEditor)
                return;

            CurrentEditor.Save();
            OnReParse();
        }

        private void OnSaveAll()
        {
            Editor editor = null;
            foreach(TabPage page in tabFiles.TabPages)
            {

                editor = (Editor)page.Controls[0].Controls[0];


                editor.Save();
            }
        }

        public void LoadHotList()
        {
            string[] HotList = new string[10];

            for(int j = 0; j < HotList.Length; ++j)
            {                
                string path = Settings.Get("HotSlot" + j);

                if (path.Length > 0)
                {
                    mnuHotList.DropDown.Items[j].Text = path;
                    ctxMenuAddToHotList.DropDown.Items[j].Text = path;
                }
                else
                {
                    mnuHotList.DropDown.Items[j].Text = "Empty slot " + j;
                    ctxMenuAddToHotList.DropDown.Items[j].Text = "Empty slot " + j;
                }
            }
        }

        private void Save2HotList(int index)
        {
            if (Scripter.DEF_FILE_NAME != CurrentEditor.Path2File)
            {
                Settings.Add("HotSlot" + index, CurrentEditor.Path2File);
                Settings.Save();
                LoadHotList();
            }
            else
            {
                UtilSys.MessageBoxInfo("Script [" + Scripter.DEF_FILE_NAME + "] is not saved. Could not add to the Hot List. Save the script first.");
            }
        }

        private void ctxHotListslot0_Click(object sender, EventArgs e)
        {
            Save2HotList(0);
        }

        private void ctxHotListslot1_Click(object sender, EventArgs e)
        {
            Save2HotList(1);
        }

        private void ctxHotListslot2_Click(object sender, EventArgs e)
        {
            Save2HotList(2);
        }

        private void ctxHotListslot3_Click(object sender, EventArgs e)
        {
            Save2HotList(3);
        }

        private void ctxHotListslot4_Click(object sender, EventArgs e)
        {
            Save2HotList(4);
        }

        private void ctxHotListslot5_Click(object sender, EventArgs e)
        {
            Save2HotList(5);
        }

        private void ctxHotListslot6_Click(object sender, EventArgs e)
        {
            Save2HotList(6);
        }

        private void ctxHotListslot7_Click(object sender, EventArgs e)
        {
            Save2HotList(7);
        }

        private void ctxHotListslot8_Click(object sender, EventArgs e)
        {
            Save2HotList(8);
        }

        private void ctxHotListslot9_Click(object sender, EventArgs e)
        {
            Save2HotList(9);
        }

        private void mnuHot0_Click(object sender, EventArgs e)
        {            
            Opener.Open(mnuHot0.Text, null);
        }

        private void mnuHot1_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuHot1.Text, null);
        }

        private void mnuHot2_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuHot2.Text, null);
        }

        private void mnuHot3_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuHot3.Text, null);
        }

        private void mnuHot4_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuHot4.Text, null);
        }

        private void mnuHot5_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuHot5.Text, null);
        }

        private void mnuHot6_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuHot6.Text, null);
        }

        private void mnuHot7_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuHot7.Text, null);
        }

        private void mnuHot8_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuHot8.Text, null);
        }

        private void mnuHot9_Click(object sender, EventArgs e)
        {
            Opener.Open(mnuHot9.Text, null);
        }

        private void ctxMenuInsertCall_Click(object sender, EventArgs e)
        {
            if (null == tvwImported.SelectedNode)
                UtilSys.MessageBox("Select a valid node (variable or function).");

            if ((string)tvwImported.SelectedNode.Tag == Opener.VARIABLE_TAG)
            {
                string tmp = tvwImported.SelectedNode.Text;
                int pos = tmp.IndexOf('=');
                CurrentEditor.Insert(tmp.Substring(0, pos-1), Color.Orange);
                return;
            }

            if ((string)tvwImported.SelectedNode.Tag == Opener.FUNCTION_TAG)
            {
                CurrentEditor.Insert("call " + tvwImported.SelectedNode.Text, Color.Blue);
                return;
            }

            UtilSys.MessageBox("Invalid node '" + tvwImported.SelectedNode.Text + "' to be inserted into Script.\r\nPlease select a node which contains a variable or a function definition.");
        }

        private void tvwImported_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            
        	TreeNode node = tvwImported.GetNodeAt(e.X, e.Y);

            if (null != node)
                tvwImported.SelectedNode = node;
        }

        private void ctxTemplateSlot0_Click(object sender, EventArgs e)
        {
            TemplateManager.Save(0);
        }

        private void ctxTemplateSlot1_Click(object sender, EventArgs e)
        {
            TemplateManager.Save(1);
        }

        private void ctxTemplateSlot2_Click(object sender, EventArgs e)
        {
            TemplateManager.Save(2);
        }

        private void ctxTemplateSlot3_Click(object sender, EventArgs e)
        {
            TemplateManager.Save(3);
        }

        private void ctxTemplateSlot4_Click(object sender, EventArgs e)
        {
            TemplateManager.Save(4);
        }

        private void ctxTemplateSlot5_Click(object sender, EventArgs e)
        {
            TemplateManager.Save(5);
        }

        private void ctxTemplateSlot6_Click(object sender, EventArgs e)
        {
            TemplateManager.Save(6);
        }

        private void ctxTemplateSlot7_Click(object sender, EventArgs e)
        {
            TemplateManager.Save(7);
        }

        private void ctxTemplateSlot8_Click(object sender, EventArgs e)
        {
            TemplateManager.Save(8);
        }

        private void ctxTemplateSlot9_Click(object sender, EventArgs e)
        {
            TemplateManager.Save(9);
        }


        private void ReflectionTest()
        {
            //AppDomain dom = AppDomain.CreateDomain("myPilotDomain");

            try
            {                
               // dom.Load(Path.Combine(Application.StartupPath, @"data\bin\Pilot.dll"));

                // Use the file name to load the assembly into the current application domain.
                Assembly a = Assembly.LoadFrom(Path.Combine(Application.StartupPath, @"data\bin\Pilot.dll"));

                // Get the type to use.
                Type myType = a.GetType("QABOT.Pilot");

                // Get the method to call.
                MethodInfo mymethod = myType.GetMethod("Version");

                // Create an instance.
                Object obj = Activator.CreateInstance(myType);

                // Execute the method.

                //object[] mParam = new object[] {3, 2};

                //object ob = mymethod.Invoke(obj, mParam);

                object ob = mymethod.Invoke(obj, null);

                string retVal = (string)ob;

                UtilSys.MessageBox(retVal);

                FieldInfo fi = myType.GetField("subCommands");

                string[] arr = (string[])fi.GetValue(obj);

                foreach (string item in arr)
                {
                    UtilSys.MessageBox(item);
                }
            }
            catch (Exception ex)
            {
                UtilSys.MessageBox(ex.Message);
            }
            finally
            {
               // AppDomain.Unload(dom);
            }
        }
        
        private void tlbNew_Click(object sender, EventArgs e)
        {
            TemplateManager.NewScript();
            UpdateDebugButtons();
        }

        private void ctxImportOpenInScripter_Click(object sender, EventArgs e)
        {
            string temp = string.Empty;

            if ((string)tvwImported.SelectedNode.Tag == Opener.FUNCTION_TAG || (string)tvwImported.SelectedNode.Tag == Opener.VARIABLE_TAG)
                temp = tvwImported.SelectedNode.Text;

            Opener.OpenIn(Opener.EnumOpener.Scripter);

            if (!string.IsNullOrEmpty(temp))
                FindAndReplace.Find(CurrentEditor, temp, true, true);            
        }

        private void ctxImportOpenInNotepad_Click(object sender, EventArgs e)
        {
            Opener.OpenIn(Opener.EnumOpener.Notepad);
        }

        private bool OnCloseAllDocuments()
        {
            while (tabFiles.TabCount > 0)
            {
                TabPage page = tabFiles.TabPages[0];
                Editor editor = (Editor)page.Controls[0].Controls[0];

                if (!editor.CanClose())
                    return false;
                else
                    tabFiles.TabPages.Remove(page);
            }

            tvwImported.Nodes.Clear();
            return true;
        }

        private void mnuWindowCloseAllDocuments_Click(object sender, EventArgs e)
        {
            OnCloseAllDocuments();
        }

        private void saveallToolStripButton_Click(object sender, EventArgs e)
        {
            OnSaveAll();
        }

        private void reparseToolStripButton_Click(object sender, EventArgs e)
        {
            OnReParse();
        }

        private void reflectionOfPilotdllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReflectionTest();
        }

        private void mnuEditUndo_Click(object sender, EventArgs e)
        {
            if (null != CurrentEditor)
                CurrentEditor.MyUndo();
        }

        private void mnuEditCut_Click(object sender, EventArgs e)
        {
            if (null != CurrentEditor)
                CurrentEditor.Cut();
        }

        private void mnuEditCopy_Click(object sender, EventArgs e)
        {
            if (null != CurrentEditor)
                CurrentEditor.Copy();
        }

        private void mnuEditPaste_Click(object sender, EventArgs e)
        {
            if (null != CurrentEditor)
                CurrentEditor.Paste();
        }

        private void mnuEditDelete_Click(object sender, EventArgs e)
        {
            if (null != CurrentEditor)
                CurrentEditor.SelectedText = string.Empty;
        }

        private void mnuEditSelectAll_Click(object sender, EventArgs e)
        {
            if (null != CurrentEditor)
                CurrentEditor.SelectAll();
        }

        private void mnuEditFindAndReplace_Click(object sender, EventArgs e)
        {
            ShowFindDialog();
        }

        private void mnuSettingsCompatibility_Click(object sender, EventArgs e)
        {
            mnuSettingsCompatibility.Checked = !mnuSettingsCompatibility.Checked;

            if (mnuSettingsCompatibility.Checked)
                Settings.Update("Compatibility", "1");
            else
                Settings.Update("Compatibility", "0");

            Opener._bCompatibilty = mnuSettingsCompatibility.Checked;
            UpdateCompatibilityStatus();
        }

        private void lstData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (null == CurrentEditor)
                return;

            if (null == lstData.SelectedItem)
                return;

            string var = (string)lstData.SelectedItem;
                        
            string[] tmp = var.Split('=');

            if (tmp.Length >= 1)
            {
                string temp = tmp[0].Trim();
                CurrentEditor.Insert(temp, Color.OrangeRed);
            }
        }

        private void mnuWindowClearOutput_Click(object sender, EventArgs e)
        {
            OnClearOutput();
        }

        private void mnuTemplatesLoad0_Click(object sender, EventArgs e)
        {
            TemplateManager.LoadItem(0);
        }

        private void mnuTemplatesLoad1_Click(object sender, EventArgs e)
        {
            TemplateManager.LoadItem(1);
        }

        private void mnuTemplatesLoad2_Click(object sender, EventArgs e)
        {
            TemplateManager.LoadItem(2);
        }

        private void mnuTemplatesLoad3_Click(object sender, EventArgs e)
        {
            TemplateManager.LoadItem(3);
        }

        private void mnuTemplatesLoad4_Click(object sender, EventArgs e)
        {
            TemplateManager.LoadItem(4);
        }

        private void mnuTemplatesLoad5_Click(object sender, EventArgs e)
        {
            TemplateManager.LoadItem(5);
        }

        private void mnuTemplatesLoad6_Click(object sender, EventArgs e)
        {
            TemplateManager.LoadItem(6);
        }

        private void mnuTemplatesLoad7_Click(object sender, EventArgs e)
        {
            TemplateManager.LoadItem(7);
        }

        private void mnuTemplatesLoad8_Click(object sender, EventArgs e)
        {
            TemplateManager.LoadItem(8);
        }

        private void mnuTemplatesLoad9_Click(object sender, EventArgs e)
        {
            TemplateManager.LoadItem(9);
        }

        private void mnuTemplatesSetDefault0_Click(object sender, EventArgs e)
        {
            TemplateManager.DefaultIndex = 0;
        }

        private void mnuTemplatesSetDefault1_Click(object sender, EventArgs e)
        {
            TemplateManager.DefaultIndex = 1;
        }

        private void mnuTemplatesSetDefault2_Click(object sender, EventArgs e)
        {
            TemplateManager.DefaultIndex = 2;
        }

        private void mnuTemplatesSetDefault3_Click(object sender, EventArgs e)
        {
            TemplateManager.DefaultIndex = 3;
        }

        private void mnuTemplatesSetDefault4_Click(object sender, EventArgs e)
        {
            TemplateManager.DefaultIndex = 4;
        }

        private void mnuTemplatesSetDefault5_Click(object sender, EventArgs e)
        {
            TemplateManager.DefaultIndex = 5;
        }

        private void mnuTemplatesSetDefault6_Click(object sender, EventArgs e)
        {
            TemplateManager.DefaultIndex = 6;
        }

        private void mnuTemplatesSetDefault7_Click(object sender, EventArgs e)
        {
            TemplateManager.DefaultIndex = 7;
        }

        private void mnuTemplatesSetDefault8_Click(object sender, EventArgs e)
        {
            TemplateManager.DefaultIndex = 8;
        }

        private void mnuTemplatesSetDefault9_Click(object sender, EventArgs e)
        {
            TemplateManager.DefaultIndex = 9;
        }

        private void mnuDoSyntaxHighlight_Click(object sender, EventArgs e)
        {
            if (null == CurrentEditor)
                return;
            
            CurrentEditor.SyntaxColorAll();            
        }

        private void mnuIndent_Click(object sender, EventArgs e)
        {
            if (null == CurrentEditor)
                return;

            CurrentEditor.IndentAll();
            CurrentEditor.Invalidate();            
        }

        private void mnuTemplatesSetDefaultBuiltIn_Click(object sender, EventArgs e)
        {
            TemplateManager.DefaultIndex = -1;
        }

        public void CreateNewDocument(string path)
        {
            tabFiles.TabPages.Add(path);
            TabPage page = tabFiles.TabPages[tabFiles.TabCount - 1];

            page.BackColor = Color.DeepSkyBlue;
            page.BorderStyle = BorderStyle.None;
            page.Text = Path.GetFileName(path);
            page.ContextMenuStrip = ctxMenuDocTabs;            

            ctlScriptEditor edtr = new ctlScriptEditor();

            page.Controls.Add(edtr);

            edtr.page = page;
            edtr.Path = path;

            edtr.Dock = DockStyle.Fill;

            tabFiles.SelectedIndex = tabFiles.TabCount - 1;
        }

        private void ShowFindDialog()
        {
            if (null == CurrentEditor)
                return;
            
            if (null == m_frmFindAndReplace || m_frmFindAndReplace.IsDisposed)
            {
                m_frmFindAndReplace = new FrmFindAndReplace();
                FindAndReplace.SearchIndex = 0;
            }
            
            int parentHalfWidth = this.Width / 2;
            int dialogHalfWidth = m_frmFindAndReplace.Width / 2;

            int parentHalfHeight = this.Height / 2;
            int dialogHalfHeight = m_frmFindAndReplace.Height / 2;


            m_frmFindAndReplace.Left = (this.Left + parentHalfWidth) - dialogHalfWidth;
            m_frmFindAndReplace.Top = (this.Top + parentHalfHeight) - dialogHalfHeight;
            m_frmFindAndReplace.Tag = CurrentEditor.SelectedText;
            m_frmFindAndReplace.Show();
            m_frmFindAndReplace.Activate();            
        }

        private void mnuFindAndReplace_Click(object sender, EventArgs e)
        {
            ShowFindDialog();
        }

        private void mnuSettingsClearSearchHistory_Click(object sender, EventArgs e)
        {
            Scripter.ClearSearchHistory();
            Scripter.ClearReplaceHistory();
        }

        private void txtQuickFind_TextChanged(object sender, EventArgs e)
        {
            m_nodeSearchResults.Nodes.Clear();

            if (0 == txtQuickFind.Text.Length)
            {
                m_nodeSearchResults.Nodes.Add("[empty]");
                return;
            }

            TreeNode parent = null;
            string findWhat = txtQuickFind.Text.ToLower();

            for (int i = 1; i < tvwImported.Nodes.Count; ++i)
            {
                TreeNode fileNode = tvwImported.Nodes[i];

                parent = null;

                foreach (TreeNode group in fileNode.Nodes) // variables, functions
                {
                    foreach (TreeNode node in group.Nodes) // the meat
                    {
                        string nodeText = node.Text.ToLower();

                        if (nodeText.StartsWith(findWhat))
                        {
                            if (null == parent)
                            {
                                parent = m_nodeSearchResults.Nodes.Add(fileNode.Text);
                                parent.BackColor = Color.LightSkyBlue;
                            }

                            TreeNode n = parent.Nodes.Add(node.Text);                            
                            n.Tag = "SEARCH_RESULT";
                        }
                    }
                }
            }
            
            if (0 == m_nodeSearchResults.Nodes.Count)
                m_nodeSearchResults.Nodes.Add("[empty]");

            m_nodeSearchResults.ExpandAll();
            tvwImported.SelectedNode = m_nodeSearchResults;
                        
            SetTreeViewScrollPos(tvwImported, new Point(0, 0));
        }

        private void SetTreeViewScrollPos(TreeView treeView, Point scrollPosition)
        {
            treeView.BeginUpdate();
            //SetScrollPos((IntPtr)treeView.Handle, SB_HORZ, scrollPosition.X, true);
            SetScrollPos((IntPtr)treeView.Handle, SB_VERT, scrollPosition.Y, true);
            treeView.EndUpdate();
        }

        public void GoToDefinition(string path, string item)
        {            
            Directory.SetCurrentDirectory(Scripter.AppPath);

            //UtilSys.MessageBoxInfo("CurrentDir: " + Directory.GetCurrentDirectory());
            //UtilSys.MessageBoxInfo(path);

            int i = 0;

            foreach (TabPage page in tabFiles.TabPages)
            {
                Editor editor = (Editor)page.Controls[0].Controls[0];

                if (editor.Path2File == path)
                {
                    tabFiles.SelectedIndex = i;
                    FindAndReplace.SearchIndex = 0;
                    FindAndReplace.Find(CurrentEditor, item, true, true);
                    return;
                }
                ++i;
            }

            Opener.Open(path, null);
            FindAndReplace.Find(CurrentEditor, item, true, true);           
        }
 
        private void tvwImported_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (null == e.Node.Tag)
                return;

            if ( "SEARCH_RESULT" == e.Node.Tag.ToString() ||
                 "FUNCTION" == e.Node.Tag.ToString() ||
                 "VARIABLE" == e.Node.Tag.ToString() 
               )
            {
                string path = string.Empty;

                if ( "SEARCH_RESULT" == e.Node.Tag.ToString() )
                    path = e.Node.Parent.Text;
                else                
                    path = e.Node.Parent.Parent.Text;
                
                string item = e.Node.Text;

                int i = 0;

                foreach (TabPage page in tabFiles.TabPages)
                {
                    Editor editor = (Editor)page.Controls[0].Controls[0];
                    
                    if (editor.Path2File == path)
                    {
                        tabFiles.SelectedIndex = i;
                        FindAndReplace.SearchIndex = 0;
                        FindAndReplace.Find(editor, item, true, true);
                        return;
                    }
                    ++i;
                }

                Opener.Open(path, null);
                FindAndReplace.Find(CurrentEditor, item, true, true);                
            }
        }

        private void txtQuickFindCommand_TextChanged(object sender, EventArgs e)
        {
            if (txtQuickFindCommand.ForeColor == Color.FromKnownColor(KnownColor.AppWorkspace))
                return;

            lstCommands.Items.Clear();

            foreach (Command item in m_list)
            {
                if (item.Name.ToLower().Contains(txtQuickFindCommand.Text.ToLower()))
                    lstCommands.Items.Add(item);
            }
            ctlHeader1.Text = "Commands [" + lstCommands.Items.Count.ToString() + "]";
            ctlHeader1.Invalidate();
        }

        private void mnuRedo_Click(object sender, EventArgs e)
        {
            if (null != CurrentEditor)
                CurrentEditor.MyRedo();
        }

        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);
            
            if (NativeMethods.WM_COPYDATA == msg.Msg)
            {
                string rawmessage = SharedMemory.Recv(ref msg);
                ParseReceivedMessage(rawmessage);
            }
        }

        private void ParseReceivedMessage(string rawmessage)
        {
            Output(rawmessage);

            const string keyOpen = "Open:";
            const string keyJump = "Jump:";

            string[] arr = rawmessage.Split('|');

            if (arr[0].StartsWith(keyOpen))
            {
                bool bOpen = true;
                string path = arr[0].Replace(keyOpen, "");
                path = path.Trim();

                if (File.Exists(path))
                {
                    int i = 0;

                    foreach (TabPage page in tabFiles.TabPages)
                    {                            
                        Editor editor = (Editor)page.Controls[0].Controls[0];

                        Output("Editor:" + editor.Path2File);

                        if (editor.Path2File == path)
                        {
                            tabFiles.SelectedIndex = i;
                            Output("i: " + i.ToString());
                            Output("Selected Index: " + tabFiles.SelectedIndex.ToString());
                            bOpen = false;
                            break;
                        }
                        ++i;
                    }
                    
                    if (bOpen)
                        Opener.Open(path, null);
                }
            }

            Scripter.DoEvents();

            if (arr.Length >= 2)
            {
                int lineNum = Convert.ToInt32(arr[1].Replace(keyJump, ""));
                CurrentEditor.JumpToLine(lineNum);
            }
        }

        private void mnuEnableTooltip_Click(object sender, EventArgs e)
        {
            mnuEnableTooltip.Checked = !mnuEnableTooltip.Checked;

            Settings.Update("EnableTooltip", mnuEnableTooltip.Checked == true ? "1" : "0");
            Settings.Save();

            m_bEnableTooltip = mnuEnableTooltip.Checked;
        }

        private void mnuFindImage_Click(object sender, EventArgs e)
        {
            OnShowFindImage();
        }

        private void OnShowFindImage()
        {
            if (null == m_frmFindImage || m_frmFindImage.IsDisposed)
            {
                m_frmFindImage = new FrmFindImage();                
            }

            Support.CenterWindowOn(this, m_frmFindImage);
        }

        private void mnuSQLConnection_Click(object sender, EventArgs e)
        {
            if (null == m_frmFindImage || m_frmFindImage.IsDisposed)
            {
                m_frmSQLConnection = new FrmSQLConnection();
            }

            Support.CenterWindowOn(this, m_frmSQLConnection);
        }


        private void Killer(string procName)
        {
            try
            {
                Process[] p;
                p = Process.GetProcessesByName(procName);

                foreach (Process proc in p)
                {
                    proc.Kill();                
                }
            }
            catch (Exception ex)
            {
                Scripter.logger.Write("Killer: " + ex.Message);             
            }
        }

        private void mnuTipSetup_Click(object sender, EventArgs e)
        {                
            if ( (null == m_frmTipSetup) || (true == m_frmTipSetup.IsDisposed) )
            {
                m_frmTipSetup = new FrmTipSetup();
            }

            Support.CenterWindowOn(this, m_frmTipSetup);
        }

        private void txtQuickFindCommand_Enter(object sender, EventArgs e)
        {
            if ("Search" == txtQuickFindCommand.Text)
            {
                txtQuickFindCommand.Text = string.Empty;
                txtQuickFindCommand.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
            }
        }

        private void txtQuickFindCommand_Leave(object sender, EventArgs e)
        {
            if (0 == txtQuickFindCommand.Text.Trim().Length)
            {
                txtQuickFindCommand.ForeColor = Color.FromKnownColor(KnownColor.AppWorkspace);
                txtQuickFindCommand.Text = "Search";                
            }            
        }

        private void txtQuickFind_Leave(object sender, EventArgs e)
        {
            if (0 == txtQuickFind.Text.Trim().Length)
            {
                txtQuickFind.ForeColor = Color.FromKnownColor(KnownColor.AppWorkspace);
                txtQuickFind.Text = "Search";
            }
        }

        private void txtQuickFind_Enter(object sender, EventArgs e)
        {
            if ("Search" == txtQuickFind.Text)
            {
                txtQuickFind.Text = string.Empty;
                txtQuickFind.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabDetails.SelectedIndex = 0;
        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabDetails.SelectedIndex = 1;
        }

        private void mnuViewDebug_Click(object sender, EventArgs e)
        {
            tabDetails.SelectedIndex = 2;
        }

        private void mnuHotListEdit_Click(object sender, EventArgs e)
        {
            if ((null == m_frmHotList) || (true == m_frmHotList.IsDisposed))
            {
                m_frmHotList = new FrmHotList();
            }

            Support.CenterWindowOn(this, m_frmHotList);
        }

        public void ServerOutput(string text)
        {
            txtServerOutput.Text += Environment.NewLine + text;

            txtServerOutput.SelectionStart = txtServerOutput.Text.Length;
            txtServerOutput.ScrollToCaret();
        }
        
        private void OnShowServerConfigurator()
        {
            if (null == m_frmServerConfigurator || m_frmServerConfigurator.IsDisposed)
            {
                m_frmServerConfigurator = new FormServerConfigure();
            }

            Support.JustCenterWindowOn(this, m_frmServerConfigurator);

            m_frmServerConfigurator.ShowDialog();
        }

        private void mnuServerConfigureAccess_Click(object sender, EventArgs e)
        {
            OnShowServerConfigurator();
        }

        private void mnuServerUpload_Click(object sender, EventArgs e)
        {
            string temp = Settings.Get("ServerIP");

            string[] arr = temp.Split(':');

            if (2 == arr.Length)
            {
                string ip = arr[0];
                int port = Convert.ToInt32(arr[1]);

                NetworkClient nc = new NetworkClient(ip, port);
                nc.UploadFileBinary(CurrentEditor.Path2File, Settings.Get("DownloadTo"), false, true);
            }
            else
                UtilSys.MessageBox("Unable to get the ip address and port number from [" + temp + "]");
        }

        private void toolServerDownload_Click(object sender, EventArgs e)
        {
            if (null == m_frmServerFolderStructure || m_frmServerFolderStructure.IsDisposed)
            {
                m_frmServerFolderStructure = new FormServerFolderStructure();
            }

            Support.CenterWindowOn(this, m_frmServerFolderStructure);
        }

        private void toolExplorer_Click(object sender, EventArgs e)
        {
            Launcher.Explorer();
        }

        private void toolMaster_Click(object sender, EventArgs e)
        {
            Launcher.Master();
        }

        private void toolPilot_ButtonClick(object sender, EventArgs e)
        {
            OnLaunchPilot();
        }

        private void toolKillPilots_Click(object sender, EventArgs e)
        {
            Killer(Globals.ProcessPilot);
        }

        private void mnuAutorun_Click(object sender, EventArgs e)
        {
            mnuAutorun.Checked = !mnuAutorun.Checked;
            Settings.Update("AutorunPilot", mnuAutorun.Checked ? "1" : "0");
            Settings.Save();
        }

        private void CheckForUpdates()
        {
            string temp = Settings.Get("ServerIP");
            string[] arr = temp.Split(':');

            if (2 != arr.Length)
            {
                UtilSys.MessageBox("Unable to get the ip address and port number from [" + temp + "]");
                return;
            }


            FormUpdate frm = new FormUpdate();
            Support.CenterWindowOn(this, frm);
            Application.DoEvents();

            // bug
            //UtilSys.MessageBoxInfo("Current Directory: " + Directory.GetCurrentDirectory() );

            // the fix!
            Directory.SetCurrentDirectory(Application.StartupPath);


            frm.Text = "Checking for updates on [" + temp + "]";
            Application.DoEvents();

            string ip = arr[0];
            int port = Convert.ToInt32(arr[1]);
            NetworkClient nc = new NetworkClient(ip, port);

            nc.DownloadFile(Globals.exeSelfUpdater, Globals.exeSelfUpdater);


            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add(Globals.exePilot, Globals.ProcessPilot);
            dic.Add(Globals.exeExplorer, Globals.ProcessExplorer);
            dic.Add(Globals.exeMaster, Globals.ProcessMaster);
                        
            foreach (KeyValuePair<string, string> item in dic)
            {
                if (nc.CheckForUpdates(item.Key))
                {
                    Killer(item.Value);
                    nc.DownloadFile(item.Key, item.Key);

                    frm.Controls[0].Text += item.Key + " updated successfully" + Environment.NewLine;                    
                }
                else
                {
                    frm.Controls[0].Text += "No updates for " + item.Key + Environment.NewLine;
                }
                Application.DoEvents();
            }
            
            if (nc.CheckForUpdates(Globals.exeScripter))
            {
                nc.DownloadFile(Globals.exeScripter, "NEW_" + Globals.exeScripter);

                m_bUpdating = true;

                frm.Controls[0].Text += "Scripter will restart to update itself";

                Application.DoEvents();

                Thread.Sleep(2000);

                frm.Close();

                Application.Exit();
            }
            else
                frm.Controls[0].Text += "No updates for " + Globals.exeScripter + Environment.NewLine;

            Application.DoEvents();
            return;                        
        }

        private void mnuServerCheckForUpdates_Click(object sender, EventArgs e)
        {            
            CheckForUpdates();
        }

        private void openToolStripButton_ButtonClick(object sender, EventArgs e)
        {
            Opener.Open(string.Empty, null);
        }

        private void openScript_Click(object sender, EventArgs e)
        {
            Opener.Open(string.Empty, "QABOT Script Files (*.scp)|*.scp|All Files (*.*)|*.*");
        }

        private void openImportModule_Click(object sender, EventArgs e)
        {
            Opener.Open(string.Empty, "QABOT Script Import Modules (*.sci)|*.sci|All Files (*.*)|*.*");            
        }

        private void openData_Click(object sender, EventArgs e)
        {
            Opener.Open(string.Empty, "QABOT Data Files (*.dat)|*.dat|All Files (*.*)|*.*");
        }

        private void openProject_Click(object sender, EventArgs e)
        {
            Opener.Open(string.Empty, "QABOT Project Files (*.qpf)|*.qpf|All Files (*.*)|*.*");
        }

        private void toolDebugStart_Click(object sender, EventArgs e)
        {
            OnStartDebug();
        }

        private void OnStartDebug()
        {
            txtVMOutput.Text = "Virtual Machine Output";
            tabDetails.SelectedIndex = 1;

            if (null == CurrentEditor)
                return;

            CurrentEditor.Save();
            
            try
            {
                m_vm = new VirtualMachine(Scripter.host);

                Script script = new Script(CurrentEditor.Path2File);
                ActionBase.vm = m_vm;                
                ScriptParser.vm = m_vm;
                Data.vm = m_vm;

                if ("No Data File" != cboExternalData.Text)
                {
                    Data.Load(cboExternalData.Text);
                }

                ScriptParser.Load(CurrentEditor.Path2File, script);
                
                m_vm.script = script;
                
                m_vm.Init();
                m_vm.Run();
                m_vm.Done();
            }
            catch (Exception ex)
            {
                txtVMOutput.Text += Environment.NewLine + ex.Message;

                if (m_vm._parseError)
                {
                    Scripter.host.MarkCurrentLine(-1, m_vm._parseErrorLineNumber, Color.NavajoWhite, m_vm._parseErrorScriptPath);
                }
            }
        }

        public void MarkCurrentLine(int lineNumber, string path)
        {
            if (null == CurrentEditor) return;

            if (CurrentEditor.Path2File == path)
                CurrentEditor.JumpToLine(lineNumber);
            else
            {
                int i = 0;
                foreach (TabPage page in tabFiles.TabPages)
                {
                    Editor editor = (Editor)page.Controls[0].Controls[0];
                                      
                    if (editor.Path2File == path)
                    {
                        tabFiles.SelectedIndex = i;
                        CurrentEditor.JumpToLine(lineNumber);
                        return;
                    }
                    ++i;
                }
                Opener.Open(path, null);
                CurrentEditor.JumpToLine(lineNumber);
            }
        }

        public void WriteVMLog(string text)
        {
            txtVMOutput.Text += Environment.NewLine + text;
            txtVMOutput.SelectionStart = txtVMOutput.Text.Length;
            txtVMOutput.ScrollToCaret();
        }

        private void toolDebugStop_Click(object sender, EventArgs e)
        {
            if (null == m_vm) return;
            m_vm.Stop();
        }

        private void mnuServerUploadFile_Click(object sender, EventArgs e)
        {
            string path2File = UtilSys.OpenFileDialog("Open File...", "All Files (*.*)|*.*", Application.StartupPath);

            if ( File.Exists(path2File) )
            {
                string temp = Settings.Get("ServerIP");

                string[] arr = temp.Split(':');

                if (2 == arr.Length)
                {
                    string ip = arr[0];
                    int port = Convert.ToInt32(arr[1]);

                    NetworkClient nc = new NetworkClient(ip, port);
                    nc.UploadFileBinary(path2File, Settings.Get("DownloadTo"), false, true);
                }
                else
                    UtilSys.MessageBox("Unable to get the ip address and port number from [" + temp + "]");
            }
        }

        private void faasfgaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 40; i++)
            {
                string str;
                string imgPath = "CLOSE_INVENIO_" + string.Format("{0:D3}", i) + ".JPG";
                //Scripter.host.WriteLog(string.Format("L\"{0:D3}\",", i));

                str = "<td><a href=\"" + imgPath  + "\"><img src=\"" + imgPath + "\" width=\"320\" height=\"240\"/></a></td>";

                Scripter.host.WriteLog(str);
            }
        }








    }
}