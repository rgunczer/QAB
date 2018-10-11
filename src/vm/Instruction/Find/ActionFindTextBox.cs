using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindTextBox : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string id = _vm.variables.Get(m_params[1]);

            AutomationElement textBox = null;
            AutomationElement aeParent = null;

            aeParent = _vm.host.TargetWindow;

            textBox = Finder.GetTextBoxByID(aeParent, id);

            if (null == textBox)
            {
                _vm.PushError("TextBox '" + id + "' NOT found on '" + aeParent.Current.Name + "'.");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("TextBox '" + id + "' found on '" + aeParent.Current.Name + "'.");
            
            _vm.host.aeCurrent = textBox;
            
            return (EnumActionResult.OK);
        }
    }
}
