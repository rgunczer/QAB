using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using VM;


namespace Parser
{
    public static class FunctionParser
    {
        public static string Parse(Actionfunction act, List<string> input)
        {
            //Debug.Print("FunctionParser->Parse (List input items)");            
            //foreach (string item in input)
            //{
            //    Debug.Print(item);
            //}
            //Debug.Print("-----------------------");

            act.Name = input[1];

            // function with parameters
            if (input.Count > 3)
            {
                for (int i = 3; i < input.Count - 1; ++i)
                {
                    if (input[i] != ",")
                        act.AddParam(input[i]);
                }
            }
            return act.Name;
        }
    }
}