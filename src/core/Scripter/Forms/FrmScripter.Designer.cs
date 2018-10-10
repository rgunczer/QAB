using Controls;
namespace Scripter
{
    partial class FrmScripter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmScripter));
            this.ctxImport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuInsertCall = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxImportOpenInScripter = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxImportOpenInNotepad = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuDocTabs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuDocSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuDocReParse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuAddToHotList = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxHotListslot0 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxHotListslot1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxHotListslot2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxHotListslot3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxHotListslot4 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxHotListslot5 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxHotListslot6 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxHotListslot7 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxHotListslot8 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxHotListslot9 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuAddToTemplates = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTemplateSlot0 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTemplateSlot1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTemplateSlot2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTemplateSlot3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTemplateSlot4 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTemplateSlot5 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTemplateSlot6 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTemplateSlot7 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTemplateSlot8 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTemplateSlot9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuDocClose = new System.Windows.Forms.ToolStripMenuItem();
            this.sbr = new System.Windows.Forms.StatusStrip();
            this.status3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status0 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuTool = new System.Windows.Forms.ToolStrip();
            this.tlbNew = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripSplitButton();
            this.openScript = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripSeparator();
            this.openImportModule = new System.Windows.Forms.ToolStripMenuItem();
            this.openData = new System.Windows.Forms.ToolStripMenuItem();
            this.openProject = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveallToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuUndo = new System.Windows.Forms.ToolStripButton();
            this.mnuRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFindAndReplace = new System.Windows.Forms.ToolStripButton();
            this.reparseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDoSyntaxHighlight = new System.Windows.Forms.ToolStripButton();
            this.mnuIndent = new System.Windows.Forms.ToolStripButton();
            this.mnu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileMru0 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileMru1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileMru2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileMru3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileMru4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileMru5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditFindAndReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHotList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHot0 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHot1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHot2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHot3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHot4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHot5 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHot6 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHot7 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHot8 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHot9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHotListEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplates = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesLoad0 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesLoad1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesLoad2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesLoad3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesLoad4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesLoad5 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesLoad6 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesLoad7 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesLoad8 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesLoad9 = new System.Windows.Forms.ToolStripMenuItem();
            this.setDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesSetDefault0 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesSetDefault1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesSetDefault2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesSetDefault3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesSetDefault4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesSetDefault5 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesSetDefault6 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesSetDefault7 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesSetDefault8 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplatesSetDefault9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTemplatesSetDefaultBuiltIn = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuServerConfigureAccess = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuServerUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuServerDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuServerUploadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuServerCheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDebugStart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.stepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepOverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.toggleBreakpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllBreakpointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableAllBreakpointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindImage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSQLConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTipSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfigHelpOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExParDesc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExDescPar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDescExPar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDescParEx = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuParExDesc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuParDescEx = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMinimizeWhenPilotIsLaunched = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingsCompatibility = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingsClearSearchHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEnableTooltip = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindowCloseAllDocuments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindowClearOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reflectionOfPilotdllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtQuickFindCommand = new System.Windows.Forms.TextBox();
            this.lstCommands = new System.Windows.Forms.ListBox();
            this.lstData = new System.Windows.Forms.ListBox();
            this.cmdSetExternalDataFileLocation = new System.Windows.Forms.Button();
            this.cboExternalData = new System.Windows.Forms.ComboBox();
            this.txtPathToCurrentFile = new System.Windows.Forms.TextBox();
            this.tabDetails = new System.Windows.Forms.TabControl();
            this.tabHelp = new System.Windows.Forms.TabPage();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.tabVMOutput = new System.Windows.Forms.TabPage();
            this.txtVMOutput = new System.Windows.Forms.TextBox();
            this.tabServerOutput = new System.Windows.Forms.TabPage();
            this.txtServerOutput = new System.Windows.Forms.TextBox();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.txtQuickFind = new System.Windows.Forms.TextBox();
            this.toolServer = new System.Windows.Forms.ToolStrip();
            this.toolServerConfigureAccess = new System.Windows.Forms.ToolStripButton();
            this.toolServerUpload = new System.Windows.Forms.ToolStripButton();
            this.toolServerDownload = new System.Windows.Forms.ToolStripButton();
            this.pic = new System.Windows.Forms.PictureBox();
            this.picShadow = new System.Windows.Forms.PictureBox();
            this.toolComponents = new System.Windows.Forms.ToolStrip();
            this.toolExplorer = new System.Windows.Forms.ToolStripButton();
            this.toolMaster = new System.Windows.Forms.ToolStripButton();
            this.toolPilot = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuAutorun = new System.Windows.Forms.ToolStripMenuItem();
            this.toolKillPilots = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tvwImported = new Controls.ctlExplorerTreeView();
            this.ctlHeader2 = new Controls.ctlHeader();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctlHeader1 = new Controls.ctlHeader();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ctlHeader3 = new Controls.ctlHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabFiles = new Controls.ctlTab();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.toolDebug = new System.Windows.Forms.ToolStrip();
            this.toolDebugStart = new System.Windows.Forms.ToolStripButton();
            this.toolDebugPause = new System.Windows.Forms.ToolStripButton();
            this.toolDebugStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolDebugStepInto = new System.Windows.Forms.ToolStripButton();
            this.toolDebugStepOver = new System.Windows.Forms.ToolStripButton();
            this.toolDebugStepOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolDebugBreakpoints = new System.Windows.Forms.ToolStripButton();
            this.faasfgaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxImport.SuspendLayout();
            this.ctxMenuDocTabs.SuspendLayout();
            this.sbr.SuspendLayout();
            this.mnuTool.SuspendLayout();
            this.mnu.SuspendLayout();
            this.tabDetails.SuspendLayout();
            this.tabHelp.SuspendLayout();
            this.tabVMOutput.SuspendLayout();
            this.tabServerOutput.SuspendLayout();
            this.tabDebug.SuspendLayout();
            this.toolServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShadow)).BeginInit();
            this.toolComponents.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.toolDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxImport
            // 
            this.ctxImport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuInsertCall,
            this.toolStripMenuItem7,
            this.ctxImportOpenInScripter,
            this.ctxImportOpenInNotepad});
            this.ctxImport.Name = "ctxMenu";
            this.ctxImport.Size = new System.Drawing.Size(156, 76);
            // 
            // ctxMenuInsertCall
            // 
            this.ctxMenuInsertCall.Name = "ctxMenuInsertCall";
            this.ctxMenuInsertCall.Size = new System.Drawing.Size(155, 22);
            this.ctxMenuInsertCall.Text = "Insert into Script";
            this.ctxMenuInsertCall.Click += new System.EventHandler(this.ctxMenuInsertCall_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(152, 6);
            // 
            // ctxImportOpenInScripter
            // 
            this.ctxImportOpenInScripter.Name = "ctxImportOpenInScripter";
            this.ctxImportOpenInScripter.Size = new System.Drawing.Size(155, 22);
            this.ctxImportOpenInScripter.Text = "Open in Scripter";
            this.ctxImportOpenInScripter.Click += new System.EventHandler(this.ctxImportOpenInScripter_Click);
            // 
            // ctxImportOpenInNotepad
            // 
            this.ctxImportOpenInNotepad.Name = "ctxImportOpenInNotepad";
            this.ctxImportOpenInNotepad.Size = new System.Drawing.Size(155, 22);
            this.ctxImportOpenInNotepad.Text = "Open in Notepad";
            this.ctxImportOpenInNotepad.Click += new System.EventHandler(this.ctxImportOpenInNotepad_Click);
            // 
            // ctxMenuDocTabs
            // 
            this.ctxMenuDocTabs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuDocSave,
            this.toolStripMenuItem8,
            this.ctxMenuDocReParse,
            this.toolStripMenuItem15,
            this.ctxMenuAddToHotList,
            this.ctxMenuAddToTemplates,
            this.toolStripMenuItem1,
            this.ctxMenuDocClose});
            this.ctxMenuDocTabs.Name = "ctxMenuDocTabs";
            this.ctxMenuDocTabs.Size = new System.Drawing.Size(159, 132);
            // 
            // ctxMenuDocSave
            // 
            this.ctxMenuDocSave.Name = "ctxMenuDocSave";
            this.ctxMenuDocSave.Size = new System.Drawing.Size(158, 22);
            this.ctxMenuDocSave.Text = "Save";
            this.ctxMenuDocSave.Click += new System.EventHandler(this.ctxMenuDocSave_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(155, 6);
            // 
            // ctxMenuDocReParse
            // 
            this.ctxMenuDocReParse.Name = "ctxMenuDocReParse";
            this.ctxMenuDocReParse.Size = new System.Drawing.Size(158, 22);
            this.ctxMenuDocReParse.Text = "&ReParse";
            this.ctxMenuDocReParse.Click += new System.EventHandler(this.ctxMenuDocReParse_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(155, 6);
            // 
            // ctxMenuAddToHotList
            // 
            this.ctxMenuAddToHotList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxHotListslot0,
            this.ctxHotListslot1,
            this.ctxHotListslot2,
            this.ctxHotListslot3,
            this.ctxHotListslot4,
            this.ctxHotListslot5,
            this.ctxHotListslot6,
            this.ctxHotListslot7,
            this.ctxHotListslot8,
            this.ctxHotListslot9});
            this.ctxMenuAddToHotList.Name = "ctxMenuAddToHotList";
            this.ctxMenuAddToHotList.Size = new System.Drawing.Size(158, 22);
            this.ctxMenuAddToHotList.Text = "Add to Hot List";
            // 
            // ctxHotListslot0
            // 
            this.ctxHotListslot0.Name = "ctxHotListslot0";
            this.ctxHotListslot0.Size = new System.Drawing.Size(101, 22);
            this.ctxHotListslot0.Text = "Slot 0";
            this.ctxHotListslot0.Click += new System.EventHandler(this.ctxHotListslot0_Click);
            // 
            // ctxHotListslot1
            // 
            this.ctxHotListslot1.Name = "ctxHotListslot1";
            this.ctxHotListslot1.Size = new System.Drawing.Size(101, 22);
            this.ctxHotListslot1.Text = "Slot 1";
            this.ctxHotListslot1.Click += new System.EventHandler(this.ctxHotListslot1_Click);
            // 
            // ctxHotListslot2
            // 
            this.ctxHotListslot2.Name = "ctxHotListslot2";
            this.ctxHotListslot2.Size = new System.Drawing.Size(101, 22);
            this.ctxHotListslot2.Text = "Slot 2";
            this.ctxHotListslot2.Click += new System.EventHandler(this.ctxHotListslot2_Click);
            // 
            // ctxHotListslot3
            // 
            this.ctxHotListslot3.Name = "ctxHotListslot3";
            this.ctxHotListslot3.Size = new System.Drawing.Size(101, 22);
            this.ctxHotListslot3.Text = "Slot 3";
            this.ctxHotListslot3.Click += new System.EventHandler(this.ctxHotListslot3_Click);
            // 
            // ctxHotListslot4
            // 
            this.ctxHotListslot4.Name = "ctxHotListslot4";
            this.ctxHotListslot4.Size = new System.Drawing.Size(101, 22);
            this.ctxHotListslot4.Text = "Slot 4";
            this.ctxHotListslot4.Click += new System.EventHandler(this.ctxHotListslot4_Click);
            // 
            // ctxHotListslot5
            // 
            this.ctxHotListslot5.Name = "ctxHotListslot5";
            this.ctxHotListslot5.Size = new System.Drawing.Size(101, 22);
            this.ctxHotListslot5.Text = "Slot 5";
            this.ctxHotListslot5.Click += new System.EventHandler(this.ctxHotListslot5_Click);
            // 
            // ctxHotListslot6
            // 
            this.ctxHotListslot6.Name = "ctxHotListslot6";
            this.ctxHotListslot6.Size = new System.Drawing.Size(101, 22);
            this.ctxHotListslot6.Text = "Slot 6";
            this.ctxHotListslot6.Click += new System.EventHandler(this.ctxHotListslot6_Click);
            // 
            // ctxHotListslot7
            // 
            this.ctxHotListslot7.Name = "ctxHotListslot7";
            this.ctxHotListslot7.Size = new System.Drawing.Size(101, 22);
            this.ctxHotListslot7.Text = "Slot 7";
            this.ctxHotListslot7.Click += new System.EventHandler(this.ctxHotListslot7_Click);
            // 
            // ctxHotListslot8
            // 
            this.ctxHotListslot8.Name = "ctxHotListslot8";
            this.ctxHotListslot8.Size = new System.Drawing.Size(101, 22);
            this.ctxHotListslot8.Text = "Slot 8";
            this.ctxHotListslot8.Click += new System.EventHandler(this.ctxHotListslot8_Click);
            // 
            // ctxHotListslot9
            // 
            this.ctxHotListslot9.Name = "ctxHotListslot9";
            this.ctxHotListslot9.Size = new System.Drawing.Size(101, 22);
            this.ctxHotListslot9.Text = "Slot 9";
            this.ctxHotListslot9.Click += new System.EventHandler(this.ctxHotListslot9_Click);
            // 
            // ctxMenuAddToTemplates
            // 
            this.ctxMenuAddToTemplates.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxTemplateSlot0,
            this.ctxTemplateSlot1,
            this.ctxTemplateSlot2,
            this.ctxTemplateSlot3,
            this.ctxTemplateSlot4,
            this.ctxTemplateSlot5,
            this.ctxTemplateSlot6,
            this.ctxTemplateSlot7,
            this.ctxTemplateSlot8,
            this.ctxTemplateSlot9});
            this.ctxMenuAddToTemplates.Name = "ctxMenuAddToTemplates";
            this.ctxMenuAddToTemplates.Size = new System.Drawing.Size(158, 22);
            this.ctxMenuAddToTemplates.Text = "Add to Templates";
            // 
            // ctxTemplateSlot0
            // 
            this.ctxTemplateSlot0.Name = "ctxTemplateSlot0";
            this.ctxTemplateSlot0.Size = new System.Drawing.Size(101, 22);
            this.ctxTemplateSlot0.Text = "Slot 0";
            this.ctxTemplateSlot0.Click += new System.EventHandler(this.ctxTemplateSlot0_Click);
            // 
            // ctxTemplateSlot1
            // 
            this.ctxTemplateSlot1.Name = "ctxTemplateSlot1";
            this.ctxTemplateSlot1.Size = new System.Drawing.Size(101, 22);
            this.ctxTemplateSlot1.Text = "Slot 1";
            this.ctxTemplateSlot1.Click += new System.EventHandler(this.ctxTemplateSlot1_Click);
            // 
            // ctxTemplateSlot2
            // 
            this.ctxTemplateSlot2.Name = "ctxTemplateSlot2";
            this.ctxTemplateSlot2.Size = new System.Drawing.Size(101, 22);
            this.ctxTemplateSlot2.Text = "Slot 2";
            this.ctxTemplateSlot2.Click += new System.EventHandler(this.ctxTemplateSlot2_Click);
            // 
            // ctxTemplateSlot3
            // 
            this.ctxTemplateSlot3.Name = "ctxTemplateSlot3";
            this.ctxTemplateSlot3.Size = new System.Drawing.Size(101, 22);
            this.ctxTemplateSlot3.Text = "Slot 3";
            this.ctxTemplateSlot3.Click += new System.EventHandler(this.ctxTemplateSlot3_Click);
            // 
            // ctxTemplateSlot4
            // 
            this.ctxTemplateSlot4.Name = "ctxTemplateSlot4";
            this.ctxTemplateSlot4.Size = new System.Drawing.Size(101, 22);
            this.ctxTemplateSlot4.Text = "Slot 4";
            this.ctxTemplateSlot4.Click += new System.EventHandler(this.ctxTemplateSlot4_Click);
            // 
            // ctxTemplateSlot5
            // 
            this.ctxTemplateSlot5.Name = "ctxTemplateSlot5";
            this.ctxTemplateSlot5.Size = new System.Drawing.Size(101, 22);
            this.ctxTemplateSlot5.Text = "Slot 5";
            this.ctxTemplateSlot5.Click += new System.EventHandler(this.ctxTemplateSlot5_Click);
            // 
            // ctxTemplateSlot6
            // 
            this.ctxTemplateSlot6.Name = "ctxTemplateSlot6";
            this.ctxTemplateSlot6.Size = new System.Drawing.Size(101, 22);
            this.ctxTemplateSlot6.Text = "Slot 6";
            this.ctxTemplateSlot6.Click += new System.EventHandler(this.ctxTemplateSlot6_Click);
            // 
            // ctxTemplateSlot7
            // 
            this.ctxTemplateSlot7.Name = "ctxTemplateSlot7";
            this.ctxTemplateSlot7.Size = new System.Drawing.Size(101, 22);
            this.ctxTemplateSlot7.Text = "Slot 7";
            this.ctxTemplateSlot7.Click += new System.EventHandler(this.ctxTemplateSlot7_Click);
            // 
            // ctxTemplateSlot8
            // 
            this.ctxTemplateSlot8.Name = "ctxTemplateSlot8";
            this.ctxTemplateSlot8.Size = new System.Drawing.Size(101, 22);
            this.ctxTemplateSlot8.Text = "Slot 8";
            this.ctxTemplateSlot8.Click += new System.EventHandler(this.ctxTemplateSlot8_Click);
            // 
            // ctxTemplateSlot9
            // 
            this.ctxTemplateSlot9.Name = "ctxTemplateSlot9";
            this.ctxTemplateSlot9.Size = new System.Drawing.Size(101, 22);
            this.ctxTemplateSlot9.Text = "Slot 9";
            this.ctxTemplateSlot9.Click += new System.EventHandler(this.ctxTemplateSlot9_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 6);
            // 
            // ctxMenuDocClose
            // 
            this.ctxMenuDocClose.Name = "ctxMenuDocClose";
            this.ctxMenuDocClose.Size = new System.Drawing.Size(158, 22);
            this.ctxMenuDocClose.Text = "&Close";
            this.ctxMenuDocClose.Click += new System.EventHandler(this.ctxMenuDocClose_Click);
            // 
            // sbr
            // 
            this.sbr.GripMargin = new System.Windows.Forms.Padding(0);
            this.sbr.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.sbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status3,
            this.status0,
            this.status1,
            this.status4,
            this.status2});
            this.sbr.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.sbr.Location = new System.Drawing.Point(0, 516);
            this.sbr.Name = "sbr";
            this.sbr.Size = new System.Drawing.Size(859, 24);
            this.sbr.TabIndex = 13;
            this.sbr.Text = "statusStrip1";
            // 
            // status3
            // 
            this.status3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.status3.Name = "status3";
            this.status3.Size = new System.Drawing.Size(15, 17);
            this.status3.Text = "-";
            this.status3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status0
            // 
            this.status0.AutoSize = false;
            this.status0.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.status0.Name = "status0";
            this.status0.Size = new System.Drawing.Size(60, 19);
            this.status0.Text = "-";
            this.status0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status1
            // 
            this.status1.AutoSize = false;
            this.status1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.status1.Name = "status1";
            this.status1.Size = new System.Drawing.Size(60, 19);
            this.status1.Text = "-";
            this.status1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status4
            // 
            this.status4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.status4.Name = "status4";
            this.status4.Size = new System.Drawing.Size(15, 17);
            this.status4.Text = "-";
            // 
            // status2
            // 
            this.status2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.status2.Name = "status2";
            this.status2.Size = new System.Drawing.Size(15, 17);
            this.status2.Text = "-";
            this.status2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mnuTool
            // 
            this.mnuTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mnuTool.CanOverflow = false;
            this.mnuTool.Dock = System.Windows.Forms.DockStyle.None;
            this.mnuTool.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.mnuTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbNew,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.saveallToolStripButton,
            this.toolStripSeparator3,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator2,
            this.mnuUndo,
            this.mnuRedo,
            this.toolStripSeparator8,
            this.mnuFindAndReplace,
            this.reparseToolStripButton,
            this.toolStripSeparator1,
            this.mnuDoSyntaxHighlight,
            this.mnuIndent});
            this.mnuTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mnuTool.Location = new System.Drawing.Point(0, 24);
            this.mnuTool.Name = "mnuTool";
            this.mnuTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuTool.Size = new System.Drawing.Size(344, 25);
            this.mnuTool.Stretch = true;
            this.mnuTool.TabIndex = 18;
            this.mnuTool.Text = "toolStrip1";
            // 
            // tlbNew
            // 
            this.tlbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbNew.Image = ((System.Drawing.Image)(resources.GetObject("tlbNew.Image")));
            this.tlbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbNew.Name = "tlbNew";
            this.tlbNew.Size = new System.Drawing.Size(23, 22);
            this.tlbNew.Text = "&New";
            this.tlbNew.ToolTipText = "New [Ctrl+N]";
            this.tlbNew.Click += new System.EventHandler(this.tlbNew_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openScript,
            this.toolStripMenuItem18,
            this.openImportModule,
            this.openData,
            this.openProject});
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(34, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.ToolTipText = "Open [Ctrl+O]";
            this.openToolStripButton.ButtonClick += new System.EventHandler(this.openToolStripButton_ButtonClick);
            // 
            // openScript
            // 
            this.openScript.Name = "openScript";
            this.openScript.Size = new System.Drawing.Size(191, 22);
            this.openScript.Text = "Script (*.scp) ...";
            this.openScript.Click += new System.EventHandler(this.openScript_Click);
            // 
            // toolStripMenuItem18
            // 
            this.toolStripMenuItem18.Name = "toolStripMenuItem18";
            this.toolStripMenuItem18.Size = new System.Drawing.Size(188, 6);
            // 
            // openImportModule
            // 
            this.openImportModule.Name = "openImportModule";
            this.openImportModule.Size = new System.Drawing.Size(191, 22);
            this.openImportModule.Text = "Import module (*.sci) ...";
            this.openImportModule.Click += new System.EventHandler(this.openImportModule_Click);
            // 
            // openData
            // 
            this.openData.Name = "openData";
            this.openData.Size = new System.Drawing.Size(191, 22);
            this.openData.Text = "Data (*.dat) ...";
            this.openData.Click += new System.EventHandler(this.openData_Click);
            // 
            // openProject
            // 
            this.openProject.Name = "openProject";
            this.openProject.Size = new System.Drawing.Size(191, 22);
            this.openProject.Text = "Project (*.qpf) ...";
            this.openProject.Click += new System.EventHandler(this.openProject_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // saveallToolStripButton
            // 
            this.saveallToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveallToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveallToolStripButton.Image")));
            this.saveallToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveallToolStripButton.Name = "saveallToolStripButton";
            this.saveallToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveallToolStripButton.Text = "Save All";
            this.saveallToolStripButton.Click += new System.EventHandler(this.saveallToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "C&ut";
            this.cutToolStripButton.Click += new System.EventHandler(this.cutToolStripButton_Click);
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "&Copy";
            this.copyToolStripButton.Click += new System.EventHandler(this.copyToolStripButton_Click);
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolStripButton.Text = "&Paste";
            this.pasteToolStripButton.Click += new System.EventHandler(this.pasteToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuUndo
            // 
            this.mnuUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuUndo.Image = ((System.Drawing.Image)(resources.GetObject("mnuUndo.Image")));
            this.mnuUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuUndo.Name = "mnuUndo";
            this.mnuUndo.Size = new System.Drawing.Size(23, 22);
            this.mnuUndo.Text = "Undo Last Action";
            this.mnuUndo.Click += new System.EventHandler(this.mnuUndo_Click);
            // 
            // mnuRedo
            // 
            this.mnuRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuRedo.Image = ((System.Drawing.Image)(resources.GetObject("mnuRedo.Image")));
            this.mnuRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuRedo.Name = "mnuRedo";
            this.mnuRedo.Size = new System.Drawing.Size(23, 22);
            this.mnuRedo.Text = "Redo Last Action";
            this.mnuRedo.Click += new System.EventHandler(this.mnuRedo_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuFindAndReplace
            // 
            this.mnuFindAndReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuFindAndReplace.Image = ((System.Drawing.Image)(resources.GetObject("mnuFindAndReplace.Image")));
            this.mnuFindAndReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFindAndReplace.Name = "mnuFindAndReplace";
            this.mnuFindAndReplace.Size = new System.Drawing.Size(23, 22);
            this.mnuFindAndReplace.Text = "Find and Replace [Ctrl+F]";
            this.mnuFindAndReplace.Click += new System.EventHandler(this.mnuFindAndReplace_Click);
            // 
            // reparseToolStripButton
            // 
            this.reparseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reparseToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("reparseToolStripButton.Image")));
            this.reparseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reparseToolStripButton.Name = "reparseToolStripButton";
            this.reparseToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.reparseToolStripButton.Text = "ReParse";
            this.reparseToolStripButton.ToolTipText = "ReParse Script";
            this.reparseToolStripButton.Click += new System.EventHandler(this.reparseToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuDoSyntaxHighlight
            // 
            this.mnuDoSyntaxHighlight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuDoSyntaxHighlight.Image = ((System.Drawing.Image)(resources.GetObject("mnuDoSyntaxHighlight.Image")));
            this.mnuDoSyntaxHighlight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuDoSyntaxHighlight.Name = "mnuDoSyntaxHighlight";
            this.mnuDoSyntaxHighlight.Size = new System.Drawing.Size(23, 22);
            this.mnuDoSyntaxHighlight.Text = "Do Syntax Coloring";
            this.mnuDoSyntaxHighlight.ToolTipText = "Do Syntax Coloring";
            this.mnuDoSyntaxHighlight.Click += new System.EventHandler(this.mnuDoSyntaxHighlight_Click);
            // 
            // mnuIndent
            // 
            this.mnuIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuIndent.Image = ((System.Drawing.Image)(resources.GetObject("mnuIndent.Image")));
            this.mnuIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuIndent.Name = "mnuIndent";
            this.mnuIndent.Size = new System.Drawing.Size(23, 22);
            this.mnuIndent.Text = "Indent";
            this.mnuIndent.Click += new System.EventHandler(this.mnuIndent_Click);
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.mnuHotList,
            this.mnuTemplates,
            this.serverToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.mnuSettings,
            this.windowToolStripMenuItem,
            this.testToolStripMenuItem});
            this.mnu.Location = new System.Drawing.Point(0, 0);
            this.mnu.Name = "mnu";
            this.mnu.Size = new System.Drawing.Size(859, 24);
            this.mnu.TabIndex = 20;
            this.mnu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.toolStripMenuItem9,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnuFileSaveAll,
            this.toolStripMenuItem10,
            this.mnuFileMru0,
            this.mnuFileMru1,
            this.mnuFileMru2,
            this.mnuFileMru3,
            this.mnuFileMru4,
            this.mnuFileMru5,
            this.toolStripMenuItem11,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(125, 22);
            this.mnuFileNew.Text = "&New";
            this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(125, 22);
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(122, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(125, 22);
            this.mnuFileSave.Text = "Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(125, 22);
            this.mnuFileSaveAs.Text = "Save As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
            // 
            // mnuFileSaveAll
            // 
            this.mnuFileSaveAll.Name = "mnuFileSaveAll";
            this.mnuFileSaveAll.Size = new System.Drawing.Size(125, 22);
            this.mnuFileSaveAll.Text = "Save All";
            this.mnuFileSaveAll.Click += new System.EventHandler(this.mnuFileSaveAll_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(122, 6);
            // 
            // mnuFileMru0
            // 
            this.mnuFileMru0.Name = "mnuFileMru0";
            this.mnuFileMru0.Size = new System.Drawing.Size(125, 22);
            this.mnuFileMru0.Text = "mru0";
            this.mnuFileMru0.Click += new System.EventHandler(this.mnuFileMru0_Click);
            // 
            // mnuFileMru1
            // 
            this.mnuFileMru1.Name = "mnuFileMru1";
            this.mnuFileMru1.Size = new System.Drawing.Size(125, 22);
            this.mnuFileMru1.Text = "mru1";
            this.mnuFileMru1.Click += new System.EventHandler(this.mnuFileMru1_Click);
            // 
            // mnuFileMru2
            // 
            this.mnuFileMru2.Name = "mnuFileMru2";
            this.mnuFileMru2.Size = new System.Drawing.Size(125, 22);
            this.mnuFileMru2.Text = "mru2";
            this.mnuFileMru2.Click += new System.EventHandler(this.mnuFileMru2_Click);
            // 
            // mnuFileMru3
            // 
            this.mnuFileMru3.Name = "mnuFileMru3";
            this.mnuFileMru3.Size = new System.Drawing.Size(125, 22);
            this.mnuFileMru3.Text = "mru3";
            this.mnuFileMru3.Click += new System.EventHandler(this.mnuFileMru3_Click);
            // 
            // mnuFileMru4
            // 
            this.mnuFileMru4.Name = "mnuFileMru4";
            this.mnuFileMru4.Size = new System.Drawing.Size(125, 22);
            this.mnuFileMru4.Text = "mru4";
            this.mnuFileMru4.Click += new System.EventHandler(this.mnuFileMru4_Click);
            // 
            // mnuFileMru5
            // 
            this.mnuFileMru5.Name = "mnuFileMru5";
            this.mnuFileMru5.Size = new System.Drawing.Size(125, 22);
            this.mnuFileMru5.Text = "mru5";
            this.mnuFileMru5.Click += new System.EventHandler(this.mnuFileMru5_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(122, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(125, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditUndo,
            this.mnuEditRedo,
            this.toolStripMenuItem2,
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste,
            this.mnuEditDelete,
            this.toolStripMenuItem3,
            this.mnuEditSelectAll,
            this.toolStripMenuItem4,
            this.mnuEditFindAndReplace});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // mnuEditUndo
            // 
            this.mnuEditUndo.Name = "mnuEditUndo";
            this.mnuEditUndo.Size = new System.Drawing.Size(168, 22);
            this.mnuEditUndo.Text = "Undo";
            this.mnuEditUndo.Click += new System.EventHandler(this.mnuEditUndo_Click);
            // 
            // mnuEditRedo
            // 
            this.mnuEditRedo.Name = "mnuEditRedo";
            this.mnuEditRedo.Size = new System.Drawing.Size(168, 22);
            this.mnuEditRedo.Text = "Redo";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(165, 6);
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(168, 22);
            this.mnuEditCut.Text = "Cut";
            this.mnuEditCut.Click += new System.EventHandler(this.mnuEditCut_Click);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(168, 22);
            this.mnuEditCopy.Text = "Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.mnuEditCopy_Click);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.Size = new System.Drawing.Size(168, 22);
            this.mnuEditPaste.Text = "Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.mnuEditPaste_Click);
            // 
            // mnuEditDelete
            // 
            this.mnuEditDelete.Name = "mnuEditDelete";
            this.mnuEditDelete.Size = new System.Drawing.Size(168, 22);
            this.mnuEditDelete.Text = "Delete";
            this.mnuEditDelete.Click += new System.EventHandler(this.mnuEditDelete_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(165, 6);
            // 
            // mnuEditSelectAll
            // 
            this.mnuEditSelectAll.Name = "mnuEditSelectAll";
            this.mnuEditSelectAll.Size = new System.Drawing.Size(168, 22);
            this.mnuEditSelectAll.Text = "Select All";
            this.mnuEditSelectAll.Click += new System.EventHandler(this.mnuEditSelectAll_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(165, 6);
            // 
            // mnuEditFindAndReplace
            // 
            this.mnuEditFindAndReplace.Name = "mnuEditFindAndReplace";
            this.mnuEditFindAndReplace.Size = new System.Drawing.Size(168, 22);
            this.mnuEditFindAndReplace.Text = "Find and Replace...";
            this.mnuEditFindAndReplace.Click += new System.EventHandler(this.mnuEditFindAndReplace_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.outputToolStripMenuItem,
            this.mnuViewDebug});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // outputToolStripMenuItem
            // 
            this.outputToolStripMenuItem.Name = "outputToolStripMenuItem";
            this.outputToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.outputToolStripMenuItem.Text = "Output";
            this.outputToolStripMenuItem.Click += new System.EventHandler(this.outputToolStripMenuItem_Click);
            // 
            // mnuViewDebug
            // 
            this.mnuViewDebug.Name = "mnuViewDebug";
            this.mnuViewDebug.Size = new System.Drawing.Size(108, 22);
            this.mnuViewDebug.Text = "Debug";
            this.mnuViewDebug.Click += new System.EventHandler(this.mnuViewDebug_Click);
            // 
            // mnuHotList
            // 
            this.mnuHotList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHot0,
            this.mnuHot1,
            this.mnuHot2,
            this.mnuHot3,
            this.mnuHot4,
            this.mnuHot5,
            this.mnuHot6,
            this.mnuHot7,
            this.mnuHot8,
            this.mnuHot9,
            this.toolStripMenuItem14,
            this.mnuHotListEdit});
            this.mnuHotList.Name = "mnuHotList";
            this.mnuHotList.Size = new System.Drawing.Size(55, 20);
            this.mnuHotList.Text = "Hot List";
            // 
            // mnuHot0
            // 
            this.mnuHot0.Name = "mnuHot0";
            this.mnuHot0.Size = new System.Drawing.Size(123, 22);
            this.mnuHot0.Text = "hot0";
            this.mnuHot0.Click += new System.EventHandler(this.mnuHot0_Click);
            // 
            // mnuHot1
            // 
            this.mnuHot1.Name = "mnuHot1";
            this.mnuHot1.Size = new System.Drawing.Size(123, 22);
            this.mnuHot1.Text = "hot1";
            this.mnuHot1.Click += new System.EventHandler(this.mnuHot1_Click);
            // 
            // mnuHot2
            // 
            this.mnuHot2.Name = "mnuHot2";
            this.mnuHot2.Size = new System.Drawing.Size(123, 22);
            this.mnuHot2.Text = "hot2";
            this.mnuHot2.Click += new System.EventHandler(this.mnuHot2_Click);
            // 
            // mnuHot3
            // 
            this.mnuHot3.Name = "mnuHot3";
            this.mnuHot3.Size = new System.Drawing.Size(123, 22);
            this.mnuHot3.Text = "hot3";
            this.mnuHot3.Click += new System.EventHandler(this.mnuHot3_Click);
            // 
            // mnuHot4
            // 
            this.mnuHot4.Name = "mnuHot4";
            this.mnuHot4.Size = new System.Drawing.Size(123, 22);
            this.mnuHot4.Text = "hot4";
            this.mnuHot4.Click += new System.EventHandler(this.mnuHot4_Click);
            // 
            // mnuHot5
            // 
            this.mnuHot5.Name = "mnuHot5";
            this.mnuHot5.Size = new System.Drawing.Size(123, 22);
            this.mnuHot5.Text = "hot5";
            this.mnuHot5.Click += new System.EventHandler(this.mnuHot5_Click);
            // 
            // mnuHot6
            // 
            this.mnuHot6.Name = "mnuHot6";
            this.mnuHot6.Size = new System.Drawing.Size(123, 22);
            this.mnuHot6.Text = "hot6";
            this.mnuHot6.Click += new System.EventHandler(this.mnuHot6_Click);
            // 
            // mnuHot7
            // 
            this.mnuHot7.Name = "mnuHot7";
            this.mnuHot7.Size = new System.Drawing.Size(123, 22);
            this.mnuHot7.Text = "hot7";
            this.mnuHot7.Click += new System.EventHandler(this.mnuHot7_Click);
            // 
            // mnuHot8
            // 
            this.mnuHot8.Name = "mnuHot8";
            this.mnuHot8.Size = new System.Drawing.Size(123, 22);
            this.mnuHot8.Text = "hot8";
            this.mnuHot8.Click += new System.EventHandler(this.mnuHot8_Click);
            // 
            // mnuHot9
            // 
            this.mnuHot9.Name = "mnuHot9";
            this.mnuHot9.Size = new System.Drawing.Size(123, 22);
            this.mnuHot9.Text = "hot9";
            this.mnuHot9.Click += new System.EventHandler(this.mnuHot9_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(120, 6);
            // 
            // mnuHotListEdit
            // 
            this.mnuHotListEdit.Name = "mnuHotListEdit";
            this.mnuHotListEdit.Size = new System.Drawing.Size(123, 22);
            this.mnuHotListEdit.Text = "Edit List...";
            this.mnuHotListEdit.Click += new System.EventHandler(this.mnuHotListEdit_Click);
            // 
            // mnuTemplates
            // 
            this.mnuTemplates.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.setDefaultToolStripMenuItem});
            this.mnuTemplates.Name = "mnuTemplates";
            this.mnuTemplates.Size = new System.Drawing.Size(68, 20);
            this.mnuTemplates.Text = "Templates";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTemplatesLoad0,
            this.mnuTemplatesLoad1,
            this.mnuTemplatesLoad2,
            this.mnuTemplatesLoad3,
            this.mnuTemplatesLoad4,
            this.mnuTemplatesLoad5,
            this.mnuTemplatesLoad6,
            this.mnuTemplatesLoad7,
            this.mnuTemplatesLoad8,
            this.mnuTemplatesLoad9});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // mnuTemplatesLoad0
            // 
            this.mnuTemplatesLoad0.Name = "mnuTemplatesLoad0";
            this.mnuTemplatesLoad0.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesLoad0.Text = "Template 0";
            this.mnuTemplatesLoad0.Click += new System.EventHandler(this.mnuTemplatesLoad0_Click);
            // 
            // mnuTemplatesLoad1
            // 
            this.mnuTemplatesLoad1.Name = "mnuTemplatesLoad1";
            this.mnuTemplatesLoad1.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesLoad1.Text = "Template 1";
            this.mnuTemplatesLoad1.Click += new System.EventHandler(this.mnuTemplatesLoad1_Click);
            // 
            // mnuTemplatesLoad2
            // 
            this.mnuTemplatesLoad2.Name = "mnuTemplatesLoad2";
            this.mnuTemplatesLoad2.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesLoad2.Text = "Template 2";
            this.mnuTemplatesLoad2.Click += new System.EventHandler(this.mnuTemplatesLoad2_Click);
            // 
            // mnuTemplatesLoad3
            // 
            this.mnuTemplatesLoad3.Name = "mnuTemplatesLoad3";
            this.mnuTemplatesLoad3.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesLoad3.Text = "Template 3";
            this.mnuTemplatesLoad3.Click += new System.EventHandler(this.mnuTemplatesLoad3_Click);
            // 
            // mnuTemplatesLoad4
            // 
            this.mnuTemplatesLoad4.Name = "mnuTemplatesLoad4";
            this.mnuTemplatesLoad4.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesLoad4.Text = "Template 4";
            this.mnuTemplatesLoad4.Click += new System.EventHandler(this.mnuTemplatesLoad4_Click);
            // 
            // mnuTemplatesLoad5
            // 
            this.mnuTemplatesLoad5.Name = "mnuTemplatesLoad5";
            this.mnuTemplatesLoad5.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesLoad5.Text = "Template 5";
            this.mnuTemplatesLoad5.Click += new System.EventHandler(this.mnuTemplatesLoad5_Click);
            // 
            // mnuTemplatesLoad6
            // 
            this.mnuTemplatesLoad6.Name = "mnuTemplatesLoad6";
            this.mnuTemplatesLoad6.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesLoad6.Text = "Template 6";
            this.mnuTemplatesLoad6.Click += new System.EventHandler(this.mnuTemplatesLoad6_Click);
            // 
            // mnuTemplatesLoad7
            // 
            this.mnuTemplatesLoad7.Name = "mnuTemplatesLoad7";
            this.mnuTemplatesLoad7.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesLoad7.Text = "Template 7";
            this.mnuTemplatesLoad7.Click += new System.EventHandler(this.mnuTemplatesLoad7_Click);
            // 
            // mnuTemplatesLoad8
            // 
            this.mnuTemplatesLoad8.Name = "mnuTemplatesLoad8";
            this.mnuTemplatesLoad8.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesLoad8.Text = "Template 8";
            this.mnuTemplatesLoad8.Click += new System.EventHandler(this.mnuTemplatesLoad8_Click);
            // 
            // mnuTemplatesLoad9
            // 
            this.mnuTemplatesLoad9.Name = "mnuTemplatesLoad9";
            this.mnuTemplatesLoad9.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesLoad9.Text = "Template 9";
            this.mnuTemplatesLoad9.Click += new System.EventHandler(this.mnuTemplatesLoad9_Click);
            // 
            // setDefaultToolStripMenuItem
            // 
            this.setDefaultToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTemplatesSetDefault0,
            this.mnuTemplatesSetDefault1,
            this.mnuTemplatesSetDefault2,
            this.mnuTemplatesSetDefault3,
            this.mnuTemplatesSetDefault4,
            this.mnuTemplatesSetDefault5,
            this.mnuTemplatesSetDefault6,
            this.mnuTemplatesSetDefault7,
            this.mnuTemplatesSetDefault8,
            this.mnuTemplatesSetDefault9,
            this.toolStripMenuItem16,
            this.mnuTemplatesSetDefaultBuiltIn});
            this.setDefaultToolStripMenuItem.Name = "setDefaultToolStripMenuItem";
            this.setDefaultToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.setDefaultToolStripMenuItem.Text = "Set Default";
            // 
            // mnuTemplatesSetDefault0
            // 
            this.mnuTemplatesSetDefault0.Name = "mnuTemplatesSetDefault0";
            this.mnuTemplatesSetDefault0.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesSetDefault0.Text = "Template 0";
            this.mnuTemplatesSetDefault0.Click += new System.EventHandler(this.mnuTemplatesSetDefault0_Click);
            // 
            // mnuTemplatesSetDefault1
            // 
            this.mnuTemplatesSetDefault1.Name = "mnuTemplatesSetDefault1";
            this.mnuTemplatesSetDefault1.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesSetDefault1.Text = "Template 1";
            this.mnuTemplatesSetDefault1.Click += new System.EventHandler(this.mnuTemplatesSetDefault1_Click);
            // 
            // mnuTemplatesSetDefault2
            // 
            this.mnuTemplatesSetDefault2.Name = "mnuTemplatesSetDefault2";
            this.mnuTemplatesSetDefault2.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesSetDefault2.Text = "Template 2";
            this.mnuTemplatesSetDefault2.Click += new System.EventHandler(this.mnuTemplatesSetDefault2_Click);
            // 
            // mnuTemplatesSetDefault3
            // 
            this.mnuTemplatesSetDefault3.Name = "mnuTemplatesSetDefault3";
            this.mnuTemplatesSetDefault3.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesSetDefault3.Text = "Template 3";
            this.mnuTemplatesSetDefault3.Click += new System.EventHandler(this.mnuTemplatesSetDefault3_Click);
            // 
            // mnuTemplatesSetDefault4
            // 
            this.mnuTemplatesSetDefault4.Name = "mnuTemplatesSetDefault4";
            this.mnuTemplatesSetDefault4.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesSetDefault4.Text = "Template 4";
            this.mnuTemplatesSetDefault4.Click += new System.EventHandler(this.mnuTemplatesSetDefault4_Click);
            // 
            // mnuTemplatesSetDefault5
            // 
            this.mnuTemplatesSetDefault5.Name = "mnuTemplatesSetDefault5";
            this.mnuTemplatesSetDefault5.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesSetDefault5.Text = "Template 5";
            this.mnuTemplatesSetDefault5.Click += new System.EventHandler(this.mnuTemplatesSetDefault5_Click);
            // 
            // mnuTemplatesSetDefault6
            // 
            this.mnuTemplatesSetDefault6.Name = "mnuTemplatesSetDefault6";
            this.mnuTemplatesSetDefault6.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesSetDefault6.Text = "Template 6";
            this.mnuTemplatesSetDefault6.Click += new System.EventHandler(this.mnuTemplatesSetDefault6_Click);
            // 
            // mnuTemplatesSetDefault7
            // 
            this.mnuTemplatesSetDefault7.Name = "mnuTemplatesSetDefault7";
            this.mnuTemplatesSetDefault7.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesSetDefault7.Text = "Template 7";
            this.mnuTemplatesSetDefault7.Click += new System.EventHandler(this.mnuTemplatesSetDefault7_Click);
            // 
            // mnuTemplatesSetDefault8
            // 
            this.mnuTemplatesSetDefault8.Name = "mnuTemplatesSetDefault8";
            this.mnuTemplatesSetDefault8.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesSetDefault8.Text = "Template 8";
            this.mnuTemplatesSetDefault8.Click += new System.EventHandler(this.mnuTemplatesSetDefault8_Click);
            // 
            // mnuTemplatesSetDefault9
            // 
            this.mnuTemplatesSetDefault9.Name = "mnuTemplatesSetDefault9";
            this.mnuTemplatesSetDefault9.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesSetDefault9.Text = "Template 9";
            this.mnuTemplatesSetDefault9.Click += new System.EventHandler(this.mnuTemplatesSetDefault9_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(124, 6);
            // 
            // mnuTemplatesSetDefaultBuiltIn
            // 
            this.mnuTemplatesSetDefaultBuiltIn.Name = "mnuTemplatesSetDefaultBuiltIn";
            this.mnuTemplatesSetDefaultBuiltIn.Size = new System.Drawing.Size(127, 22);
            this.mnuTemplatesSetDefaultBuiltIn.Text = "BuiltIn";
            this.mnuTemplatesSetDefaultBuiltIn.Click += new System.EventHandler(this.mnuTemplatesSetDefaultBuiltIn_Click);
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuServerConfigureAccess,
            this.mnuServerUpload,
            this.mnuServerDownload,
            this.mnuServerUploadFile,
            this.toolStripMenuItem17,
            this.mnuServerCheckForUpdates});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // mnuServerConfigureAccess
            // 
            this.mnuServerConfigureAccess.Name = "mnuServerConfigureAccess";
            this.mnuServerConfigureAccess.Size = new System.Drawing.Size(169, 22);
            this.mnuServerConfigureAccess.Text = "Configure Access...";
            this.mnuServerConfigureAccess.Click += new System.EventHandler(this.mnuServerConfigureAccess_Click);
            // 
            // mnuServerUpload
            // 
            this.mnuServerUpload.Name = "mnuServerUpload";
            this.mnuServerUpload.Size = new System.Drawing.Size(169, 22);
            this.mnuServerUpload.Text = "Upload Script";
            this.mnuServerUpload.Click += new System.EventHandler(this.mnuServerUpload_Click);
            // 
            // mnuServerDownload
            // 
            this.mnuServerDownload.Name = "mnuServerDownload";
            this.mnuServerDownload.Size = new System.Drawing.Size(169, 22);
            this.mnuServerDownload.Text = "Download File(s)...";
            this.mnuServerDownload.Click += new System.EventHandler(this.toolServerDownload_Click);
            // 
            // mnuServerUploadFile
            // 
            this.mnuServerUploadFile.Name = "mnuServerUploadFile";
            this.mnuServerUploadFile.Size = new System.Drawing.Size(169, 22);
            this.mnuServerUploadFile.Text = "Upload File...";
            this.mnuServerUploadFile.Click += new System.EventHandler(this.mnuServerUploadFile_Click);
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(166, 6);
            // 
            // mnuServerCheckForUpdates
            // 
            this.mnuServerCheckForUpdates.Name = "mnuServerCheckForUpdates";
            this.mnuServerCheckForUpdates.Size = new System.Drawing.Size(169, 22);
            this.mnuServerCheckForUpdates.Text = "Check for Updates";
            this.mnuServerCheckForUpdates.Click += new System.EventHandler(this.mnuServerCheckForUpdates_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDebugStart,
            this.toolStripMenuItem12,
            this.stepToolStripMenuItem,
            this.stepOverToolStripMenuItem,
            this.toolStripMenuItem13,
            this.toggleBreakpointToolStripMenuItem,
            this.deleteAllBreakpointsToolStripMenuItem,
            this.disableAllBreakpointsToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.debugToolStripMenuItem.Text = "&Debug";
            // 
            // mnuDebugStart
            // 
            this.mnuDebugStart.Name = "mnuDebugStart";
            this.mnuDebugStart.Size = new System.Drawing.Size(181, 22);
            this.mnuDebugStart.Text = "Start";
            this.mnuDebugStart.Click += new System.EventHandler(this.toolDebugStart_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(178, 6);
            // 
            // stepToolStripMenuItem
            // 
            this.stepToolStripMenuItem.Enabled = false;
            this.stepToolStripMenuItem.Name = "stepToolStripMenuItem";
            this.stepToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.stepToolStripMenuItem.Text = "Step Into";
            // 
            // stepOverToolStripMenuItem
            // 
            this.stepOverToolStripMenuItem.Enabled = false;
            this.stepOverToolStripMenuItem.Name = "stepOverToolStripMenuItem";
            this.stepOverToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.stepOverToolStripMenuItem.Text = "Step Over";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(178, 6);
            // 
            // toggleBreakpointToolStripMenuItem
            // 
            this.toggleBreakpointToolStripMenuItem.Enabled = false;
            this.toggleBreakpointToolStripMenuItem.Name = "toggleBreakpointToolStripMenuItem";
            this.toggleBreakpointToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.toggleBreakpointToolStripMenuItem.Text = "Toggle Breakpoint";
            // 
            // deleteAllBreakpointsToolStripMenuItem
            // 
            this.deleteAllBreakpointsToolStripMenuItem.Enabled = false;
            this.deleteAllBreakpointsToolStripMenuItem.Name = "deleteAllBreakpointsToolStripMenuItem";
            this.deleteAllBreakpointsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.deleteAllBreakpointsToolStripMenuItem.Text = "Delete All Breakpoints";
            // 
            // disableAllBreakpointsToolStripMenuItem
            // 
            this.disableAllBreakpointsToolStripMenuItem.Enabled = false;
            this.disableAllBreakpointsToolStripMenuItem.Name = "disableAllBreakpointsToolStripMenuItem";
            this.disableAllBreakpointsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.disableAllBreakpointsToolStripMenuItem.Text = "Disable All Breakpoints";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFindImage,
            this.mnuSQLConnection,
            this.mnuTipSetup});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // mnuFindImage
            // 
            this.mnuFindImage.Name = "mnuFindImage";
            this.mnuFindImage.Size = new System.Drawing.Size(162, 22);
            this.mnuFindImage.Text = "Find Image...";
            this.mnuFindImage.Click += new System.EventHandler(this.mnuFindImage_Click);
            // 
            // mnuSQLConnection
            // 
            this.mnuSQLConnection.Name = "mnuSQLConnection";
            this.mnuSQLConnection.Size = new System.Drawing.Size(162, 22);
            this.mnuSQLConnection.Text = "SQL Connection...";
            this.mnuSQLConnection.Click += new System.EventHandler(this.mnuSQLConnection_Click);
            // 
            // mnuTipSetup
            // 
            this.mnuTipSetup.Name = "mnuTipSetup";
            this.mnuTipSetup.Size = new System.Drawing.Size(162, 22);
            this.mnuTipSetup.Text = "Tip Setup...";
            this.mnuTipSetup.Click += new System.EventHandler(this.mnuTipSetup_Click);
            // 
            // mnuSettings
            // 
            this.mnuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConfigHelpOrder,
            this.mnuMinimizeWhenPilotIsLaunched,
            this.mnuSettingsCompatibility,
            this.mnuSettingsClearSearchHistory,
            this.mnuEnableTooltip});
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(58, 20);
            this.mnuSettings.Text = "Settings";
            // 
            // mnuConfigHelpOrder
            // 
            this.mnuConfigHelpOrder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExParDesc,
            this.mnuExDescPar,
            this.toolStripMenuItem5,
            this.mnuDescExPar,
            this.mnuDescParEx,
            this.toolStripMenuItem6,
            this.mnuParExDesc,
            this.mnuParDescEx});
            this.mnuConfigHelpOrder.Name = "mnuConfigHelpOrder";
            this.mnuConfigHelpOrder.Size = new System.Drawing.Size(341, 22);
            this.mnuConfigHelpOrder.Text = "Help Order";
            // 
            // mnuExParDesc
            // 
            this.mnuExParDesc.Name = "mnuExParDesc";
            this.mnuExParDesc.Size = new System.Drawing.Size(236, 22);
            this.mnuExParDesc.Tag = "210";
            this.mnuExParDesc.Text = "Example, Parameters, Description";
            this.mnuExParDesc.Click += new System.EventHandler(this.mnuExParDesc_Click);
            // 
            // mnuExDescPar
            // 
            this.mnuExDescPar.Name = "mnuExDescPar";
            this.mnuExDescPar.Size = new System.Drawing.Size(236, 22);
            this.mnuExDescPar.Tag = "201";
            this.mnuExDescPar.Text = "Example, Description, Parameters";
            this.mnuExDescPar.Click += new System.EventHandler(this.mnuExDescPar_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(233, 6);
            // 
            // mnuDescExPar
            // 
            this.mnuDescExPar.Name = "mnuDescExPar";
            this.mnuDescExPar.Size = new System.Drawing.Size(236, 22);
            this.mnuDescExPar.Tag = "021";
            this.mnuDescExPar.Text = "Description, Example, Parameters";
            this.mnuDescExPar.Click += new System.EventHandler(this.mnuDescExPar_Click);
            // 
            // mnuDescParEx
            // 
            this.mnuDescParEx.Name = "mnuDescParEx";
            this.mnuDescParEx.Size = new System.Drawing.Size(236, 22);
            this.mnuDescParEx.Tag = "012";
            this.mnuDescParEx.Text = "Description, Parameters, Example";
            this.mnuDescParEx.Click += new System.EventHandler(this.mnuDescParEx_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(233, 6);
            // 
            // mnuParExDesc
            // 
            this.mnuParExDesc.Name = "mnuParExDesc";
            this.mnuParExDesc.Size = new System.Drawing.Size(236, 22);
            this.mnuParExDesc.Tag = "120";
            this.mnuParExDesc.Text = "Parameters, Example, Description";
            this.mnuParExDesc.Click += new System.EventHandler(this.mnuParExDesc_Click);
            // 
            // mnuParDescEx
            // 
            this.mnuParDescEx.Name = "mnuParDescEx";
            this.mnuParDescEx.Size = new System.Drawing.Size(236, 22);
            this.mnuParDescEx.Tag = "102";
            this.mnuParDescEx.Text = "Parameters, Description, Example";
            this.mnuParDescEx.Click += new System.EventHandler(this.mnuParDescEx_Click);
            // 
            // mnuMinimizeWhenPilotIsLaunched
            // 
            this.mnuMinimizeWhenPilotIsLaunched.Name = "mnuMinimizeWhenPilotIsLaunched";
            this.mnuMinimizeWhenPilotIsLaunched.Size = new System.Drawing.Size(341, 22);
            this.mnuMinimizeWhenPilotIsLaunched.Text = "Minimize Scripter when Pilot is Launched";
            this.mnuMinimizeWhenPilotIsLaunched.Click += new System.EventHandler(this.mnuMinimizeWhenPilotIsLaunched_Click);
            // 
            // mnuSettingsCompatibility
            // 
            this.mnuSettingsCompatibility.Name = "mnuSettingsCompatibility";
            this.mnuSettingsCompatibility.Size = new System.Drawing.Size(341, 22);
            this.mnuSettingsCompatibility.Text = "Compatibility (Replace old commands like Log, Pilot etc.)";
            this.mnuSettingsCompatibility.Click += new System.EventHandler(this.mnuSettingsCompatibility_Click);
            // 
            // mnuSettingsClearSearchHistory
            // 
            this.mnuSettingsClearSearchHistory.Name = "mnuSettingsClearSearchHistory";
            this.mnuSettingsClearSearchHistory.Size = new System.Drawing.Size(341, 22);
            this.mnuSettingsClearSearchHistory.Text = "Clear Find And Replace History";
            this.mnuSettingsClearSearchHistory.Click += new System.EventHandler(this.mnuSettingsClearSearchHistory_Click);
            // 
            // mnuEnableTooltip
            // 
            this.mnuEnableTooltip.Name = "mnuEnableTooltip";
            this.mnuEnableTooltip.Size = new System.Drawing.Size(341, 22);
            this.mnuEnableTooltip.Text = "Enable Tooltip";
            this.mnuEnableTooltip.Click += new System.EventHandler(this.mnuEnableTooltip_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuWindowCloseAllDocuments,
            this.mnuWindowClearOutput});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // mnuWindowCloseAllDocuments
            // 
            this.mnuWindowCloseAllDocuments.Name = "mnuWindowCloseAllDocuments";
            this.mnuWindowCloseAllDocuments.Size = new System.Drawing.Size(170, 22);
            this.mnuWindowCloseAllDocuments.Text = "Close All Documents";
            this.mnuWindowCloseAllDocuments.Click += new System.EventHandler(this.mnuWindowCloseAllDocuments_Click);
            // 
            // mnuWindowClearOutput
            // 
            this.mnuWindowClearOutput.Name = "mnuWindowClearOutput";
            this.mnuWindowClearOutput.Size = new System.Drawing.Size(170, 22);
            this.mnuWindowClearOutput.Text = "Clear Output";
            this.mnuWindowClearOutput.Click += new System.EventHandler(this.mnuWindowClearOutput_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reflectionOfPilotdllToolStripMenuItem,
            this.faasfgaToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // reflectionOfPilotdllToolStripMenuItem
            // 
            this.reflectionOfPilotdllToolStripMenuItem.Name = "reflectionOfPilotdllToolStripMenuItem";
            this.reflectionOfPilotdllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.reflectionOfPilotdllToolStripMenuItem.Text = "Reflection (Pilot.dll)";
            this.reflectionOfPilotdllToolStripMenuItem.Click += new System.EventHandler(this.reflectionOfPilotdllToolStripMenuItem_Click);
            // 
            // txtQuickFindCommand
            // 
            this.txtQuickFindCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuickFindCommand.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtQuickFindCommand.Location = new System.Drawing.Point(0, 24);
            this.txtQuickFindCommand.Name = "txtQuickFindCommand";
            this.txtQuickFindCommand.Size = new System.Drawing.Size(144, 20);
            this.txtQuickFindCommand.TabIndex = 52;
            this.txtQuickFindCommand.Text = "Search";
            this.txtQuickFindCommand.TextChanged += new System.EventHandler(this.txtQuickFindCommand_TextChanged);
            this.txtQuickFindCommand.Leave += new System.EventHandler(this.txtQuickFindCommand_Leave);
            this.txtQuickFindCommand.Enter += new System.EventHandler(this.txtQuickFindCommand_Enter);
            // 
            // lstCommands
            // 
            this.lstCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCommands.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lstCommands.FormattingEnabled = true;
            this.lstCommands.IntegralHeight = false;
            this.lstCommands.Location = new System.Drawing.Point(0, 47);
            this.lstCommands.Margin = new System.Windows.Forms.Padding(0);
            this.lstCommands.Name = "lstCommands";
            this.lstCommands.ScrollAlwaysVisible = true;
            this.lstCommands.Size = new System.Drawing.Size(144, 282);
            this.lstCommands.Sorted = true;
            this.lstCommands.TabIndex = 37;
            this.lstCommands.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstCommands_MouseDoubleClick);
            this.lstCommands.SelectedIndexChanged += new System.EventHandler(this.lstCommands_SelectedIndexChanged);
            // 
            // lstData
            // 
            this.lstData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lstData.FormattingEnabled = true;
            this.lstData.IntegralHeight = false;
            this.lstData.Location = new System.Drawing.Point(1, 50);
            this.lstData.Margin = new System.Windows.Forms.Padding(0);
            this.lstData.Name = "lstData";
            this.lstData.ScrollAlwaysVisible = true;
            this.lstData.Size = new System.Drawing.Size(143, 78);
            this.lstData.TabIndex = 48;
            this.lstData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstData_MouseDoubleClick);
            // 
            // cmdSetExternalDataFileLocation
            // 
            this.cmdSetExternalDataFileLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSetExternalDataFileLocation.BackgroundImage = global::Scripter.Properties.Resources.Database;
            this.cmdSetExternalDataFileLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdSetExternalDataFileLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSetExternalDataFileLocation.Location = new System.Drawing.Point(119, 22);
            this.cmdSetExternalDataFileLocation.Name = "cmdSetExternalDataFileLocation";
            this.cmdSetExternalDataFileLocation.Size = new System.Drawing.Size(25, 23);
            this.cmdSetExternalDataFileLocation.TabIndex = 47;
            this.cmdSetExternalDataFileLocation.UseVisualStyleBackColor = true;
            this.cmdSetExternalDataFileLocation.Click += new System.EventHandler(this.cmdSetExternalDataFileLocation_Click);
            // 
            // cboExternalData
            // 
            this.cboExternalData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboExternalData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExternalData.FormattingEnabled = true;
            this.cboExternalData.Location = new System.Drawing.Point(0, 24);
            this.cboExternalData.Name = "cboExternalData";
            this.cboExternalData.Size = new System.Drawing.Size(113, 21);
            this.cboExternalData.TabIndex = 46;
            this.cboExternalData.SelectedIndexChanged += new System.EventHandler(this.mnuComboDataFile_SelectedIndexChanged);
            // 
            // txtPathToCurrentFile
            // 
            this.txtPathToCurrentFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPathToCurrentFile.BackColor = System.Drawing.Color.LightSkyBlue;
            this.txtPathToCurrentFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPathToCurrentFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPathToCurrentFile.Location = new System.Drawing.Point(0, 0);
            this.txtPathToCurrentFile.Name = "txtPathToCurrentFile";
            this.txtPathToCurrentFile.ReadOnly = true;
            this.txtPathToCurrentFile.Size = new System.Drawing.Size(529, 21);
            this.txtPathToCurrentFile.TabIndex = 48;
            this.txtPathToCurrentFile.WordWrap = false;
            // 
            // tabDetails
            // 
            this.tabDetails.Controls.Add(this.tabHelp);
            this.tabDetails.Controls.Add(this.tabVMOutput);
            this.tabDetails.Controls.Add(this.tabServerOutput);
            this.tabDetails.Controls.Add(this.tabDebug);
            this.tabDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDetails.Location = new System.Drawing.Point(0, 0);
            this.tabDetails.Margin = new System.Windows.Forms.Padding(0);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.Padding = new System.Drawing.Point(0, 0);
            this.tabDetails.SelectedIndex = 0;
            this.tabDetails.Size = new System.Drawing.Size(529, 128);
            this.tabDetails.TabIndex = 50;
            // 
            // tabHelp
            // 
            this.tabHelp.Controls.Add(this.rtbInfo);
            this.tabHelp.Location = new System.Drawing.Point(4, 22);
            this.tabHelp.Margin = new System.Windows.Forms.Padding(0);
            this.tabHelp.Name = "tabHelp";
            this.tabHelp.Size = new System.Drawing.Size(521, 102);
            this.tabHelp.TabIndex = 0;
            this.tabHelp.Text = "Help";
            this.tabHelp.UseVisualStyleBackColor = true;
            // 
            // rtbInfo
            // 
            this.rtbInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbInfo.HideSelection = false;
            this.rtbInfo.Location = new System.Drawing.Point(0, 0);
            this.rtbInfo.Margin = new System.Windows.Forms.Padding(0);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.Size = new System.Drawing.Size(521, 102);
            this.rtbInfo.TabIndex = 3;
            this.rtbInfo.Text = "";
            // 
            // tabVMOutput
            // 
            this.tabVMOutput.Controls.Add(this.txtVMOutput);
            this.tabVMOutput.Location = new System.Drawing.Point(4, 22);
            this.tabVMOutput.Name = "tabVMOutput";
            this.tabVMOutput.Size = new System.Drawing.Size(521, 102);
            this.tabVMOutput.TabIndex = 1;
            this.tabVMOutput.Text = "VM Output";
            this.tabVMOutput.UseVisualStyleBackColor = true;
            // 
            // txtVMOutput
            // 
            this.txtVMOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtVMOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVMOutput.Location = new System.Drawing.Point(0, 0);
            this.txtVMOutput.Margin = new System.Windows.Forms.Padding(0);
            this.txtVMOutput.Multiline = true;
            this.txtVMOutput.Name = "txtVMOutput";
            this.txtVMOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtVMOutput.Size = new System.Drawing.Size(521, 102);
            this.txtVMOutput.TabIndex = 0;
            this.txtVMOutput.WordWrap = false;
            // 
            // tabServerOutput
            // 
            this.tabServerOutput.Controls.Add(this.txtServerOutput);
            this.tabServerOutput.Location = new System.Drawing.Point(4, 22);
            this.tabServerOutput.Name = "tabServerOutput";
            this.tabServerOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabServerOutput.Size = new System.Drawing.Size(521, 102);
            this.tabServerOutput.TabIndex = 3;
            this.tabServerOutput.Text = "Server Output";
            this.tabServerOutput.UseVisualStyleBackColor = true;
            // 
            // txtServerOutput
            // 
            this.txtServerOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtServerOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServerOutput.Location = new System.Drawing.Point(3, 3);
            this.txtServerOutput.Margin = new System.Windows.Forms.Padding(0);
            this.txtServerOutput.Multiline = true;
            this.txtServerOutput.Name = "txtServerOutput";
            this.txtServerOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServerOutput.Size = new System.Drawing.Size(515, 96);
            this.txtServerOutput.TabIndex = 2;
            this.txtServerOutput.WordWrap = false;
            // 
            // tabDebug
            // 
            this.tabDebug.Controls.Add(this.txtDebug);
            this.tabDebug.Location = new System.Drawing.Point(4, 22);
            this.tabDebug.Margin = new System.Windows.Forms.Padding(0);
            this.tabDebug.Name = "tabDebug";
            this.tabDebug.Size = new System.Drawing.Size(521, 102);
            this.tabDebug.TabIndex = 2;
            this.tabDebug.Text = "Debug";
            this.tabDebug.UseVisualStyleBackColor = true;
            // 
            // txtDebug
            // 
            this.txtDebug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDebug.Location = new System.Drawing.Point(0, 0);
            this.txtDebug.Margin = new System.Windows.Forms.Padding(0);
            this.txtDebug.Multiline = true;
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDebug.Size = new System.Drawing.Size(521, 102);
            this.txtDebug.TabIndex = 1;
            this.txtDebug.WordWrap = false;
            // 
            // txtQuickFind
            // 
            this.txtQuickFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuickFind.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtQuickFind.Location = new System.Drawing.Point(0, 24);
            this.txtQuickFind.Name = "txtQuickFind";
            this.txtQuickFind.Size = new System.Drawing.Size(178, 20);
            this.txtQuickFind.TabIndex = 51;
            this.txtQuickFind.Text = "Search";
            this.txtQuickFind.TextChanged += new System.EventHandler(this.txtQuickFind_TextChanged);
            this.txtQuickFind.Leave += new System.EventHandler(this.txtQuickFind_Leave);
            this.txtQuickFind.Enter += new System.EventHandler(this.txtQuickFind_Enter);
            // 
            // toolServer
            // 
            this.toolServer.Dock = System.Windows.Forms.DockStyle.None;
            this.toolServer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolServerConfigureAccess,
            this.toolServerUpload,
            this.toolServerDownload});
            this.toolServer.Location = new System.Drawing.Point(617, 24);
            this.toolServer.Name = "toolServer";
            this.toolServer.Size = new System.Drawing.Size(87, 25);
            this.toolServer.TabIndex = 57;
            this.toolServer.Text = "toolStrip1";
            // 
            // toolServerConfigureAccess
            // 
            this.toolServerConfigureAccess.Image = ((System.Drawing.Image)(resources.GetObject("toolServerConfigureAccess.Image")));
            this.toolServerConfigureAccess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolServerConfigureAccess.Name = "toolServerConfigureAccess";
            this.toolServerConfigureAccess.Size = new System.Drawing.Size(31, 22);
            this.toolServerConfigureAccess.Text = "-";
            this.toolServerConfigureAccess.Click += new System.EventHandler(this.mnuServerConfigureAccess_Click);
            // 
            // toolServerUpload
            // 
            this.toolServerUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolServerUpload.Image = ((System.Drawing.Image)(resources.GetObject("toolServerUpload.Image")));
            this.toolServerUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolServerUpload.Name = "toolServerUpload";
            this.toolServerUpload.Size = new System.Drawing.Size(23, 22);
            this.toolServerUpload.Text = "Upload Script";
            this.toolServerUpload.Click += new System.EventHandler(this.mnuServerUpload_Click);
            // 
            // toolServerDownload
            // 
            this.toolServerDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolServerDownload.Image = ((System.Drawing.Image)(resources.GetObject("toolServerDownload.Image")));
            this.toolServerDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolServerDownload.Name = "toolServerDownload";
            this.toolServerDownload.Size = new System.Drawing.Size(23, 22);
            this.toolServerDownload.Text = "Download File(s)";
            this.toolServerDownload.Click += new System.EventHandler(this.toolServerDownload_Click);
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(798, 1);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(40, 33);
            this.pic.TabIndex = 52;
            this.pic.TabStop = false;
            this.pic.Visible = false;
            // 
            // picShadow
            // 
            this.picShadow.Location = new System.Drawing.Point(709, 1);
            this.picShadow.Name = "picShadow";
            this.picShadow.Size = new System.Drawing.Size(83, 32);
            this.picShadow.TabIndex = 53;
            this.picShadow.TabStop = false;
            this.picShadow.Visible = false;
            // 
            // toolComponents
            // 
            this.toolComponents.Dock = System.Windows.Forms.DockStyle.None;
            this.toolComponents.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolComponents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolExplorer,
            this.toolMaster,
            this.toolPilot});
            this.toolComponents.Location = new System.Drawing.Point(344, 24);
            this.toolComponents.Name = "toolComponents";
            this.toolComponents.Size = new System.Drawing.Size(90, 25);
            this.toolComponents.TabIndex = 58;
            this.toolComponents.Text = "toolStrip2";
            // 
            // toolExplorer
            // 
            this.toolExplorer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolExplorer.Image = ((System.Drawing.Image)(resources.GetObject("toolExplorer.Image")));
            this.toolExplorer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExplorer.Name = "toolExplorer";
            this.toolExplorer.Size = new System.Drawing.Size(23, 22);
            this.toolExplorer.Text = "Launch QABOT-Explorer";
            this.toolExplorer.ToolTipText = "Launch QABOT-Explorer [Ctrl-E]";
            this.toolExplorer.Click += new System.EventHandler(this.toolExplorer_Click);
            // 
            // toolMaster
            // 
            this.toolMaster.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolMaster.Image = ((System.Drawing.Image)(resources.GetObject("toolMaster.Image")));
            this.toolMaster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMaster.Name = "toolMaster";
            this.toolMaster.Size = new System.Drawing.Size(23, 22);
            this.toolMaster.Text = "Launch QABOT-Master";
            this.toolMaster.Click += new System.EventHandler(this.toolMaster_Click);
            // 
            // toolPilot
            // 
            this.toolPilot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolPilot.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAutorun,
            this.toolKillPilots});
            this.toolPilot.Image = ((System.Drawing.Image)(resources.GetObject("toolPilot.Image")));
            this.toolPilot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPilot.Name = "toolPilot";
            this.toolPilot.Size = new System.Drawing.Size(34, 22);
            this.toolPilot.Text = "Launch QABOT-Pilot";
            this.toolPilot.ToolTipText = "Launch QABOT-Pilot [Ctrl-P]";
            this.toolPilot.ButtonClick += new System.EventHandler(this.toolPilot_ButtonClick);
            // 
            // mnuAutorun
            // 
            this.mnuAutorun.Name = "mnuAutorun";
            this.mnuAutorun.Size = new System.Drawing.Size(113, 22);
            this.mnuAutorun.Text = "Autorun";
            this.mnuAutorun.Click += new System.EventHandler(this.mnuAutorun_Click);
            // 
            // toolKillPilots
            // 
            this.toolKillPilots.Name = "toolKillPilots";
            this.toolKillPilots.Size = new System.Drawing.Size(113, 22);
            this.toolKillPilots.Text = "Kill Pilot";
            this.toolKillPilots.Click += new System.EventHandler(this.toolKillPilots_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tvwImported);
            this.panel1.Controls.Add(this.txtQuickFind);
            this.panel1.Controls.Add(this.ctlHeader2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(178, 461);
            this.panel1.TabIndex = 60;
            // 
            // tvwImported
            // 
            this.tvwImported.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvwImported.ContextMenuStrip = this.ctxImport;
            this.tvwImported.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvwImported.Indent = 12;
            this.tvwImported.Location = new System.Drawing.Point(0, 47);
            this.tvwImported.Margin = new System.Windows.Forms.Padding(0);
            this.tvwImported.Name = "tvwImported";
            this.tvwImported.Size = new System.Drawing.Size(178, 414);
            this.tvwImported.TabIndex = 49;
            this.tvwImported.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwImported_NodeMouseDoubleClick);
            this.tvwImported.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwImported_MouseDown);
            // 
            // ctlHeader2
            // 
            this.ctlHeader2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ctlHeader2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlHeader2.Location = new System.Drawing.Point(0, 0);
            this.ctlHeader2.Name = "ctlHeader2";
            this.ctlHeader2.Size = new System.Drawing.Size(178, 21);
            this.ctlHeader2.TabIndex = 49;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ctlHeader1);
            this.panel2.Controls.Add(this.txtQuickFindCommand);
            this.panel2.Controls.Add(this.lstCommands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(144, 329);
            this.panel2.TabIndex = 61;
            // 
            // ctlHeader1
            // 
            this.ctlHeader1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ctlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctlHeader1.Name = "ctlHeader1";
            this.ctlHeader1.Size = new System.Drawing.Size(144, 21);
            this.ctlHeader1.TabIndex = 53;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ctlHeader3);
            this.panel3.Controls.Add(this.cboExternalData);
            this.panel3.Controls.Add(this.cmdSetExternalDataFileLocation);
            this.panel3.Controls.Add(this.lstData);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(144, 128);
            this.panel3.TabIndex = 62;
            // 
            // ctlHeader3
            // 
            this.ctlHeader3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ctlHeader3.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlHeader3.Location = new System.Drawing.Point(0, 0);
            this.ctlHeader3.Name = "ctlHeader3";
            this.ctlHeader3.Size = new System.Drawing.Size(144, 20);
            this.ctlHeader3.TabIndex = 52;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(144, 461);
            this.splitContainer1.SplitterDistance = 329;
            this.splitContainer1.TabIndex = 63;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabFiles);
            this.splitContainer2.Panel1.Controls.Add(this.txtPathToCurrentFile);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabDetails);
            this.splitContainer2.Size = new System.Drawing.Size(529, 461);
            this.splitContainer2.SplitterDistance = 329;
            this.splitContainer2.TabIndex = 64;
            // 
            // tabFiles
            // 
            this.tabFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFiles.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabFiles.ItemSize = new System.Drawing.Size(60, 23);
            this.tabFiles.Location = new System.Drawing.Point(-2, 22);
            this.tabFiles.Name = "tabFiles";
            this.tabFiles.Padding = new System.Drawing.Point(13, 0);
            this.tabFiles.SelectedIndex = 0;
            this.tabFiles.Size = new System.Drawing.Size(532, 308);
            this.tabFiles.TabIndex = 50;
            this.tabFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabFiles_MouseDown);
            this.tabFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabFiles_MouseUp);
            this.tabFiles.SelectedIndexChanged += new System.EventHandler(this.tabFiles_SelectedIndexChanged);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(0, 52);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(859, 461);
            this.splitContainer3.SplitterDistance = 144;
            this.splitContainer3.TabIndex = 65;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.panel1);
            this.splitContainer4.Size = new System.Drawing.Size(711, 461);
            this.splitContainer4.SplitterDistance = 529;
            this.splitContainer4.TabIndex = 0;
            // 
            // toolDebug
            // 
            this.toolDebug.Dock = System.Windows.Forms.DockStyle.None;
            this.toolDebug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolDebugStart,
            this.toolDebugPause,
            this.toolDebugStop,
            this.toolStripSeparator4,
            this.toolDebugStepInto,
            this.toolDebugStepOver,
            this.toolDebugStepOut,
            this.toolStripSeparator7,
            this.toolDebugBreakpoints});
            this.toolDebug.Location = new System.Drawing.Point(434, 24);
            this.toolDebug.Name = "toolDebug";
            this.toolDebug.Size = new System.Drawing.Size(183, 25);
            this.toolDebug.TabIndex = 66;
            this.toolDebug.Text = "toolStrip1";
            // 
            // toolDebugStart
            // 
            this.toolDebugStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDebugStart.Image = ((System.Drawing.Image)(resources.GetObject("toolDebugStart.Image")));
            this.toolDebugStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDebugStart.Name = "toolDebugStart";
            this.toolDebugStart.Size = new System.Drawing.Size(23, 22);
            this.toolDebugStart.Text = "Start Debugging";
            this.toolDebugStart.Click += new System.EventHandler(this.toolDebugStart_Click);
            // 
            // toolDebugPause
            // 
            this.toolDebugPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDebugPause.Enabled = false;
            this.toolDebugPause.Image = ((System.Drawing.Image)(resources.GetObject("toolDebugPause.Image")));
            this.toolDebugPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDebugPause.Name = "toolDebugPause";
            this.toolDebugPause.Size = new System.Drawing.Size(23, 22);
            this.toolDebugPause.Text = "Break All";
            // 
            // toolDebugStop
            // 
            this.toolDebugStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDebugStop.Image = ((System.Drawing.Image)(resources.GetObject("toolDebugStop.Image")));
            this.toolDebugStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDebugStop.Name = "toolDebugStop";
            this.toolDebugStop.Size = new System.Drawing.Size(23, 22);
            this.toolDebugStop.Text = "Stop Debugging";
            this.toolDebugStop.Click += new System.EventHandler(this.toolDebugStop_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolDebugStepInto
            // 
            this.toolDebugStepInto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDebugStepInto.Enabled = false;
            this.toolDebugStepInto.Image = ((System.Drawing.Image)(resources.GetObject("toolDebugStepInto.Image")));
            this.toolDebugStepInto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDebugStepInto.Name = "toolDebugStepInto";
            this.toolDebugStepInto.Size = new System.Drawing.Size(23, 22);
            this.toolDebugStepInto.Text = "Step Into";
            // 
            // toolDebugStepOver
            // 
            this.toolDebugStepOver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDebugStepOver.Enabled = false;
            this.toolDebugStepOver.Image = ((System.Drawing.Image)(resources.GetObject("toolDebugStepOver.Image")));
            this.toolDebugStepOver.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDebugStepOver.Name = "toolDebugStepOver";
            this.toolDebugStepOver.Size = new System.Drawing.Size(23, 22);
            this.toolDebugStepOver.Text = "Step Over";
            // 
            // toolDebugStepOut
            // 
            this.toolDebugStepOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDebugStepOut.Enabled = false;
            this.toolDebugStepOut.Image = ((System.Drawing.Image)(resources.GetObject("toolDebugStepOut.Image")));
            this.toolDebugStepOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDebugStepOut.Name = "toolDebugStepOut";
            this.toolDebugStepOut.Size = new System.Drawing.Size(23, 22);
            this.toolDebugStepOut.Text = "Step Out";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolDebugBreakpoints
            // 
            this.toolDebugBreakpoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDebugBreakpoints.Enabled = false;
            this.toolDebugBreakpoints.Image = ((System.Drawing.Image)(resources.GetObject("toolDebugBreakpoints.Image")));
            this.toolDebugBreakpoints.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDebugBreakpoints.Name = "toolDebugBreakpoints";
            this.toolDebugBreakpoints.Size = new System.Drawing.Size(23, 22);
            this.toolDebugBreakpoints.Text = "Breakpoints";
            // 
            // faasfgaToolStripMenuItem
            // 
            this.faasfgaToolStripMenuItem.Name = "faasfgaToolStripMenuItem";
            this.faasfgaToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.faasfgaToolStripMenuItem.Text = "faasfga";
            this.faasfgaToolStripMenuItem.Click += new System.EventHandler(this.faasfgaToolStripMenuItem_Click);
            // 
            // FrmScripter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(859, 540);
            this.Controls.Add(this.splitContainer3);
            this.Controls.Add(this.mnuTool);
            this.Controls.Add(this.toolComponents);
            this.Controls.Add(this.toolDebug);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.picShadow);
            this.Controls.Add(this.sbr);
            this.Controls.Add(this.mnu);
            this.Controls.Add(this.toolServer);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnu;
            this.Name = "FrmScripter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QABOT - Scripter";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmScripter_KeyDown);
            this.ctxImport.ResumeLayout(false);
            this.ctxMenuDocTabs.ResumeLayout(false);
            this.sbr.ResumeLayout(false);
            this.sbr.PerformLayout();
            this.mnuTool.ResumeLayout(false);
            this.mnuTool.PerformLayout();
            this.mnu.ResumeLayout(false);
            this.mnu.PerformLayout();
            this.tabDetails.ResumeLayout(false);
            this.tabHelp.ResumeLayout(false);
            this.tabVMOutput.ResumeLayout(false);
            this.tabVMOutput.PerformLayout();
            this.tabServerOutput.ResumeLayout(false);
            this.tabServerOutput.PerformLayout();
            this.tabDebug.ResumeLayout(false);
            this.tabDebug.PerformLayout();
            this.toolServer.ResumeLayout(false);
            this.toolServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShadow)).EndInit();
            this.toolComponents.ResumeLayout(false);
            this.toolComponents.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.toolDebug.ResumeLayout(false);
            this.toolDebug.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip sbr;

        private System.Windows.Forms.ToolStrip mnuTool;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlbNew;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ContextMenuStrip ctxImport;
        private System.Windows.Forms.ToolStripMenuItem ctxImportOpenInScripter;
        private System.Windows.Forms.ToolStripButton mnuUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuInsertCall;
        public System.Windows.Forms.ContextMenuStrip ctxMenuDocTabs;
        public System.Windows.Forms.ToolStripMenuItem ctxMenuDocClose;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuDocReParse;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuDocSave;
        private System.Windows.Forms.ToolStripMenuItem ctxImportOpenInNotepad;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuAddToHotList;
        private System.Windows.Forms.ToolStripMenuItem ctxHotListslot0;
        private System.Windows.Forms.ToolStripMenuItem ctxHotListslot1;
        private System.Windows.Forms.ToolStripMenuItem ctxHotListslot2;
        private System.Windows.Forms.ToolStripMenuItem ctxHotListslot3;
        private System.Windows.Forms.ToolStripMenuItem ctxHotListslot4;
        private System.Windows.Forms.ToolStripMenuItem ctxHotListslot5;
        private System.Windows.Forms.ToolStripMenuItem ctxHotListslot6;
        private System.Windows.Forms.ToolStripMenuItem ctxHotListslot7;
        private System.Windows.Forms.ToolStripMenuItem ctxHotListslot8;
        private System.Windows.Forms.ToolStripMenuItem ctxHotListslot9;
        private System.Windows.Forms.MenuStrip mnu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuHotList;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuEditUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem mnuFileMru0;
        private System.Windows.Forms.ToolStripMenuItem mnuFileMru1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileMru2;
        private System.Windows.Forms.ToolStripMenuItem mnuFileMru3;
        private System.Windows.Forms.ToolStripMenuItem mnuFileMru4;
        private System.Windows.Forms.ToolStripMenuItem mnuFileMru5;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuEditRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCut;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuEditPaste;
        private System.Windows.Forms.ToolStripMenuItem mnuEditDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuEditSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuEditFindAndReplace;
        private System.Windows.Forms.ToolStripMenuItem mnuHot0;
        private System.Windows.Forms.ToolStripMenuItem mnuHot1;
        private System.Windows.Forms.ToolStripMenuItem mnuHot2;
        private System.Windows.Forms.ToolStripMenuItem mnuHot3;
        private System.Windows.Forms.ToolStripMenuItem mnuHot4;
        private System.Windows.Forms.ToolStripMenuItem mnuHot5;
        private System.Windows.Forms.ToolStripMenuItem mnuHot6;
        private System.Windows.Forms.ToolStripMenuItem mnuHot7;
        private System.Windows.Forms.ToolStripMenuItem mnuHot8;
        private System.Windows.Forms.ToolStripMenuItem mnuHot9;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuConfigHelpOrder;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDebugStart;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem stepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepOverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuWindowCloseAllDocuments;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toggleBreakpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllBreakpointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableAllBreakpointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuMinimizeWhenPilotIsLaunched;
        private System.Windows.Forms.ToolStripMenuItem mnuExParDesc;
        private System.Windows.Forms.ToolStripMenuItem mnuExDescPar;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuDescExPar;
        private System.Windows.Forms.ToolStripMenuItem mnuDescParEx;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem mnuParExDesc;
        private System.Windows.Forms.ToolStripMenuItem mnuParDescEx;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        public System.Windows.Forms.ToolStripMenuItem mnuTemplates;
        public System.Windows.Forms.ToolStripMenuItem ctxMenuAddToTemplates;
        public System.Windows.Forms.ToolStripMenuItem ctxTemplateSlot0;
        public System.Windows.Forms.ToolStripMenuItem ctxTemplateSlot1;
        public System.Windows.Forms.ToolStripMenuItem ctxTemplateSlot2;
        public System.Windows.Forms.ToolStripMenuItem ctxTemplateSlot3;
        public System.Windows.Forms.ToolStripMenuItem ctxTemplateSlot4;
        public System.Windows.Forms.ToolStripMenuItem ctxTemplateSlot5;
        public System.Windows.Forms.ToolStripMenuItem ctxTemplateSlot6;
        public System.Windows.Forms.ToolStripMenuItem ctxTemplateSlot7;
        public System.Windows.Forms.ToolStripMenuItem ctxTemplateSlot8;
        public System.Windows.Forms.ToolStripMenuItem ctxTemplateSlot9;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem15;
        private System.Windows.Forms.ListBox lstCommands;
        private System.Windows.Forms.ListBox lstData;
        public System.Windows.Forms.ComboBox cboExternalData;
        private System.Windows.Forms.Button cmdSetExternalDataFileLocation;
        private System.Windows.Forms.TabControl tabDetails;
        private System.Windows.Forms.TabPage tabHelp;
        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.TabPage tabVMOutput;
        private System.Windows.Forms.TextBox txtVMOutput;
        private System.Windows.Forms.TabPage tabDebug;
        
        public System.Windows.Forms.ToolStripStatusLabel status0;
        public System.Windows.Forms.ToolStripStatusLabel status1;
        public System.Windows.Forms.ToolStripStatusLabel status2;
        private System.Windows.Forms.ToolStripButton saveallToolStripButton;
        private System.Windows.Forms.ToolStripButton reparseToolStripButton;
        private System.Windows.Forms.TextBox txtPathToCurrentFile;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reflectionOfPilotdllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsCompatibility;
        private System.Windows.Forms.ToolStripStatusLabel status3;
        private System.Windows.Forms.ToolStripMenuItem mnuWindowClearOutput;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDefaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesLoad0;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesLoad1;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesLoad2;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesLoad3;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesLoad4;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesLoad5;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesLoad6;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesLoad7;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesLoad8;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesLoad9;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesSetDefault0;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesSetDefault1;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesSetDefault2;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesSetDefault3;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesSetDefault4;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesSetDefault5;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesSetDefault6;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesSetDefault7;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesSetDefault8;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesSetDefault9;
        public System.Windows.Forms.ToolStripStatusLabel status4;
        private System.Windows.Forms.ToolStripButton mnuDoSyntaxHighlight;
        private System.Windows.Forms.ToolStripButton mnuIndent;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem16;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplatesSetDefaultBuiltIn;
        public ctlTab tabFiles;
        private System.Windows.Forms.ToolStripButton mnuFindAndReplace;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsClearSearchHistory;
        private System.Windows.Forms.TextBox txtQuickFind;
        private System.Windows.Forms.TextBox txtQuickFindCommand;
        private System.Windows.Forms.ToolStripButton mnuRedo;
        private ctlHeader ctlHeader1;
        private ctlHeader ctlHeader2;
        private ctlHeader ctlHeader3;
        public ctlExplorerTreeView tvwImported;
        private System.Windows.Forms.ToolStripMenuItem mnuEnableTooltip;
        private System.Windows.Forms.TextBox txtDebug;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFindImage;
        private System.Windows.Forms.ToolStripMenuItem mnuSQLConnection;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.PictureBox picShadow;
        private System.Windows.Forms.ToolStripMenuItem mnuTipSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuViewDebug;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem mnuHotListEdit;
        private System.Windows.Forms.ToolStrip toolServer;
        private System.Windows.Forms.ToolStripButton toolServerConfigureAccess;
        private System.Windows.Forms.ToolStripButton toolServerUpload;
        private System.Windows.Forms.ToolStripButton toolServerDownload;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuServerConfigureAccess;
        private System.Windows.Forms.ToolStripMenuItem mnuServerUpload;
        private System.Windows.Forms.ToolStripMenuItem mnuServerDownload;
        private System.Windows.Forms.TabPage tabServerOutput;
        private System.Windows.Forms.TextBox txtServerOutput;
        private System.Windows.Forms.ToolStrip toolComponents;
        private System.Windows.Forms.ToolStripButton toolExplorer;
        private System.Windows.Forms.ToolStripButton toolMaster;
        private System.Windows.Forms.ToolStripSplitButton toolPilot;
        private System.Windows.Forms.ToolStripMenuItem mnuAutorun;
        private System.Windows.Forms.ToolStripMenuItem toolKillPilots;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem17;
        private System.Windows.Forms.ToolStripMenuItem mnuServerCheckForUpdates;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ToolStripSplitButton openToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem openScript;
        private System.Windows.Forms.ToolStripMenuItem openData;
        private System.Windows.Forms.ToolStripMenuItem openProject;
        private System.Windows.Forms.ToolStrip toolDebug;
        private System.Windows.Forms.ToolStripButton toolDebugStart;
        private System.Windows.Forms.ToolStripButton toolDebugPause;
        private System.Windows.Forms.ToolStripButton toolDebugStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolDebugStepInto;
        private System.Windows.Forms.ToolStripButton toolDebugStepOver;
        private System.Windows.Forms.ToolStripButton toolDebugStepOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolDebugBreakpoints;
        private System.Windows.Forms.ToolStripMenuItem mnuServerUploadFile;
        private System.Windows.Forms.ToolStripMenuItem openImportModule;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem18;
        private System.Windows.Forms.ToolStripMenuItem faasfgaToolStripMenuItem;        
    }
}

