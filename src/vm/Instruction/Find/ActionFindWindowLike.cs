using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;


namespace VM
{
    class ActionFindWindowLike : ActionFindWindow
    {
        protected override AutomationElement GetWindow(string title)
        {
            AutomationElement wnd = null;

            wnd = Finder.GetTopLevelWindowLike(title);

            return (wnd);
        }
    }
}
