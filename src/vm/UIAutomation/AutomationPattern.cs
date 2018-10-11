using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using System.Diagnostics;


namespace VM
{
    static class AutomationPattern
    {        
        public static void Invoke(AutomationElement ae)
        {
            Debug.Assert(null != ae);

            try
            {
                InvokePattern invPattern = ae.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                invPattern.Invoke();                
            }
            catch
            {
                throw;
            }
        }

        public static string GetValue(AutomationElement ae)
        {
            Debug.Assert(null != ae);

            try
            {
                ValuePattern valPattern = ae.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                return valPattern.Current.Value;
            }
            catch
            {
                throw;
            }
        }

        public static void Value(AutomationElement ae, string value)
        {
            Debug.Assert(null != ae);

            try
            {
                ValuePattern valPattern = ae.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                valPattern.SetValue(value);
            }
            catch
            {
                throw;
            }            
        }

        public static void Select(AutomationElement ae)
        {
            try
            {
                SelectionItemPattern pattern = ae.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
                pattern.Select();
            }
            catch
            {               
                throw;
            }
        }  

        public static void ScrollIntoView(AutomationElement ae)
        {
            try
            {
                ScrollItemPattern pattern = ae.GetCurrentPattern(ScrollItemPattern.Pattern) as ScrollItemPattern;

                if (null == pattern)                
                    return;

                pattern.ScrollIntoView();                                                                                                
            }
            catch
            {                
                
            }
        }

    }
}
