using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using Util;
using Parser;
using Scripter;


namespace Controls
{
    public class Editor : RichTextBox
    {
        // enums
        private enum EnumIntellisenseMode
        {
            Undefined = -1,
            Functions = 0,
            Variables = 1,
            StringConstant = 2,
            SysSubCommand = 3,
            Command = 4,
            LogSubCommand = 5,
            ImportFolder = 6,
            KeyboardSubCommand = 7,
            EnvironmentSubCommand = 8,
            SubCommand = 9,
            RegisterSubCommand = 10,
        };

        // vars
        public static FrmScripter frm = null;        

        private const short WM_PAINT = 0x00f;

        private EnumIntellisenseMode m_ISmode = EnumIntellisenseMode.Undefined;

        private List<string> m_ListVariables = new List<string>();
        private List<string> m_ListFunctions = new List<string>();
        
        private Intellisense m_is = null;

        private Stack<string> m_Undo = new Stack<string>();
        private Stack<string> m_Redo = new Stack<string>();

        private bool m_bLoading = false;

        public bool _bHighlightCurrentLine = true;

        public TabPage page = null;        

        private Timer m_MouseRestTimer = new Timer();

        private bool m_bMouseInside = false;

        public static bool _Paint = true;
        private bool m_bDirty = false;
        public string m_path = string.Empty;

        private bool m_bHandleBlockComments = false;


        private Timer m_timer = new Timer();

        private SyntaxData m_CurSyntaxData = null;
        
        // properties
        public List<string> Variables
        {
            get { return m_ListVariables; }
        }

        public List<string> Functions
        {
            get { return m_ListFunctions; }
        }

        public string Path2File
        {
            get { return m_path; }
        }
        
        public bool Loading
        {
            get { return m_bLoading; }
            set { m_bLoading = value; }
        }
        
        public bool Dirty
        {
            get { return m_bDirty; }
            set 
            { 
                m_bDirty = value;

                if (null != page)
                {
                    if (m_bDirty)
                        page.Text = Path.GetFileName(m_path) + " *";
                    else
                        page.Text = Path.GetFileName(m_path);

                    page.ToolTipText = m_path;
                }
            }
        }

        private string CurrentLine
        {
            get
            {
                if (0 == Lines.Length)
                    return string.Empty;

                return Lines[GetLineFromCharIndex(SelectionStart)];
            }    
        }

        private int CurrentLineNumber
        {
            get
            {
                return GetLineFromCharIndex(SelectionStart);
            }
        }

        private int CurrentLineCaretPos        
        {
            get
            {
                return SelectionStart - GetFirstCharIndexOfCurrentLine();
            }
        }

        private List<string> TokensInCurrentLine
        {
            get
            {
                try
                {
                    Scripter.Scripter.logger.Write("Current Line Number: " + CurrentLineNumber.ToString());

                    if (CurrentLineNumber < Lines.Length)
                    {
                        string line = Lines[CurrentLineNumber];
                        List<string> tokens = Tokenizer.Init(line);
                        return tokens;
                    }
                    return new List<string>();
                }
                catch (Exception ex)
                {
                    UtilSys.MessageBox("TokensInCurrentLine: " + ex.Message);
                    throw;
                }                                                                
            }
        }

        public void JumpToLine(int lineNum)
        {
            try
            {
                SelectionStart = GetFirstCharIndexFromLine(lineNum);

                int len = Lines[lineNum].Length;

                SelectionLength = len;
            }
            catch
            {
                                
            }
        }

        public bool CanLaunch
        {
            get
            {
                const string cmd = "sys.start";
                const string cmdOld = "Start";

                foreach (string line in Lines)
                {
                    string tmp = line.Trim();

                    if (tmp.Length >= cmd.Length)
                    {
                        if (tmp.StartsWith(@"\\"))
                            continue;

                        if (tmp.StartsWith(cmd) || tmp.StartsWith(cmdOld))
                            return true;
                    }
                }
                return false;
            }
        }
        
        private string LastWordInCurrentLine
        {
            get
            {                
                int posCurrent = SelectionStart-1;
                int posStart = GetFirstCharIndexOfCurrentLine();
                int length = posCurrent - posStart;

                if (posCurrent < 1)
                    return string.Empty;

                char ch;

                List<char> chars = new List<char>();

                for (int i = posCurrent; i >= posStart; --i)
			    {
                    ch = Text[i]; 
                
                    if (ch == ' ' || ch == '\t' || ch == 10)
			            break;
                    
                    chars.Add(ch);
                }

                chars.Reverse();

                string word = new string(chars.ToArray());
                                
                return word;
            }
        }

        private string CommandInCurrentLine
        {
            get 
            {
                int posFirst = GetFirstCharIndexFromLine(CurrentLineNumber);
                string line = Text.Substring(posFirst, SelectionStart - posFirst).Trim();
                string cmd = string.Empty;                

                for (int i=0; i<line.Length; ++i)
                {
                    if (' ' == line[i] || '.' == line[i])
                        break;
                
                    cmd += line[i];
                }                
                cmd = cmd.Trim();
                cmd = cmd.ToLower();
                return cmd;
            }
        }

        public Point IntelliSensePosition
        {
            get
            {
                Point ptClient;
                                
                Point point = GetPositionFromCharIndex(SelectionStart);
                Point ptScreen = PointToScreen(point);
                ptClient = Scripter.Scripter.ScripterForm.PointToClient(ptScreen);
                ptClient.Y += Font.Height;
                                
                return ptClient;
            }
        }              

        public void Insert(string text, Color color)
        {            
            SelectionColor = color;
            SelectionLength = 0;
            
            if (color == Color.OrangeRed)
                SelectedText = text;
            else
                SelectedText = text + " ";

            SelectionStart += text.Length + 1;
            Focus();
        }
        
        public void AddLine(string line)
        {
            AppendText(line);
            AppendText(Environment.NewLine);            
        }

