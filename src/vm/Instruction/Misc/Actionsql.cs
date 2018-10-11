using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Util;


namespace VM
{
    public class Actionsql : ActionBase
    {
        private string m_connString = string.Empty;

        public override EnumActionResult Execute()
        {
            try
            {
                string cmd = _vm.variables.Get(m_params[0]);
                string param = string.Empty;
                string varName = string.Empty;
                
                if (m_params.Count >= 2)
                    param = _vm.variables.Get(m_params[1]);

                if (m_params.Count >= 3)
                    varName = m_params[2];

                switch (cmd)
                {
                    case "sql.Connect":
                        m_connString = param;
                        _vm.host.conn = new SqlConnection(param);
                        _vm.host.conn.Open(); 
                    break;

                    case "sql.ExecuteReader":
                    {                        
                        SqlCommand command = _vm.host.conn.CreateCommand();
                        command.CommandText = param;
                        command.CommandType = CommandType.Text;
                        
                        _vm.host.reader = command.ExecuteReader();
                    }
                    break;

                    case "sql.read":
                        _vm.host._ret = _vm.host.reader.Read() == false ? "False" : "True"; 
                    break;

                    case "sql.ExecuteNonQuery":
                    {
                        varName = m_params[3];

                        SqlCommand command = _vm.host.conn.CreateCommand();
                        command.CommandText = param;
                        command.CommandType = CommandType.Text;

                        _vm.host._ret = command.ExecuteNonQuery().ToString();

                        _vm.variables.Update(varName, _vm.host._ret);
                    }
                    break;

                    case "sql.FieldCount":
                        _vm.host._ret = _vm.host.reader.FieldCount.ToString();
                    break;
                        
                    case "sql.Disconnect":

                        if (null != _vm.host.reader)
                            _vm.host.reader.Close();

                        if (null != _vm.host.conn)
                            _vm.host.conn.Close();

                    break;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Empty;

                foreach (string param in m_params)
                {
                    msg += param + " ";
                }

                msg += ex.Message;

                _vm.host.WriteLog(msg);
                _vm.host.WriteLog(msg);
                return EnumActionResult.ERROR;
            }

            return EnumActionResult.OK;
        }

        public override string ToString()
        {
            return m_params[0];
        }


    }
}