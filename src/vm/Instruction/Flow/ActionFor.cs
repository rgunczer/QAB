using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class Actionfor : ActionFlowIterator
    {
        private enum Condition
        {
            BIGGER, // >
            SMALLER, // <
            NOT_EQUAL, // !=
            EQUAL, // ==                       
            BIGGER_OR_EQUAL, // >=
            SMALLER_OR_EQUAL, // <=            
        };
        

        int start;
        int stop;
        int step;
        int cur;
         
        private bool grow;

        Condition m_condition;
                
        bool startIsVariable = false;
        bool endIsVariable = false;

        string m_varNameStart = string.Empty;
        string m_varNameEnd = string.Empty;

        private string m_start;
        private string m_stop;
        private string m_step;


        public int Value
        {
            get { return (cur); }
        }

        
        private void Run()
        {
            cur += step;
            _vm.variables.Update(m_varName, cur.ToString());

            TestEnd();
        }

        private void TestEnd()
        {
            if (endIsVariable)
                stop = Convert.ToInt32(_vm.variables.Get(m_varNameEnd));

            switch (m_condition)
            {
                case Condition.BIGGER:
                    if (cur <= stop)
                        m_done = true;
                break;

                case Condition.SMALLER:
                    if (cur >= stop)
                        m_done = true;
                break;

                case Condition.BIGGER_OR_EQUAL:                
                case Condition.SMALLER_OR_EQUAL:                
                case Condition.NOT_EQUAL:                
                case Condition.EQUAL:
                    throw new Exception("Unsupported Condition [" + m_condition.ToString() + "] ");
            }

            if (m_done)
                Done();
        }
       
        private void Init()
        {
            m_done = false;

            if (startIsVariable)
                start = Convert.ToInt32(_vm.variables.Get(m_varNameStart));

            cur = start;

            _vm.variables.Add(m_varName, cur.ToString());

            m_initialized = true;
        }

        public override EnumActionResult Execute()
        {
            if (!m_initialized)
            {
                Init();
                TestEnd();
            }
            else
                Run();            
                                    
            return (EnumActionResult.OK);
        }

        public override string ToString()
        {
            return ("(" + m_start + "; " + m_stop + "; " + m_step + ")");
        }

        public bool Parse(string start, string stop, string step)
        {           
            m_varName = string.Empty;

            m_start = start;
            m_stop = stop;
            m_step = step;

            if (!ParseStart(start))
                return (false);

            if (!ParseStop(stop))
                return (false);

            if (!ParseStep(step))
                return (false);
                    
            return (true);
        }

        private bool ParseStart(string input)
        {
            string[] s = input.Split('=');

            if (2 != s.Length)
                return (false);

            m_varName = s[0];
            string tmp = s[1];

            if ('@' == tmp[0])
            {
                start = 0;
                startIsVariable = true;
                m_varNameStart = tmp;
            }
            else
                start = Convert.ToInt32(s[1]); // init

            return (true);
        }

        private bool ParseStop(string input)
        {
            string[] s = null;

            if (input.Contains("<"))
            {
                m_condition = Condition.SMALLER;
                grow = true;
                s = input.Split('<');
            }
            else
            {
                m_condition = Condition.BIGGER;
                s = input.Split('>');
                grow = false;
            }

            if (2 != s.Length)
                return (false);
            
            if (m_varName != s[0])
                return (false);


            string tmp = s[1];

            if ('@' == tmp[0] || "sql.FieldCount" == tmp)
            {                
                endIsVariable = true;
                m_varNameEnd = tmp;
            }
            else
                stop = Convert.ToInt32(s[1]); // condition
             
            return (true);
        }

        private bool ParseStep(string input)
        {
            if (!input.Contains(m_varName))
                return (false);

            string op = input.Replace(m_varName, "");

            // possible values
            // i++
            // i--
            // i+=2 // not supported, yet!
            // i-=2 // not supported, yet!

            if (op.Contains("+="))
            {
                string tmp = op.Replace("+=", "");
                step = Convert.ToInt32(tmp);
                return (true);
            }

            if (op.Contains("-="))
            {
                string tmp = op.Replace("-=", "");
                step = -Convert.ToInt32(tmp);
                return (true);
            }

            switch (op)
            {
                case "++":                    
                    step = 1;
                break;

                case "--":                    
                    step = -1;
                break;

                case "+=":
                case "-=":
                case "*=":
                case @"/=":
                    throw new Exception("Unsupported [" + op + "] operator");

                default:
                    return (false);                
            }
            return (true);
        }
    }
}
