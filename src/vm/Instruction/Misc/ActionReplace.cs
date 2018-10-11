using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace VM
{
    class ActionReplace : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string path = _vm.variables.Get(m_params[1]);
            string oldValue = _vm.variables.Get(m_params[3]);
            string newValue = _vm.variables.Get(m_params[5]);

            string buffer = null;

    		using(StreamReader reader = File.OpenText(path))
				buffer = reader.ReadToEnd();

            buffer = buffer.Replace(oldValue, newValue);

            using (StreamWriter sw = new StreamWriter(path))                
                sw.Write(buffer);
                
            return EnumActionResult.OK;
        }
    }
}
