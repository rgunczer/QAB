using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Automation;
using System.Collections.Generic;

using Util;
using System.Windows.Forms;


namespace VM
{
    public enum VMStatus
    {
        INITIALIZATION = 0,
        READY = 1,
        IN_PLAYBACK = 2,
        STOPPED = 3,
        END = 4,
        QUIT = 5,
        ERROR = 6,
    };

    public class VirtualMachine // VM
    {
        private IHost m_host = null; // the host application: Pilot, Scripter
        public IHost host
        {
            get { return m_host; }
        }

        public bool _parseError = false;
        public string _parseErrorScriptPath = string.Empty;
        public int _parseErrorLineNumber = -1;

        private int m_ScreenshotCounter;

        private int m_IP;
        private DateTime m_dtStartTime;
        private DateTime m_dtEndTime;
        private Script m_script = null;
        private VariableManager m_variables = null;
        private int m_iSpeed = 3000;
        private ControlUltraTree m_utree = null;
        private ControlGrid m_grid = null;
        private string m_ActionErrorMessage = string.Empty;
        private bool m_bSaveScreenshotOnError = false;
        private ActionBase m_actionError = null;
        private bool m_bErrorIsHandled = false;
        private bool m_bQuitIsHandled = false;

        private Dictionary<string, AutomationElement> m_dicCache = new Dictionary<string, AutomationElement>();
        private Dictionary<string, AutomationElement> m_dicCacheWindow = new Dictionary<string, AutomationElement>();
        private Dictionary<string, ControlBase> m_dicControls = new Dictionary<string, ControlBase>();
        private Dictionary<string, int> m_dicGotoCounts = new Dictionary<string, int>();

        private List<string> m_listAfterEachCommand = new List<string>();
        private List<string> m_listAfterCommands = new List<string>();
        private List<string> m_listOnQuit = new List<string>();
        private List<string> m_listOnError = new List<string>();
                
        private Stack<int> m_InstrStack = new Stack<int>();
        private Stack<int> m_CallStack = new Stack<int>();
        private Stack<int> m_IfStack = new Stack<int>();
        private Stack<int> m_LoopStack = new Stack<int>();
        private Stack<string> m_FnNameStack = new Stack<string>();
        private Stack<string> m_ErrStack = new Stack<string>();

        public Dictionary<string, AutomationElement> Cache { get { return m_dicCache; } }
        public Dictionary<string, AutomationElement> CacheWindow { get { return m_dicCacheWindow; } }


        public void PushError(string msg)
        {
            m_ErrStack.Push(msg);
        }

        public VariableManager variables
        {
            get
            {
                DateTime dt = DateTime.Now;
                int ev = dt.Year;        

                Debug.Assert(null != m_variables);
                return m_variables;
            }
        }
        
        public string ActionErrorMessage 
        {
            get
            {
                return m_ActionErrorMessage;
            }

            set
            {
                m_ActionErrorMessage = value;
                host.WriteLog(m_ActionErrorMessage);                
            }
        }

        // properties
        public string Scope
        {
            get { return m_FnNameStack.Peek(); }
        }

        public Scope CurrentScope
        {
            get { return m_variables.CurrentScope; }
        }

        public Script script
        {
            get { return m_script; }
            set { m_script = value; }
        }

        public int Speed
        {
            get 
            { 
                return m_iSpeed; 
            }
            set
            {
                m_iSpeed = value;
                variables.UpdateSystem("sys.speed", m_iSpeed.ToString());
                //host.WriteLog("VM Speed Set to: " + m_iSpeed);
            }
        }

        public bool SaveScreenshotOnError
        {
            get
            {
                return m_bSaveScreenshotOnError;
            }
            set
            {
                m_bSaveScreenshotOnError = value;
                variables.UpdateSystem("sys.SaveScreenshotOnError", m_bSaveScreenshotOnError.ToString());
                //host.WriteLog("VM SaveScreenshotOnError Set to: " + m_bSaveScreenshotOnError);
            }
        }

        public int ScreenWidth
        {
            get
            {
                variables.UpdateSystem("env.ScreenWidth", Screen.PrimaryScreen.Bounds.Width.ToString());
                return Screen.PrimaryScreen.Bounds.Width;
            }
        }

        public int ScreenHeight
        {
            get
            {
                variables.UpdateSystem("env.ScreenHeight", Screen.PrimaryScreen.Bounds.Height.ToString());
                return Screen.PrimaryScreen.Bounds.Height;
            }
        }

