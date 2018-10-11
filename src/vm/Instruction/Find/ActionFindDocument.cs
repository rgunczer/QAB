using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindDocument : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string id = _vm.variables.Get(m_params[1]);

            AutomationElement aeParent = _vm.host.TargetWindow;
            AutomationElement ae = Finder.GetDocument(aeParent, id);

            if (null == ae)
            {
                _vm.PushError("Document '" + id + "' NOT found on '" + aeParent.Current.Name + "'.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Document '" + id + "' found on '" + aeParent.Current.Name + "'.");

            _vm.host.aeCurrent = ae;

            return (EnumActionResult.OK);
        }
    }
}
