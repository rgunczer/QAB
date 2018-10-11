using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Util;


namespace VM
{
    public class VariableManager
    {        
        private VirtualMachine m_vm = null;
        private Script m_script = null;
               
        public const string _GlobalScopeName = "Global";
                
        private Dictionary<string, string> m_system = new Dictionary<string, string>();
        private Dictionary<string, string> m_externals = new Dictionary<string, string>();        
        private Stack<Scope> m_scopes = new Stack<Scope>();

        private Scope m_global = null;
                
        public VariableManager(VirtualMachine vm, Script script)
        {
            m_vm = vm;
            m_script = script;
            Reset();
        }

        public void Reset()
        {            
            m_system.Clear();
            m_scopes.Clear();            
            m_externals.Clear();
           
            m_global = new Scope(_GlobalScopeName);

            m_scopes.Push(m_global);
        }

        public Scope CurrentScope
        {
            get { return m_scopes.Peek(); }
        }

        public List<string> Dump()
        {
            List<string> lst = new List<string>();

            lst.Add("--------------------");
            lst.Add("Externals (from *.dat file):");

            foreach (KeyValuePair<string, string> kv in m_externals)
            {
                lst.Add(kv.Key + " = " + kv.Value);
            }

            lst.Add("--------------------");
            lst.Add("Locals (function params):");
           
            Scope sc = m_scopes.Peek();
            
            if (sc.Name != _GlobalScopeName)
            {
                foreach(Scope scope in m_scopes)
                {
                    lst.Add("Scope: " + scope.Name);

                    lst.Add("Variables: " + scope.Name);
                
                    foreach (KeyValuePair<string, string> kv in scope.Variables)
                    {
                        lst.Add(kv.Key + " = " + kv.Value);
                    }

                    lst.Add("Constants: " + scope.Name);

                    foreach (KeyValuePair<string, string> kv in scope.Consts)
                    {
                        lst.Add(kv.Key + " = " + kv.Value);
                    }
                }
            }
    
            return lst;
        }

        public void AddSystem(string key, string value)
        {
            m_system.Add(key, value);
        }

        public void UpdateSystem(string key, string value)
        {
            if (m_system.ContainsKey(key))
                m_system[key] = value;
            else
                throw new Exception("System Variable [" + key + "] does not exist.");
        }

        public void AddExternal(string key, string value)
        {
            m_externals.Add(key, value);
        }

        public void RemoveExternals()
        {
            m_externals.Clear();
        }

        public void AddLocals(string fnName, Dictionary<string, string> local)
        {
            Scope scope = new Scope(fnName, local);
                        
            for (int i = 1; i < m_vm.script.Scopes.Count; ++i)
            {
                if (m_vm.script.Scopes[i].Name == fnName)
                {
                    foreach (KeyValuePair<string, string> kv in m_vm.script.Scopes[i].Variables)
                    {
                        scope.Variables.Add(kv.Key, kv.Value);
                    }

                    foreach (KeyValuePair<string, string> kv in m_vm.script.Scopes[i].Consts)
                    {
                        scope.Consts.Add(kv.Key, kv.Value);
                    }
                    break;
                }
            }
            m_scopes.Push(scope);
        }

        public void AddLocalVariable(string key, string value)
        {
            Scope scope = m_scopes.Peek();

            if (null == scope)
                return;

            scope.Variables.Add(key, value);
        }

        public void UpdateLocalVariable(string key, string value)
        {
            Scope scope = m_scopes.Peek();

            if (null == scope)
                return;

            if (scope.Variables.ContainsKey(key))
                scope.Variables[key] = value;
            else
                throw new Exception("Local Variable [" + key + "] does not exist.");
        }

        public void RemoveLocalVariable(string key)
        {
            Scope scope = m_scopes.Peek();

            if (null == scope)
                return;

            if (scope.Variables.ContainsKey(key))
                scope.Variables.Remove(key);
            else
                throw new Exception("Local Variable [" + key + "] does not exist.");
        }

        public void RemoveLocals(string fnName)
        {
            Scope scope = m_scopes.Peek();

            if (scope.Name == fnName)
            {
                m_scopes.Pop();
            }
        }

        public void Add(string key, string value)
        {
            Scope scope = m_scopes.Peek();

            if (scope.Variables.ContainsKey(key))
                throw new Exception("Variable [" + key + "] in scope [" + scope.Name + "] already exists.");
            else
                scope.Variables.Add(key, value);            
        }

        public void Remove(string key)
        {
            Scope scope = m_scopes.Peek();

            if (scope.Variables.ContainsKey(key))
                scope.Variables.Remove(key);     
            else
                throw new Exception("Variable [" + key + "] in scope [" + scope.Name + "] does not exists.");
        }

        public void AddConst(string key, string value)
        {
            Scope scope = m_scopes.Peek();

            if (scope.Consts.ContainsKey(key))
                throw new Exception("Const [" + key + "] in scope [" + scope.Name + "] already exists.");
            else
                scope.Consts.Add(key, value);
        }

        public void AddVariableInScope(Scope scope, string key, string value)
        {
            Debug.Assert(null != scope);

            if (scope.Variables.ContainsKey(key))
            {
                string msg = "Variable with name [" + key + "] already exist in [" + scope.Name + "] scope";
                m_vm.host.WriteLog(msg);
                throw new System.Exception(msg);
            }
            else
                scope.Variables.Add(key, value);
        }

        public void RemoveVariableInScope(Scope scope, string key)
        {
            Debug.Assert(null != scope);

            if (scope.Variables.ContainsKey(key))
                scope.Variables.Remove(key);
            else
            {
                string msg = "Variable with name [" + key + "] not found in [" + scope.Name + "] scope";
                m_vm.host.WriteLog(msg);
                throw new System.Exception(msg);
            }
        }

