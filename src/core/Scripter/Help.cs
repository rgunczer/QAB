using System;
using System.Collections.Generic;
using System.Text;


namespace Scripter
{
    public class HelpItem
    {
        private static string _order = string.Empty;

        private string m_desc = string.Empty;
        private string m_param = string.Empty;
        private string m_example = string.Empty;

        public string Desc
        {
            get { return m_desc; }
            set { m_desc = value; }
        }

        public string Param
        {
            get { return m_param; }
            set { m_param = value; }
        }

        public string Example
        {
            get { return m_example; }
            set { m_example = value; }
        }

        public static void SetOrder(string ord)
        {
            _order = ord;
        }

        public static string GetOrder()
        {
            return _order;
        }

        public override string ToString()
        {
            string tmp = string.Empty;

            string[] arr = new string[3];
            arr[0] = "\\ul Description:\\ul0 \n" + m_desc;
            arr[1] = "\\ul Parameter(s):\\ul0 \n" + m_param;
            arr[2] = "\\ul Example:\\ul0 \n" + m_example;

            for (int i = 0; i < 3; ++i)
            {
                int index = int.Parse(_order.Substring(i, 1));

                if (arr[index].Length > 0)
                    tmp += arr[index] + "\r\n";
            }

            return tmp;
        }
    }


    public static class Help
    {
        private static string NL = Environment.NewLine;

        private static Dictionary<string, HelpItem> m_dic = new Dictionary<string, HelpItem>();

        public static HelpItem Get(string key)
        {
            if (m_dic.ContainsKey(key))
                return m_dic[key];
            else
                return null;
        }

        private static void CreateHelpItem(string key, string desc, string param, string example)
        {
            HelpItem item = new HelpItem();
            
            item.Desc = desc;
            item.Param = param;
            item.Example = example;

            m_dic.Add(key, item);
        }

