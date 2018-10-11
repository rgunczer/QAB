using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using Util;


namespace VM
{
    class ActionUltraTree : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string id = _vm.variables.Get(m_params[1]);
            string op = _vm.variables.Get(m_params[3]);

            AutomationElement tree = Finder.GetTree(_vm.host.TargetWindow, id);

            if (null == tree)
            {
                _vm.host.WriteLog("UltraTree '" + id + "' NOT found.");
                return (EnumActionResult.ERROR);
            }
            _vm.host.WriteLog("UltraTree '" + id + "' found.");

            _vm.host.UpdateMarker(tree.Current.BoundingRectangle);

            _vm.host.aeCurrent = tree;

            switch (op)
            {
                case "ExpandAll":
                    ExpandAll(tree);
                    Explore(tree);
                break;

                case "CollapseAll":
                    CollapseAll(tree);
                break;

                case "Explore":
                    Explore(tree);
                break;

                default:
                    _vm.host.WriteLog(m_type + " Unrecognized command: '" + op + "'");
                    return (EnumActionResult.ERROR);
            }            
            return (EnumActionResult.OK);                    
        }        
        
        private void ExpandAll(AutomationElement tree)
        {
            PropertyCondition typeTreeItem = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TreeItem);
            AutomationElementCollection col = tree.FindAll(TreeScope.Descendants, typeTreeItem);

            if (0 == col.Count)
            {
                _vm.host.WriteLog("No elements nothing to expand");
                return;
            }

            double LeftParent = col[0].Current.BoundingRectangle.Left;

            bool hasChildren = false;
            
            for (int i = col.Count-1; i > -1; --i)
            {
                AutomationElement cur = col[i];
                _vm.host.UpdateMarker(cur.Current.BoundingRectangle);
                UtilSys.Wait(50);

                double LeftCurrent = cur.Current.BoundingRectangle.Left;

                if (LeftCurrent == LeftParent) // root element
                {
                    if (!hasChildren)
                    {
                        UtilAutomation.ClickOn(cur.Current.BoundingRectangle, true);
                        UtilSys.Wait(100);
                    }
                    hasChildren = false;                    
                }
                else // child element
                    hasChildren = true;                                    
            }
        }

        private void Explore(AutomationElement tree)
        {
            _vm.host.UpdateMarker(tree.Current.BoundingRectangle);

            PropertyCondition typeTreeItem = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TreeItem);
            AutomationElementCollection col = tree.FindAll(TreeScope.Descendants, typeTreeItem);

            if (0 == col.Count)
            {
                _vm.host.WriteLog("No elements, nothing to explore.");
                return;
            }

            ControlUltraTree utree = new ControlUltraTree();
            utree.Name = tree.Current.AutomationId;

            double LeftParent = col[0].Current.BoundingRectangle.Left;

            foreach (AutomationElement elem in col)
            {
                double LeftCurrent = elem.Current.BoundingRectangle.Left;

                if (LeftCurrent == LeftParent) // root element
                    utree.Roots.Add(elem.Current.Name, elem);
                else
                    utree.Children.Add(elem.Current.Name, elem);
            }                        
            _vm.AddControl(utree);
        }
        
        private void CollapseAll(AutomationElement tree)
        {            
            ControlUltraTree utree = _vm.GetUltraTree();

            if (null == utree)
            {
                string msg = "'CollapseAll' command is used in pair with 'ExpandAll'.";
                _vm.host.WriteLog(msg);
                return;
            }

            List<AutomationElement> list = new List<AutomationElement>();

            foreach (KeyValuePair<string, AutomationElement> item in utree.Roots)
	        {
                list.Add(item.Value);
	        }

            for (int i = list.Count - 1; i > -1; --i)
            {
                _vm.host.UpdateMarker(list[i].Current.BoundingRectangle);
                UtilAutomation.ClickOn(list[i].Current.BoundingRectangle, true);
                UtilSys.Wait(40);
            }
            _vm.RemoveControl(tree.Current.Name);
        }

    }
}
