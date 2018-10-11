using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindTextBoxUnder : ActionBase
    {
        // FindClosestUnder "TextBox"
        public override EnumActionResult Execute()
        {           
            PropertyCondition typeCond = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit);
            AutomationElementCollection col = _vm.host.TargetWindow.FindAll(TreeScope.Descendants, typeCond);

            if (null == col)
                return EnumActionResult.ERROR;

            AutomationElement aeClosest = null;
            int TargetPosY = (int)_vm.host.aeCurrent.Current.BoundingRectangle.Top;
            int minYdist = 1000;

            foreach (AutomationElement item in col)
            {
                if (item.Current.BoundingRectangle.Top > TargetPosY)
                {
                    int dist = (int)item.Current.BoundingRectangle.Top - TargetPosY;

                    if (dist < minYdist)
                    {
                        minYdist = dist;
                        aeClosest = item;
                    }
                }
            }

            if (null != aeClosest)
            {
                _vm.host.aeCurrent = aeClosest;
                return EnumActionResult.OK;
            }

            _vm.PushError("Closest TextBox control NOT found");
            return EnumActionResult.ERROR;
        }
    }
}
