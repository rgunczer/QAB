using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Util;


namespace VM
{
    class Actionexpression : ActionBase
    {
        private static Random m_Rand = new Random();

        private string accu = string.Empty;

        private string GetRandomNumberDouble()
        {
            return m_Rand.NextDouble().ToString();
        }

        private string GetRandomNumberInteger(int min, int max)
        {
            return m_Rand.Next(min, max).ToString();
        }
                    
        public string RandomNumber(double min, double max)
        {
            double num = (max - min) * m_Rand.NextDouble() + min;
            return num.ToString();
        }
        
        private string GetRandomFloatInRangeFromParams(string paramLow,  string paramHigh)
        {
            string result = string.Empty;

            string strLow = _vm.variables.Get(paramLow);
            string strHigh = _vm.variables.Get(paramHigh);
            
            double low;
            double high;

            if (double.TryParse(strLow, out low) && double.TryParse(strHigh, out high))
            {
                result = RandomNumber(low, high);                
                return result;
            }
            else
            {
                string msg = "Error parsing Random Parameters";

                _vm.host.WriteLog(msg);
                throw new SystemException(msg);
            }            
        }
        
        public override EnumActionResult Execute()
        {
            accu = string.Empty;

            string varName = m_params[0];
            List<string> buffer = new List<string>();

            StringBuilder sb = new StringBuilder();

            bool IsRandom = false;
            bool IsProcessExists = false;
            bool IsProcessClose = false;
            bool IsLen = false;
            bool IsSubString = false;


            if (5 == m_params.Count) // simple add sub mult or divide?
            {
                if (
                    "=" == m_params[1] &&
                    (
                        "+" == m_params[3] ||
                        "-" == m_params[3] ||
                        "*" == m_params[3] ||
                        "/" == m_params[3]
                    )
                    )
                {
                    string str0 = _vm.variables.Get(m_params[2]);
                    string str1 = _vm.variables.Get(m_params[4]);

                    if (Support.IsDecimal(str0) && Support.IsDecimal(str1)) // two doubles
                    {
                        double op0 = Convert.ToDouble(str0);
                        double op1 = Convert.ToDouble(str1);
                        double result = 0;

                        switch (m_params[3])
                        {
                            case "+":
                                result = op0 + op1;
                            break;

                            case "-":
                                result = op0 - op1;
                            break;

                            case "*":
                                result = op0 * op1;
                            break;

                            case "/":
                                result = op0 / op1;
                            break;
                        }

                        string value = result.ToString();
                        _vm.variables.Update(varName, value);

                        return EnumActionResult.OK;
                    }
                    
                    if (Support.IsInteger(str0) && Support.IsInteger(str1)) // two integers
                    {
                        int op0 = Convert.ToInt32(str0);
                        int op1 = Convert.ToInt32(str1);
                        int result = 0;

                        switch (m_params[3])
                        {
                            case "+":
                                result = op0 + op1;
                                break;

                            case "-":
                                result = op0 - op1;
                                break;

                            case "*":
                                result = op0 * op1;
                                break;

                            case "/":
                                result = op0 / op1;
                                break;
                        }

                        string value = result.ToString();
                        _vm.variables.Update(varName, value);

                        return EnumActionResult.OK;
                    }
                    
                    // double int or int double
                    if ( ( Support.IsDecimal(str0) && Support.IsInteger(str1) ) || ( Support.IsInteger(str0) && Support.IsDecimal(str1) ) )
                    {
                        double op0 = Convert.ToDouble(str0);
                        double op1 = Convert.ToDouble(str1);
                        double result = 0;

                        switch (m_params[3])
                        {
                            case "+":
                                result = op0 + op1;
                                break;

                            case "-":
                                result = op0 - op1;
                                break;

                            case "*":
                                result = op0 * op1;
                                break;

                            case "/":
                                result = op0 / op1;
                                break;
                        }

                        string value = result.ToString();
                        _vm.variables.Update(varName, value);

                        return EnumActionResult.OK;
                    }
                    // at this point the two operands are not numbers
                }
            }

            for (int i = 2; i < m_params.Count; ++i)
            {
                if ("+" == m_params[i])
                    continue;

                if ("Len" == m_params[i])
                    IsLen = true;

                if (IsLen)
                {                                        
                    if (")" == m_params[i])
                    {
                        string value = _vm.variables.Get(m_params[4]);
                        sb.Append(value.Length.ToString());
                        continue;
                    }
                    continue;
                }

                if ("SubString" == m_params[i])
                    IsSubString = true;


                if (IsSubString)
                {
                    buffer.Add(m_params[i]);

                    if (")" == m_params[i]) // closing 
                    {
                        if (6 == buffer.Count) // one with default param (the length)
                        {
                            string sub = string.Empty;

                            string temp = _vm.variables.Get(buffer[2]);
                            int from = Convert.ToInt32(_vm.variables.Get(buffer[4]));

                            sub = temp.Substring(from);

                            sb.Append(sub);
                            continue;
                        }
                        else if (8 == buffer.Count)
                        {
                            string sub = string.Empty;

                            string temp = _vm.variables.Get(buffer[2]);
                            int from = Convert.ToInt32(_vm.variables.Get(buffer[4]));
                            int len = Convert.ToInt32(_vm.variables.Get(buffer[6]));

                            sub = temp.Substring(from, len);

                            sb.Append(sub);

                            continue;
                        }
                        else
                            throw new Exception("Error parsing SubString command");

                        //continue;
                    }
                    continue;
                }


                // ProcessClose("process") 
                if ("ProcessClose" == m_params[i])
                    IsProcessClose = true;

                if (IsProcessClose)
                {
                    buffer.Add(m_params[i]);

                    if (")" == m_params[i])
                    {
                        // return a default random float
                        if (buffer[0] == "ProcessClose" &&
                            buffer[1] == "(" &&
                            //lstRandom[2] == "Skype" &&
                            buffer[3] == ")")
                        {
                            string result = string.Empty;
                            string procName = _vm.variables.Get(buffer[2]);

                            Process[] p;
                            p = Process.GetProcessesByName(procName);

                            if (p.Length == 0)
                            {
                                int procID;

                                if (int.TryParse(procName, out procID))
                                {
                                    Process proc = null;

                                    try
                                    {
                                        proc = Process.GetProcessById(procID);

                                        proc.Kill();

                                        if (proc.WaitForExit(1000 * 60))
                                            result = "1";
                                        else
                                            result = "0";
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        _vm.host.WriteLog("Exception:" + ex.Message);
                                        result = "0";
                                    }

                                    sb.Append(result);
                                    continue;
                                }
                                else
                                {
                                    result = "0";
                                    sb.Append(result);

                                    continue;
                                }
                            }

                            if (p.Length > 0)
                            {
                                List<int> lst = new List<int>();

                                for (int j = 0; j < p.Length; ++j)
                                {
                                    lst.Add(p[j].Id);
                                }

                                lst.Sort();

                                int id = lst[lst.Count - 1];

                                foreach (Process proc in p)
                                {
                                    if (proc.Id == id)
                                    {
                                        proc.Kill();

                                        if (proc.WaitForExit(1000 * 60))
                                        {
                                            result = "1";
                                            break;
                                        }
                                        else
                                            result = "0";
                                    }
                                }
                            }
                            else
                                result = "0";

                            sb.Append(result);
                            continue;
                        }
                        else
                        {
                            string msg = "Error parsing ProcessClose Parameters";

                            _vm.host.WriteLog(msg);
                            throw new SystemException(msg);
                        }
                    }
                    continue;
                }

                // ProcessExists("process")
                if ("ProcessExists" == m_params[i])
                    IsProcessExists = true;

                if (IsProcessExists)
                {
                    buffer.Add(m_params[i]);

                    if (")" == m_params[i])
                    {
                        // return a default random float
                        if (buffer[0] == "ProcessExists" &&
                            buffer[1] == "(" &&
                            //lstRandom[2] == "Skype" &&
                            buffer[3] == ")")
                        {
                            string result = string.Empty;
                            string procName = _vm.variables.Get(buffer[2]);

                            Process[] p = Process.GetProcessesByName(procName);

                            if (p.Length > 0)
                                result = p[0].Id.ToString();
                            else
                                result = "0";

                            sb.Append(result);
                            continue;
                        }
                        else
                        {
                            string msg = "Error parsing ProcessExists Parameters";

                            _vm.host.WriteLog(msg);
                            throw new SystemException(msg);
                        }
                    }
                    continue;
                }

                if ("Random" == m_params[i])
                    IsRandom = true;

                if (IsRandom)
                {
                    buffer.Add(m_params[i]);

                    if (")" == m_params[i]) // random 
                    {
                        // scenarios
                        // Random() -> Random(0, 1, 0) generate a float number 
                        // Random(10) -> Random(10, 1, 0)
                        // Random(10, 20) -> Random(10, 20, 0)
                        // Random(10, 20, 1) -> return an integer

                        IsRandom = false;

                        string result = string.Empty;

                        // return a default random float ?
                        if (buffer[0] == "Random" &&
                            buffer[1] == "(" &&
                            buffer[2] == ")")
                        {
                            result = GetRandomNumberDouble();
                            sb.Append(result);
                            continue;
                        }

                        // return a default random integer ?
                        if (buffer[0] == "Random" &&
                            buffer[1] == "(" &&
                            //buffer[2] == "1" &&
                            buffer[3] == ")")
                        {
                            string intFlag = _vm.variables.Get(buffer[2]);

                            if ("1" == intFlag)
                                result = GetRandomNumberInteger(0, 2);
                            else if ("0" == intFlag)
                                result = GetRandomNumberDouble();
                            else
                            {
                                string msg = "Error parsing Random Parameters";

                                _vm.host.WriteLog(msg);
                                throw new SystemException(msg);                                
                            }

                            sb.Append(result);
                            continue;
                        }

                        // return a float number between range
                        if (buffer[0] == "Random" &&
                            buffer[1] == "(" &&
                            //lstRandom[2] == "-10" &&
                            buffer[3] == "," &&
                            //lstRandom[4] == "10" &&
                            buffer[5] == ")")
                        {
                            string res = GetRandomFloatInRangeFromParams(buffer[2], buffer[4]);
                            sb.Append(res);
                            continue;
                        }

                        // return an int  number between range
                        if (buffer[0] == "Random" &&
                            buffer[1] == "(" &&
                            //lstRandom[2] == "-10" &&
                            buffer[3] == "," &&
                            //lstRandom[4] == "10" &&
                            buffer[5] == "," &&
                            //buffer[6] == "1" &&
                            buffer[7] == ")")
                        {                            
                            string intFlag = _vm.variables.Get(buffer[6]);

                            if ("1" == intFlag)
                            {
                                string strLow = _vm.variables.Get(buffer[2]);
                                string strHigh = _vm.variables.Get(buffer[4]);

                                int low;
                                int high;

                                if (int.TryParse(strLow, out low) && int.TryParse(strHigh, out high))
                                {
                                    result = GetRandomNumberInteger(low, high + 1);
                                    sb.Append(result);
                                    continue;
                                }
                                else
                                {
                                    string msg = "Error parsing Random Parameters";

                                    _vm.host.WriteLog(msg);
                                    throw new SystemException(msg);
                                }
                            }
                            else if ("0" == intFlag)
                            {
                                string res = GetRandomFloatInRangeFromParams(buffer[2], buffer[4]);
                                sb.Append(res);
                                continue;
                            }
                            else
                            {
                                string msg = "Error parsing Random Parameters";

                                _vm.host.WriteLog(msg);
                                throw new SystemException(msg);
                            }
                        }

                        // should never get here
                        continue;
                    }
                    continue;
                }

                string param = m_params[i];
                string tmp = string.Empty;

                if ( "sql.fields[" == param )
                {
                    accu += param;
                    continue;
                }

                if (accu.Length > 0)
                {
                    accu += param;

                    if (param != "]")
                        continue;

                    param = accu;
                    accu = string.Empty;
                }
                   
                tmp = _vm.variables.Get(param);

                sb.Append(tmp);
            }

            string valu = sb.ToString();

            _vm.variables.Update(varName, valu);

            return EnumActionResult.OK;
        }

        public override string ToString()
        {
            string tmp = string.Empty;

            for (int i = 0; i < m_params.Count; ++i)
            {
                tmp += " " + m_params[i];
            }
            return (tmp.Trim());
        }


    }
}