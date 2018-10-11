using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class Actionforeach : ActionFlowIterator
    {
        string m_VarValue = string.Empty;
        string m_ArrName = string.Empty;
        int m_ItemIndex = 0;
        int m_ItemIndexMax = 0;
        

        private void Init()
        {            
            m_initialized = true;
            m_done = false;

            m_varName = m_params[1];
            m_ArrName = m_params[3];

            string theArray = _vm.variables.Get(m_params[3]);
            string[] arr = theArray.Split('|');

            m_ItemIndex = 0;
            m_ItemIndexMax = arr.Length;

            string tmp = m_ArrName + "[" + m_ItemIndex.ToString() + "]";

            m_VarValue = _vm.variables.Get(tmp);

            _vm.variables.Add(m_varName, m_VarValue);
        }

        private void Update()
        {            
            ++m_ItemIndex;

            if(m_ItemIndex < m_ItemIndexMax)
            {
                string tmp = m_ArrName + "[" + m_ItemIndex.ToString() + "]";

                m_VarValue = _vm.variables.Get(tmp);

                _vm.variables.Update(m_varName, m_VarValue);
            }
            else
                Done();
        }
        
        public override EnumActionResult Execute()
        {
            if (!m_initialized)
                Init();
            else
                Update();

            return EnumActionResult.OK;
        }
    }
}