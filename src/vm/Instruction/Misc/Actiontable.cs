using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    public class Actiontable : ActionBase
    {
        public override EnumActionResult Execute()
        {
            AutomationElement aeTemp = null;

            string cmd = _vm.variables.Get(m_params[0]);

            switch (cmd)
            {
                case "table.find":
                    // more to come...(flanker)
                break;

                case "table.GetRowCount":
                {
                    string varName = m_params[1];
                    AutomationElementCollection col = _vm.host.aeCurrent.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom));
                    _vm.variables.Update(varName, col.Count.ToString());
                }
                break;

                case "table.SetCellValue":
                {
                    int index = Convert.ToInt32(_vm.variables.Get(m_params[1]));
                    string colName = _vm.variables.Get(m_params[3]);
                    string value = _vm.variables.Get(m_params[5]);

                    AutomationElementCollection col = _vm.host.aeCurrent.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom));

                    AutomationElement row = col[index];
                    aeTemp = _vm.host.aeCurrent;
                    _vm.host.aeCurrent = row;
                    UtilSys.Wait(500);
                    _vm.host.aeCurrent = aeTemp;

                    AutomationElementCollection cells = row.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom));

                    foreach (AutomationElement cell in cells)
                    {
                        if (cell.Current.Name == colName + " Row " + (index - 1).ToString())
                        {
                            aeTemp = _vm.host.aeCurrent;
                            _vm.host.aeCurrent = cell;
                            UtilSys.Wait(500);
                            _vm.host.aeCurrent = aeTemp;

                            AutomationPattern.Value(cell, value);
                            return EnumActionResult.OK;
                        }
                    }
                    return EnumActionResult.ERROR;
                }
                //break;

                case "table.GetCellValue":
                {
                    int index = Convert.ToInt32(_vm.variables.Get(m_params[1]));
                    string colName = _vm.variables.Get(m_params[3]);
                    string varName = m_params[5];

                    AutomationElementCollection col = _vm.host.aeCurrent.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom));

                    AutomationElement row = col[index];


                    aeTemp = _vm.host.aeCurrent;
                    _vm.host.aeCurrent = row;
                    UtilSys.Wait(500);
                    _vm.host.aeCurrent = aeTemp;

                    AutomationElementCollection cells = row.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom));

                    foreach (AutomationElement cell in cells)
                    {
                        if (cell.Current.Name == colName + " Row " + (index - 1).ToString())
                        {
                            aeTemp = _vm.host.aeCurrent;
                            _vm.host.aeCurrent = cell;
                            UtilSys.Wait(500);
                            _vm.host.aeCurrent = aeTemp;

                            string value = AutomationPattern.GetValue(cell);
                            _vm.variables.Update(varName, value);
                            return EnumActionResult.OK;
                        }
                    }
                    return EnumActionResult.ERROR;
                }
                //break;

                default:
                    return EnumActionResult.ERROR;
            }
            return EnumActionResult.OK;
        }
    }
}
