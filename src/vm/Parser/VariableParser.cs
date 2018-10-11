using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using VM;


namespace Parser
{
    public static class VariableParser
    {        
        private static bool _IsConst = false;
        private static string _errorMsg = string.Empty;

        private static bool Validate(List<string> input)
        {
            _errorMsg = string.Empty;

            if (input.Count > 4)
            {
                if ("[" != input[3] || "]" != input[input.Count - 1])
                {
                    _errorMsg = "Invalid array declaration." + Environment.NewLine + "Array declaration should start with '[' and end with ']' character.";
                    return false;
                }
            }
            else
            {
                if (4 != input.Count)
                {
                    _errorMsg = "Incomplete or invalid variable / constant declaration.";
                    return false;
                }
            }

            if (input[0] == "var" || input[0] == "const")
            {
                _IsConst = input[0] == "const" ? true : false;

                if ('@' == input[1][0])
                {
                    if (1 == input[1].Length)
                    {
                        _errorMsg = "Variable name cannot be a single '@' character.";
                        return false;
                    }

                    if ('@' == input[1][0] && '@' == input[1][1])
                    {
                        _errorMsg = "Variable name cannot beging with double '@@' character.";
                        return false;
                    }

                    if ("=" == input[2])
                        return true;
                    else
                    {
                        _errorMsg = "Missing equal sign after variable / constant name.";
                        return false;
                    }
                }
                else
                {
                    _errorMsg = "Variable name must begin with '@' character.";
                    return false;
                }
            }
            return false;
        }

        public static void Parse(int lineNumber, string pathScript, Script script, List<string> input)
        {
            Debug.Assert(null != input);

            if (!Validate(input))
            {
                string line = string.Empty;

                foreach (string item in input)
                {
                    line += item + " ";
                }

                string msg = "Validation failed in script: " + Environment.NewLine + pathScript + Environment.NewLine + Environment.NewLine +
                             "on line [" + lineNumber.ToString() + "]:" + Environment.NewLine + line + Environment.NewLine + Environment.NewLine +
                             _errorMsg + Environment.NewLine + Environment.NewLine +
                             "Valid variable: var @name = \"Jennifer\"" + Environment.NewLine +
                             "Valid constant: const @name = \"Jennifer\"" + Environment.NewLine +
                             "Valid array: var @names = [ \"Jennifer\", \"Lopez\", \"Dexter\" ]";

                throw new Exception(msg);
            }

            string name = input[1];
            string value = input[3];

            // array?
            if (input.Count > 4)
            {
                value = string.Empty;

                for(int i = 4; i < input.Count - 1; ++i)
                {
                    if(input[i] == ",")
                        value += "|";
                    else
                        value += input[i];
                }
            }

            if (_IsConst)
                script.AddConst(name, value, lineNumber, pathScript);
            else            
                script.AddVariable(name, value, lineNumber, pathScript);                            
        }

    }
}