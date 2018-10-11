using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Util;
using VM;


namespace Parser
{
    public static class ScriptParser
    {
        private static VirtualMachine _vm = null;
        public static VirtualMachine vm
        {
            set { _vm = value; }
        }

        private const string _ImportBeginMark = @"// <<< import BEGIN - file ";
        private const string _ImportEndMark = @"// <<< import END - file ";

        private static Script _script = null;
        private static List<string> _lines = null;
        private static List<string> _tokens = null;

        private static string _raw = string.Empty;
        private static string _comment = string.Empty;

        public static bool _bWriteLog = false;

        private static int _lineNum = 0;

        public static List<string> Lines
        {
            get { return _lines; }
        }

        private static Stack<string> _LoadedFiles = new Stack<string>();
        private static Stack<int> _LineNumbers = new Stack<int>();


        private static ActionBase CreateActionObject(string type)
        {
            if (_bWriteLog)
                _vm.host.WriteLog("ScriptParser::CreateActionObject type = '" + type + "'");

            try
            {
                Type actionType = Type.GetType("VM.Action" + type);

                if (null == actionType)
                    throw new Exception("Error parsing:\r\n" + _script.Path + "\r\nParser->CreateActionObject(Unknown Action type [" + type + "])");                

                ActionBase act = (ActionBase)System.Activator.CreateInstance(actionType);

                act.Comment = _comment;
                act.RawCmdLine = _raw;

                return (act);
            }
            catch
            {
                throw;                
            }
        }

        public static void Load(string path, Script script)
        {
            Debug.Assert(null != path);
            Debug.Assert(null != script);
            
            _script = script;
            _tokens = new List<string>();
            
            if (_bWriteLog)
                _vm.host.WriteLog("\r\n<Parser>");

            try
            {             
                _lines = UtilIO.ReadFile(path);

                if (_bWriteLog)
                    _vm.host.WriteLog("Parser Load File: '" + path + "', Lines: " + _lines.Count);

                _LoadedFiles.Push(path);
                _LineNumbers.Push(0);
                
                ProcessLines();
            }
            catch
            {                
                throw;
            }

            if (_bWriteLog)
            {
                DumpFinalScriptToLogFile();
                _vm.host.WriteLog("</Parser>");
            }            
        }

        private static string InlineBlockComment(string line)
        {
            int posBlockBegin = line.IndexOf(@"/*");
            int posBlockEnd = line.IndexOf(@"*/");

            if (-1 != posBlockBegin && -1 != posBlockEnd)
                return line.Substring(0, posBlockBegin) + line.Substring(posBlockEnd+2, line.Length - (posBlockEnd+2));

            return line;
        }

        private static void ProcessLines()
        {
            string line = string.Empty;
            bool blockComment = false;

            _lineNum = -1;

            for (int i = 0; i < _lines.Count; ++i)
            {
                ++_lineNum;
             
                _raw = _lines[i];
                line = _lines[i].Trim();

                if (!blockComment)
                    line = InlineBlockComment(line);

                if (blockComment && line.StartsWith(@"*/"))
                {
                    blockComment = false;
                    continue;
                }

                if (blockComment)
                    continue;

                if (!blockComment && line.StartsWith(@"/*"))
                {
                    blockComment = true;
                    continue;
                }

                if (line.Contains(_ImportEndMark))
                {
                    _script.GoBackToGlobalScope();
                    _LoadedFiles.Pop();
                    _lineNum = _LineNumbers.Pop();
                }

                if (UtilIO.SkipLine(line))
                {
                    if (_bWriteLog)
                        _vm.host.WriteLog("Skipped\t" + line);

                    continue;
                }

                if (_bWriteLog)
                    _vm.host.WriteLog("Parsing\t" + line);

                List<string> rawTokens = Tokenizer.Init(line);

                SeparateComment(rawTokens);
                _tokens = Compatibility.CreateTokens(_tokens); // compatibility layer         

                ProcessTokens(_lineNum);                
            }
        }

        private static void DumpFinalScriptToLogFile()
        {            
            Debug.Assert(null != _lines);

            const string sep = "---------------------------------------";

            _vm.host.WriteLog(" ");
            _vm.host.WriteLog(" ");
            _vm.host.WriteLog(sep);
            _vm.host.WriteLog(">>> Final Loaded Script");
            _vm.host.WriteLog(" ");

            foreach(string line in _lines)
            {
                _vm.host.WriteLog(line);
            }

            _vm.host.WriteLog(sep);
            _vm.host.WriteLog(" ");
            _vm.host.WriteLog(" ");
        }

