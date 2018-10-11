using System;
using System.Collections.Generic;
using System.Text;
using VM;


namespace Parser
{
    public static class CallParser
    {        
        // VALID TOKEN FORMAT
        // token[0] = "call"
        // token[1] = "function_name"
        // token[2] = "("
        // token[LAST] = ")"        
        private static bool ValidateTokens(List<string> input)
        {
            if (input.Count < 4) // min token count, anything less is no good for us
                return false;

            if (input[0] == "call" || input[0] == "Call")
            {
                if (input[2] == "(")
                {
                    if (input[input.Count - 1] == ")")
                        return true; // valid function call                    
                }
            }
            return false;
        }

        public static void Parse(Actioncall act, List<string> input)
        {
            if (!ValidateTokens(input))
            {
                string line = string.Empty;

                foreach (string item in input)
                {
                    line += item + " ";
                }
                throw new Exception("CallParser->Parse:: Invalid function call: '" + line + "'");
            }
            
            act.FunctionName = input[1];

            if (input.Count == 4) // function call has no parameters  4 token "Call", "NameOfTheFunc", "(", ")"
                return;
            
            string param = string.Empty;
            bool bComplexExp = false;

            for (int i = 3; i < input.Count; ++i)
            {
                if ("+" == input[i])
                {
                    bComplexExp = true;
                    break;
                }
            }

            if (bComplexExp)
            {
                string tmp = string.Empty;

                for (int i = 3; i < input.Count; ++i)
                {
                    if (input[i] == "," || input[i] == ")") // end of argument
                    {
                        act.Parameters.Add(tmp);
                        tmp = string.Empty;
                    }
                    else
                    {
                        if ("+" == input[i])
                            tmp += " + ";
                        else
                            tmp += input[i];
                    }
                }
            }
            else
            {
                for (int i = 3; i < input.Count; ++i)
                {
                    if (input[i] != "," && input[i] != ")")
                        act.Parameters.Add(input[i]);
                }
            }
        }
    }
}
