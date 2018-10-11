using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using System.Drawing;
using Util;


namespace VM
{
    public enum EnumControlTypes
    {
        Button,
        Edit,
        ComboBox,
        CheckBox,
        DataGrid,
        DataItem,
        RadioButton,
        Table,
        Menu,
        MenuBar,
        MenuItem,
        ToolBar,
        Tab,
        TabItem,
        Text,
        Window,
        Tree,
        Document,
        List,
        ListItem,
        SplitButton,
        Pane,
        HyperLink,
        Calendar,
        Header,
        HeaderItem,
        Custom,
    };

    public static class Finder
    {
        private static VirtualMachine _vm = null;
        public static VirtualMachine vm
        {
            set { _vm = value; }
        }

        private static Dictionary<EnumControlTypes, PropertyCondition> _dicTypes = new Dictionary<EnumControlTypes, PropertyCondition>() 
        { 
            { EnumControlTypes.Button,         new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button) },
            { EnumControlTypes.Edit,           new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit) },
            { EnumControlTypes.ComboBox,       new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ComboBox) },
            { EnumControlTypes.CheckBox,       new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.CheckBox) },
            { EnumControlTypes.DataGrid,       new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataGrid) },
            { EnumControlTypes.DataItem,       new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem) },
            { EnumControlTypes.RadioButton,    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.RadioButton) },
            { EnumControlTypes.Table,          new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Table) },
            { EnumControlTypes.Menu,           new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Menu) },
            { EnumControlTypes.MenuBar,        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.MenuBar) },
            { EnumControlTypes.MenuItem,       new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.MenuItem) },
            { EnumControlTypes.ToolBar,        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ToolBar) },
            { EnumControlTypes.Tab,            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Tab) },
            { EnumControlTypes.TabItem,        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem) },
            { EnumControlTypes.Text,           new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text) },
            { EnumControlTypes.Window,         new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window) },
            { EnumControlTypes.Tree,           new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Tree) },
            { EnumControlTypes.Document,       new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Document) },
            { EnumControlTypes.List,           new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.List) },
            { EnumControlTypes.ListItem,       new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem) },
            { EnumControlTypes.SplitButton,    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.SplitButton) },
            { EnumControlTypes.Pane,           new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane) },
            { EnumControlTypes.HyperLink,      new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Hyperlink) },
            { EnumControlTypes.Calendar,       new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Calendar) },  
            { EnumControlTypes.Header,         new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Header) },
            { EnumControlTypes.HeaderItem,     new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.HeaderItem) },            
            { EnumControlTypes.Custom,         new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom) },            
        };


        public static AutomationElement FindMenuBar(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCond = new AndCondition(idCond, _dicTypes[EnumControlTypes.MenuBar]);
            AutomationElement ae = parent.FindFirst(TreeScope.Descendants, andCond);

            return ae;
        }

        public static AutomationElement GetHeaderItem(AutomationElement parent, string name)
        {
            PropertyCondition nameCond = new PropertyCondition(AutomationElement.NameProperty, name);
            AndCondition andCond = new AndCondition(nameCond, _dicTypes[EnumControlTypes.HeaderItem]);
            AutomationElement headerItem = parent.FindFirst(TreeScope.Descendants, andCond);

            return headerItem;
        }

        public static AutomationElement GetHeader(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCond = new AndCondition(idCond, _dicTypes[EnumControlTypes.Header]);
            AutomationElement header = parent.FindFirst(TreeScope.Descendants, andCond);

            return header;
        }

        public static AutomationElement GetByType(AutomationElement parent, EnumControlTypes type)
        {
            PropertyCondition cond = _dicTypes[type];
            AutomationElement ae = parent.FindFirst(TreeScope.Descendants, cond);
            return ae;
        }
        
        public static AutomationElement GetControlFromClientPos(AutomationElement window, int x, int y)
        {
            Point ptScr = UtilAutomation.Convert2Screen(window, new System.Windows.Point(x, y));

            System.Windows.Point pp = new System.Windows.Point();
            pp.X = ptScr.X;
            pp.Y = ptScr.Y;

            return( AutomationElement.FromPoint(pp) );
        }

        public static AutomationElement GetControlFromScreenPos(int x, int y)
        {
            Point ptScr = new Point(x, y);

            System.Windows.Point pp = new System.Windows.Point();
            pp.X = ptScr.X;
            pp.Y = ptScr.Y;

            return( AutomationElement.FromPoint(pp) );
        }

        public static AutomationElement GetHyperLink(AutomationElement parent, string name)
        {
            PropertyCondition nameCond = new PropertyCondition(AutomationElement.NameProperty, name);
            AndCondition andCond = new AndCondition(nameCond, _dicTypes[EnumControlTypes.HyperLink]);
            AutomationElement elem = parent.FindFirst(TreeScope.Descendants, andCond);

            return (elem);
        }

        public static AutomationElement GetPane(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCond = new AndCondition(idCond, _dicTypes[EnumControlTypes.Pane]);
            AutomationElement pne = parent.FindFirst(TreeScope.Descendants, andCond);

            return (pne);
        }

        public static AutomationElement GetSplitButton(AutomationElement parent, string name)
        {
            PropertyCondition nameCond = new PropertyCondition(AutomationElement.NameProperty, name);
            AndCondition andCond = new AndCondition(nameCond, _dicTypes[EnumControlTypes.SplitButton]);
            AutomationElement elem = parent.FindFirst(TreeScope.Descendants, andCond);

            return (elem);
        }
        
        public static AutomationElement GetListItem(AutomationElement parent, string name)
        {
            PropertyCondition nameCond = new PropertyCondition(AutomationElement.NameProperty, name);
            AndCondition andCond = new AndCondition(nameCond, _dicTypes[EnumControlTypes.ListItem]);
            AutomationElement lst = parent.FindFirst(TreeScope.Descendants, andCond);

            return (lst);
        }
        
        public static AutomationElement GetList(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);                      
            AndCondition andCond = new AndCondition(idCond, _dicTypes[EnumControlTypes.List]);
            AutomationElement lst = parent.FindFirst(TreeScope.Descendants, andCond);

            return (lst);
        }


        public static AutomationElement GetDocument(AutomationElement parent, string automationID)
        {
            AutomationElement doc = null;
            PropertyCondition idCondition = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCondition = new AndCondition(_dicTypes[EnumControlTypes.Document], idCondition);

            doc = parent.FindFirst(TreeScope.Descendants, andCondition);

            return (doc);
        }

        public static AutomationElement GetChildWindowByTitleLike(AutomationElement parent, string windowTitle)
        {
            string title = null;     
            AutomationElementCollection windows = parent.FindAll(TreeScope.Children, _dicTypes[EnumControlTypes.Window]);
                        
            foreach (AutomationElement window in windows)
            {
                title = window.Current.Name;

                if (title.Length >= windowTitle.Length)
                {
                    title = title.Substring(0, windowTitle.Length);

                    if (title == windowTitle)
                        return (window); // return first valid  
                }
            }
            return (null);
        }


        public static AutomationElement GetChildWindowByTitle(AutomationElement parent, string windowTitle)
        {
            AutomationElement window = null;            
            PropertyCondition nameCondition = new PropertyCondition(AutomationElement.NameProperty, windowTitle);
            AndCondition andCondition = new AndCondition(_dicTypes[EnumControlTypes.Window], nameCondition);

            window = parent.FindFirst(TreeScope.Descendants, andCondition);

            return (window);
        }

        public static AutomationElement GetChildWindowByID(AutomationElement parent, string automationID)
        {
            AutomationElement window = null;
            PropertyCondition nameCondition = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCondition = new AndCondition(_dicTypes[EnumControlTypes.Window], nameCondition);

            window = parent.FindFirst(TreeScope.Descendants, andCondition);

            return (window);
        }


        public static AutomationElementCollection GetWindowsByName(string windowTitle, int procID)
        {
            AutomationElementCollection windows = null;

            PropertyCondition[] cond = new PropertyCondition[3];

            cond[0] = new PropertyCondition(AutomationElement.NameProperty, windowTitle);
            cond[1] = new PropertyCondition(AutomationElement.ProcessIdProperty, procID);
            cond[2] = _dicTypes[EnumControlTypes.Window];

            AndCondition andCond = new AndCondition(cond);

            windows = AutomationElement.RootElement.FindAll(TreeScope.Children, andCond);

            return (windows);
        }

        public static AutomationElementCollection GetWindowsByID(string automationID, int procID)
        {
            AutomationElementCollection windows = null;

            PropertyCondition[] cond = new PropertyCondition[3];

            cond[0] = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            cond[1] = new PropertyCondition(AutomationElement.ProcessIdProperty, procID);
            cond[2] = _dicTypes[EnumControlTypes.Window];

            AndCondition andCond = new AndCondition(cond);

            windows = AutomationElement.RootElement.FindAll(TreeScope.Children, andCond);

            return (windows);
        }     

        public static AutomationElement GetTopLevelWindowLike(string windowTitle)
        {
            AutomationElementCollection windows = AutomationElement.RootElement.FindAll(TreeScope.Children, _dicTypes[EnumControlTypes.Window]);

            string title = null;            

            foreach (AutomationElement window in windows)
            {
                title = window.Current.Name;

                if (title.Length >= windowTitle.Length)
                {
                    title = title.Substring(0, windowTitle.Length);

                    if (title == windowTitle)
                        return (window);                    
                }
            }
            return (null);
        }

        public static AutomationElement GetTopLevelWindow(string windowTitle)
        {
            AutomationElement window = null;           
                                    
            PropertyCondition nameCondition = new PropertyCondition(AutomationElement.NameProperty, windowTitle);            
            AndCondition andCondition = new AndCondition(_dicTypes[EnumControlTypes.Window], nameCondition);
            
            window = AutomationElement.RootElement.FindFirst(TreeScope.Children, andCondition);

            return (window);
        }


        public static string[] GetDataGridHeadNames(AutomationElement head)
        {
            int i = 0;
            AutomationElementCollection items = GetDataGridHeaderItems(head);

            string[] columns = new string[items.Count];

            foreach (AutomationElement item in items)
            {
                columns[i++] = item.Current.Name;
            }
            return (columns);
        }

        public static AutomationElement GetTree(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AutomationElement tree = parent.FindFirst(TreeScope.Descendants, idCond);

            return (tree);
        }

        public static AutomationElement GetTextLike(AutomationElement parent, string pattern)
        {
            AutomationElement element = null;

            AutomationElementCollection items = parent.FindAll(TreeScope.Children, _dicTypes[EnumControlTypes.Text]);

            foreach (AutomationElement item in items)
            {
                if ( item.Current.Name.StartsWith(pattern) )
                    return item;
            }

            return element;
        }

        public static AutomationElement GetText(AutomationElement parent, string name)
        {
            PropertyCondition nameCondition = new PropertyCondition(AutomationElement.NameProperty, name);
            AndCondition andCondition = new AndCondition(_dicTypes[EnumControlTypes.Text], nameCondition);
            
            AutomationElement txt = parent.FindFirst(TreeScope.Descendants, andCondition);

            return (txt);
        }

        public static AutomationElement GetTextOnWindow(AutomationElement parent)
        {                        
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, "65535");
            AndCondition andCondition = new AndCondition(_dicTypes[EnumControlTypes.Text], idCond);

            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCondition);

            return (element);
        }

        public static AutomationElement GetMenuItem(AutomationElement parent, string name)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.NameProperty, name);

            AndCondition andCond = new AndCondition(idCond, _dicTypes[EnumControlTypes.MenuItem]);

            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);

            return (element);
        }
        
        public static AutomationElement GetMenu(AutomationElement parent, string name)
        {           
            AutomationElement element = null;

            PropertyCondition idCond = new PropertyCondition(AutomationElement.NameProperty, name);
            AndCondition andCond = new AndCondition(idCond, _dicTypes[EnumControlTypes.Menu]);
            element = parent.FindFirst(TreeScope.Descendants, andCond);           

            if (null == element)
            {
                _vm.host.WriteLog("GetMenu using MouseLeftClickPos");
                Point p = new Point();

                p.X = (int)_vm.host.LeftClickPos.X;
                p.Y = (int)_vm.host.LeftClickPos.Y;

                NativeMethods.POINT pt;
                pt.X = p.X;
                pt.Y = p.Y;

                IntPtr ptr = NativeMethods.WindowFromPoint(pt);

                element = AutomationElement.FromHandle(ptr);

                if (element.Current.Name != name || element.Current.ControlType != ControlType.Menu)
                    element = null;
            }

            if (null == element)
            {
                _vm.host.WriteLog("GetMenu using Desktop");
                PropertyCondition nameCondition = new PropertyCondition(AutomationElement.NameProperty, name);
                AndCondition andCondition = new AndCondition(_dicTypes[EnumControlTypes.Menu], nameCondition);

                //Ask the Desktop to find the element
                element = AutomationElement.RootElement.FindFirst(TreeScope.Descendants, andCondition);
            }
            return (element);
        }

        public static AutomationElement GetComboBoxByID(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);            

            AndCondition andCond = new AndCondition(idCond, _dicTypes[EnumControlTypes.ComboBox]);

            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);

            return (element);
        }

        public static AutomationElement GetComboBoxByName(AutomationElement parent, string name)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.NameProperty, name);
            
            AndCondition andCond = new AndCondition(idCond, _dicTypes[EnumControlTypes.ComboBox]);

            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);

            return (element);
        }

        public static AutomationElement GetCheckBox(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);

            AndCondition andCond = new AndCondition(idCond, _dicTypes[EnumControlTypes.CheckBox]);

            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);

            return (element);
        }

        public static AutomationElement GetDataGrid(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCond = new AndCondition(idCond, _dicTypes[EnumControlTypes.DataGrid]);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);

            return (element);
        }

        public static AutomationElement GetDataGridHeader(AutomationElement grid)
        {
            AutomationElement head = grid.FindFirst(TreeScope.Children, _dicTypes[EnumControlTypes.Header]);
            return (head);
        }

        public static AutomationElementCollection GetDataGridHeaderItems(AutomationElement head)
        {
            AutomationElementCollection items = head.FindAll(TreeScope.Children, _dicTypes[EnumControlTypes.HeaderItem]);
            return (items);
        }

        public static AutomationElementCollection GetDataGridRowCells(AutomationElement item)
        {
            AutomationElementCollection items = item.FindAll(TreeScope.Children, _dicTypes[EnumControlTypes.Text]);
            return (items);
        }

        public static AutomationElement GetRadioButton(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCond = new AndCondition(_dicTypes[EnumControlTypes.RadioButton], idCond);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);
            return (element);
        }

        public static AutomationElement GetCustomByName(AutomationElement parent, string name)
        {
            PropertyCondition nameCondition = new PropertyCondition(AutomationElement.NameProperty, name);
            AndCondition andCond = new AndCondition(_dicTypes[EnumControlTypes.Custom], nameCondition);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);

            return (element);
        }

        public static AutomationElement GetCustomByID(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCond = new AndCondition(_dicTypes[EnumControlTypes.Custom], idCond);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);

            return (element);
        }

        public static AutomationElement GetButtonByID(AutomationElement parent, string automationID)
        {
            return GetButtonByID(parent, automationID, 5, 150);
        }
        
        public static AutomationElement GetButtonByID(AutomationElement parent, string automationID, int maxAttemp, int waitInterval)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCond = new AndCondition(_dicTypes[EnumControlTypes.Button], idCond);
            AutomationElement ae = null;
            
            int attempt = 0;
            
            do 
            {
                ae = parent.FindFirst(TreeScope.Descendants, andCond);

                if (null != ae)
                    return (ae);

                ++attempt;
                UtilSys.Wait(waitInterval);
            } while ( attempt < maxAttemp );
        
            return (ae);
        }

        public static AutomationElement GetButtonByName(AutomationElement parent, string name)
        {
            PropertyCondition nameCond = new PropertyCondition(AutomationElement.NameProperty, name);
            AndCondition andCond = new AndCondition(_dicTypes[EnumControlTypes.Button], nameCond);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);
            return (element);
        }

        public static AutomationElement GetToolBarByID(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCond = new AndCondition(_dicTypes[EnumControlTypes.ToolBar], idCond);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);
            return (element);
        }

        public static AutomationElement GetDataItemByID(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCond = new AndCondition(_dicTypes[EnumControlTypes.DataItem], idCond);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);
            return (element);
        }

        public static AutomationElement GetDataItemByName(AutomationElement parent, string name)
        {
            PropertyCondition nameCond = new PropertyCondition(AutomationElement.NameProperty, name);
            AndCondition andCond = new AndCondition(_dicTypes[EnumControlTypes.DataItem], nameCond);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);
            return (element);
        }
        
        public static AutomationElementCollection GetToolBarButtons(AutomationElement item)
        {
            PropertyCondition p = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button);
            AutomationElementCollection items = item.FindAll(TreeScope.Descendants, p);
            return (items);
        }

        public static AutomationElementCollection GetToolBarMenuItems(AutomationElement item)
        {
            PropertyCondition p = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.MenuItem);
            AutomationElementCollection items = item.FindAll(TreeScope.Descendants, p);
            return (items);
        }


        public static AutomationElement GetTableByID(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCond = new AndCondition(_dicTypes[EnumControlTypes.Table], idCond);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);
            return (element);
        }


        public static AutomationElement GetTabByID(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCond = new AndCondition(_dicTypes[EnumControlTypes.Tab], idCond);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);
            return (element);
        }

        public static AutomationElement GetTabItemByName(AutomationElement parent, string name)
        {
            PropertyCondition nameCond = new PropertyCondition(AutomationElement.NameProperty, name);
            AndCondition andCond = new AndCondition(_dicTypes[EnumControlTypes.TabItem], nameCond);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);
            return (element);
        }

        public static AutomationElement GetTextBoxByID(AutomationElement parent, string automationID)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, automationID);
            AndCondition andCond = new AndCondition(_dicTypes[EnumControlTypes.Edit], idCond);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);            
            return (element);
        }


    }
}
