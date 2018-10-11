using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class Actionregister : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string type = m_params[0];

            switch (type)
            {
                case "register.AfterEachCommand":
                    throw new Exception(type + " is not supported!");
                //break;

                case "register.AfterCommands":
                    throw new Exception(type + " is not supported!");
                //break;

                case "register.OnError":
                {
                    switch (m_params[1])
                    {
                        case "+=":
                            _vm.AddOnError(m_params[2]);
                        break;

                        case "-=":
                            _vm.RemoveOnError(m_params[2]);
                        break;

                        default:
                            throw new Exception("Unrecognized operator [" + m_params[1] + "]");
                        //break;
                    }
                }
                break;

                case "register.OnQuit":
                {
                    switch (m_params[1])
                    {
                        case "+=":
                            _vm.AddOnQuit(m_params[2]);
                        break;

                        case "-=":
                            _vm.RemoveOnQuit(m_params[2]);
                        break;

                        default:
                            throw new Exception("Unrecognized operator [" + m_params[1] + "]");
                        //break;
                    }
                }
                break;

                default:
                    throw new Exception("Unrecognized command type [" + type + "]");            
            }
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