        private static void SeparateComment(List<string> rawTokens)
        {
            _tokens.Clear();

            _comment = string.Empty;

            bool bComment = false;

            foreach (string token in rawTokens)
            {
                //_vm.host.WriteLog("RAW TOKEN: [" + token + "]");

                if (bComment == false)
                {
                    if (token == @"//")
                        bComment = true;

                    if (token.Length > 1)
                    {
                        if (token[0] == '/' && token[1] == '/')
                            bComment = true;
                    }
                }

                if (!bComment)
                    _tokens.Add(token);
                else
                    _comment += token + " ";
            }
        }

        private static string GetActionType()
        {
            Debug.Assert(_tokens.Count >= 0);

            string type = _tokens[0];

            if (_tokens[0].Contains("."))
            {
                string[] buf = _tokens[0].Split('.');
                type = buf[0];
                return type;
            }

            if ('@' == type[0] && ( type.EndsWith("++") || type.EndsWith("--") )) 
            {
                type = "increment";
                return type;
            }

            if ('@' == type[0] && "=" == _tokens[1])
            {
                type = "expression";
                return type;
            }

            return type;
        }

        private static void ProcessTokens(int lineNumber)
        {
            try
            {                
                Debug.Assert(_tokens.Count > 0);

                string actionType = GetActionType();
                
                switch(actionType)
                {
                    case "#import":                    
                        LoadImportScript(_tokens[1]);                    
                    return;

                    case "const":
                    case "var":
                        VariableParser.Parse(lineNumber, _LoadedFiles.Peek(), _script, _tokens);                        
                    return;
                }

                ActionBase act = CreateActionObject(actionType);            

                switch (actionType)
                {
                    case "function":
                    {
                        string name = FunctionParser.Parse((Actionfunction)act, _tokens);

                        if (!_script.AddFunction(name))
                        {
                            string msg = "Function [" + name + "] already exists." + Environment.NewLine + "Unable to load script.";
                            throw new Exception(msg);
                        }
                    }
                    break;

                    case "call":
                        CallParser.Parse((Actioncall)act, _tokens);
                    break;

                    case "for":
                        ParseForLoop((Actionfor)act, _tokens);
                    break;

                    default:
                        act.AddParams(_tokens);
                    break;
                }
                act.LineNumber = lineNumber;
                act.Path2Script = _LoadedFiles.Peek();
                _script.AddAction(act);
            }
            catch
            {
                _vm.ParseError(_LoadedFiles.Pop(), lineNumber);
                throw;
            }
                       
        }

        // here we simply import other script file into our _lines List at the current position 
        private static void LoadImportScript(string path2Import)
        {
            Debug.Assert(null != path2Import);

            _vm.host.WriteLog("Loading Import Script: " + path2Import);

            string tmp = path2Import.Trim();            

            string path = Path.Combine(_vm.host.StartupPath, tmp);

            if (!File.Exists(path))            
                throw new FileNotFoundException("File '" + path + "' does not exist.");            

            try
            {
                List<string> lines = UtilIO.ReadFile(path);

                if (_bWriteLog)
                    _vm.host.WriteLog("Loading Import Script: '" + path + "', Lines: " + lines.Count);

                _LoadedFiles.Push(path);
                _LineNumbers.Push(_lineNum);
                _lineNum = -1;                

                int index = _lines.IndexOf(_raw);
                _lines.RemoveAt(index);

                _lines.Insert(index, _ImportEndMark + "[" + path + "]");                

                for(int i = lines.Count-1; i > -1; --i)
                {
                    _lines.Insert(index, lines[i]);
                }
                _lines.Insert(index, _ImportBeginMark + "[" + path + "]");
            }
            catch
            {                
                throw;
            }
        }
        
        private static bool ParseForLoop(Actionfor act, List<string> tokens)
        {
            if (tokens[0] == "for" && tokens[1] == "(" && tokens[tokens.Count - 1] == ")")
            {
                string[] parts = new string[3];
                int index = 0;

                for (int i = 2; i < tokens.Count-1; ++i)
                {
                    if (tokens[i] != ";")
                        parts[index] += tokens[i];
                    else
                        ++index;
                }                
                return act.Parse(parts[0], parts[1], parts[2]);
            }
            else
            {
                throw new Exception("Error parsing for statement");
            }
        }        

    }
}