using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace Scripter
{
    public class SyntaxData
    {
        // vars
        private string m_cmd = string.Empty;
        private string[] m_arrSubCmds = null;
        private Color m_intellisenseBackColor = Color.White;
        private string m_intellisenseTitle = string.Empty;

        // properties
        public string cmd
        {
            get { return m_cmd; }
        }

        public string[] SubCmds
        {
            get { return m_arrSubCmds; }
        }

        public Color IntellisenseBackColor
        {
            get { return m_intellisenseBackColor; }
        }

        public string IntellisenseTitle
        {
            get { return m_intellisenseTitle; }
        }

        // ctor
        public SyntaxData(string cmd, string[] arrSubCmds, Color intellisenseColor, string intellisenseText)
        {
            m_cmd = cmd;
            m_arrSubCmds = arrSubCmds;
            m_intellisenseBackColor = intellisenseColor;
            m_intellisenseTitle = intellisenseText;
        }

    }
}
