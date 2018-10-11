using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionTestWindowMessage : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string msgExpected = _vm.variables.Get(m_params[1]);

            AutomationElement ae = Finder.GetTextOnWindow(_vm.host.TargetWindow);

            if (null == ae)
            {
                _vm.host.WriteLog("Text not found on '" +  _vm.host.TargetWindow.Current.Name  + "' window.");
                return (EnumActionResult.ERROR);
            }
            
            string msg = "Test Window Message";
            string curText = ae.Current.Name;
            curText = curText.Replace('\r', ' ');
            curText = curText.Replace('\n', ' ');

            if (curText == msgExpected)
            {
                _vm.host.WriteLog(msg + " SUCCESS: ('" + curText + "' == '" + msgExpected + "')");
                return (EnumActionResult.TEST_OK);
            }
                        
            _vm.host.WriteLog(msg + " FAILED: ('" + curText + "' != '" + msgExpected  + "')");
            return (EnumActionResult.TEST_FAILED);                        
        }
    }
}
