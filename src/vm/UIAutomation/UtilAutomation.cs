using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using Util;


namespace VM
{
    static class UtilAutomation
    {
        private static VirtualMachine _vm = null;
        public static VirtualMachine vm
        {
            set { _vm = value; }
        }

        private static List<AutomationElement> aeList = new List<AutomationElement>();

        public static AutomationElement GetElemFromList(string Name)
        {           
            foreach (AutomationElement item in aeList)
            {
                _vm.host.UpdateMarker(item.Current.BoundingRectangle);
                UtilSys.Wait(200);

                //if (item.Current.Name == Name)
                  //  return item;
            }
            return null;
        }


        public static bool ValidClickPos(System.Windows.Point clickablePoint)
        {
            if (0 == (int)clickablePoint.X && 0 == (int)clickablePoint.Y)
            {
                return (false);
            }
            return (true);
        }

        public static Point Convert2Screen(AutomationElement window, System.Windows.Point ptClient)
        {
            Point ptScreen = new Point();
            IntPtr handle = new IntPtr(window.Current.NativeWindowHandle);

            Point point = new Point();
            point.X = (int)ptClient.X;
            point.Y = (int)ptClient.Y;

            NativeMethods.ClientToScreen(handle, ref point);

            ptScreen.X = point.X;
            ptScreen.Y = point.Y;

            return ptScreen;
        }

        public static Point Convert2Client(AutomationElement window, System.Windows.Point ptClient)
        {
            Point ptScreen = new Point();
            IntPtr handle = new IntPtr(window.Current.NativeWindowHandle);

            Point point = new Point();
            point.X = (int)ptClient.X;
            point.Y = (int)ptClient.Y;

            NativeMethods.ScreenToClient(handle, ref point);

            ptScreen.X = point.X;
            ptScreen.Y = point.Y;

            return ptScreen;
        }


        public static void ClientClick(AutomationElement window, System.Windows.Point pos, bool doubleClick)
        {
            Debug.Assert(null != window);

            IntPtr handle = new IntPtr(window.Current.NativeWindowHandle);

            Point point = new Point();
            point.X = (int)pos.X;
            point.Y = (int)pos.Y;
            NativeMethods.ClientToScreen(handle, ref point);

            System.Windows.Point p = new System.Windows.Point();
            p.X = point.X;
            p.Y = point.Y;

            SetCursorPos(p);
            SendLeftClick();

            if (doubleClick)
            {
                System.Threading.Thread.Sleep(10);
                SendLeftClick();
            }
        }


        public static void ClientRightClick(AutomationElement window, System.Windows.Point pos, bool doubleClick)
        {
            Debug.Assert(null != window);

            IntPtr handle = new IntPtr(window.Current.NativeWindowHandle);

            Point point = new Point();
            point.X = (int)pos.X;
            point.Y = (int)pos.Y;
            NativeMethods.ClientToScreen(handle, ref point);

            System.Windows.Point p = new System.Windows.Point();
            p.X = point.X;
            p.Y = point.Y;

            SetCursorPos(p);
            SendRightClick();

            if (doubleClick)
            {
                System.Threading.Thread.Sleep(10);
                SendRightClick();
            }
        }
        

        public static void SetClientCurosPos(AutomationElement window, System.Windows.Point pos)
        {
            Debug.Assert(null != window);

            IntPtr handle = new IntPtr(window.Current.NativeWindowHandle);

            Point point = new Point();
            point.X = (int)pos.X;
            point.Y = (int)pos.Y;
            NativeMethods.ClientToScreen(handle, ref point);

            System.Windows.Point p = new System.Windows.Point();
            p.X = point.X;
            p.Y = point.Y;

            SetCursorPos(p);
        }


        public static void ScreenClick(System.Windows.Point pos, bool doubleClick)
        {
            SetCursorPos(pos);
            SendLeftClick();

            if (doubleClick)
            {
                System.Threading.Thread.Sleep(10);
                SendLeftClick();
            }
        }

        public static void ScreenRightClick(System.Windows.Point pos, bool doubleClick)
        {
            SetCursorPos(pos);
            SendRightClick();

            if (doubleClick)
            {
                System.Threading.Thread.Sleep(10);
                SendRightClick();
            }
        }        
        