        public VMStatus PlaybackStatus
        {
            get { return (m_Status); }
            set { m_Status = value; }
        }

        private VMStatus m_Status = VMStatus.READY;        
        public VMStatus Status
        {
            get { return (m_Status); }
            set { m_Status = value; }
        }

//--------------------------------------------------------------------------------------
// ctor    
        public VirtualMachine(IHost host)
        {            
            m_host = host;
            m_script = null;
            m_iSpeed = 3000;
            m_Status = VMStatus.READY;

            ActionBase.vm = this;
            Finder.vm = this;
            UtilAutomation.vm = this;
            Data.vm = this;
            FrmTip.vm = this;
        }

        public string GetLastError()
        {
            if (m_ErrStack.Count > 0)
                return m_ErrStack.Pop();
            else
                return string.Empty;
        }
        
        public void ParseError(string scriptPath, int lineNumber)
        {
            _parseError = true;
            _parseErrorScriptPath = scriptPath;
            _parseErrorLineNumber = lineNumber;
        }

        public void AddAfterEachCommand(string functionName)
        {
            m_listAfterEachCommand.Add(functionName);
        }

        public void RemoveAfterEachCommand(string functionName)
        {
            m_listAfterEachCommand.Remove(functionName);
        }

        public void AddAfterCommands(string functionName)
        {
            m_listAfterCommands.Add(functionName);
        }

        public void RemoveAfterCommands(string functionName)
        {
            m_listAfterCommands.Remove(functionName);
        }

        public void AddOnError(string functionName)
        {
            m_listOnError.Add(functionName);
        }

        public void RemoveOnError(string functionName)
        {
            m_listOnError.Remove(functionName);
        }

        public void AddOnQuit(string functionName)
        {
            m_listOnQuit.Add(functionName);
        }

        public void RemoveOnQuit(string functionName)
        {
            m_listOnQuit.Remove(functionName);
        }




        public ControlUltraTree GetUltraTree()
        {
            return (m_utree);
        }

        public ControlGrid GetGrid()
        {
            return (m_grid);
        }

        public void AddControl(ControlBase ctrl)
        {
            if (ctrl is ControlUltraTree)
                m_utree = (ControlUltraTree)ctrl;

            if (ctrl is ControlGrid)
                m_grid = (ControlGrid)ctrl;

            if (m_dicControls.ContainsKey(ctrl.Name))
                m_dicControls[ctrl.Name] = ctrl;
            else
                m_dicControls.Add(ctrl.Name, ctrl);
        }

        public void RemoveControl(string id)
        {
            m_dicControls.Remove(id);
        }

        public ControlBase GetControl(string id)
        {
            if(m_dicControls.ContainsKey(id))
                return (m_dicControls[id]);

            return (null);
        }

        public void Stop()
        {
            m_Status = VMStatus.STOPPED;
        }
                                    
        public TimeSpan UpdateDuration()
        {
            m_dtEndTime = DateTime.Now;
            TimeSpan duration = m_dtEndTime - m_dtStartTime;
            return duration;            
        }

