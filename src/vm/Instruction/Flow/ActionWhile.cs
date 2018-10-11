using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class Actionwhile : ActionFlowIterator
    {
        public override EnumActionResult Execute()
        {
            string left = _vm.variables.Get(m_params[2]);
            string op = m_params[3];
            string right = _vm.variables.Get(m_params[4]);

            if (left == "sql.read")
                left = _vm.host.reader.Read() == false ? "False" : "True";

            if (right == "sql.read")
                right = _vm.host.reader.Read() == false ? "False" : "True";            

            switch (op)
            {
                case "==":
                    m_done = left == right ? false : true;
                break;

                case "!=":
                    m_done = left != right ? false : true;
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
                                m_done = leftNumber > rightNumber ? false : true;
                            break;

                            case "<":
                                m_done = leftNumber < rightNumber ? false :true;
                            break;

                            case "<=":
                                m_done = leftNumber <= rightNumber ? false : true;
                            break;

                            case ">=":
                                m_done = leftNumber >= rightNumber ? false : true;
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
                                m_done = leftDateTime > rightDateTime ? false : true;
                            break;

                            case "<":
                                m_done = leftDateTime < rightDateTime ? false : true;
                            break;

                            case "<=":
                                m_done = leftDateTime <= rightDateTime ? false : true;
                            break;

                            case ">=":
                                m_done = leftDateTime >= rightDateTime ? false : true;
                            break;
                        }
                        return EnumActionResult.OK;
                    }

                    throw new Exception("while statement -> Operators cannot be resolved to number or to date.");
                }
                //break;

                default:
                    throw new Exception("while statement -> Unknow operator: " + op);                
            }            
            return EnumActionResult.OK;
        }

    }
}