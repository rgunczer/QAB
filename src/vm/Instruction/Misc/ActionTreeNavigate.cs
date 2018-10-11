using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionTreeNavigate : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string tmp = _vm.variables.Get(m_params[1]);
            
            string[] path =  tmp.Split('|');

            AutomationElement treeItem = null;

            foreach(string name in path)
            {
                treeItem = UtilAutomation.FindTreeItem(_vm.host.aeCurrent, name);

                if (null == treeItem)
                {
                    _vm.host.WriteLog("TreeItem '" + name + "' NOT found.");
                    return EnumActionResult.ERROR;
                }
                    
                _vm.host.WriteLog("TreeItem '" + name + "' found.");
                _vm.host.aeCurrent = treeItem;

                if (DoWork("Expand") == false)
                {
                    if (DoWork("Expand") == false)
                        return (EnumActionResult.ERROR);
                }

                Util.UtilSys.Wait(500);
            }            
            return EnumActionResult.OK;
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
