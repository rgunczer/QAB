using System;
using System.Collections.Generic;
using System.Text;
using Util;


namespace VM
{
    class Actionif : ActionFlow
    {
        public bool result = true;

        public override EnumActionResult Execute()
        {
            if (m_params.Count > 4)
            {
                string msg = "Error in: " + m_path + Environment.NewLine +
                             "line: " + m_LineNumber.ToString() + Environment.NewLine +
                             "if statement -> Unsupported number of parameters [" + m_params.Count.ToString() + "]" + Environment.NewLine + Environment.NewLine +                    
                             "Format supported: if [leftSide] [Operator] [RightSide] eg.: if 1 > 0";

                throw new Exception(msg);
            }

            string left = _vm.variables.Get(m_params[1]);
            string op = m_params[2];
            string right = _vm.variables.Get(m_params[3]);
            
            switch (op)
            {
                case "==":
                    if (left == right)
                        result = true;
                    else
                        result = false;

                break;

                case "!=":
                    if (left != right)
                        result = true;
                    else
                        result = false;
                break;

                case "<":                                    
                case ">":
                case ">=":
                case "<=":
                {
                    // try to convert operators to numbers
                    double leftNumber, rightNumber;

                    if ( double.TryParse(left, out leftNumber) && double.TryParse(right, out rightNumber) )
                    {
                        switch (op)
                        {
                            case ">":
                                result = leftNumber > rightNumber ? true : false;
                            break;

                            case "<":
                                result = leftNumber < rightNumber ? true : false;
                            break;

                            case "<=":
                                result = leftNumber <= rightNumber ? true : false;
                            break;

                            case ">=":
                                result = leftNumber >= rightNumber ? true : false;
                            break;
                        }
                        return EnumActionResult.OK;
                    }

                    // try to convert operators to datetime values
                    DateTime leftDateTime, rightDateTime;

                    if ( DateTime.TryParse(left, out leftDateTime) && DateTime.TryParse(right, out rightDateTime) )
                    {
                        switch (op)
                        {
                            case ">":
                                result = leftDateTime > rightDateTime ? true : false;
                            break;

                            case "<":
                                result = leftDateTime < rightDateTime ? true : false;
                            break;

                            case "<=":
                                result = leftDateTime <= rightDateTime ? true : false;
                            break;

                            case ">=":
                                result = leftDateTime >= rightDateTime ? true : false;
                            break;
                        }
                        return EnumActionResult.OK;
                    }

                    throw new Exception("if statement -> Operators cannot be resolved to number or to date.");
                }
                //break;

                default:
                    throw new Exception("if statement -> Unknow operator: " + op);                
            }
            
            return (EnumActionResult.OK);
        }
    }
}
