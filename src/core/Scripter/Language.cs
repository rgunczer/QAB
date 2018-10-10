using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace Scripter
{
    public static class Language
    {
        public static int LongestKeyWordLength = 0;
        public static int ShortestKeyWordLength = 0;
        public static Dictionary<string, SyntaxData> SubCommands = new Dictionary<string, SyntaxData>();

        public static string[] StringConsts =
        {
            "ByID",
            "ByName",
            "ByTitleLike",            
            "ByTitle",
            "On",
            "Off",
            "Indeter",
            "Max",            
            "Expand",
            "Collapse",
            "Select",
            "ExpandAll",
            "CollapseAll",
            "FontFamily",
            "FontSize",
            "FontStyle",
            "FontColor",
            "BorderWidth",
            "BorderColor",
            "LeftColor",
            "RightColor", 
            "True",
            "False",
            "UI-Button",
            "UI-Edit",
            "UI-ComboBox",
            "UI-CheckBox",
            "UI-DataGrid",
            "UI-DataItem",
            "UI-RadioButton",
            "UI-Table",
            "UI-Menu",
            "UI-MenuItem",
            "UI-ToolBar",
            "UI-Tab",
            "UI-TabItem",
            "UI-Text",
            "UI-Window",
            "UI-Tree",
            "UI-Document",
            "UI-List",
            "UI-ListItem",
            "UI-SplitButton",
            "UI-Pane",
            "UI-HyperLink",
            //"UI-Calendar", // unsupported!?
            "UI-Header",
            "UI-HeaderItem",
        };
        
        public static string[] commands = 
        {
            // find
            "FindWindow",
            "FindWindowLike",
            "FindTextBox",
            "FindComboBox",
            "FindButton",
            "FindCheckBox",
            "FindChildWindow",
            "FindDataGrid",
            "FindDataGridItem",
            "FindDocument",
            "FindMenu",
            "FindMenuItem",
            "FindProcess",
            "FindRadioButton",
            "FindTab",
            "FindTabItem",
            "FindTable",
            "FindToolbar",
            "FindToolbarButton",
            "FindTree",
            "FindTreeItem",
            "FindList",
            "FindListItem",
	        "FindText",
	        "FindTextBoxUnder",
            "FindByClientPos",

            // pattern
            "Invoke",
            "Select",
            "Value",
            "CloseWindow",

            // test
            "TestDataGridItem",
            "TestDocumentNotContain",
            "TestEditBox",
            "TestWindowMessage",

            // cache
            "Cache",
            "GetFromCache",
            "CacheWindow",
            "GetWindowFromCache",

            "ClickOnWindow",
            "CheckBoxToggle",
            "ClickUltraTreeItemChild",
            "ClickUltraTreeItemRoot",
            "ComboBoxMakeSelection",
            "CopyFile",

            // new era!
            "log",            
            "sys",

            "Screenshot",
            "SendKeys",
            "SetFocus",
            "TreeItem",
            "UltraTree",
            "Wait4Button",
            "WindowMove",
            "WindowResize",
            "WindowVisualState",

            // script flow commands            

            "call",
            "function",
            "return",

            "Label",
            "Goto",
            
            "for",
            "loop",
            
            "if",
            "else",
            "endif",

            "var",

            
            "ComboBoxForceSelection",
            "DataGridGetItems",
            "FindSplitButton",
            "#import",
            "FindPane",
            "UserInput",
            "TipSetup",
            "TipClient",
            "TipScreen",
            "TipOn",
            "Replace",
            "XMLRead",
            "UserInputYesNo",
            "Launch", 
            "TreeNavigate",   
            "ScrollIntoView",
            "foreach",            
            "SelectDataGridItem",
            "keyboard",
            "env",
            "register",
            "mouse",
            "Random", 
            "WindowGetSize",
            "while",
            "break",
            "continue",
            "GetDate",
            "GetCellValue",
            "SetCellValue",
            "FindLastDataItem",
            "GetDataGridItemCount",
            "Len",
            "SubString",
            "FindByType",
            "FindHeader",
            "FindHeaderItem",   
            "datetime",
            "FindMenuBar",
            "FindToolbarButtonByIndex",
            "FindCustom",

            "sql",
            "table",
            "FindToolbarMenuItem",
            "const",
            "severity",
            "FindImage",
            "report",

            "GetWindowTitle",
            "FindTextLike",
            "FindComboBoxByName",
        };

        static Language()
        {
            int longest = -1;
            int shortest = 1000;

            foreach (string s in commands)
            {
                if (s.Length > longest)
                    longest = s.Length;

                if (s.Length < shortest)
                    shortest = s.Length;
            }

            LongestKeyWordLength = longest;
            ShortestKeyWordLength = shortest;

              
            SubCommands.Add("sys", new SyntaxData("sys.",
                                   new string[] { "start", "norm", "compact", "uncompact", "quit", "speed", "stop", "wait", "min", "SaveScreenshotOnError", "record" },
                                   Color.LightGreen,
                                   "System Sub Command"));

            SubCommands.Add("env", new SyntaxData("env.",
                                   new string[] { "PathRoot", "PathCurrent", "ScreenWidht", "ScreenHeight" },
                                   Color.Gold,
                                   "Environment Sub Command"));

            SubCommands.Add("log", new SyntaxData("log.",
                                   new string[] { "write", "path", "level", "clear" },
                                   Color.Aquamarine,
                                   "Log Sub Command"));

            SubCommands.Add("keyboard", new SyntaxData("keyboard.",
                                        new string[] { "LeftCtrlDown", "LeftCtrlUp" },
                                        Color.SandyBrown,
                                        "Keyboard Sub Command"));

            SubCommands.Add("register", new SyntaxData("register.",
                                        new string[] { /*"AfterEachCommand", "AfterCommands",*/ "OnError", "OnQuit" },
                                        Color.Aquamarine,
                                        "Register Sub Command"));

            SubCommands.Add("mouse", new SyntaxData("mouse.", 
                                     new string[] { "RightClickOn", "RightClickClient", "RightClickScreen",
                                                    "ClickOn", "ClickScreen", "ClickClient", 
                                                    "DoubleClickOn", "DoubleClickClient", "DoubleClickScreen", 
                                                    "SetDragSource", "SetDropSource", "DoDragAndDrop",
                                                   },
                                     Color.Gainsboro,
                                     "Mouse Sub Command"));

            SubCommands.Add("datetime", new SyntaxData("datetime.", 
                                        new string[] { "AddDays", "AddMonths",  "AddYears",
                                                       "AddHours", "AddMinutes", "AddSeconds",                                                                              
                                                      },
                                        Color.Peru,
                                        "DateTime Sub Command"));


            SubCommands.Add("sql", new SyntaxData("sql.",
                                   new string[] { "Connect", "Disconnect", "ExecuteReader", "ExecuteNonQuery", "read", "fields", "FieldCount" },
                                   Color.CadetBlue,
                                   "SQL Sub Command"));


            SubCommands.Add("table", new SyntaxData("table.",
                                   new string[] { "GetRowCount", "SetCellValue", "GetCellValue" },
                                   Color.CadetBlue,
                                   "Table Sub Command"));


            SubCommands.Add("severity", new SyntaxData("severity.",
                                   new string[] { "minor", "major", "critical", "blocking" },
                                   Color.Yellow,
                                   "Severity Type"));


            SubCommands.Add("report", new SyntaxData("report.",
                                   new string[] { "FunctionalityToTest", "ExpectedResult", "Comments" },
                                   Color.YellowGreen,
                                   "Report Fields"));





        }

    }
}