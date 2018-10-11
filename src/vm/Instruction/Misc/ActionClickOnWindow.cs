using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;


namespace VM
{
    class ActionClickOnWindow : ActionBase
    {
        public override EnumActionResult Execute()
        {
            Rect rc = _vm.host.TargetWindow.Current.BoundingRectangle;
            System.Windows.Point pos = new System.Windows.Point();
            pos.X = rc.Left + (rc.Right - rc.Left) / 2;
            pos.Y = rc.Top + (rc.Bottom - rc.Top) / 2;

            UtilAutomation.ScreenClick(pos, false);

            return (EnumActionResult.OK);
        }
    }
}