        public void Update(string key, string value)
        {
            Scope scope = m_scopes.Peek();

            Debug.Assert(null != scope);

            if (scope == m_global)
            {
                UpdateVariableInScope(m_global, key, value);
                return;
            }
            else
            {
                if (UpdateVariableInScope(scope, key, value))
                    return;

                UpdateVariableInScope(m_global, key, value);
            }
        }

        public bool UpdateVariableInScope(Scope scope, string key, string value)
        {                        
            // variable
            if (scope.Variables.ContainsKey(key))
            {
                scope.Variables[key] = value;
                return true;
            }

            // array
            if (key.Contains("[") && key.Contains("]"))
            {
                // parse index
                int pos0 = key.IndexOf('[');
                int pos1 = key.IndexOf(']');

                string arrName = key.Substring(0, pos0);
                int index = int.Parse(key.Substring(pos0 + 1, pos1 - pos0 - 1));

                string arrValues = scope.Variables[arrName];
                string[] arr = arrValues.Split('|');

                arr[index] = value;

                string arrNewValues = string.Join("|", arr);

                scope.Variables[arrName] = arrNewValues;

                return true;
            }
            
            // const ?
            if (scope.Consts.ContainsKey(key))
            {
                throw new System.Exception("Constant [" + key + "] cannot be modified.");            
            }

            if (scope == m_global)
                throw new Exception("Update failed:" + Environment.NewLine + "Variable [" + key + "] NOT found.");
            else
                return false;
        }

        public string Get(string input)
        {
            string value = string.Empty;

            if (string.IsNullOrEmpty(input))
                return value;
            
            if (input.EndsWith("++"))
            {
                string varName = input.Substring(0, input.Length - 2);

                string varValue = Get(varName);
                int number;
                
                if (int.TryParse(varValue, out number))
                {
                    ++number;
                    Update(varName, number.ToString());

                    return varValue;
                }
                else
                    return input;
            }

            if (input.EndsWith("--"))
            {
                string varName = input.Substring(0, input.Length - 2);

                string varValue = Get(varName);
                int number;

                if (int.TryParse(varValue, out number))
                {                    
                    --number;
                    Update(varName, number.ToString());

                    return varValue;
                }
                else
                    return input;
            }

            if ( input.StartsWith("sql.fields") )
            {
                string temp = input.Replace("sql.fields", "");
                string str = temp.Substring(1, temp.Length - 2);
                int index = -1;

                if ('@' == str[0])                
                    str = Get(str);

                if (int.TryParse(str, out index))
                {
                    string v = m_vm.host.reader[index].ToString();
                    return v;
                }
                else
                {
                    string v = m_vm.host.reader[str].ToString();
                    return v;
                }
            }

            if (input == "sql.FieldCount")
                return m_vm.host.reader.FieldCount.ToString();

            // evaluate complex expression, do we need that here?
            if (input.Contains(" + ")) 
            {
                string tmp = input.Replace(" + ", "|");
                string[] parts = tmp.Split('|');

                for (int i = 0; i < parts.Length; ++i)
                    value += Get(parts[i]); // call us again ;-)   

                return value;
            }

            // system variable (we prefix system variables with dollar sign or with "sys.")
            if ('$' == input[0] || input.StartsWith("sys.") || input.StartsWith("env.") ) 
            {
                if (m_system.ContainsKey(input))
                    return m_system[input];
                else
                {
                    string msg = "System variable [" + input + "] NOT found.";
                    m_vm.host.WriteLog(msg);
                    throw new Exception(msg);
                }
            }

            // just a string literal
            if ('@' != input[0]) 
                return input;

            if ('@' == input[0] && input.Contains(" "))
                return input;

            // local plain variable (not an array) variable in function
            Scope scope = m_scopes.Peek();

            if (scope.Name == m_vm.Scope)
            {                                
                if (scope.Variables.ContainsKey(input))
                    return scope.Variables[input];

                if (scope.Consts.ContainsKey(input))
                    return scope.Consts[input];                
            }

            // external variables
            if (m_externals.ContainsKey(input))
                return m_externals[input];

            // handle array related requests both local and global
            if (input.Contains("[") && input.Contains("]")) // array
            {
                // parse index
                int pos0 = input.IndexOf('[');
                int pos1 = input.IndexOf(']');

                string tmpIndex = input.Substring(pos0 + 1, pos1 - pos0 - 1);
                string arrName = input.Substring(0, pos0);

                if ('@' == tmpIndex[0]) // the index is another variable
                {
                    tmpIndex = Get(tmpIndex);
                }

                int arrIndex = int.Parse(tmpIndex);

                if (scope.Name == m_vm.Scope)
                {
                    if (null != scope.Variables)
                    {
                        if (scope.Variables.ContainsKey(arrName))
                        {
                            string arrValue = scope.Variables[arrName];
                            string ret = GetArrayValue(arrValue, arrIndex);
                            return ret;
                        }
                    }
                }
                
                if (m_global.Variables.ContainsKey(arrName))
                {
                    string arrValue = m_global.Variables[arrName];
                    return GetArrayValue(arrValue, arrIndex);
                }
            }

            if (m_global.Variables.ContainsKey(input))
                return m_global.Variables[input];

            if (m_global.Consts.ContainsKey(input))
                return m_global.Consts[input];
                        
            throw new Exception("Value [" + input + "] NOT found.");
        }

        private string GetArrayValue(string arrValue, int index)
        {
            string[] tmp = arrValue.Split('|');
            return tmp[index];
        }
    }
}