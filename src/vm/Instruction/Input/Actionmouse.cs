using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using Util;
using System.Windows.Automation;


namespace VM
{
    class Actionmouse : ActionBase
    {
        private static AutomationElement _aeDrag = null;
        private static AutomationElement _aeDrop = null;


        public override EnumActionResult Execute()
        {
            string posX = string.Empty;
            string posY = string.Empty;
            System.Windows.Point pos = new System.Windows.Point();

            string type = m_params[0];
            
            switch (type)
            {
                case "mouse.SetDragSource":
                    _aeDrag = _vm.host.aeCurrent;
                break;

                case "mouse.SetDropSource":
                    _aeDrop = _vm.host.aeCurrent;
                break;

                case "mouse.DoDragAndDrop":
                    UtilAutomation.MouseHover(_aeDrag);
                    UtilSys.Wait(1000);
                    UtilAutomation.MouseLeftDown();
                    UtilSys.Wait(1000);
                    UtilAutomation.MouseHover(_aeDrop);
                    UtilSys.Wait(1000);
                    UtilAutomation.MouseLeftUp();
                break;

                case "mouse.RightClickOn":
                    if (_vm.host.IsRegion)
                        _vm.host.LeftClickPos = UtilAutomation.RightClickOn(_vm.host.ImageRegion);
                    else
                        _vm.host.LeftClickPos = UtilAutomation.RightClickOn(_vm.host.aeCurrent.Current.BoundingRectangle);                                                
                break;

                case "mouse.RightClickClient":
                {
                    posX = _vm.variables.Get(m_params[1]);
                    posY = _vm.variables.Get(m_params[3]);

                    pos.X = Convert.ToDouble(posX);
                    pos.Y = Convert.ToDouble(posY);

                    UtilAutomation.ClientRightClick(_vm.host.TargetWindow, pos, false);
                }
                break;

                case "mouse.RightClickScreen":
                {
                    posX = _vm.variables.Get(m_params[1]);
                    posY = _vm.variables.Get(m_params[3]);

                    pos.X = Convert.ToDouble(posX);
                    pos.Y = Convert.ToDouble(posY);

                    UtilAutomation.ScreenRightClick(pos, false);
                }
                break;

                case "mouse.ClickOn":
                    if (_vm.host.IsRegion)
                    {
                        if (!UtilAutomation.ClickOn(_vm.host.ImageRegion, false))
                            return (EnumActionResult.ERROR);
                    }
                    else
                    {
                        if (!UtilAutomation.ClickOn(_vm.host.aeCurrent.Current.BoundingRectangle, false))
                            return (EnumActionResult.ERROR);
                    }
                break;

                case "mouse.ClickClient":
                {                    
                    posX = _vm.variables.Get(m_params[1]);
                    posY = _vm.variables.Get(m_params[3]);

                    pos.X = Convert.ToDouble(posX);
                    pos.Y = Convert.ToDouble(posY);

                    UtilAutomation.ClientClick(_vm.host.TargetWindow, pos, false);
                }
                break;

                case "mouse.ClickScreen":
                {
                    posX = _vm.variables.Get(m_params[1]);
                    posY = _vm.variables.Get(m_params[3]);

                    pos.X = Convert.ToDouble(posX);
                    pos.Y = Convert.ToDouble(posY);

                    UtilAutomation.ScreenClick(pos, false);
                }                
                break;
                                
                case "mouse.DoubleClickOn":
                    if (_vm.host.IsRegion)
                    {
                        if (!UtilAutomation.ClickOn(_vm.host.ImageRegion, true))
                            return (EnumActionResult.ERROR);
                    }
                    else
                    {
                        if (!UtilAutomation.ClickOn(_vm.host.aeCurrent.Current.BoundingRectangle, true))
                            return (EnumActionResult.ERROR);
                    }
                break;

                case "mouse.DoubleClickClient":
                {
                    posX = _vm.variables.Get(m_params[1]);
                    posY = _vm.variables.Get(m_params[3]);

                    pos.X = Convert.ToDouble(posX);
                    pos.Y = Convert.ToDouble(posY);

                    UtilAutomation.ClientClick(_vm.host.TargetWindow, pos, true);                    
                }
                break;

                case "mouse.DoubleClickScreen":
                {
                    posX = _vm.variables.Get(m_params[1]);
                    posY = _vm.variables.Get(m_params[3]);

                    pos.X = Convert.ToDouble(posX);
                    pos.Y = Convert.ToDouble(posY);

                    UtilAutomation.ScreenClick(pos, true);
                }
                break;

                default:
                    _vm.host.WriteLog("Actionmouse->Unrecognized command [" + type + "].");
                return EnumActionResult.ERROR;
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