        static Help()
        {
            string desc;
            string param;
            string example;

            desc = @"\cf2 FindWindow \cf0 is used to find a target window.";
            param = @"One or two param see the examples. Case sensitive.";
            example = @"\cf3 FindWindow ""Invenio"" \cf0 - this command can be used to find the window in running applications that has ""Invenio"" in its title bar." + NL +
                      @"\cf3 FindWindow ""Calculator"", ""calc.exe"" \cf0  - this command can be used to find the ""Calculator"" window, and if it fails, it will launch ""calc.exe"".";
            CreateHelpItem("FindWindow", desc, param, example);

            desc = "\\cf2 FindWindowLike \\cf0 has the same function as FindWindow, except that it will compare only part of the title when searching for a window.";
            param = "";
            example = "\\cf3 FindWindowLike \"Inven\" \\cf0 - this command can be used to find the window in running applications that has text beginning with \"Inven\" in its title bar.\r\n" +
                      "\\cf3 FindWindowLike \"Calcul\", \"calc.exe\" \\cf0 - this command can be used to find the \"Calculator\" window, and if it fails, it will launch \"calc.exe\".";            
            CreateHelpItem("FindWindowLike", desc, param, example);

            desc = "\\cf2 FindTextBox \\cf0 is used to find a textbox in a window.";
            param = "";
            example = "\\cf3 FindTextBox \"txtName\" \\cf0 (only one parameter is used which is the automationID of the control)";            
            CreateHelpItem("FindTextBox", desc, param, example);

            desc = "\\cf2 FindComboBox \\cf0 is used to find a combo box in a window.";
            param = "";
            example = "\\cf3 FindComboBox \"lstAuthType\" \\cf0 (only one parameter is used which is the automationID of the control)";            
            CreateHelpItem("FindComboBox", desc, param, example);

            desc = "\\cf2 FindButton \\cf0 is used to find a button in a window.";
            param = "This command requires two parameters. One of the parameters defines how to find the button. There are two possible values: \"ByID\" and \"ByName\". \"ByID\" refers to the automationID, and \"ByName\" refers to the actual text on the button."; 
            example = "\\cf3 FindButton \"ByID\", \"cmdOk\" \\cf0 This command finds a button by its automationID\r\n" +
                      "\\cf3 FindButton \"ByName\", \"OK\" \\cf0 This command finds a button by button text";
            CreateHelpItem("FindButton", desc, param, example);

            desc = "\\cf2 FindCheckBox \\cf0 is used to find a checkbox in a window."; 
            param = "This command has only one parameter which is the automationID of the checkbox.";
            example = "\\cf3 FindCheckBox \"chkServiceUser\" \\cf0";
            CreateHelpItem("FindCheckBox", desc, param, example);

            desc = "\\cf2 FindChildWindow \\cf0 is used to find a dialog window.";
            param = "This command has three required parameters." + NL +
                    "The first defines how to find a child window. The possible values are \"ByID\", \"ByTitle\" and \"ByTitleLike\"." + NL +
                    "The second is a value which depends on the first parameter." + NL +
                    "The third is a timeout value which specifies the number of attempts made to find the child window.";                        
            example = "\\cf3 FindChildWindow \"ByID\", \"LogonForm\", \"20\" \\cf0 \r\n" +
                      "\\cf3 FindChildWindow \"ByTitleLike\", \"Login\", \"20\" \\cf0 \r\n" +
                      "\\cf3 FindChildWindow \"ByTitle\", \"Login into Invenio\", \"20\" \\cf0";
            CreateHelpItem("FindChildWindow", desc, param, example);

            desc = "\\cf2 FindDataGrid \\cf0 is used to find a data grid in a window.";
            param = "This command requires only one parameter which is the automationID of the data grid.";
            example = "\\cf3 FindDataGrid \"grdResult\" \\cf0";
            CreateHelpItem("FindDataGrid", desc, param, example);

            desc = "\\cf2 FindDataGridItem \\cf0 is used to find a data grid item inside a data grid.";
            param = "This command requires only one parameter which is the name of the data grid item.";
            example = "\\cf3 FindDataGridItem \"Administrators\" \\cf0";
            CreateHelpItem("FindDataGridItem", desc, param, example);

            desc = "\\cf2 FindDocument \\cf0 is used to find a document in a window.";
            param = "This command requires only one parameter which is the automationID of the document (multi-line textbox).";
            example = "\\cf3 FindDocument \"txtData\" \\cf0";
            CreateHelpItem("FindDocument", desc, param, example);

            desc = "\\cf2 FindMenu \\cf0 is used to find a menu or a context menu in a window."; 
            param = "This command requires only one parameter which is the automationID of the menu."; 
            example = "\\cf3 FindMenu \"mnuFolder\" \\cf0";
            CreateHelpItem("FindMenu", desc, param, example);

            desc = "\\cf2 FindMenuItem \\cf0 is used to find a menu item in a menu.";
            param = "This command requires only one parameter which is the name of the menu item.";
            example = "\\cf3 FindMenuItem \"Copy\" \\cf0";
            CreateHelpItem("FindMenuItem", desc, param, example);

            desc = "\\cf2 FindProcess \\cf0 is used to find a process and to find a Window from the process.";
            param = "This command requires only one parameter which is the name of the process.";
            example = "\\cf3 FindProcess \"calc\" \\cf0";
            CreateHelpItem("FindProcess", desc, param, example);

            desc = "\\cf2 FindRadioButton \\cf0 is used to find a radio button in a window.";
            param = "This command requires only one parameter which is the automationID of the radio button.";
            example = "\\cf3 FindRadioButton \"rdbGroup\" \\cf0";
            CreateHelpItem("FindRadioButton", desc, param, example);

            desc = "\\cf2 FindTab \\cf0 is used to find a tab control in a window."; 
            param = "This command requires only one parameter which is the automationID of the tab."; 
            example = "\\cf3 FindTab \"tabOptions\" \\cf0";
            CreateHelpItem("FindTab", desc, param, example);

            desc = "\\cf2 FindTabItem \\cf0 is used to find a tab item control inside the tab control.";
            param = "This command requires only one parameter which is the name of the tab item.";
            example = "\\cf3 FindTab \"Tasks\" \\cf0";
            CreateHelpItem("FindTabItem", desc, param, example);

            desc = "\\cf2 FindTable \\cf0 is used to find a table control in a window.";
            param = "This command requires only one parameter which is the automationID of the table.";
            example = "\\cf3 FindTable \"tblData\" \\cf0";
            CreateHelpItem("FindTable", desc, param, example);

            desc = "\\cf2 FindToolbar \\cf0 is used to find a toolbar control in a window."; 
            param = "This command requires only one parameter which is the automationID of the toolbar.";
            example = "\\cf3 FindToolbar \"tblData\" \\cf0";
            CreateHelpItem("FindToolbar", desc, param, example);

            desc = "\\cf2 FindToolbarButton \\cf0 is used to find a toolbar button in a toolbar.";
            param = "This command requires only one parameter which is the name of the toolbar button."; 
            example = "\\cf3 FindToolbarButton \"Add User\" \\cf0";
            CreateHelpItem("FindToolbarButton", desc, param, example);

            desc = "\\cf2 FindTree \\cf0 is used to find a tree control in a window.";
            param = "This command requires only one parameter which is the automationID of the tree.";
            example = "\\cf3 FindTree \"tvwSideBar\" \\cf0";
            CreateHelpItem("FindTree", desc, param, example);

            desc = "\\cf2 FindTreeItem \\cf0 is used to find a tree item inside a tree control.";
            param = "This command requires only one parameter which is the name of the tree item.";
            example = "\\cf3 FindTreeItem \"Public\" \\cf0";
            CreateHelpItem("FindTreeItem", desc, param, example);




            desc = "\\cf2 Invoke \\cf0 is typically used for buttons to invoke them. Basically, this command can be used to simulate a mouse click on a button without using a mouse. It is important to note that invoking a button that brings up a modal dialog will freeze the application. This is a known issue. In this case, use the MouseClickOn function. This command requires no parameters.";
            param = "";
            example = "\\cf3 Invoke \\cf0";
            CreateHelpItem("Invoke", desc, param, example);

            desc = "\\cf2 Select \\cf0 is used to select an item which can be a tab page or a tree element.";
            param = "This command requires no parameter.";
            example = "\\cf3 Select \\cf0";
            CreateHelpItem("Select", desc, param, example);

            desc = "\\cf2 Value \\cf0 is used for textboxes to set their text value.";
            param = "This command requires one parameter which is the value to be set.";
            example = "\\cf3 Value \"Invenio User\" \\cf0";
            CreateHelpItem("Value", desc, param, example);

            desc = "\\cf2 CloseWindow \\cf0 is used to close the last selected window.";
            param = "This command has no parameters.";
            example = "\\cf3 CloseWindow \\cf0";
            CreateHelpItem("CloseWindow", desc, param, example);

            desc = "\\cf2 TestDataGridItem \\cf0 is used to find - test item on DataGrid";
            param = "Two required parameters. First, the name of the column(s) separated with '|' character. Second, text what to find in column(s).";
            example = "\\cf3 TestDataGridItem \"House Id\", \"00034\" \\cf0" + NL +
            	      "\\cf3 TestDataGridItem \"Created on|House Id|Title\", \"8/25/2009 7:47:37 PM|555-0199@2009-08-27T00:00:02|Lorem Ipsum2\" \\cf0";
            CreateHelpItem("TestDataGridItem", desc, param, example);

            desc = "\\cf2 TestDocumentNotContain \\cf0 is used to test multi-line textboxes that do not contain a specific word.";
            param = "One required parameter. The text we are looking for.";
            example = "\\cf3 TestDocumentNotContain \"Error\" \\cf0";
            CreateHelpItem("TestDocumentNotContain", desc, param, example);

            desc = "\\cf2 TestEditBox \\cf0 is used to test values in a textbox.";
            param = "This command requires a single parameter which is the expected value.";
            example = "\\cf3 TestEditBox \"145. \" \\cf0";
            CreateHelpItem("TestEditBox", desc, param, example);

            desc = "\\cf2 TestWindowMessage \\cf0 is used to test messages in a message box.";
            param = "This command requires a single parameter which is the expected value.";
            example = "\\cf3 TestWindowMessage \"Operation completed successfully\" \\cf0";
            CreateHelpItem("TestWindowMessage", desc, param, example);

            desc = "\\cf2 Cache \\cf0 is used to speed up operations on the UI. Each GUI element that is used frequently can be cached. Finding a GUI element is a slow process. Once an element is found, it can be put on a list with its ID as a parameter for this command and later on it can be accessed much faster.";
            param = "One required parameter. The name - key in cache.";
            example = "\\cf3 Cache \"UserNameTextBox\" \\cf0";
            CreateHelpItem("Cache", desc, param, example);

            desc = "\\cf2 GetFromCache \\cf0 is used to access cached GUI elements. This command requires a single parameter which is the ID of the element needed from the cache.";
            param = "One required parameter. The name - key in cache.";
            example = "\\cf3 GetFromCache \"UserNameTextBox\" \\cf0";
            CreateHelpItem("GetFromCache", desc, param, example);

            desc = "\\cf2 CacheWindow \\cf0 has the same function as Cache but it is used with a window.";
            param = "";
            example = "";
            CreateHelpItem("CacheWindow", desc, param, example);

            desc = "\\cf2 GetWindowFromCache \\cf0 has the same function as GetCacheFrom but it is used with a window.";
            param = "";
            example = "";
            CreateHelpItem("GetWindowFromCache", desc, param, example);

            desc = "\\cf2 ClickOnWindow \\cf0 command is used to perform a single mouse click on a window.";
            param = "This command requires no parameter.";
            example = "\\cf3 ClickOnWindow \\cf0";
            CreateHelpItem("ClickOnWindow", desc, param, example);

            desc = "\\cf2 CheckBoxToggle \\cf0 command is used to set checkbox values.";
            param = "It has one parameter which can be set to \"On\" \"Off\" or \"Indeter\"";
            example = "\\cf3 CheckBoxToggle \"On\" \\cf0" + NL +
                      "\\cf3 CheckBoxToggle \"Off\" \\cf0 " + NL +
                      "\\cf3 CheckBoxToggle \"Indeter\" \\cf0";
            CreateHelpItem("CheckBoxToggle", desc, param, example);

            desc = "\\cf2 ClickUltraTreeItemChild \\cf0 - certain UI components are provided by third-party developers, and UltraTree is one of them. Unfortunately, automation has difficulties in handling such controls." + NL +
                   "The command is used to perform a real mouse click on child items specified as a parameter for this command.";
            param = "This command is used after the UltraTree command, which explores nodes inside and places them on parent and child lists.";
            example = "\\cf3 ClickUltraTreeItemChild \"Users\" \\cf0";
            CreateHelpItem("ClickUltraTreeItemChild", desc, param, example);

            desc = "\\cf2 ClickUltraTreeItemRoot \\cf0 has the same function as ClickUltraTreeItemChild but clicking is performed on parent items." + NL +
                   "This command is currently not used because the command UltraTree \"_sidebarTree\", \"ExpandAll\" will perform clicks on parent items.";
            param = "One required parameter. The name of the root item on which to perform a click.";
            example = "";
            CreateHelpItem("ClickUltraTreeItemRoot", desc, param, example);

            desc = "\\cf2 ComboBoxMakeSelection \\cf0 is used to make a selection from a combo box.";
            param = "This commmand has a single parameter which is the name of the item to be selected.";
            example = "\\cf3 ComboBoxMakeSelection \"Basic\" \\cf0";
            CreateHelpItem("ComboBoxMakeSelection", desc, param, example);

            desc = "\\cf2 CopyFile \\cf0 helps you to copy files using the Robot.";
            param = "The first parameter is the source of the file and the second one is the destination (Where to copy).";
            example = "\\cf3 CopyFile \"C:\\\\QABOT\\\\Invenio\\\\setup.exe\", \"C:\\\\QABOT\\\\Backup\\\\Invenio\\\\setup.exe\"  \\cf0";
            CreateHelpItem("CopyFile", desc, param, example);


            desc = "\\cf2 log \\cf0 is used to group log related commands. Like log.write, log.path etc.";
            param = "";
            example = "\\cf3 log.write \"Here we begin testing our new function.\" \\cf0 -- this command will write to the log file the text given as a parameter." + NL +
                      "\\cf3 log.path \"C:\\\\QABOT\\\\Invenio\\\\test.log\" \\cf0 -- this command is used to set or get the current path to the log file." + NL +
                      "\\cf3 log.level \\cf0 -- this command is currently unsupported" + NL +
                      "\\cf3 log.clear \\cf0 -- this command will clear the current log file.";

            CreateHelpItem("log", desc, param, example);


            desc = "\\cf2 sys \\cf0 is used to control the Robot system directly.";
            param = "";
            example = "\\cf3 sys.start \\cf0 -- defines the entry point of the script execution." + NL +
                      "\\cf3 sys.quit \\cf0 -- this command closes the QABOT-Pilot." + NL +
                      "\\cf3 sys.speed = 1000 \\cf0 -- this command adjusts the playback speed. Lower values result in faster playback. Higher values result in slower playback" + NL +
                      "\\cf3 sys.stop \\cf0 -- this command stops playback. It is useful if you want the script to test a specific certain line" + NL +
                      "\\cf3 sys.wait = 3000 \\cf0 -- this command tells the QABOT to wait 3 seconds" + NL +
                      "\\cf3 sys.min \\cf0 -- this command minimizes the QABOT window to the taskbar" + NL +
                      "\\cf3 sys.norm \\cf0 -- this command restores the QABOT window to its original size" + NL +
                      "\\cf3 sys.compact \\cf0 -- this command compacts the qabot window to the bottom-right corner" + NL +
                      "\\cf3 sys.uncompact \\cf0 -- this command restores the qabot window to its original size" + NL +
                      "\\cf3 sys.SaveScreenshotOnError = \"True\" \\cf0 -- this command sets the flag to True or False. During a playback error is a screenshot will be saved or not." + NL +
                      "\\cf3 sys.record = \"True\" \\cf0 -- this command will instruct the pilot to save screenshots during the script execution.";
//            +NL +
//
//                      "\\cf3 sys.error\" \\cf0 -- status indicator" + NL +

            CreateHelpItem("sys", desc, param, example);


            desc = "\\cf2 Screenshot \\cf0 is used to take a screenshot and save it to a path specified for this command as a parameter." + NL +
                   "The path specified must exist before calling this function. After using this command, make sure to wait at least one second before executing the next command.";
            param = "One required parameter. The path where to save the screenshot.";
            example = " \\cf3 Screenshot \"c:\\\\qabot\\\\screenshots\\\\setupPhase1.jpg\"";
            CreateHelpItem("Screenshot", desc, param, example);

            desc = "\\cf2 SendKeys \\cf0 is used to send keystrokes to the target application. This command requires one parameter. Before using this command make sure that the target application has the focus for keyboard input.";
            param = "One required parameter. The key values to send." + NL +
                    "Special key table:" + NL +
                    "BACKSPACE = {BACKSPACE}, {BS}, or {BKSP}" + NL +
                    "BREAK = {BREAK}" + NL +
                    "CAPS LOCK = {CAPSLOCK}" + NL +
                    "DEL or DELETE = {DELETE} or {DEL}" + NL +
                    "DOWN ARROW = {DOWN}" + NL +
                    "END = {END}" + NL +
                    "ENTER = {ENTER}or ~" + NL +
                    "ESC = {ESC}" + NL +
                    "HELP = {HELP}" + NL +
                    "HOME = {HOME}" + NL +
                    "INS or INSERT = {INSERT} or {INS}" + NL +
                    "LEFT ARROW = {LEFT}" + NL +
                    "NUM LOCK = {NUMLOCK}" + NL +
                    "PAGE DOWN = {PGDN}" + NL +
                    "PAGE UP = {PGUP}" + NL +
                    "PRINT SCREEN = {PRTSC}" + NL +
                    "RIGHT ARROW = {RIGHT}" + NL +
                    "SCROLL LOCK = {SCROLLLOCK}" + NL +
                    "TAB = {TAB}" + NL +
                    "UP ARROW = {UP}" + NL +
                    "F1 = {F1}" + NL +
                    "F2 = {F2}" + NL +
                    "F3 = {F3}" + NL +
                    "F4 = {F4}" + NL +
                    "F5 = {F5}" + NL +
                    "F6 = {F6}" + NL +
                    "F7 = {F7}" + NL +
                    "F8 = {F8}" + NL +
                    "F9 = {F9}" + NL +
                    "F10 = {F10}" + NL +
                    "F11 = {F11}" + NL +
                    "F12 = {F12}" + NL +
                    "F13 = {F13}" + NL +
                    "F14 = {F14}" + NL +
                    "F15 = {F15}" + NL +
                    "F16 = {F16}" + NL +
                    "" + NL +
                    "To specify keys combined with any combination of the SHIFT, CTRL, and ALT keys, precede the key code with one or more of the following codes:" + NL +
                    "Key = Code" + NL +
                    "SHIFT = +" + NL +
                    "CTRL = ^" + NL +
                    "ALT = %";
            example = "\\cf3 SendKeys \"This is a test\" \\cf0";
            CreateHelpItem("SendKeys", desc, param, example);

            desc = "\\cf2 SetFocus \\cf0 is used to set focus on a current control. This command requires no parameters.";
            param = "";
            example = "\\cf3 SetFocus \\cf0";
            CreateHelpItem("SetFocus", desc, param, example);

            desc = "\\cf2 TreeItem \\cf0 is used to manipulate tree items.";
            param = "Command requires a single parameter. Possible values are:";
            example = "\\cf3 TreeItem \"Expand\" \\cf0" + NL +
                      "\\cf3 TreeItem \"Collapse\" \\cf0" + NL +
                      "\\cf3 TreeItem \"Select\" \\cf0";
            CreateHelpItem("TreeItem", desc, param, example);

            desc = "\\cf2 UltraTree \\cf0 is used to explore third-party ultra tree control.";
            param = "This command requires two parameters: the first one is the automationID of the control, and the second parameter is the command to execute.";
            example = "\\cf3 UltraTree \"_sidebarTree\", \"ExpandAll\" \\cf0 -- expand all parent nodes in tree control." + NL +
                      "\\cf3 UltraTree \"_sidebarTree\", \"CollapseAll\" \\cf0 -- collapse all parent nodes in tree control.";
            CreateHelpItem("UltraTree", desc, param, example);

            desc = "\\cf2 Wait4Button \\cf0 is used on setup forms if the user waits for a certain button to be available.";
            param = "This command requires three parameters: the first one is the type of search (ByID or ByName) the second one is the automationID of the button, and the third parameter is the timeout value.";
            example = "\\cf3 Wait4Button \"ByID\", \"639\", \"60\" \\cf0";
            CreateHelpItem("Wait4Button", desc, param, example);

            desc = "\\cf2 WindowMove \\cf0 is used to move the target window to a location specified by screen coordinates.";
            param = "";
            example = "\\cf3 WindowMove \"120\", \"200\" \\cf0 ";
            CreateHelpItem("WindowMove", desc, param, example);

            desc = "\\cf2 WindowResize \\cf0 is used to resize the target window.";
            param = "";
            example = "\\cf3 WindowResize \"800\", \"600\" \\cf0";
            CreateHelpItem("WindowResize", desc, param, example);

            desc = "\\cf2 WindoVisualState \\cf0 is used to set the visual state of the target window.";
            param = "";
            example = "\\cf3 WindowVisualState \"Min\" \\cf0 -- minimalizes the target window" + NL +
                      "\\cf3 WindowVisualState \"Max\" \\cf0 -- maximalizes the target window" + NL +
                      "\\cf3 WindowVisualState \"Norm\" \\cf0 -- restores the window to its original size";
            CreateHelpItem("WindowVisualState", desc, param, example);

            desc = "\\cf2 call \\cf0 jumps to a function. Call a subroutine.";
            param = "";
            example = " \\cf3 call CreateUser(\"bot0000\", \"girl\") \\cf0";
            CreateHelpItem("call", desc, param, example);

            desc = "\\cf2 function \\cf0 is a small part of a script that can be called using the call keyword and is executed several times. Functions are typically used when GUI steps are the same, and only the data changes.";
            param = "";
            example = " \\cf3 function CreateUser(@name, @pwd) \\cf0";
            CreateHelpItem("function", desc, param, example);

            desc = "\\cf2 return \\cf0 is used in conjunction with the function keyword. It instructs the Robot to return from a function";
            param = "";
            example = " \\cf3 return \\cf0";
            CreateHelpItem("return", desc, param, example);

            desc = "\\cf2 Label \\cf0 is used to mark a location in a script.";
            param = "This command is used with Goto and requires a single parameter.";
            example = " \\cf3 Label \"here\" \\cf0";
            CreateHelpItem("Label", desc, param, example);

            desc = "\\cf2 Goto \\cf0 is used to jump back in a script. This command requires two parameters; the first one is a location marked with the Label command, and the second parameter is a number that specifies how many times to jump. If the number is zero, jumping will be perfomed an unlimited number of times.";
            param = "";
            example = "\\cf3 Goto \"here\", \"0\" \\cf0 -- jump an unlimited number of times" + NL +
                      "\\cf3 Goto \"here\", \"2\" \\cf0 -- jump twice";
            CreateHelpItem("Goto", desc, param, example);

            desc = "\\cf2 for \\cf0 is used to specify a for loop. A for loop has three parts: initialization, end testing and increment. Each part is required." + NL +
                         "In the example below, @i is equal to zero which is the initialization part." + NL +
                         "Next, the value of @i is tested which is smaller than 30. If it is true, actions are performed inside for loop. If it is false, we jump to loop instruction." + NL +
                         "The last part defines how the loop variable is incremented. In the example below, its value is incremented by one with the ++ command.";
            param = "";
            example = "\\cf3 for (@i=0; @i<30; @i++)" + NL +
                      "\t commands... " + NL +                
                      "loop \\cf0";
            CreateHelpItem("for", desc, param, example);

            desc = "\\cf2 loop \\cf0 is the closing command of the following commands (for, while, foreach)";
            param = "";
            example = " \\cf3 loop \\cf0";
            CreateHelpItem("loop", desc, param, example);

            desc = "\\cf2 if \\cf0 is used to control the flow of the script execution." + NL +
                   "List of supported operators:" + NL +
                    "'==' -- equal to" + NL +
                    "'!=' -- not equal to" + NL +
                    "'>'  -- greater than" + NL +
                    "'<'  -- less than" + NL +
                    "'>=' -- greater than or equal to" + NL +
                    "'<=' -- less than or equal to";
            param = "";
            example = "\\cf3 if @direction == \"forward\" \\cf0 ";
            CreateHelpItem("if", desc, param, example);

            desc = "\\cf2 else \\cf0 is used optionally by the if command to specify an alternative execution path if the condition's result is false.";
            param = "";
            example = "\\cf3 else \\cf0";
            CreateHelpItem("else", desc, param, example);

            desc = "\\cf2 endif \\cf0 is used to close the if control structure. Each if command must be closed using an endif command.";
            param = "";
            example = "\\cf3 endif \\cf0";
            CreateHelpItem("endif", desc, param, example);

            desc = "\\cf2 var \\cf0 is used to define a variable.";
            param = "";
            example = "\\cf3 var @title = \"Login Into Invenio\" \\cf0 -- this line will define a simple variable" + NL +
                      "\\cf3 var @machines = [ \"node1\", \"node2\", \"flanker\", \"test-vm\" ] \\cf0 -- this line will define an array";
            CreateHelpItem("var", desc, param, example);


            desc = "\\cf2 FindList \\cf0 is used to find a list box control in a target window.";
            param = "This command requires one parameter which is the automationID of the control.";
            example = "\\cf3 FindList \"1\" \\cf0";
            CreateHelpItem("FindList", desc, param, example);

            desc = "\\cf2 FindListItem \\cf0 is used to find a list item inside a list box.";
            param = "This command requires one parameter which is the name of the item.";
            example = "\\cf3 FindListItem \"George\" \\cf0";
            CreateHelpItem("FindListItem", desc, param, example);

            desc = "\\cf2 ComboBoxForceSelection \\cf0 is used to make a selection from a third-party combo box.";
            param = "This command has a single parameter which is the name of the item to be selected.";
            example = "\\cf3 ComboBoxForceSelection \"Project\" \\cf0";
            CreateHelpItem("ComboBoxForceSelection", desc, param, example);

            desc = "\\cf2 DataGridGetItems \\cf0 is used to list all items from a DataGrid.";
            param = "Command does not require parameter.";
            example = "\\cf3 DataGridGetItems \\cf0 ";
            CreateHelpItem("DataGridGetItems", desc, param, example);

            desc = "\\cf2 FindSplitButton \\cf0 is used to find a split button. They are typically found in a combo box ";
            param = "This command has a single parameter which is the name of the button.";
            example = "\\cf3 FindSplitButton \"Open\" \\cf0 ";
            CreateHelpItem("FindSplitButton", desc, param, example);

            desc = "\\cf2 #import \\cf0 is used to import external script file.";
            param = "One required parameter. The path to the script file to be imported.";
            example = "\\cf3 #import \"data\\\\scripts\\\\test\\\\Functions.scp\"  \\cf0";
            CreateHelpItem("#import", desc, param, example);

            desc = "\\cf2 FindPane \\cf0 is used to find pane control on target window.";
            param = "One required parameter. The automation id of the control.";
            example = "\\cf3 FindPane \"btnPlay\"  \\cf0";
            CreateHelpItem("FindPane", desc, param, example);

            desc = "\\cf2 UserInput \\cf0 is used to prompt the user to enter a value.";
            param = "This command requires two parameters; the first one is the prompt text and the second one is the variable name that indicates where to insert the value entered.";
            example = "\\cf3 UserInput \"enter window title:\", @window  \\cf0";
            CreateHelpItem("UserInput", desc, param, example);
            
            desc = "\\cf2 TipSetup \\cf0 is used to set common parameters for tips displayed on user interface by QABOT.";
            param = "This command requires two parameters; the first one is the parameters name and the second one is the parameters value.";
            example = "\\cf3 TipSetup \"FontFamily\", \"Arial\" \\cf0";
            CreateHelpItem("TipSetup", desc, param, example);

            desc = "\\cf2 TipClient \\cf0 is used to display tips on user interface using client coordinates.";
            param = "This command requires two parameters; the first one is the time in miliseconds to display tip and the second one is the text what to display.";
            example = "\\cf3 TipClient \"3000\", \"Text to display\" \\cf0";
            CreateHelpItem("TipClient", desc, param, example);

            desc = "\\cf2 TipScreen \\cf0 is used to display tips on user interface using screen coordinates.";
            param = "This command requires two parameters; the first one is the time in miliseconds to display tip and the second one is the text what to display.";            
            example = "\\cf3 TipScreen \"3000\", \"Text to display\" \\cf0";
            CreateHelpItem("TipScreen", desc, param, example);

            desc = "\\cf2 TipOn \\cf0 is used to display tips on user interface using the last selected control on target window as position.";
            param = "This command requires two parameters; the first one is the time in miliseconds to display tip and the second one is the text what to display.";            
            example = "\\cf3 TipOn \"3000\", \"Text to display\" \\cf0";
            CreateHelpItem("TipOn", desc, param, example);
            
            desc = "\\cf2 Replace \\cf0 is used to replace text in textual files.";
            param = "This command requires three parameters; the first one is the full path to the file, second one is the value what to find, and the third is the new value.";
            example = "\\cf3 Replace \"C:\\\\test.config\", \"display=LOW\", \"display=HIGH\" \\cf0";
            CreateHelpItem("Replace", desc, param, example);

            desc = "\\cf2 XMLRead \\cf0 is used to read values from XML file.";
            param = "This command requires three parameters; the first one is the full path to the file, second one is the xpath to the value, and the third is the script level variable where to copy value.";
            example = "\\cf3 XMLRead \"c:\\\\Harris\\\\Invenio\\\\app\\\\dam3gui\\\\Invenio.exe.config\", \"/configuration/system.serviceModel/client/endpoint[1]/@address\", @value  \\cf0";
            CreateHelpItem("XMLRead", desc, param, example);
                               
            desc = "\\cf2 FindText \\cf0 is used to find text control on the target form.";
            param = "This command requires one parameters; the actual text of the control.";
            example = "\\cf3 FindText \"Domain:\"  \\cf0";
            CreateHelpItem("FindText", desc, param, example);
            	        
            desc = "\\cf2 FindTextBoxUnder \\cf0 is used to find TextBox closest to a control currently selected.";
            param = "This command requires no parameters.";
            example = "\\cf3 FindTextBoxUnder \\cf0";
            CreateHelpItem("FindTextBoxUnder", desc, param, example);

            desc = "\\cf2 FindByClientPos \\cf0 is used to find a Control based on it's client coordinates.";
            param = "This command has two optional parameters. X and Y client coordinates. If there is no parameters, then command will use the mouse current position.";
            example = "\\cf3 FindByClientPos \"200\", \"75\" \\cf0 -- this command will try to find a control at the given client cooridates" + NL +
                      "\\cf3 FindByClientPos \\cf0 -- this command will try to find control under current mouse position";
            CreateHelpItem("FindByClientPos", desc, param, example);
            
            desc = "\\cf2 UserInputYesNo \\cf0 is used to display a dialog to the user to select an answer to a Yes/No question.";
            param = "This command requires two parameters. The question and the variable where to place the answer.";
            example = "\\cf3 UserInputYesNo \"Are you sure you want to start?\", @answer \\cf0";
            CreateHelpItem("UserInputYesNo", desc, param, example);
            
            desc = "\\cf2 Launch \\cf0 is used to launch application at the given path.";
            param = "This command has two parameters." + NL +
                    "First, (required) Path to the application." + NL +
                    "Second, (optional) Command line arguments.";
            example = "\\cf3 Launch \"c:\\\\Harris\\\\Invenio\\\\app\\\\dam3gui\\\\Invenio.exe\" \\cf0" + NL +
                      "\\cf3 Launch \"notepad.exe\", \"desc.txt\" \\cf0";
            CreateHelpItem("Launch", desc, param, example);

            desc = "\\cf2 TreeNavigate \\cf0 is used to do a navigation in tree control in a single call.";
            param = "This command requires one parameter. Folder names separated with '|' character.";
            example = "\\cf3 TreeNavigate \"Users|Admin|Inbox\" \\cf0";
            CreateHelpItem("TreeNavigate", desc, param, example);
            
            desc = "\\cf2 ScrollIntoView \\cf0 is used to scroll a DataGridItem into a visible area of the Grid control.";
            param = "This command requires no parameters.";
            example = "\\cf3 ScrollIntoView \\cf0";
            CreateHelpItem("ScrollIntoView", desc, param, example);
            
            desc = "\\cf2 foreach \\cf0 is used to iterate on each element of a array.";
            param = "This command requires no parameters.";
            example = "\\cf3 foreach @m in @machines " + NL +
                      "\t commands... " + NL +
                      "loop \\cf0";
            CreateHelpItem("foreach", desc, param, example);            

            desc = "\\cf2 SelectDataGridItem \\cf0 - this command is used to select a row from a datagrid control.";
            param = "This command requires one parameter. Zero based index of the row.";
            example = "\\cf3 SelectDataGridItem \"0\" \\cf0 -- this command selects the first row from the datagrid control.";
            CreateHelpItem("SelectDataGridItem", desc, param, example);
            
            desc = "\\cf2 WindowGetSize \\cf0 - this command is used to get the size of the window.";
            param = "This command requires two parameters(variables). Where to put dimensions, widht and height.";
            example = "\\cf3 WindowGetSize @widht, @height \\cf0";
            CreateHelpItem("WindowGetSize", desc, param, example);
            
            desc = "\\cf2 while \\cf0 - conditional loop command.";
            param = "This command requires and expression to evaluate (result true/false)." + NL +
                    "List of supported operators:" + NL +
                    "'==' -- equal to" + NL +
                    "'!=' -- not equal to" + NL +
                    "'>'  -- greater than" + NL +
                    "'<'  -- less than" + NL +
                    "'>=' -- greater than or equal to" + NL +
                    "'<=' -- less than or equal to";                        
            example = "\\cf3 while (@val != \"today\") " + NL +
                      "\tcommands..." + NL +                             
                      "loop \\cf0"; 
            CreateHelpItem("while", desc, param, example);

            desc = "\\cf2 break \\cf0 - command is used to exit from loop structure (for, while, foreach).";
            param = "This command requires no parameters.";
            example = "\\cf3 while (@val != \"today\") " + NL +
                      "\tbreak" + NL +
                      "loop \\cf0";
            CreateHelpItem("break", desc, param, example);
            
            desc = "\\cf2 continue \\cf0 - command is used to jump to next iteration of the loop structure (for, while, foreach).";
            param = "This command requires no parameters.";
            example = "\\cf3 while (@val != \"today\") " + NL +
                      "\tcontinue" + NL +
                      "loop \\cf0";
            CreateHelpItem("continue", desc, param, example);
            
            desc = "\\cf2 GetDate \\cf0 - this command is used to get the actual date time on the system.";
            param = "This command requires two parameters. First is the mask and the second on the variable where to put returned date time."  + NL +
                    "\"y yy yyy yyyy\"  -> \"8 08 008 2008\"                       year" + NL +
                    "\"M MM MMM MMMM\"  -> \"3 03 Mar March\"                      month" + NL +
                    "\"d dd ddd dddd\"  -> \"9 09 Sun Sunday\"                     day" + NL +
                    "\"h hh H HH\"      -> \"4 04 16 16\"                          hour 12/24" + NL +
                    "\"m mm\"           -> \"5 05\"                                minute" + NL +
                    "\"s ss\"           -> \"7 07\"                                second" + NL +
                    "\"f ff fff ffff\"  -> \"1 12 123 1230\"                       sec.fraction" + NL +
                    "\"F FF FFF FFFF\"  -> \"1 12 123 123\"                        without zeroes" + NL +
                    "\"t tt\"           -> \"P PM\"                                A.M. or P.M." + NL +
                    "\"z zz zzz\"       -> \"-6 -06 -06:00\"                       time zone" + NL +
                    "\"t\"              -> \"4:05 PM\"                             ShortTime" + NL +
                    "\"d\"              -> \"3/9/2008\"                            ShortDate" + NL +
                    "\"T\"              -> \"4:05:07 PM\"                          LongTime" + NL +
                    "\"D\"              -> \"Sunday, March 09, 2008\"              LongDate" + NL +
                    "\"f\"              -> \"Sunday, March 09, 2008 4:05 PM\"      LongDate+ShortTime" + NL +
                    "\"F\"              -> \"Sunday, March 09, 2008 4:05:07 PM\"   FullDateTime" + NL +
                    "\"g\"              -> \"3/9/2008 4:05 PM\"                    ShortDate+ShortTime" + NL +
                    "\"G\"              -> \"3/9/2008 4:05:07 PM\"                 ShortDate+LongTime" + NL +
                    "\"m\"              -> \"March 09\"                            MonthDay" + NL +
                    "\"y\"              -> \"March, 2008\"                         YearMonth" + NL +
                    "\"r\"              -> \"Sun, 09 Mar 2008 16:05:07 GMT\"       RFC1123" + NL +
                    "\"s\"              -> \"2008-03-09T16:05:07\"                 SortableDateTime" + NL +
                    "\"u\"              -> \"2008-03-09 16:05:07Z\"                UniversalSortableDateTime";
            example = "\\cf3 GetDate \"MM/dd/yyyy\", @date \\cf0";
            CreateHelpItem("GetDate", desc, param, example);




            desc = "\\cf2 GetCellValue \\cf0 - command is used to extract a value from a given cell to a given variable.";
            param = "This command requires three parameters. the row number, the column name, and the variable where to put value";
            example = "\\cf3 GetCellValue 1, \"Created on\", @value \\cf0";                                            
            CreateHelpItem("GetCellValue", desc, param, example);


            desc = "\\cf2 SetCellValue \\cf0 - command is used to set new value from a given cell from a given variable.";
            param = "This command requires three parameters. the row number, the column name, and the variable from where to read the new cell value";
            example = "\\cf3 SetCellValue 1, \"Created on\", @value \\cf0";
            CreateHelpItem("SetCellValue", desc, param, example);
            
            desc = "\\cf2 FindLastDataItem \\cf0 - command is used to select the last item in DataGrid.";
            param = "This command requires no parameters.";
            example = "\\cf3 FindLastDataItem \\cf0";
            CreateHelpItem("FindLastDataItem", desc, param, example);


            desc = "\\cf2 GetDataGridItemCount \\cf0 - command is used to get the number of items in DataGrid.";
            param = "This command requires one parameter. The variable where to put the result.";
            example = "\\cf3 GetDataGridItemCount @itemCount \\cf0";
            CreateHelpItem("GetDataGridItemCount", desc, param, example);
            
            
            desc = "\\cf2 Len \\cf0 - command is used to get the length of the string stored in a variable passed to this functiona as a parameter.";
            param = "This command requires one parameter. The variable where to look for a string.";
            example = "\\cf3 @lenght = Len(@text) \\cf0";
            CreateHelpItem("Len", desc, param, example);


            desc = "\\cf2 SubString \\cf0 - command is used to return a substring from a given string.";
            param = "This command has three parameters. First two is required and the third is optional. First is a source string, second is the starting index (zero based), third is the length.";
            example = "\\cf3 @result = SubString(@text, 0) \\cf0 -- this command will return the original string." + NL +
                      "\\cf3 @result = SubString(@text, 1) \\cf0 -- this command will return the original string without the first character." + NL +
                      "\\cf3 @result = SubString(@text, 0, 3) \\cf0 -- this command will return the first three characters from the string.";
                      
            CreateHelpItem("SubString", desc, param, example);

            
            desc = "\\cf2 FindByType \\cf0 - command is used to find a control on a form by it's type";
            param = "This command requires one parameter. The type of the control (possible types are enumerated as string constants beginning with \"UI-\").";
            example = "\\cf3 FindByType \"UI-Button\" \\cf0";
            CreateHelpItem("FindByType", desc, param, example);

            
            desc = "\\cf2 FindHeader \\cf0 - command is used to find a header control by it's automationID property";
            param = "This command requires one parameter. The AutomationID.";
            example = "\\cf3 FindHeader \"Header\" \\cf0";
            CreateHelpItem("FindHeader", desc, param, example);


            desc = "\\cf2 FindHeaderItem \\cf0 - command is used to find a HeaderItem control by it's Name property";
            param = "This command requires one parameter. The Name (text) on it.";
            example = "\\cf3 FindHeaderItem \"Created on\" \\cf0";
            CreateHelpItem("FindHeaderItem", desc, param, example);


            desc = "\\cf2 mouse.* \\cf0 commands used to perform mouse clicks based on parameters.";
            param = "";
            example = "\\cf3 mouse.ClickOn \\cf0 -- used to perform a real mouse click on a button or another GUI element that has been found in a previous step. Requires no parameters." + NL +
                      "\\cf3 mouse.ClickClient 300, 100 \\cf0 -- performs a mouse click operation inside a window. Coordinates are defined in client coordinates." + NL +
                      "\\cf3 mouse.ClickScreen 1020, 10 \\cf0 -- performs a mouse click based on screen coordinates." + NL +
                      "\\cf3 mouse.RightClickOn \\cf0 -- performs a right mouse click on a GUI element that has been found in a previous step. Typically used to open a context menu." + NL +
                      "\\cf3 mouse.RightClickClient \\cf0 -- performs a mouse click (with the right button) operation inside a window. Coordinates are defined in client coordinates." + NL +
                      "\\cf3 mouse.RightClickScreen \\cf0 -- performs a mouse click (with the right button) based on screen coordinates." + NL +
                      "\\cf3 mouse.DoubleClickOn \\cf0 -- performs double-click on a GUI element that has been found in a previous step." + NL +
                      "\\cf3 mouse.DoubleClickClient 100, 256 \\cf0 -- performs double-click based on client coordinates." + NL +
                      "\\cf3 mouse.DoubleClickScreen 1000, 512 \\cf0 -- performs double-click based on screen coordinates." + NL +
                      "\\cf3 mouse.SetDragSource \\cf0 -- sets the drag source GUI element." + NL +
                      "\\cf3 mouse.SetDropSource \\cf0 -- sets the drop target GUI element." + NL +
                      "\\cf3 mouse.DoDragAndDrop \\cf0 -- performs the drag and drop action.";
            CreateHelpItem("mouse", desc, param, example);


            desc = "\\cf2 datetime.* \\cf0 - command is used to Add or Subtract Years, Months, Days, Hours, Minutes, Seconds from a variable which holds a datetime value.";
            param = "This command requires three parameters. The amount what we want to add or subtract, the format of the date to be written, and the variable which contains the date that we want to modify.";
            example = "\\cf3 datetime.AddDays 1, \"MM/dd/yyyy\", @date \\cf0 -- this command adds One Day to the Date stored in variable @date." + NL + 
                      "\\cf3 datetime.AddMonths 1, \"MM/dd/yyyy\", @date \\cf0" + NL + 
                      "\\cf3 datetime.AddYears 1, \"MM/dd/yyyy\", @date \\cf0" + NL + 
                      "\\cf3 datetime.AddHours -1, \"G\", @date \\cf0";
            CreateHelpItem("datetime", desc, param, example);


            desc = "\\cf2 register \\cf0 - command is used to register a function to get executed afeter an Event. Like (OnError, OnQuit)";
            param = "This command requires one parameter. The name of the function. \"+=\" register. \"-=\" unregister.";
            example = "\\cf3 register.OnError += HandleError \\cf0 -- this command will register function HandleError" + NL +
                      "\\cf3 register.OnError -= HandleError \\cf0 -- this command will unregister function HandleError" + NL +
                      "\\cf3 register.OnQuit += HandleQuit \\cf0 -- this command will register function HandleQuit" + NL +
                      "\\cf3 register.OnQuit -= HandleQuit \\cf0 -- this command will unregister function HandleQuit";

            CreateHelpItem("register", desc, param, example);




            desc = "\\cf2 FindMenuBar \\cf0 - command is used to find the menubar.";
            param = "This command requires one parameter. The AutomationID of the MenuBar.";
            example = "\\cf3 FindMenuBar \"menuStrip1\" \\cf0";

            CreateHelpItem("FindMenuBar", desc, param, example);


            desc = "\\cf2 FindToolbarButtonByIndex \\cf0 - command is used to find a toolbarbutton by it's index.";
            param = "This command requires one parameter. The index of the button from right to left (zero based index).";
            example = "\\cf3 FindToolbarButtonByIndex 0 \\cf0 -- this command will select the first button in the toolbar.";
            CreateHelpItem("FindToolbarButtonByIndex", desc, param, example);



            desc = "\\cf2 FindToolbarMenuItem \\cf0 - command is used to find a toolbarmenuitem by it's text.";
            param = "This command requires one parameter. The text of the menu item.";
            example = "\\cf3 FindToolbarMenuItem \"Save As\" \\cf0 -- this command will select the Save As menu item from the toolbar.";
            CreateHelpItem("FindToolbarMenuItem", desc, param, example);



            desc = "\\cf2 FindCustom \\cf0 is used to find a custom in a window.";
            param = "This command requires two parameters. One of the parameters defines how to find the custom. There are two possible values: \"ByID\" and \"ByName\". \"ByID\" refers to the automationID, and \"ByName\" refers to the actual text on the custom.";
            example = "\\cf3 FindCustom \"ByID\", \"cmdOk\" \\cf0 This command finds a custom by its automationID\r\n" +
                      "\\cf3 FindCustom \"ByName\", \"OK\" \\cf0 This command finds a custom by custom text";
            CreateHelpItem("FindCustom", desc, param, example);

            
            desc = "\\cf2 table.* \\cf0 - command is used to Add or Subtract Years, Months, Days, Hours, Minutes, Seconds from a variable which holds a datetime value.";
            param = "This command requires three parameters. The amount what we want to add or subtract, the format of the date to be written, and the variable which contains the date that we want to modify.";
            example = "\\cf3 table.GetRowCount @rowCount \\cf0 -- this command will count rows in table and place the value into a variable." + NL +
                      "\\cf3 table.SetCellValue 1, \"Name\", \"Muller\" \\cf0 -- this command will set cell's new text value, require three parameters, row index (zero based), column name, new value to set." + NL +
                      "\\cf3 table.GetCellValue 1, \"Name\", @value \\cf0 -- this command will get the value from the cell, require three parameters, row index (zero based), column name, variable where to put value from the cell.";                      
            CreateHelpItem("table", desc, param, example);
            

            desc = "\\cf2 const \\cf0 is used to define a constant.";
            param = "";
            example = "\\cf3 const @title = \"Login Into Invenio\" \\cf0 -- this line will define a simple constant variable. It's value later cannot be modified."; // +NL +
                      /*"\\cf3 const @machines = [ \"node1\", \"node2\", \"flanker\", \"test-vm\" ] \\cf0 -- this line will define an array";*/
            CreateHelpItem("const", desc, param, example);


            desc = "\\cf2 sql.* \\cf0 commands related to access SQL Server.";
            param = "";
            example = "\\cf3 sql.Connect @connectionString \\cf0 -- this line will create a connection to the SQL Server from a given connection string variable." + NL +
                      "\\cf3 sql.Disconnect \\cf0 -- this line will disconnect from the database server" + NL +
                      "\\cf3 sql.ExecuteNonQuery @sqlExpression \\cf0 -- this line will execute the SQL statement in a variable and return the number of affected row count." + NL +
                      "\\cf3 sql.ExecuteReader @sqlExpression \\cf0 -- this line will create an SQL Reader object. " + NL +
                      "\\cf3 sql.FieldCount \\cf0 -- this instruction will return the number of fields in a SQL Reader object " + NL +
                      "\\cf3 sql.fields[Name] \\cf0 or \\cf3 sql.fields[@colName] \\cf0 or \\cf3 sql.fields[0] \\cf0 -- this command is used to access fields in a row returned from a sql.ExecuteReader" + NL +
                      "\\cf3 sql.read \\cf0 -- this line will get the next row from SQL Reader object";

            CreateHelpItem("sql", desc, param, example);

            
            desc = "\\cf2 severity \\cf0 is used to define how script outcome is to be measured.";
            param = "command requires no parameters.";
            example = "\\cf3 severity.minor \\cf0 -- indicates issues that do not affect functionality or usability significantly" + NL +
                      "\\cf3 severity.major \\cf0 -- indicates issues that affect functionality and usability considerably" + NL +
                      "\\cf3 severity.critical \\cf0 -- indicates issues that cause system crashes, workflow breakdowns and loss of focus for a specific task" + NL +
                      "\\cf3 severity.blocking \\cf0 -- indicates issues that prevent further use";                      

            CreateHelpItem("severity", desc, param, example);



            desc = "\\cf2 FindImage \\cf0 is used to find images on the screen";
            param = "Command requires one parameter the path to the image that we want to find";
            example = "\\cf3 FindImage @pathToImage \\cf0";
            CreateHelpItem("FindImage", desc, param, example);


            desc = "\\cf2 GetWindowTitle \\cf0 is used get the current window's title into a variable";
            param = "Command requires one parameter the variable name where to put the window  name";
            example = "\\cf3 GetWindowTitle @title \\cf0";
            CreateHelpItem("GetWindowTitle", desc, param, example);





            desc = "\\cf2 FindTextLike \\cf0 is used to find text on the UI starting with param given";
            param = "Command requires one parameter the starting text";
            example = "\\cf3 FindTextLike \"Welcome to\" \\cf0";
            CreateHelpItem("FindTextLike", desc, param, example);




            desc = "\\cf2 FindComboBoxByName \\cf0 is used to find ComboBox by name";
            param = "Command requires one parameter the name of the control";
            example = "\\cf3 FindComboBoxByName \"Name:\" \\cf0";
            CreateHelpItem("FindComboBoxByName", desc, param, example);

            


        }





    }
}