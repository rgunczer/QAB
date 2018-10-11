using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    public enum EnumVariableType
    {
        Variable = 1,
        Array = 2,

    };

    public abstract class VariableBase
    {
        public EnumVariableType type;
        protected string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