        public static void Expand(AutomationElement item)
        {
            try
            {
                ExpandCollapsePattern pattern = item.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
                pattern.Expand();
            }
            catch
            {
                throw;
            }            
        }

        public static bool Collapse(AutomationElement item)
        {
            ExpandCollapsePattern pattern = item.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
            pattern.Collapse();

            return (true);
        }
     
        public static void MouseHover(AutomationElement ae)
        {
            Debug.Assert(null != ae);

            System.Windows.Point ptTarget = new System.Windows.Point();

            //_vm.host.WriteLog("In MouseHover");

            //_vm.host.WriteLog(ae.Current.Name);
            //_vm.host.WriteLog("X: " + ae.Current.BoundingRectangle.X.ToString());
            //_vm.host.WriteLog("Widht: " + ae.Current.BoundingRectangle.Width.ToString());
            //_vm.host.WriteLog("Y: " + ae.Current.BoundingRectangle.Y.ToString());
            //_vm.host.WriteLog("Height: " + ae.Current.BoundingRectangle.Height.ToString());

            ptTarget.X = ae.Current.BoundingRectangle.X + (ae.Current.BoundingRectangle.Width / 2);
            ptTarget.Y = ae.Current.BoundingRectangle.Y + (ae.Current.BoundingRectangle.Height / 2);

            //_vm.host.WriteLog("Target X: " + ptTarget.X.ToString());
            //_vm.host.WriteLog("Target Y: " + ptTarget.Y.ToString());

            SetCursorPos(ptTarget);            
        }

        public static bool ClickOn(System.Windows.Rect rc, bool doubleClick)
        {                                   
            System.Windows.Point ptTarget = new System.Windows.Point();
            
            ptTarget.X = rc.X + (rc.Width / 2);
            ptTarget.Y = rc.Y + (rc.Height / 2);

            //_vm.host.WriteLog("Target X: " + ptTarget.X.ToString());
            //_vm.host.WriteLog("Target Y: " + ptTarget.Y.ToString());

            SetCursorPos(ptTarget);
            
            SendLeftClick();

            if (doubleClick)
            {
                System.Threading.Thread.Sleep(10);
                SendLeftClick();
            }            

            return (true);
        }

        public static System.Windows.Point RightClickOn(System.Windows.Rect rc)
        {
            System.Windows.Point pt;
            System.Windows.Point ptTarget = new System.Windows.Point();

            ptTarget.X = rc.X + (rc.Width / 2);
            ptTarget.Y = rc.Y + (rc.Height / 2);

            SetCursorPos(ptTarget);
            
            pt = ptTarget;

            SendRightClick();

            return pt;
        }

        public static void MouseLeftDown()
        {            
            const int MOUSEEVENTF_LEFTDOWN = 0x02;

            int x = 0;
            int y = 0;

            NativeMethods.mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
        }

        public static void MouseLeftUp()
        {            
            const int MOUSEEVENTF_LEFTUP = 0x04;

            int x = 0;
            int y = 0;

            NativeMethods.mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }

        private static void SendLeftClick()
        {         
            const int MOUSEEVENTF_LEFTDOWN = 0x02;
            const int MOUSEEVENTF_LEFTUP = 0x04;
    
            int x = 0;
            int y = 0;

            NativeMethods.mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            NativeMethods.mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }

