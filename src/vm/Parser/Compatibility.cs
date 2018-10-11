using System;
using System.Collections.Generic;
using System.Text;
using Util;


namespace Parser
{
    static class Compatibility
    {
        public static List<string> CreateTokens(List<string> pTokens)
        {
            List<string> tokens = new List<string>();

            foreach (string item in pTokens)
            {
                tokens.Add(item);
            }

            try
            {
                for (int i = 0; i < tokens.Count; ++i)
                {
                    if ("MouseClickOn" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "mouse.ClickOn");
                        return tokens;
                    }

                    if ("MouseDoubleClickOn" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "mouse.DoubleClickOn");
                        return tokens;
                    }

                    if ("MouseRightClickOn" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "mouse.RightClickOn");
                        return tokens;
                    }

                    if ("MouseClickClient" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "mouse.ClickClient");
                        return tokens;
                    }

                    if ("MouseClickScreen" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "mouse.ClickScreen");
                        return tokens;
                    }

                    if ("MouseDoubleClickClient" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "mouse.DoubleClickClient");
                        return tokens;
                    }

                    if ("MouseDoubleClickScreen" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "mouse.DoubleClickScreen");
                        return tokens;
                    }

                    if ("Function" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "function");
                        return tokens;
                    }

                    if ("Loop" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "loop");
                        return tokens;
                    }

                    if ("next" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "loop");
                        return tokens;
                    }
                    
                    if ("Return" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "return");
                        return tokens;
                    }

                    if ("Call" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "call");
                        return tokens;
                    }

                    if ("For" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "for");
                        return tokens;
                    }

                    if ("Start" == tokens[0]) // convert old standalone Start command into sys command
                    {
                        tokens.Clear();
                        tokens.Add("sys.start");
                        return tokens;
                    }

                    if ("Log" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "log.write");
                        return tokens;
                    }

                    if ("Log2File" == tokens[0])
                    {
                        tokens.RemoveAt(0);
                        tokens.Insert(0, "log.path");
                        return tokens;
                    }

                    if ("Pilot" == tokens[0]) // convert old Pilot commands into sys commands
                    {
                        if ("Quit" == tokens[1])
                        {
                            tokens.Clear();
                            tokens.Add("sys.quit");
                            return tokens;
                        }

                        if ("Speed" == tokens[1])
                        {
                            string speed = tokens[3];

                            tokens.Clear();
                            tokens.Add("sys.speed");
                            tokens.Add("=");
                            tokens.Add(speed);
                            return tokens;
                        }

                        if ("Stop" == tokens[1])
                        {
                            tokens.Clear();
                            tokens.Add("sys.stop");
                            return tokens;
                        }

                        if ("Wait" == tokens[1])
                        {
                            string wait = tokens[3];

                            tokens.Clear();
                            tokens.Add("sys.wait");
                            tokens.Add("=");
                            tokens.Add(wait);
                            return tokens;
                        }

                        if ("Min" == tokens[1])
                        {
                            tokens.Clear();
                            tokens.Add("sys.min");                                                        
                            return tokens;
                        }

                        if ("Norm" == tokens[1])
                        {
                            tokens.Clear();
                            tokens.Add("sys.norm");                                                        
                            return tokens;
                        }

                        if ("Compact" == tokens[1])
                        {
                            tokens.Clear();
                            tokens.Add("sys.compact");
                            return tokens;
                        }

                        if ("UnCompact" == tokens[1])
                        {
                            tokens.Clear();
                            tokens.Add("sys.uncompact");
                            return tokens;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UtilSys.MessageBoxError(ex.Message);
                throw;
            }
            return tokens;
        }
        

    }
}
