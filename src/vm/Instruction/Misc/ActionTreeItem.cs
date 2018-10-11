using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionTreeItem : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string cmd = _vm.variables.Get(m_params[1]);

            if (DoWork(cmd) == false)
            {
                if (DoWork(cmd) == false)
                    return (EnumActionResult.ERROR);
            }
            return (EnumActionResult.OK);
        }

        private bool DoWork(string cmd)
        {
            switch (cmd)
            {
                case "Expand":
                {
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            UtilAutomation.Expand(_vm.host.aeCurrent);
                            return (true);
                        }
                        catch (System.InvalidOperationException)
                        {
                            return (true);    
                        }
                        catch (System.Exception ex)
                        {
                            _vm.host.WriteLog(ex.Message);                                                                                                                                                
                            UtilSys.Wait(1000);
                        }
                    }                    
                }
                return (false);                

                case "Collapse":
                    UtilAutomation.Collapse(_vm.host.aeCurrent);
                break;

                case "Select":
                    AutomationPattern.Select(_vm.host.aeCurrent);
                break;

                default:
                    _vm.host.WriteLog("Unrecognized TreeItem Command: '" + cmd + "'");
                    return (false);                
            }
            return (true);
        }
    }
}