        private static void SendRightClick()
        {
            const int MOUSEEVENTF_RIGHTDOWN = 0x08;
            const int MOUSEEVENTF_RIGHTUP = 0x10;

            int x = 0;
            int y = 0;

            NativeMethods.mouse_event(MOUSEEVENTF_RIGHTDOWN, x, y, 0, 0);
            NativeMethods.mouse_event(MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
        }

        private static void SendLeftClickOLD()
        {            
            uint resSendInput;

            NativeMethods.INPUT[] input = new NativeMethods.INPUT[1];

            input[0] = new NativeMethods.INPUT();

            input[0].type = NativeMethods.INPUTTYPE.MOUSE;
            input[0].mi.dx = 0;
            input[0].mi.dy = 0;
            input[0].mi.dwFlags = NativeMethods.MOUSEEVENTF.LEFTDOWN;

            resSendInput = NativeMethods.SendInput(1, input, Marshal.SizeOf(input[0]));

            if (resSendInput == 0 || Marshal.GetLastWin32Error() != 0)
                System.Diagnostics.Debug.WriteLine(Marshal.GetLastWin32Error());

            input[0].mi.dx = 0;
            input[0].mi.dy = 0;
            input[0].mi.dwFlags = NativeMethods.MOUSEEVENTF.LEFTUP;

            resSendInput = NativeMethods.SendInput(1, input, Marshal.SizeOf(input[0]));

            if (resSendInput == 0 || Marshal.GetLastWin32Error() != 0)
                System.Diagnostics.Debug.WriteLine(Marshal.GetLastWin32Error());

            UtilSys.Wait(100);
        }
        
        private static void SendRightClickOLD()
        {
            uint resSendInput;

            NativeMethods.INPUT[] input = new NativeMethods.INPUT[1];

            input[0] = new NativeMethods.INPUT();

            input[0].type = NativeMethods.INPUTTYPE.MOUSE;
            input[0].mi.dx = 0;
            input[0].mi.dy = 0;
            input[0].mi.dwFlags = NativeMethods.MOUSEEVENTF.RIGHTDOWN;

            resSendInput = NativeMethods.SendInput(1, input, Marshal.SizeOf(input[0]));

            if (resSendInput == 0 || Marshal.GetLastWin32Error() != 0)
                System.Diagnostics.Debug.WriteLine(Marshal.GetLastWin32Error());

            input[0].mi.dx = 0;
            input[0].mi.dy = 0;
            input[0].mi.dwFlags = NativeMethods.MOUSEEVENTF.RIGHTUP;

            resSendInput = NativeMethods.SendInput(1, input, Marshal.SizeOf(input[0]));

            if (resSendInput == 0 || Marshal.GetLastWin32Error() != 0)
                System.Diagnostics.Debug.WriteLine(Marshal.GetLastWin32Error());

            UtilSys.Wait(100);
        }
        
        public static void SetCursorPos(int x, int y)
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);
        }
                
