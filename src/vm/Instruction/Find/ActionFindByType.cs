using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionFindByType : ActionBase
    {
        private static Dictionary<string, EnumControlTypes> _dicTypes = new Dictionary<string, EnumControlTypes>()
        {
            { "UI-Button", EnumControlTypes.Button },
            { "UI-Edit", EnumControlTypes.Edit },
            { "UI-ComboBox", EnumControlTypes.ComboBox },
            { "UI-CheckBox", EnumControlTypes.CheckBox },
            { "UI-DataGrid", EnumControlTypes.DataGrid },
            { "UI-DataItem", EnumControlTypes.DataItem },
            { "UI-RadioButton", EnumControlTypes.RadioButton },
            { "UI-Table", EnumControlTypes.Table }, 
            { "UI-Menu", EnumControlTypes.Menu },
            { "UI-MenuItem", EnumControlTypes.MenuItem },
            { "UI-ToolBar", EnumControlTypes.ToolBar },
            { "UI-Tab", EnumControlTypes.Tab },
            { "UI-TabItem", EnumControlTypes.TabItem },
            { "UI-Text", EnumControlTypes.Text },
            { "UI-Window", EnumControlTypes.Window },
            { "UI-Tree", EnumControlTypes.Tree },
            { "UI-Document", EnumControlTypes.Document },
            { "UI-List", EnumControlTypes.List },
            { "UI-ListItem", EnumControlTypes.ListItem }, 
            { "UI-SplitButton", EnumControlTypes.SplitButton },
            { "UI-Pane", EnumControlTypes.Pane },
            { "UI-HyperLink", EnumControlTypes.HyperLink },
            { "UI-Calendar", EnumControlTypes.Calendar },
            { "UI-Header", EnumControlTypes.Header },
            { "UI-HeaderItem", EnumControlTypes.HeaderItem },                           
        };

        public override EnumActionResult Execute()
        {
            string type = _vm.variables.Get(m_params[1]);

            EnumControlTypes ty;

            if (ActionFindByType._dicTypes.ContainsKey(type))
                ty = ActionFindByType._dicTypes[type];
            else
                throw new Exception("Unknown Control Type [" + type + "]");

            AutomationElement ae = Finder.GetByType(_vm.host.TargetWindow, ty);
            
            if (null == ae)
            {
                _vm.PushError("Control '" + type + "' NOT found on '" + _vm.host.TargetWindow.Current.Name + "'-");
                return (EnumActionResult.ERROR);
            }

            _vm.host.WriteLog("Control '" + type + "' found on '" + _vm.host.TargetWindow.Current.Name + "'-");
            _vm.host.aeCurrent = ae;

            return (EnumActionResult.OK);
        }
    }
}
