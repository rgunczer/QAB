using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Util;
using Parser;


namespace VM
{
    public class Script
    {
        private int m_index = 0;
        private string m_path = string.Empty;
        private List<ActionBase> m_lstActions = new List<ActionBase>();
        private Dictionary<string, int> m_dicFuncEntryIndex = new Dictionary<string, int>(); 
        private List<Scope> m_scopes = new List<Scope>();
        private int m_iStartIndex = -1;        
        private Scope m_curScope = null;

        public string Path
        {
            get { return m_path; }
        }

        public List<Scope> Scopes
        {
            get { return m_scopes; }
        }

        public int EntryPoint
        {
            get { return m_iStartIndex; }            
        }

        public List<ActionBase> Actions
        {
            get { return m_lstActions; }
        }

        public Script(string path)
        {
            m_path = path;
            CleanUp();
        }

        private void CleanUp()
        {                        
            m_index = 0;
            m_iStartIndex = -1;
            m_lstActions.Clear();
            m_dicFuncEntryIndex.Clear();
        
            m_scopes.Clear();
            m_curScope = new Scope("Global");
            m_scopes.Add(m_curScope); // begin with global scope
        }

        public bool AddFunction(string name)
        {
            Debug.Assert(0 != name.Length);

            if (m_dicFuncEntryIndex.ContainsKey(name))
                return false;
            
            m_dicFuncEntryIndex.Add(name, m_lstActions.Count);
            
            // create new scope for the function
            m_curScope = new Scope(name);
            m_scopes.Add(m_curScope);
            return true;
        }

        public void GoBackToGlobalScope()
        {
            Debug.Assert(0 != m_scopes.Count);
            m_curScope = m_scopes[0];
        }

        public void AddAction(ActionBase act)
        {
            try
            {
                Debug.Assert(null != act);

                if (act is Actionsys)
                {
                    if ("sys.start" == act.Params[0])
                    {
                        if (-1 == m_iStartIndex)
                        {
                            m_iStartIndex = m_lstActions.Count + 1;
                            m_curScope = m_scopes[0]; // go back to global scope
                        }
                        else
                            throw new Exception("Error parsing:" + Environment.NewLine + m_path + Environment.NewLine + "Script->AddAction(More than one [" + act.Params[0] + "] command found)");
                    }
                }
                m_index++;
                m_lstActions.Add(act);
            }
            catch
            {
                throw;
            }
        }
        
        public void AddVariable(string key, string value, int lineNumber, string pathScript)
        {            
            if (m_curScope.Variables.ContainsKey(key))
            {
                Variable v = m_curScope.parsedVariables[key];


                throw new Exception("Error parsing:" + Environment.NewLine + m_path + Environment.NewLine + " Variable [" + key + "] is defined more than once in scope [" + m_curScope.Name + "], Line: " + v.LineNumber + ", in file: " + v.Path);
            }

            m_curScope.Variables.Add(key, value);
            m_curScope.parsedVariables.Add(key, new Variable() { LineNumber = lineNumber, Path = pathScript, Name = key, Value = value });
        }

        public void AddConst(string key, string value, int lineNumber, string pathScript)
        {
            if (m_curScope.Consts.ContainsKey(key))
            {
                Variable v = m_curScope.parsedConstants[key];

                throw new Exception("Error parsing:" + Environment.NewLine + m_path + Environment.NewLine + " Constant [" + key + "] is defined more than once in scope [" + m_curScope.Name + "], Line: " + v.LineNumber + ", in file: " + v.Path);
            }

            m_curScope.Consts.Add(key, value);
            m_curScope.parsedConstants.Add(key, new Variable() { LineNumber = lineNumber, Path = pathScript, Name = key, Value = value });
        }

        public int GetFuncEntryIndex(string name)
        {
            if (m_dicFuncEntryIndex.ContainsKey(name))
                return (m_dicFuncEntryIndex[name]);
            else
                return (-1);
        }

        public ActionBase GetAction(int index)
        {
            if (index >= m_lstActions.Count || index < 0)
                throw new System.Exception("Unable to get Action at Index [" + index + "]." + Environment.NewLine + "Number of Actions: " + m_lstActions.Count);

            return (m_lstActions[index]);
        }

        public List<string> DumpVariables()
        {
            List<string> lst = new List<string>();

            foreach (Scope sc in m_scopes)
            {
                lst.Add("Scope: " + sc.Name); 
                
                foreach (KeyValuePair<string, string> kv in sc.Variables)
                {
                    lst.Add(kv.Key + " = " + kv.Value);
                }
                lst.Add("--------------------");
            }
            return lst;
        }

        public List<string[]> Dump()
        {
            List<string[]> lst = new List<string[]>();

            try
            {
                foreach (ActionBase action in m_lstActions)
                {
                    lst.Add(action.Dump());
                }
                return (lst);
            }
            catch
            {                
                throw;
            }            
        }
    }
}