        public bool Init()
        {
            m_Status = VMStatus.INITIALIZATION;

            string path = Path.Combine(host.StartupPath, @"data\log\" + Path.GetFileNameWithoutExtension(m_script.Path) + ".log");

            host.SetLogFile(path, host.BigBrother);

            m_variables = new VariableManager(this, m_script);

            StringBuilder sb = new StringBuilder();

            sb.Append("<Playback ");

            string[,] attributes =
            {            
                { "script=", m_script.Path },
                { "On=", UtilSys.GetDateTime() },
                { "Autorun=", host.IsAutorun.ToString() },
            };

            for(int i=0; i<attributes.GetLength(0); ++i)
            {
                sb.Append(" " + attributes[i, 0] + "\"" + attributes[i, 1] + "\"");
            }

            sb.Append(">");

            string msg = sb.ToString();           
            
            host.WriteLog(msg);            

            m_dtStartTime = DateTime.Now;

            m_actionError = null;

            // register events are using this
            m_bErrorIsHandled = false;
            m_bQuitIsHandled = false;

            m_utree = null;
            m_grid = null;
            m_dicControls.Clear();
            m_dicCache.Clear();
            m_dicGotoCounts.Clear();
            m_InstrStack.Clear();
            m_CallStack.Clear();
            m_IfStack.Clear();
            m_LoopStack.Clear(); // this stack is used by instructions like for, while, foreach
            m_FnNameStack.Clear();
            m_ErrStack.Clear();

            m_listAfterEachCommand.Clear();
            m_listAfterCommands.Clear();
            m_listOnError.Clear();
            m_listOnQuit.Clear();

            host.ReleaseSQLStuff();               

            m_FnNameStack.Push(VariableManager._GlobalScopeName);
                  
            m_variables.Reset();
            m_variables.AddSystem("$ERROR", "False");
            m_variables.AddSystem("sys.speed", Speed.ToString());
            m_variables.AddSystem("sys.SaveScreenshotOnError", m_bSaveScreenshotOnError.ToString());
            m_variables.AddSystem("env.ScreenWidht", Screen.PrimaryScreen.Bounds.Width.ToString());
            m_variables.AddSystem("env.ScreenHeight", Screen.PrimaryScreen.Bounds.Height.ToString());


            foreach(KeyValuePair<string, string> kv in m_script.Scopes[0].Variables)
            {
                m_variables.Add(kv.Key, kv.Value);
            }

            foreach (KeyValuePair<string, string> kv in m_script.Scopes[0].Consts)
            {
                m_variables.AddConst(kv.Key, kv.Value);
            }

            foreach(KeyValuePair<string, string> kv in Data.Vars)
            {
                m_variables.AddExternal(kv.Key, kv.Value);
            }

            m_IP = m_script.EntryPoint;

            if (-1 == m_IP)
            {
                m_ErrStack.Push("No entry point in script.\r\nEdit script file and enter the [sys.start] command from which point you want playback to begin.");
                return false;
            }

            m_Status = VMStatus.IN_PLAYBACK;
            return true;
        }

        public void Done()
        {            
            host.SetTitle(null);

            bool quit = false;
            string msg = "<Done ";

            TimeSpan duration = UpdateDuration();
            string msgRest = " On=\"" + m_dtEndTime.ToString("dd-MM-yyyy HH:mm:ss") + "\" Duration=\"" + Util.Support.FormatTime4Me(duration) + "\"/>";
            
            switch(m_Status)
            {
                case VMStatus.STOPPED:
                    msg += "Result=\"FAILED\"" + msgRest;
                break;

                case VMStatus.END:
                    msg += "Result=\"PASSED\"" + msgRest;
                break;

                case VMStatus.QUIT:
                    msg += "Result=\"PASSED\"" + msgRest;
                    quit = true;
                break;

                case VMStatus.INITIALIZATION:
                case VMStatus.ERROR:
                {
                    msg += "Result=\"FAILED\"" + msgRest + "\r\n<ErrorStack>\r\n";

                    while (m_ErrStack.Count > 0)
                    {
                        string err = m_ErrStack.Pop();
                        msg += err + Environment.NewLine;
                    }

                    msg += "</ErrorStack>";
                    
                    if (m_bSaveScreenshotOnError)
                    {
                        string datePart = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second;
                        string tmp = Path.Combine(host.StartupPath, @"data\screenshots");

                        string actionStr = string.Empty;

                        if (null != m_actionError)
                            actionStr = " " + m_actionError.Type.ToString();

                        string path = Path.Combine(tmp, datePart + "_" + Path.GetFileName(m_script.Path) + "_" + actionStr + ".jpg");
                        UtilSys.TakeScreenShot(path);
                        UtilSys.Wait(2000);
              
                        m_host.WriteLog("<ScreenshotOnError path=\"" + path + "\"/>");
                    }

                    DateTime dt = DateTime.Now;
                    
                    if (host.IsAutorun)
                        quit = true;
                }
                break;
            }

            msg += "\r\n</Playback>";
            m_host.WriteLog("</steps>");            
            host.WriteLog(msg);
            
            if (quit)
                host.Exit();
        }
        
        private int GetMatchingIfStatement(int instPointer)
        {
            int ifCount = 0;
            int ip = instPointer;
            ActionBase act = null;
            int maxIP = script.Actions.Count - 1;

            ++ip;

            while (ip < maxIP)
            {
                act = script.Actions[ip];

                if (ifCount > 0)
                {
                    if (act is Actionendif)
                        --ifCount;
                }
                else
                {
                    if (act is Actionendif || act is Actionelse)
                        return ip;
                }

                if (act is Actionif)
                    ++ifCount;

                ++ip;
            }
            return ip;
        }

        private int GetMatchingLoopStatement(int instPointer)
        {
            int Count = 0;
            int ip = instPointer;
            ActionBase act = null;
            int maxIP = script.Actions.Count - 1;

            ++ip;

            while (ip < maxIP)
            {
                act = script.Actions[ip];

                if (act is Actionloop)
                {
                    if (Count > 0)
                        --Count;
                    else
                        return ip;
                }
                else if (act is ActionFlowIterator)
                    ++Count;

                ++ip;
            }
            return ip;
        }

        private int GetClosestLoopStatement(int instPointer)
        {
            int ip = instPointer;
            ActionBase act = null;
            int maxIP = script.Actions.Count - 1;

            ++ip;

            while (ip < maxIP)
            {
                act = script.Actions[ip];

                if (act is Actionloop)
                    return ip;                

                ++ip;
            }
            return ip;
        }

        private bool HandleError(ActionBase action)
        {
            if (null == m_actionError)
                m_actionError = action;

            m_variables.UpdateSystem("$ERROR", "True");

            int tmpInstPtr = m_IP;
            ++tmpInstPtr;

            if (tmpInstPtr < script.Actions.Count) // peek ahead one instruction is it a if statement?
            {
                ActionBase actTmp = m_script.GetAction(tmpInstPtr); ;

                if (actTmp is Actionif)
                {
                    Actionif actIF = (Actionif)actTmp;
                    Debug.Assert(null != actIF);

                    if (actIF.Params[1] == "$ERROR" || actIF.Params[3] == "$ERROR")                    
                        return true; // error is handled (do not let the playback to be stopped)                    
                }
            }

            // unhandled error...
            if (!m_bErrorIsHandled && m_listOnError.Count > 0)
            {
                m_bErrorIsHandled = true;

                int pos = m_script.Actions.IndexOf(action) + 1;

                foreach (string funcName in m_listOnError)
                {
                    if (m_FnNameStack.Count > 0)
                    {
                        string fn = m_FnNameStack.Peek();

                        if (fn == funcName) // do not insert calls inside our function
                            continue;
                    }

                    Actioncall call = new Actioncall();
                    call.FunctionName = funcName;
                                        
                    m_script.Actions.Insert(pos, call);
                    ++pos;
                }

                ActionError err = new ActionError();

                m_script.Actions.Insert(pos, err);
                                
                return true; // show must go on
            }
            else
            {
                m_Status = VMStatus.ERROR;
                return false; // we stop the playback
            }
        }

        private void HandleQuit(ActionBase action)
        {
            if (m_bQuitIsHandled)
                return;

            if (0 == m_listOnQuit.Count)
                return;

            m_bQuitIsHandled = true;

            int pos = m_script.Actions.IndexOf(action) + 1; // insert before

            foreach (string funcName in m_listOnQuit)
            {
                if (m_FnNameStack.Count > 0)
                {
                    string fn = m_FnNameStack.Peek();

                    if (fn == funcName) // do not insert calls inside our function
                        continue;
                }

                Actioncall call = new Actioncall();
                call.FunctionName = funcName;

                m_script.Actions.Insert(pos, call);
                ++pos;
            }

            Actionsys actSys = new Actionsys();
            actSys.AddParams(new List<string>() { "sys.quit" });
            m_script.Actions.Insert(pos, actSys);

            m_Status = VMStatus.IN_PLAYBACK;            
        }

        public void Run()
        {
            m_ScreenshotCounter = 0;

            SaveScreenshotOnPlaybackBegin();

            ActionBase action = null;
            int ip = 0;

            m_host.WriteLog("<steps>");

            while (VMStatus.IN_PLAYBACK == m_Status)
            {
                ip = m_IP;

                action = m_script.GetAction(m_IP);

                Step(action);

                if (action is ActionFlow)   // if, for, next, loop, foreach, return, function, call, expression, else, label, goto, endif etc.
                    StepFlow(action);

                switch (action.Result)
                {
                    case EnumActionResult.OK:
                        m_variables.UpdateSystem("$ERROR", "False");
                    break;

                    case EnumActionResult.ERROR:
                    case EnumActionResult.TIMEOUT:
                    case EnumActionResult.TEST_FAILED:
                    {
                        if (!HandleError(action)) // unhandled error will result is playback stop
                        {
                            SaveScreenshotOnPlaybackEnd();
                            return;
                        }
                    }
                    break;        

                    case EnumActionResult.STOPPED:
                        m_Status = VMStatus.STOPPED;
                        SaveScreenshotOnPlaybackEnd();
                        return;                    
                }

                if (VMStatus.QUIT == m_Status)
                    HandleQuit(action);

                if (ip == m_IP) // the IP is not set artifically (by a flow command)
                    ++m_IP; // go to the next command

                if (IsFinished())
                {
                    SaveScreenshotOnPlaybackEnd();
                    return;
                }
                else
                    UtilSys.Wait(m_iSpeed);                     
            }
            SaveScreenshotOnPlaybackEnd();
        }

        private void SaveScreenshotOnPlaybackBegin()
        {
            string pathScreenshot = Path.Combine(host.StartupPath, @"data\screenshots\" + Path.GetFileNameWithoutExtension(m_script.Path) + "-Begin" + ".jpg");
            UtilSys.TakeScreenShotJPG(pathScreenshot);
            UtilSys.Wait(1000);
        }

        private void SaveScreenshotOnPlaybackEnd()
        {
            string pathScreenshot = Path.Combine(host.StartupPath, @"data\screenshots\" + Path.GetFileNameWithoutExtension(m_script.Path) + "-End" + ".jpg");
            UtilSys.TakeScreenShotJPG(pathScreenshot);
            UtilSys.Wait(1000);
        }

        private int StepFlow(ActionBase action)
        {
// function
            if (action is Actionfunction)
                return (++m_IP);

// return
            if (action is Actionreturn)
            {
                m_IP = m_InstrStack.Pop();
                m_CallStack.Pop();
                m_variables.RemoveLocals(m_FnNameStack.Pop());

                return (++m_IP);
            }

// call
            if (action is Actioncall)
            {                
                Actioncall call = (Actioncall)action;
                Debug.Assert(null != call);

                m_InstrStack.Push(m_IP);
                m_IP = m_script.GetFuncEntryIndex(call.FunctionName);
                m_CallStack.Push(m_IP);

                if (-1 == m_IP)
                {
                    string errMsg = "Function: '" + call.FunctionName + "' NOT found.";

                    m_Status = VMStatus.ERROR;

                    m_ErrStack.Push(errMsg);
                    host.MarkCurrentLine(m_IP, action.LineNumber, Color.Red, action.Path2Script);
                    action.Result = EnumActionResult.ERROR;

                    throw new Exception(errMsg);
                }

                Actionfunction fn = (Actionfunction)m_script.GetAction(m_IP);
                Debug.Assert(null != fn);

                Dictionary<string, string> vars = new Dictionary<string, string>();                

                if (call.Parameters.Count > 0)
                {
                    for (int i = 0; i < call.Parameters.Count; ++i)
                    {
                        string key = fn.Parameters[i];
                        string value = m_variables.Get(call.Parameters[i]);

                        vars.Add(key, value);
                    }
                }

                m_variables.AddLocals(fn.Name, vars); // create a new scope as well                

                m_FnNameStack.Push(fn.Name);

                return m_IP;
            }

// Label
            if (action is ActionLabel)
                return (++m_IP);

// goto
            if (action is ActionGoto)
            {
                ActionGoto actGoto = (ActionGoto)action;

                int jump2Index = -1;
                int i = 0;

                foreach(ActionBase item in m_script.Actions) // locate position where to goto
                {
                    if (item is ActionLabel)
                    {
                        ActionLabel actLabel = (ActionLabel)item;

                        if(actLabel.GetLabel == actGoto.label)
                        {
                            jump2Index = i;
                            break;
                        }
                    }
                    ++i;
                }

                if (-1 == jump2Index)
                    throw new Exception("Error locating label '" + actGoto.label + "' for GOTO command.");

                if ("0" != actGoto.jumpCount) // not a FOR-EVER kind of goto
                {
                    if (!m_dicGotoCounts.ContainsKey(actGoto.label))
                        m_dicGotoCounts.Add(actGoto.label, Convert.ToInt32(actGoto.jumpCount)); // init                

                    if( m_dicGotoCounts[actGoto.label] > 0)
                        --m_dicGotoCounts[actGoto.label];
                    else
                    {
                        m_dicGotoCounts.Remove(actGoto.label);
                        m_script.Actions.IndexOf(actGoto);
                        return m_IP;
                    }
                }
                m_IP = jump2Index;

                return m_IP;
            }

// foreach
// for
// while
            if (action is ActionFlowIterator)
            {                
                ActionFlowIterator act = (ActionFlowIterator)action;

                Debug.Assert(null != act);

                if (0 == m_LoopStack.Count)
                {
                    m_LoopStack.Push(m_IP);
                }
                else if (m_LoopStack.Peek() != m_IP)
                {
                    m_LoopStack.Push(m_IP);
                }
                
                if (act.IsDone)
                {
                    m_LoopStack.Pop();
                    m_IP = GetMatchingLoopStatement(m_IP);                    
                }
                
                return (++m_IP);                
            }

// break
            if (action is Actionbreak)
            {                
                int ip = m_LoopStack.Pop();

                ActionFlowIterator act = (ActionFlowIterator)m_script.Actions[ip];

                act.Done(); // probably it will cleanup itself from the vm

                m_IP = GetClosestLoopStatement(m_IP);

                return (++m_IP);
            }

// loop
// continue
            if (action is Actionloop || action is Actioncontinue)
            {
                m_IP = m_LoopStack.Peek();
                return m_IP;                
            }            

// if
            if (action is Actionif)
            {
                Actionif actIF = (Actionif)action;
                m_IfStack.Push(m_IP);
                
                if (actIF.result == true)
                {
                    return (++m_IP); // go inside if block
                }
                else // bypass if block (find else or endif statement (matching! nested if satetement are skipped as well)
                {
                    m_IP = GetMatchingIfStatement(m_IP);
                    return m_IP;
                }                
            }

// endif
            if (action is Actionendif)
            {
                m_IfStack.Pop();                
                return (++m_IP);
            }

// else
            if (action is Actionelse)
            {
                int index = m_IfStack.Peek();
                Actionif actIF = (Actionif)script.Actions[index];
                
                if (actIF.result == true) // then skip the else part
                {
                    m_IP = GetMatchingIfStatement(m_IP);
                    return m_IP;
                }
                else
                    return (++m_IP);
            }

            // unknown command
            throw new Exception("Player::StepFlow Unknown FlowCommand [" + action.RawCmdLine.Trim() + "]");
        }
        
        private void RecordPlayback()
        {
            string path = Path.Combine( Path.Combine(host.StartupPath, "data"), "screenshots");
            string fileName = Path.GetFileName(m_script.Path);                                   
            string pathScreenshot = Path.Combine(host.StartupPath, @"data\screenshots\" + Path.GetFileNameWithoutExtension(m_script.Path) + "_" + string.Format("{0:D3}", m_ScreenshotCounter)  + ".jpg");
            
            UtilSys.TakeScreenShotJPG(pathScreenshot);

            ++m_ScreenshotCounter;
        }
        
        private void Step(ActionBase action)
        {
            Debug.Assert(null != action);

            try
            {
                int ip = m_IP;

                host.SetTitle(action.RawCmdLine.Trim());
                host.MarkCurrentLine(m_IP, action.LineNumber, Color.MediumTurquoise, action.Path2Script);
                
                if (host.RecordPlayback)                
                    RecordPlayback();


//                m_ActionErrorMessage = string.Empty;
                
                action.Result = action.Execute();

//                Pilot.MainForm.ScriptViewUpdateResult(ip, action.StatusString);
                
/*
                if (action is Actionregister)
                    return;

                if (action is Actioncall)
                    return;

                foreach (string funcName in m_listAfterEachCommand)
                {
                    string fn = m_FnNameStack.Peek();

                    if (fn == funcName) // do not insert calls inside our function
                        continue;

                    Actioncall call = new Actioncall();
                    call.Name = funcName;
                    int pos = m_script.Actions.IndexOf(m_action);
                    m_script.Actions.Insert(pos + 1, call);

                    Pilot.MainForm.ShowScriptInGrid();
                }
*/
                if (EnumActionResult.ERROR == action.Result)
                    host.UpdateResult(m_IP, action.Result.ToString());
            }
            catch (Exception ex)
            {
                m_Status = VMStatus.ERROR;
                m_ErrStack.Push(ex.Message);
                host.MarkCurrentLine(m_IP, action.LineNumber, Color.Red, action.Path2Script);
                action.Result = EnumActionResult.ERROR;
                throw;                  
            }
        }        

        private bool IsFinished()
        {
            if (VMStatus.IN_PLAYBACK == m_Status)
            {
                if (m_IP > script.Actions.Count - 1)
                {
                    m_Status = VMStatus.END;
                    return true;
                }
            }
            return false;
        }

    }
}