        public Editor()
        {            
            m_is = new Intellisense(this);

            // event handlers
            KeyDown += new System.Windows.Forms.KeyEventHandler(EventKeyDown);
            TextChanged += new System.EventHandler(EventTextChanged);
            KeyUp += new KeyEventHandler(EventKeyUp);

            MouseDown += new MouseEventHandler(Editor_MouseDown);
            MouseUp += new MouseEventHandler(Editor_MouseUp);
            MouseMove += new MouseEventHandler(Editor_MouseMove);
            
            GotFocus += new EventHandler(Editor_GotFocus);            
            Resize += new EventHandler(Editor_Resize);
            MouseEnter += new EventHandler(Editor_MouseEnter);
            MouseLeave += new EventHandler(Editor_MouseLeave);

            m_timer.Tick += new EventHandler(m_timer_Tick);

            // style
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true);

            // properties
            AcceptsTab = true;
            BorderStyle = BorderStyle.None;
            WordWrap = false;
            DetectUrls = false;
            Font = new Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            HideSelection = false;


            m_timer.Enabled = false;

            MenuItem[] ctxMenuItems = new MenuItem[] 
            {
                new MenuItem("Go To Definition", new EventHandler(ctxMenuGotoDef)), 
                new MenuItem("-"),
                new MenuItem("Cut", new EventHandler(ctxMenuCut)), 
                new MenuItem("Copy", new EventHandler(ctxMenuCopy)), 
                new MenuItem("Paste", new EventHandler(ctxMenuPaste)), 
                new MenuItem("-"), 
                new MenuItem("Delete",  new EventHandler(ctxMenuDelete)), 
                new MenuItem("-"), 
                new MenuItem("Select All",  new EventHandler(ctxMenuSelectAll)), 
            };

            ContextMenu ctxMenu = new ContextMenu(ctxMenuItems);

            ContextMenu = ctxMenu;
        }

