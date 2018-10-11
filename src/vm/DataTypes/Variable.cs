using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    public class Variable : VariableBase
    {
        private string value;

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set { this.path = value; }
        }

        private int lineNumber;

        public int LineNumber
        {
            get { return lineNumber; }
            set { this.lineNumber = value; }
        }

    }
}