        public static void SetCursorPos(System.Windows.Point clickablePoint)
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)clickablePoint.X, (int)clickablePoint.Y);
        }

        public static string GetText(AutomationElement element)
        {
            string text = string.Empty;

             TextPattern txtPattern = element.GetCurrentPattern(TextPattern.Pattern) as TextPattern;
             text = txtPattern.DocumentRange.GetText(-1);                 

            return (text);            
        }

        public static void WalkControlElements(AutomationElement rootElement, TreeNode treeNode)
        {
            AutomationElement elementNode = TreeWalker.ContentViewWalker.GetFirstChild(rootElement);

            while (null != elementNode)
            {
                string name = elementNode.Current.Name;
                //string progname = elementNode.ControlType.ProgrammaticName;
                string ctrlType = elementNode.Current.LocalizedControlType;

                TreeNode childTreeNode = treeNode.Nodes.Add(name);
                
                WalkControlElements(elementNode, childTreeNode);
                elementNode = TreeWalker.ControlViewWalker.GetNextSibling(elementNode); 
            }
        }

        public static void CloseWindow(AutomationElement window)
        {
            try
            {
                WindowPattern wndPattern = window.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
                wndPattern.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void WalkControlsRaw(AutomationElement root)
        {
            AutomationElement node = TreeWalker.ControlViewWalker.GetFirstChild(root);

            while (null != node)
            {
                aeList.Add(node);

                WalkControlsRaw(node);
                node = TreeWalker.RawViewWalker.GetNextSibling(node);
            }
        }

        public static void WalkControlElements(AutomationElement rootElement)
        {
            AutomationElement elementNode = TreeWalker.ContentViewWalker.GetFirstChild(rootElement);

            while (null != elementNode)
            {                                                
                aeList.Add(elementNode);

                WalkControlElements(elementNode);
                elementNode = TreeWalker.ControlViewWalker.GetNextSibling(elementNode);
            }
        }

        public static bool GetClickablePoint(AutomationElement element, out System.Windows.Point pt)
        {
            pt = new System.Windows.Point();
            pt.X = 0;
            pt.Y = 0;

            try
            {
                pt = element.GetClickablePoint();
                return (true);
            }
            catch (System.Exception)
            {
                return (false);
            }
        }

        public static AutomationElement FindDialogWithinProcess(int procID, string windowTitle)
        {
            AutomationElement dialog = null;
            
            PropertyCondition propProcID = new PropertyCondition(AutomationElement.ProcessIdProperty, procID);
            PropertyCondition propName = new PropertyCondition(AutomationElement.NameProperty, windowTitle);            

            AndCondition andCond = new AndCondition(propProcID, propName);

            // Children = Specifies that the search include the element's immediate children. 
            dialog = AutomationElement.RootElement.FindFirst(TreeScope.Children, andCond); 
            return (dialog);
        }

        public static AutomationElement FindChildDialog(AutomationElement parentWindow, string windowTitle)
        {
            AutomationElement dialog = null;
            
            PropertyCondition[] cond = new PropertyCondition[2];
            
            cond[0] = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
            cond[1] = new PropertyCondition(AutomationElement.NameProperty, windowTitle);            
            //cond[2] = new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "Dialog");
            
            AndCondition andCond = new AndCondition(cond);

            dialog = parentWindow.FindFirst(TreeScope.Children, andCond);

            return (dialog);
        }


        public static AutomationElement FindControlByCaption(AutomationElement window, string caption)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.NameProperty, caption);
            AutomationElement element = window.FindFirst(TreeScope.Descendants, idCond);

            return (element);
        }

        public static AutomationElement FindControlByType(AutomationElement parent, string type)
        {
            PropertyCondition condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
            AutomationElement element = parent.FindFirst(TreeScope.Descendants, condition);

            return (element);
        }

        public static AutomationElement FindTree(AutomationElement window, string id)
        {
            PropertyCondition idCond = new PropertyCondition(AutomationElement.AutomationIdProperty, id);
            PropertyCondition typeCond = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Tree);

            AndCondition andCond = new AndCondition(idCond, typeCond);

            AutomationElement element = window.FindFirst(TreeScope.Descendants, andCond);

            return (element);
        }
        
        public static AutomationElementCollection GetComboBoxItems(AutomationElement combo)
        {
            PropertyCondition typeCond = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.List);
            AutomationElement list = combo.FindFirst(TreeScope.Children, typeCond);

            typeCond = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem);

            AutomationElementCollection col = list.FindAll(TreeScope.Descendants, typeCond);
            return (col);
        }

        public static AutomationElementCollection GetDataGridRows(AutomationElement grid)
        {
            PropertyCondition typeCond = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem);

            AutomationElementCollection col = grid.FindAll(TreeScope.Children, typeCond);
            return (col);
        }

        public static AutomationElement GetFirstChild(AutomationElement parent)
        {
            return parent.FindFirst(TreeScope.Children, null);
        }

        public static void ToggleCheckBox(AutomationElement chk, ToggleState TargetState)
        {
            TogglePattern togglePattern = chk.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern;

            int i = 0;
            while (i < 3) // 3 state, on, off, indeter
            {
                if (TargetState != togglePattern.Current.ToggleState)
                {
                    ++i;
                    togglePattern.Toggle();
                    UtilSys.Sleep(50);
                }
                if (TargetState == togglePattern.Current.ToggleState)
                    return;
            }
        }
        
        public static void MakeSelectionItem(AutomationElement elem)
        {
            SelectionItemPattern pattern = elem.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
            pattern.Select();
        }

        public static void SetWindowVisualState(AutomationElement window, WindowVisualState TargetState)
        {
            WindowPattern pattern = window.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
            //pattern.SetWindowVisualState(WindowVisualState.Normal);
            pattern.SetWindowVisualState(TargetState);
        }

        public static void WindowMove(AutomationElement window, System.Windows.Point pos)
        {
            TransformPattern pattern = window.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
            pattern.Move(pos.X, pos.Y);            
        }

        public static void WindowResize(AutomationElement window, System.Windows.Point pos)
        {
            TransformPattern pattern = window.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
            pattern.Resize(pos.X, pos.Y);
        }

        public static AutomationElement FindTreeItem(AutomationElement parent, string name)
        {
            PropertyCondition nameCond = new PropertyCondition(AutomationElement.NameProperty, name);
            PropertyCondition typeCond = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TreeItem);

            AndCondition andCond = new AndCondition(nameCond, typeCond);

            AutomationElement element = parent.FindFirst(TreeScope.Descendants, andCond);

            return (element);
        }

                
 


    }
}