        void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (0 == SelectionLength)
                    SelectionStart = GetCharIndexFromPosition(e.Location);
            }
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            Point pos = PointToClient(Control.MousePosition);

            string word = GetWordUnderMouse(pos);
            
            if (word.Length > 0)
                frm.HoverHelp(word);
            else            
                frm.HideInfoTip();            
            
            m_timer.Enabled = false;            
        }

        void Editor_MouseMove(object sender, MouseEventArgs e)
        {
            if (!frm.EnableTooptip)
                return;
            
            m_timer.Enabled = false;
            m_timer.Interval = 300;
            m_timer.Enabled = true;            
        }

        void Editor_MouseLeave(object sender, EventArgs e)
        {
            m_bMouseInside = false;
            frm.HideInfoTip();
            m_timer.Enabled = false; 
        }

        void Editor_MouseEnter(object sender, EventArgs e)
        {
            m_bMouseInside = true;
        }
        
        private static string GetWord(string input, int position)
        {
            try
            {
                int from = 0;
                int sp2 = input.Length;

                for (int i = position; i > 0; --i)
                {
                    char ch = input[i];

                    if (ch == ' ' || ch == '\n' || ch == '\t')
                    {
                        from = i;
                        break;
                    }

                    if (ch == '(' || ch == ',' || ch == ')')
                    {
                        from = i + 1;
                        break;
                    }
                }

                for (int i = position; i < input.Length; ++i)
                {
                    char ch = input[i];

                    if (ch == ' ' || ch == '\n' || ch == '\t' || ch == ')' || ch == ',' || ch == '(')
                    {
                        sp2 = i;
                        break;
                    }
                }

                if (sp2 < from)
                    return string.Empty;

                return input.Substring(from, sp2 - from).Replace("\n", "").Replace("\t", "").Trim();
            }
            catch (Exception ex)
            {
                Scripter.Scripter.Output("GetWord: " + ex.Message);
                return string.Empty;
            }    
        }
        
        public string GetWordUnderMouse(Point location)
        {
            if (!m_bMouseInside)
                return string.Empty;

            return GetWord(Text, GetCharIndexFromPosition(location));
        }
        
        void Editor_Resize(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;
        }

        private unsafe NativeMethods.POINT GetScrollPos()
        {
            NativeMethods.POINT res = new NativeMethods.POINT();
            IntPtr ptr = new IntPtr(&res);
            NativeMethods.SendMessage(Handle, NativeMethods.EM_GETSCROLLPOS, 0, ptr);
            return res;
        }

        private unsafe void SetScrollPos(NativeMethods.POINT point)
        {
            IntPtr ptr = new IntPtr(&point);
            NativeMethods.SendMessage(Handle, NativeMethods.EM_SETSCROLLPOS, 0, ptr);
        }

        void Editor_GotFocus(object sender, EventArgs e)
        {
            ShowStatusAndDebugInfo();
        }

        void ctxMenuGotoDef(object obj, EventArgs ea)
        {
            string path = string.Empty;
            string word = GetWord(Text, SelectionStart);
            string toFind = frm.LookUpProjectExplorer(word, ref path);

            if (string.IsNullOrEmpty(toFind))
                return;
            
            Scripter.Scripter.Output("Path: " + path + ", toFind: " + toFind);
            Scripter.Scripter.ScripterForm.GoToDefinition(path, toFind);            
        }

        void ctxMenuCut(object obj, EventArgs ea)
        {
            Cut();
        }

        void ctxMenuCopy(object obj, EventArgs ea)
        {
            Copy();
        }

        void ctxMenuPaste(object obj, EventArgs ea)
        {
            MyPaste();            
        }
        
        public void MyPaste()
        {
            try
            {
                if (Clipboard.ContainsText(TextDataFormat.Rtf) || Clipboard.ContainsText(TextDataFormat.Text))
                {
                    string text = Clipboard.GetData(DataFormats.Text).ToString();
                    string[] lines = text.Split('\n');
                    int lineNumber = 0;

                    foreach (string line in lines)
	                {
                        lineNumber = CurrentLineNumber;

                        SelectedText = line;
                        SyntaxColorLines(lineNumber, lineNumber+1);
                    }
                    SelectionLength = 0;
                }
                else
                    Paste();
            }
            catch (Exception ex)
            {
                Scripter.Scripter.logger.Write("MyPaste: " + ex.Message);
                Paste(DataFormats.GetFormat("Text"));
            }                            
        }

        void ctxMenuDelete(object obj, EventArgs ea)
        {
            SelectedText = string.Empty;
        }

        void ctxMenuSelectAll(object obj, EventArgs ea)
        {
            SelectAll();
        }

        void Editor_MouseUp(object sender, MouseEventArgs e)
        {
            ShowStatusAndDebugInfo();
            Parent.Invalidate();
            m_is.Hide();            
        }

        public bool CanClose()
        {
            if (Dirty)
            {
                DialogResult res = MessageBox.Show("Save changes to '" + Path.GetFileName(m_path) + "' file?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                    Save();

                if (res == DialogResult.Cancel)
                    return false;
            }

            return true;
        }

        public void MyUndo()
        {
            if (m_Undo.Count > 0)
            {
                m_bLoading = true;
                Rtf = m_Undo.Pop();
                m_Redo.Push(Rtf);
                m_bLoading = false;
            }
        }

        public void MyRedo()
        {
            if (m_Redo.Count > 0)
            {
                m_bLoading = true;
                Rtf = m_Redo.Pop();
                m_Undo.Push(Rtf);
                m_bLoading = false;
            }
        }


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PAINT)
            {
                if (_Paint)
                {
                    base.WndProc(ref m);
                    
                    Parent.Invalidate();
                }
                else
                    m.Result = IntPtr.Zero;
            }
            else
                base.WndProc(ref m);
        }

        public void InitUndoStack()
        {
            m_Undo.Clear();
            m_Undo.Push(Rtf);
            m_Redo.Clear();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //base.OnPaintBackground(pevent);
        }

        public void IndentAll()               
        {            
            m_bLoading = true;

            string temp = string.Empty;
            string mid = string.Empty;

            bool pass = false;
            
            int indent = 0;

            for(int i=0; i<Lines.Length; ++i)
            {
                string line = Lines[i];

                int posFirstChar = GetFirstCharIndexFromLine(i);
                int length = line.Length;

                SelectionStart = posFirstChar;
                SelectionLength = length;

                string trimmedLine = line.Trim();
                string indentedLine = string.Empty;

                if (!pass)
                {
                    if (trimmedLine.StartsWith("/*"))
                        pass = true;
                }

                if (pass)                
                {
                    if (trimmedLine.EndsWith("*/"))
                        pass = false;
                }
                
                if (pass)
                    continue;

                if (
                    !trimmedLine.StartsWith("loop") && 
                    !trimmedLine.StartsWith("endif") && 
                    !trimmedLine.StartsWith("else") && 
                    !trimmedLine.StartsWith("return") &&
                    !trimmedLine.StartsWith("next") 
                    )
                {
                    for (int j = 0; j < indent; j++)
                        indentedLine += "\t";

                    indentedLine += trimmedLine;

                    SelectedText = indentedLine;
                }

                if (
                    trimmedLine.StartsWith("if") || 
                    trimmedLine.StartsWith("for") || 
                    trimmedLine.StartsWith("function") ||
                    trimmedLine.StartsWith("while") ||
                    trimmedLine.StartsWith("foreach")
                    )
                    ++indent;

                if (
                    trimmedLine.StartsWith("loop") || 
                    trimmedLine.StartsWith("endif") || 
                    trimmedLine.StartsWith("else") || 
                    trimmedLine.StartsWith("return") ||
                    trimmedLine.StartsWith("next")
                    )
                {
                    --indent;

                    if (indent < 0)
                        indent = 0;

                    for (int j = 0; j < indent; j++)
                        indentedLine += "\t";

                    indentedLine += trimmedLine;

                    SelectedText = indentedLine;
                }

                if (trimmedLine.StartsWith("else"))
                    ++indent;
            }

            m_bLoading = false;            
        }

        public void SaveAs()
        {
            string path = UtilSys.SaveFileDialog(Application.StartupPath);

            if (string.Empty == path)
                return;

            _Save(path);
            m_path = path;
            Dirty = false;
        }

        public void Save()
        {
            if (Scripter.Scripter.DEF_FILE_NAME == m_path)
            {
                _Save(m_path);                
                return;
            }
            
            if (Dirty)
            {
                _Save(m_path);
                Dirty = false;
            }
        }

        private void _Save(string path)
        {
            if (string.Empty == path || Scripter.Scripter.DEF_FILE_NAME == path)
            {
                SaveAs();
                return;
            }

            try
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (string line in Lines)
                    {
                        sw.WriteLine(line);
                    }
                }
                Dirty = false;
            }
            catch (System.Exception)
            {
                throw;
            }
            Dirty = false;

            Scripter.Scripter.Output("Saved file: " + m_path); 

            Opener.mru.Add(path);
            Opener.mru.Save();
            frm.UpdateMRUMenu(); 
        }

        public void HandleBlockComments()
        {
            m_bHandleBlockComments = true;

            int selectionStart = SelectionStart;
            int selectionLength = SelectionLength;

            int pos = 0;
            int len = Text.Length;

            while (true)
            {
                int posBegin = Text.IndexOf(@"/*", pos);

                if (-1 == posBegin)
                    break;

                int posEnd = Text.IndexOf(@"*/", posBegin);

                if (-1 != posEnd)
                    posEnd += 2;

                int length = posEnd - posBegin;

                if (length > 0 && -1 != posBegin && -1 != posEnd)
                {
                    SelectionStart = posBegin;
                    SelectionLength = length;
                    SelectionColor = Color.Gray;
                }
                else
                    break;                

                pos = (posBegin + length);
            }

            // Restore the users current selection point.
            SelectionStart = selectionStart;
            SelectionLength = selectionLength;

            m_bHandleBlockComments = false;
        }

        private void EventTextChanged(object sender, EventArgs e)
        {            
            if (m_bHandleBlockComments)
                return;
            
            if (0 == Text.Length)
                return;
            
            Dirty = true;

            int start = GetFirstCharIndexFromLine(CurrentLineNumber);
            int end = start + CurrentLine.Length;
                      
            _Paint = false;

            int selectionStart = SelectionStart;
            int selectionLength = SelectionLength;
            
            SyntaxColorRegion(start, end);

            SelectionStart = selectionStart;
            SelectionLength = selectionLength;

            _Paint = true;

            if (!m_bLoading)
            {
                m_Undo.Push(Rtf);
                m_Redo.Clear();
            }
        }

        public void SyntaxColorAll()
        {
            SyntaxColorLines(0, Lines.Length);
            HandleBlockComments();
            Invalidate();
        }

        public void SyntaxColorLines(int from, int to)
        {
            int selectionStart = SelectionStart;
            int selectionLength = SelectionLength;

            m_bLoading = true;

            int start;
            int len;
            int end;

            for (int i = from; i < to; ++i)
            {
                start = GetFirstCharIndexFromLine(i);
                len = Lines[i].Length;
                end = start + len;

                SyntaxColorRegion(start, end);
            }

            m_bLoading = false;

            SelectionStart = selectionStart;
            SelectionLength = selectionLength;
        }

        private void SyntaxColorRegion(int start, int end)
        {            
            string line = Text.Substring(start, end - start);

            if (0 == line.Length)
                return;          
            
            // Split the line into tokens
            string prevToken = string.Empty;
                   
            List<Token> toks = Tokenizer.GetTokens(line);            

            int index = start;

            Color curColor = Color.Black;

            bool IsCmdColorized = true;

            string token = string.Empty;
       
            foreach (Token tok in toks)
            {
                token = tok.text;

                if (0 == token.Length)
                    continue;

                if (" " == token)
                {
                    ++index;
                    prevToken = " ";
                    continue;
                }

                // Set the token's default color and font
                SelectionStart = index;
                SelectionLength = token.Length;
                SelectionColor = curColor;
                SelectionFont = Font;

                if ( token.StartsWith("//") )
                {
                    // Find the start of the comment and then extract the whole comment
                    int length = line.Length - (index - start);
                    string commentText = Text.Substring(index, length);
                    SelectionStart = index;
                    SelectionLength = length;
                    SelectionColor = Color.Gray;
                    break;
                }
                
                if (token.StartsWith("@"))  // variable
                {
                    SelectionColor = Color.OrangeRed;
                }

                ColorizeCommand(token, ref IsCmdColorized);
                ColorizeSubCommand(token);

                if (token.StartsWith("\"")) // known consts like "Min", "ByID" etc
                {
                    for (int i = 0; i < Language.StringConsts.Length; ++i)
                    {
                        if ("\"" + Language.StringConsts[i] + "\"" == token)
                        {
                            SelectionStart = index + 1;
                            SelectionLength = token.Length - 2;

                            SelectionColor = Color.Green;
                            break;
                        }
                    }
                }

                if ("<bp>" == token)
                    SelectionColor = Color.Red;                
                
                index += token.Length;
                prevToken = token;
            }            
        }

        private void ColorizeSubCommand(string token)
        {
            foreach (KeyValuePair<string, SyntaxData> item in Language.SubCommands)
            {
                foreach (string subcmd in item.Value.SubCmds)
                {
                    if (subcmd == token)
                    {
                        SelectionColor = Color.Green;
                        return;
                    }
                }
            }
        }

        private void ColorizeCommand(string token, ref bool showHelp)
        {
            if (token.Length > Language.LongestKeyWordLength) // token's lenght is too big do not iterate
                return;

            if (token.Length < Language.ShortestKeyWordLength) // token is too short
                return;

            for (int i = 0; i < Language.commands.Length; ++i)
            {
                if (Language.commands[i] == token)
                {
                    SelectionColor = Color.Blue;                           
                    
                    if (!m_bLoading && showHelp)
                        frm.ShowHelpOnCurrentCommand(token);

                    showHelp = false;

                    break;
                }
            } 
        }

        private string[] GetFolderTree(string line)
        {
            string[] buffer = null;
            buffer = Directory.GetDirectories(Scripter.Scripter.AppPath);

            for (int i=0; i<buffer.Length; ++i)
                buffer[i] = buffer[i].Replace(Scripter.Scripter.AppPath + @"\", "");

            return buffer;
        }

        private void UpdateImportFolderISList(bool reposition)
        {
            Intellisense._is.color = Color.CadetBlue;

            string line = CurrentLine;
            
            line = line.Replace("#import", "").Trim();
            
            try
            {
                int len = line.Length;

                if (line[0] == '"')
                    line = line.Substring(1, line.Length-1);

                len = line.Length;

                if (line[len - 1] == '"')
                    line = line.Substring(0, line.Length - 1);

                len = line.Length;

                if (line[len-1] == '\\')
                    line = line.Substring(0, line.Length - 1);

                string[] buffer = null;

                string path = Path.Combine(Scripter.Scripter.AppPath, line);

                buffer = Directory.GetDirectories(path);

                string temp = string.Empty;

                for (int i = 0; i < buffer.Length; ++i)
                {
                    int pos = buffer[i].LastIndexOf('\\');

                    //temp = buffer[i].Replace(path, "");
                    //buffer[i] = temp.Substring(1, temp.Length - 1);
                    buffer[i] = buffer[i].Substring(pos+1, buffer[i].Length - pos - 1);
                }

                string[] files = Directory.GetFiles(path);

                for (int i = 0; i < files.Length; ++i)
                {
                    files[i] = Path.GetFileName(files[i]);
                    //temp = files[i].Replace(path, "");
                    //files[i] = temp.Substring(1, temp.Length - 1);
                }

                m_is.Clear();

                foreach (string s in buffer)
                {
                    m_is.AddItem(s);
                }

                foreach (string s in files)
                {
                    m_is.AddItem(s);
                }

                m_is.CalcWidth();
                m_is.CalcHeight();

                if (reposition)
                    m_is.Show(IntelliSensePosition);
            }
            catch (Exception)
            {
                m_is.Hide();
                m_ISmode = EnumIntellisenseMode.Undefined;
            }                       
        }

        private void FillIntellisense(EnumIntellisenseMode mode, bool show)
        {
            string[] buffer = null;
            m_ISmode = mode;
            bool bWasVisible = m_is.IsVisible;
            
            m_is.Hide();

            switch (m_ISmode)
            {
                case EnumIntellisenseMode.ImportFolder:
                    buffer = GetFolderTree(CurrentLine);
                    Intellisense._is.color = Color.CadetBlue;  
                    Intellisense._is._text = "Import Folder";                  
                break;

                case EnumIntellisenseMode.StringConstant:
                    buffer = Language.StringConsts;
                    Intellisense._is.color = Color.Tan;
                    Intellisense._is._text = "String Constant";
                break;

                case EnumIntellisenseMode.Command:
                    buffer = Language.commands;
                    Intellisense._is.color = Color.Turquoise;
                    Intellisense._is._text = "Command";
                break;

                case EnumIntellisenseMode.Variables:
                    buffer = Variables.ToArray();
                    Intellisense._is.color = Color.DarkOrange;
                    Intellisense._is._text = "Variable";
                break;

                case EnumIntellisenseMode.Functions:
                    buffer = Functions.ToArray();
                    Intellisense._is.color = Color.DarkSalmon;
                    Intellisense._is._text = "Function";
                break;

                case EnumIntellisenseMode.SubCommand:
                    buffer = m_CurSyntaxData.SubCmds;
                    Intellisense._is.color = m_CurSyntaxData.IntellisenseBackColor;
                    Intellisense._is._text = m_CurSyntaxData.IntellisenseTitle;
                break;
            }

            Debug.Assert(null != buffer);

            m_is.Clear();

            foreach(string s in buffer)
            {
                m_is.AddItem(s);
            }

            if (show)
            {
                m_is.CalcWidth();
                m_is.CalcHeight();
                m_is.Show(IntelliSensePosition);
            }
            else if (bWasVisible)
                m_is.Show();
        }

        private string ReverseString(string temp)
        {
            char[] ca = temp.ToCharArray();
            Array.Reverse(ca);
            return (new String(ca));
        }
        
        private void EventKeyUp(object sender, KeyEventArgs e)
        {
            ShowStatusAndDebugInfo();

            if (
                e.KeyCode == Keys.Enter ||
                e.KeyCode == Keys.Delete ||
                e.KeyCode == Keys.Back ||
                e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.PageUp ||
                e.KeyCode == Keys.PageDown
                )
            {
                Parent.Invalidate();
            }

            if (e.KeyCode == Keys.F9)
            {
                SetBreakpoint();
                Parent.Invalidate();
            }
        }

        public void SetBreakpoint()
        {
            _Paint = false;

            int temp = SelectionStart;

            SelectionStart = GetFirstCharIndexOfCurrentLine();
            SelectionLength = Lines[CurrentLineNumber].Length;

            if (0 != SelectionLength)
            {
                string tmp = SelectedText;

                if (0 != tmp.Trim().Length)
                {
                    if (!tmp.StartsWith("var") && !tmp.StartsWith("const"))
                    {
                        if (!SelectedText.StartsWith("//"))
                        {
                            if (SelectedText.EndsWith(" <bp>"))
                            {
                                SelectedText = SelectedText.Replace(" <bp>", "");
                            }
                            else
                            {
                                SelectedText = SelectedText + " <bp>";
                            }
                        }
                    }
                }
            }

            SelectionStart = temp;
            SelectionLength = 0;

            _Paint = true;
        }

        public bool IsBookmarkInLine(int lineNum)
        {
            if (Lines[lineNum].EndsWith(" <bp>"))
                return true;

            return false;
        }

        public void ShowStatusAndDebugInfo()
        {
            string line = CurrentLine;
            line = line.Insert(CurrentLineCaretPos, "|");

            ScripterDebug.ShowTokensInCurrentLine(TokensInCurrentLine, line, CurrentLineNumber);
            ScripterDebug.ShowCaretPosInCurrentLine(line);
            ScripterDebug.ShowCurrentLineNumber(CurrentLineNumber);
            ScripterDebug.ShowCurrentColumnNumber(CurrentLineCaretPos);
            ScripterDebug.ShowCurrentPos(SelectionStart);
        }

        private bool HandlePeriodKey(bool showIntellisense)
        {
            string line = CurrentLine;
            int pos = CurrentLineCaretPos;            
            
            line = line.Insert(pos, ".");

            return HandleSubCommand(line, pos, showIntellisense);
        }

        private bool HandleSubCommand(string line, int pos, bool showIntellisense)
        {
            string key = string.Empty;
            foreach(string keyz in Language.SubCommands.Keys)
            {
                key = keyz + ".";
                if (line.Length >= key.Length)
                {
                    int from = pos - key.Length + 1;

                    if (from < 0)
                        return false;

                    string temp = line.Substring(from, key.Length);

                    if (temp == key)
                    {
                        m_ISmode = EnumIntellisenseMode.SubCommand;
                        m_CurSyntaxData = Language.SubCommands[keyz];
                        FillIntellisense(m_ISmode, showIntellisense);
                        return true;
                    }
                }
            }
            m_is.Hide();
            m_CurSyntaxData = null;
            return false;
        }

        private void CheckVariableDeclaration()
        {                        
            string[] tokens = CurrentLine.Split(' ');

            if (tokens.Length == 4)
            {
                if (tokens[0] == "var")  // 0
                {
                    string varName = tokens[1]; // 1

                    if (varName[0] == '@')
                    {
                        if (tokens[2] == "=") // 2
                        {
                            string item = tokens[1] + " = " + tokens[3];

                            for (int i = 0; i < m_ListVariables.Count; ++i)
                            {
                                string str = m_ListVariables[i];

                                if (str.Length > varName.Length)
                                {
                                    if (str.Substring(0, varName.Length) == varName)
                                    {
                                        m_ListVariables.RemoveAt(i);
                                    }
                                }
                            }
                            m_ListVariables.Add(item); // 3                            
                        }
                    }
                }
            }
        }

        private bool IsCommentLine()
        {
            string temp = CurrentLine.Trim();

            if (temp.StartsWith(@"//"))
                return true;
            
            return false;
        }

        private void EventKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
                return;

            NativeMethods.POINT scrollPos = GetScrollPos();
            
            //Scripter.Output(scrollPos.Y.ToString());

            if (Keys.F9 == e.KeyCode)
                return;

            if (Keys.Tab == e.KeyCode)
            {
                if (SelectionLength > 0)
                {
                    int oldSelStart = SelectionStart;
                    int oldSelLength = SelectionLength;

                    int lineFrom = GetLineFromCharIndex(SelectionStart);
                    int lineTo = GetLineFromCharIndex(SelectionStart + SelectionLength);

                    int linesAffected = lineTo - lineFrom;
                    int tabCnt = 0;

                    for (int i = lineFrom; i < lineTo; ++i)
                    {
                        string line = Lines[i];

                        int first = GetFirstCharIndexFromLine(i);
                        int len = line.Length;

                        SelectionStart = first;
                        SelectionLength = len;

                        if (!e.Shift)
                            SelectedText = "\t" + line;
                        else
                        {
                            if (line.StartsWith("\t"))
                            {
                                SelectedText = line.Substring(1, line.Length - 1);
                                ++tabCnt;
                            }
                        }
                    }

                    if (!e.Shift)
                    {
                        SelectionStart = oldSelStart + 1;
                        SelectionLength = (oldSelLength + linesAffected) - 1;
                    }
                    else
                    {
                        if (tabCnt > 0)
                        {
                            SelectionStart = oldSelStart - 1;
                            SelectionLength = (oldSelLength - tabCnt) + 1;
                        }
                        else
                        {
                            SelectionStart = oldSelStart;
                            SelectionLength = oldSelLength;
                        }
                    }

                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    return;
                }
            }
            
            if (e.Control)
            {
                bool handled = false;

                m_is.Hide();

                switch(e.KeyCode)
                {
                    case Keys.C:
                        Copy();
                        handled = true;
                    break;
            
                    case Keys.V:
                        MyPaste();
                        handled = true;
                    break;

                    case Keys.Z:
                        MyUndo();
                        handled = true;
                    break;

                    case Keys.X:
                        Cut();
                        handled = true;
                    break;
                }

                if (handled)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                }
            }
            
            if (e.KeyCode == Keys.Escape)
            {                
                m_is.Hide();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (IsCommentLine())
                return;

            if (e.KeyData == Keys.OemPeriod) // .
            {
                HandlePeriodKey(true);
                return;
            }

            if (e.KeyValue == 220) // \ backslash
            {
                if ("#import" == CommandInCurrentLine)
                {
                    UpdateImportFolderISList(true);
                    m_ISmode = EnumIntellisenseMode.ImportFolder;
                }
                return;
            }
            
            if (e.KeyData == Keys.Space)
            {
                switch (CommandInCurrentLine)
                {
                    case "call":                    
                        if (Functions.Count > 0)                        
                            FillIntellisense(EnumIntellisenseMode.Functions, true);
                    return;                    
                }
            }

            if (e.KeyCode == Keys.OemQuotes && e.Shift) // double quote mark
            {
                switch(CommandInCurrentLine)
                {
                    case "#import":                
                        FillIntellisense(EnumIntellisenseMode.ImportFolder, true);
                    return;                    
                    
                    case "var":
                    case "log":
                    case "sys":
                    return;

                    default:
                    {
                        int numOfQuotes = 1;
                        string ln = CurrentLine;
                        
                        foreach (char ch in ln)
                        {
                            if ('"' == ch)
                                ++numOfQuotes;
                        }

                        if (0 == numOfQuotes % 2)
                            return;

                        FillIntellisense(EnumIntellisenseMode.StringConstant, true);
                    }
                    return;
                }
            }

            if (e.Shift && e.KeyValue == 50) // @ variable
            {                
                if (0 == m_ListVariables.Count)
                    return;
                
                if ("var" == CommandInCurrentLine)
                    return;

                FillIntellisense(EnumIntellisenseMode.Variables, true);
                return;
            }

            if (e.KeyCode == Keys.Back) // if we go back, delete 
            {
                Point pt;
                Point ptScreen;
                Point ptClient = new Point(0, 0);

                string line = CurrentLine;

                int pos = CurrentLineCaretPos;

                if (0 == pos)
                {
                    m_ISmode = EnumIntellisenseMode.Undefined;
                    m_is.Hide();
                    return;
                }

                --pos;

                line = line.Remove(pos, 1);

                if (0 == line.Length)
                {
                    m_ISmode = EnumIntellisenseMode.Undefined;
                    m_is.Hide();
                    return;
                }

                if ( line.Contains("#import") )
                {
                    m_ISmode = EnumIntellisenseMode.ImportFolder;
                    UpdateImportFolderISList(true);
                    return;
                }
                                
                // characters to watch
                // ,
                // (
                // @
                // white space
                string buffer = string.Empty;
                
                for (int i = pos-1; i > -1; --i)
                {
                    char ch = line[i];

                    switch (ch)
                    {
                        case '@': // variable
                        {
                            if ("var" != CommandInCurrentLine)
                            {
                                FillIntellisense(EnumIntellisenseMode.Variables, false);

                                string buf = "@" + ReverseString(buffer);

                                m_is.Update(buf);

                                ptClient = new Point(0, 0);

                                pt = GetPositionFromCharIndex(SelectionStart - (buffer.Length + 2));
                                ptScreen = PointToScreen(pt);

                                ptClient = Scripter.Scripter.ScripterForm.PointToClient(ptScreen);
                                ptClient.Y += Font.Height;

                                m_is.SetPos(ptClient);
                            }
                        }
                        return;                        

                        case '(': // function call parameter?
                            m_is.Hide();
                        return;

                        case '"': // string constant or? more to come...(flanker)
                        {
                            FillIntellisense(EnumIntellisenseMode.StringConstant, false);

                            if (buffer.Length > 0)
                                m_is.Update(ReverseString(buffer));
                            else
                            {
                                m_is.CalcHeight();
                                m_is.CalcWidth();
                            }

                            ptClient = new Point(0, 0);

                            pt = GetPositionFromCharIndex(SelectionStart - (buffer.Length + 2));
                            ptScreen = PointToScreen(pt);

                            ptClient = Scripter.Scripter.ScripterForm.PointToClient(ptScreen);
                            ptClient.Y += Font.Height;

                            m_is.SetPos(ptClient);
                        }
                        return;

                        case ' ': // whitespace
                        {
                            int j = i;

                            if (j - 4 >= 0)
                            {
                                char[] cmd = new char[4];

                                cmd[0] = line[j - 1];
                                cmd[1] = line[j - 2];
                                cmd[2] = line[j - 3];
                                cmd[3] = line[j - 4];

                                if (cmd[3] == 'c' && cmd[2] == 'a' && cmd[1] == 'l' && cmd[0] == 'l') // call ?
                                {
                                    FillIntellisense(EnumIntellisenseMode.Functions, false);

                                    if (buffer.Length > 0)
                                        m_is.Update(ReverseString(buffer));
                                    else
                                    {
                                        m_is.CalcHeight();
                                        m_is.CalcWidth();
                                    }

                                    ptClient = new Point(0, 0);

                                    pt = GetPositionFromCharIndex(SelectionStart - (buffer.Length + 2));
                                    ptScreen = PointToScreen(pt);

                                    ptClient = Scripter.Scripter.ScripterForm.PointToClient(ptScreen);
                                    ptClient.Y += Font.Height;
                                    
                                    m_is.SetPos(ptClient);

                                    return;
                                }                                                                    
                            }
                            m_is.Hide();                            
                        }
                        return;                        

                        case '.': // we found a dot
                        {
                            string part = line.Substring(0, i+1);

                            if (HandleSubCommand(part, i, false))
                            {
                                if (buffer.Length > 0)
                                    m_is.Update(ReverseString(buffer));
                                else
                                {
                                    m_is.CalcHeight();
                                    m_is.CalcWidth();
                                }

                                ptClient = new Point(0, 0);

                                pt = GetPositionFromCharIndex(SelectionStart - (buffer.Length + 2));
                                ptScreen = PointToScreen(pt);

                                ptClient = Scripter.Scripter.ScripterForm.PointToClient(ptScreen);
                                ptClient.Y += Font.Height;
                                
                                m_is.SetPos(ptClient);
                                return;
                            }                            
                        }
                        return;
                    }
                    buffer += ch;
                }

                FillIntellisense(EnumIntellisenseMode.Command, false);

                if (line.Length > 0)
                    m_is.Update(line);
                else
                {
                    m_is.CalcHeight();
                    m_is.CalcWidth();
                }

                ptClient = new Point(0, 0);

                pt = GetPositionFromCharIndex(SelectionStart - (line.Length + 1));
                ptScreen = PointToScreen(pt);

                ptClient = Scripter.Scripter.ScripterForm.PointToClient(ptScreen);
                ptClient.Y += Font.Height;

                m_is.SetPos(ptClient);
                
                return;                                        
            }

            if (e.KeyValue < 48 || (e.KeyValue >= 58 && e.KeyValue <= 64) || (e.KeyValue >= 91 && e.KeyValue <= 96) || e.KeyValue > 122)
            {
                if (!m_is.IsVisible)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                        case Keys.Down:
                        case Keys.Return:
                        case Keys.Tab:
                        case Keys.Space:                            
                            m_ISmode = EnumIntellisenseMode.Undefined;
                            CheckVariableDeclaration();
                        break;
                    }
                }
                else
                {
                    bool handled = false;

                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            handled = true;
                            m_is.Down();
                        break;

                        case Keys.Down:
                            handled = true;
                            m_is.Up();
                        break;

                        case Keys.Return:
                        case Keys.Tab:
                        case Keys.Space:
                        {
                            AutoComplete();
                            handled = true;

                            if (m_ISmode != EnumIntellisenseMode.ImportFolder)
                                m_is.Hide();                            
                        }
                        break;
                    }

                    if (handled)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        return;
                    }
                }               
            }
            else // Letter or number typed, search for it in the listview
            {
                if (e.KeyValue == 107 || e.KeyValue == 109) // + -
                    return;                    

                char val = (char)e.KeyValue;

                if (e.Shift)
                {
                    if (e.KeyValue == 50)
                        val = '@';

                    if (e.KeyValue == 51)
                        val = '#';
                }

                string tmp = LastWordInCurrentLine;
                string Typed = tmp + val;               
                
                Typed = Typed.ToLower();                                

                if (m_is.IsVisible)
                {
                    switch(m_ISmode)
                    {
                        case EnumIntellisenseMode.SubCommand:
                        {
                            string str = Typed.Replace(m_CurSyntaxData.cmd, "");
                            m_is.Update(str);
                        }
                        break;

                        case EnumIntellisenseMode.Functions:
                        {
                            string str = Typed.Replace("call ", "").Trim();                            
                            m_is.Update(str);
                        }
                        break;

                        case EnumIntellisenseMode.Command:
                        {
                            m_is.Update(Typed.Trim());
                        }
                        break;

                        case EnumIntellisenseMode.Variables:
                        {
                            string temp = string.Empty;

                            for (int i = Typed.Length - 1; i > -1; --i)
                            {
                                char ch = Typed[i];                                                               
                                temp += ch;
                                
                                if ('@' == ch)
                                    break;                                
                            }

                            temp = ReverseString(temp);
                            m_is.Update(temp);
                        }
                        break;

                        case EnumIntellisenseMode.StringConstant:
                        {
                            string temp = string.Empty;

                            for (int i = Typed.Length - 1; i > -1; --i)
                            {
                                char ch = Typed[i];

                                if ('"' == ch)
                                    break;
                                
                                temp += ch;
                            }
                            temp = ReverseString(temp);
                            m_is.Update(temp);                         
                        }
                        break;

                        case EnumIntellisenseMode.ImportFolder:
                        {
                            string temp = string.Empty;

                            for (int i = Typed.Length - 1; i > -1; --i)
                            {
                                char ch = Typed[i];

                                if ('"' == ch || '\\' == ch)
                                    break;

                                temp += ch;
                            }
                            temp = ReverseString(temp);
                            m_is.Update(temp);
                        }
                        break;
                    }
                }
                else // intellisense isn't visible
                {
                    if (m_ISmode == EnumIntellisenseMode.Undefined)
                    {
                        FillIntellisense(EnumIntellisenseMode.Command, false);
                    }
                    m_is.Update(Typed.Trim());
                }
            }
        }
            
        public void AutoComplete()
        {
            switch (m_ISmode)
            {
                case EnumIntellisenseMode.Command:
                {
                    bool found = false;
                    string word = LastWordInCurrentLine;

                    SelectionStart = SelectionStart - word.Length;
                    SelectionLength = word.Length;

                    foreach (string keys in Language.SubCommands.Keys)
                    {
                        if (keys == m_is.Selected)
                        {
                            SelectedText = m_is.Selected;
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                        SelectedText = m_is.Selected + " ";
                }
                break;

                case EnumIntellisenseMode.ImportFolder:
                {
                    string line = CurrentLine;

                    int curPos = SelectionStart;
                    int ColumnIndex = SelectionStart - GetFirstCharIndexOfCurrentLine();
                    int quotePosStart = 0;
                    int backslashPosStart = 0;

                    for (int i = ColumnIndex - 1; i > -1; --i)
                    {
                        char ch = line[i];

                        if ('"' == ch)
                        {
                            quotePosStart = i + 1;
                            break;
                        }

                        if ('\\' == ch)
                        {
                            backslashPosStart = i + 1;
                            break;
                        }
                    }

                    if (0 != quotePosStart)
                    {
                        SelectionStart = GetFirstCharIndexFromLine(CurrentLineNumber) + quotePosStart;

                        SelectionLength = ColumnIndex - quotePosStart;

                        if (Text.Length > (SelectionStart + SelectionLength)) // we have characters after selection?
                        {
                            char endCh = Text[SelectionStart + SelectionLength];

                            if ('"' == endCh)
                                SelectedText = m_is.Selected;
                            else
                                SelectedText = m_is.Selected + "\\";
                        }
                        else
                        {
                            if ( m_is.Selected.Contains(".") )
                                SelectedText = m_is.Selected + "\"";
                            else
                                SelectedText = m_is.Selected + "\\";
                        }
                        UpdateImportFolderISList(true);
                    }

                    if (0 != backslashPosStart)
                    {
                        SelectionStart = GetFirstCharIndexFromLine(CurrentLineNumber) + backslashPosStart;

                        SelectionLength = ColumnIndex - backslashPosStart;

                        if (Text.Length > (SelectionStart + SelectionLength)) // we have characters after selection?
                        {
                            char endCh = Text[SelectionStart + SelectionLength];

                            if ('"' == endCh)
                                SelectedText = m_is.Selected;
                            else
                            {
                                if (m_is.Selected.Contains("."))
                                {
                                    SelectedText = m_is.Selected + "\"";                                    
                                    ScriptExplorer.Parse(frm, this);
                                }
                                else
                                    SelectedText = m_is.Selected + "\\";
                            }
                        }
                        else
                        {
                            if (m_is.Selected.Contains("."))
                            {
                                SelectedText = m_is.Selected + "\"";
                                ScriptExplorer.Parse(frm, this);
                            }
                            else
                                SelectedText = m_is.Selected + "\\";
                        }
                        UpdateImportFolderISList(true);
                    }
                }
                return;

                case EnumIntellisenseMode.StringConstant:
                {
                    string line = CurrentLine;

                    int curPos = SelectionStart;
                    int ColumnIndex = SelectionStart - GetFirstCharIndexOfCurrentLine();
                    int quotePosStart = 0;

                    for (int i = ColumnIndex - 1; i > -1; --i)
                    {
                        char ch = line[i];

                        if ('"' == ch)
                        {
                            quotePosStart = i + 1;
                            break;
                        }
                    }

                    SelectionStart = GetFirstCharIndexFromLine(CurrentLineNumber) + quotePosStart;

                    SelectionLength = ColumnIndex - quotePosStart;

                    if (Text.Length > (SelectionStart + SelectionLength)) // we have characters after selection?
                    {
                        char endCh = Text[SelectionStart + SelectionLength];

                        if ('"' == endCh)
                            SelectedText = m_is.Selected;
                        else
                            SelectedText = m_is.Selected + "\"";
                    }
                    else
                        SelectedText = m_is.Selected + "\"";
                }
                break;

                case EnumIntellisenseMode.Functions:
                {
                    string line = CurrentLine;
                    
                    int curPos = SelectionStart;
                    int ColumnIndex = SelectionStart - GetFirstCharIndexOfCurrentLine();

                    string tmp = line.ToLower().Replace("call", "");
                    tmp = tmp.Trim();

                    int num = GetFirstCharIndexOfCurrentLine();

                    SelectionStart = SelectionStart - tmp.Length;
                    SelectionLength = tmp.Length;
                    SelectedText = m_is.Selected;
                }                    
                break;

                case EnumIntellisenseMode.Variables:
                {
                    string line = CurrentLine;
                    int curPos = SelectionStart;
                    int ColumnIndex = SelectionStart - GetFirstCharIndexOfCurrentLine();
                    int dotPosStart = 0;

                    for (int i = ColumnIndex - 1; i > -1; --i)
                    {
                        char ch = line[i];

                        if ('@' == ch)
                        {
                            dotPosStart = i + 1;
                            break;
                        }
                    }

                    SelectionStart = GetFirstCharIndexFromLine(CurrentLineNumber) + dotPosStart;

                    SelectionLength = ColumnIndex - dotPosStart;

                    string[] buf = m_is.Selected.Split('=');

                    SelectedText = buf[0].Replace('@', ' ').Trim();
                }
                break;

                case EnumIntellisenseMode.SubCommand:                                                
                {
                    string line = CurrentLine;
                    int curPos = SelectionStart;
                    int ColumnIndex = SelectionStart - GetFirstCharIndexOfCurrentLine();                    
                    int dotPosStart = 0;                    

                    for (int i = ColumnIndex-1; i > -1; --i)
                    {
                        char ch = line[i];

                        if ('.' == ch)
                        {
                            dotPosStart = i+1;
                            break;
                        }
                    }

                    SelectionStart = GetFirstCharIndexFromLine(CurrentLineNumber) + dotPosStart;

                    SelectionLength = ColumnIndex - dotPosStart;

                    SelectedText = m_is.Selected;
                }
                break;
            }
            m_ISmode = EnumIntellisenseMode.Undefined;           
        }
        
    }
}