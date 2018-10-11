using System;
using System.Collections.Generic;
using System.Text;
using Util;
using VM;


namespace VM
{
    public static class Data
    {
        private static VirtualMachine _vm = null;
        private static Dictionary<string, string> items = new Dictionary<string, string>();
        private static List<string> comments = new List<string>();

        
        public static Dictionary<string, string> Vars
        {
            get { return items; }
        }

        public static VirtualMachine vm
        {
            set 
            {
                _vm = value;
            }
        }

        public static List<string> Dump()
        {
            List<string> lst = new List<string>();
            int i = 0;
            foreach (KeyValuePair<string, string> pair in items)  
            {  
                string key = pair.Key;  
                string value = pair.Value;
                string comment = comments[i];

                string tmp = key + " = " + value;
                
                if (comment.Length > 0)
                    tmp += comment;

                lst.Add(tmp);
                ++i;
            }
            return lst;
        }

        public static int GetLines()
        {
            return items.Count;
        }


        private static void ParseScopedDataFile(List<string> lst)
        {
            List<string> scopedList = new List<string>();
            int i;
            string scope = System.Environment.MachineName.ToLower();

            bool bRead = false;
            bool bData = false;

            for (i = 0; i < lst.Count; ++i)
			{
                if (bRead)
                {
                    if (bData)
                    {
                        if ("}" == lst[i].Trim()) // end
                            break;

                        scopedList.Add(lst[i]);
                        continue;
                    }

                    if ("{" == lst[i].Trim()) // begin
                    {
                        bData = true;
                        continue;
                    }
                }
                else
                {
                    if (lst[i].Trim().ToLower() == scope)                    
                        bRead = true;                        
                }
            }
            
            ParseVariables(scopedList);
        }

        public static void Load(string path)
        {
            List<string> lst = UtilIO.ReadFile(path);

            items.Clear();
            comments.Clear();

            if (0 == lst.Count)
                return;

            if ("v1.1" == lst[0])
            {
                ParseScopedDataFile(lst);
                return;
            }

            ParseVariables(lst);
        }

        private static void ParseVariables(List<string> lst)
        {
            string key = string.Empty;
            string value = string.Empty;
            string comment = string.Empty;

            foreach (string line in lst)
            {
                string str = line.Trim();

                //_vm.host.WriteLog(str);
                                
                if (UtilIO.SkipLine(str))
                {
                    //_vm.host.WriteLog("Line '" + str + "' skipped.");
                }
                else
                {
                    //_vm.host.WriteLog("Parsing line: '" + str + "'");

                    // look 4 comment
                    int index = line.LastIndexOf(@"//");

                    string[] result = new string[2];

                    if (-1 == index) // no comment
                    {
                        result[0] = line;
                        result[1] = "";
                    }
                    else
                    {
                        result[0] = line.Substring(0, index);
                        result[1] = line.Substring(index, line.Length - index);
                    }

                    if (2 == result.Length)
                        comment = result[1].Trim();
                    else
                        comment = "";

                    key = ParseKey(result[0]);
                    value = string.Empty;

                    string tmp = result[0].Replace(key, "");

                    bool write = false;
                    for (int i = 0; i < tmp.Length; ++i)
                    {
                        if (write)
                            value += tmp[i];
                        else
                        {
                            if (tmp[i] == '=')
                                write = true;
                        }
                    }

                    value = value.Trim();
                    value = value.Substring(1, value.Length - 2);

                    items.Add(key, value);
                    comments.Add(comment);                    
                }
            }
        }

        private static string ParseKey(string input)
        {
            int i = 0;
            string key = string.Empty;
            string buf = input.Trim();

            foreach (char c in buf)
            {
                if ('=' != c)
                {
                    key += c;
                    ++i; // count character copy
                }
                else
                {
                    key = key.Trim();
                    return (key);
                }
            }
            return "";
        }

    }
}
