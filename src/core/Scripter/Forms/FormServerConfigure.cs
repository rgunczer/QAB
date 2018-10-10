using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using Util;


namespace Scripter
{
    public partial class FormServerConfigure : Form
    {
        List<string> m_lst = null;


        public FormServerConfigure()
        {
            InitializeComponent();
        }

        private bool IsValidAddress()
        {
            string[] arr = cboServer.Text.Split(':');

            if (2 == arr.Length)
                return true;

            UtilSys.MessageBox("Invalid IP Address and Port format.");
            return false;
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {
            if (!IsValidAddress())
                return;
                            
            string[] arr = cboServer.Text.Split(':');                        
            int port = Convert.ToInt32(arr[1]);

            NetworkClient nc = new NetworkClient(arr[0], port);
            nc.Test();                            
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!IsValidAddress())
                return;

            Settings.Update("ServerIP", cboServer.Text);
            AddToHistory(cboServer.Text);
            Scripter.ScripterForm.DisplayActiveServerIP();
            Close();            
        }

        private void AddToHistory(string text)
        {
            if ( string.IsNullOrEmpty(text) )
                return;

            int index = 0;

            foreach (string item in m_lst)
            {
                if (item == text)
                {
                    m_lst.RemoveAt(index);
                    break;
                }
                ++index;
            }

            m_lst.Add(text);
            Scripter.SaveServerIPHistory(m_lst);
        }
        
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormServerConfigure_Load(object sender, EventArgs e)
        {
            cboServer.Items.Clear();

            m_lst = Scripter.LoadServerIPHistory();

            for (int i = m_lst.Count - 1; i > -1; --i)
            {
                cboServer.Items.Add(m_lst[i]);
            }

            cboServer.Text = Settings.Get("ServerIP");            
        }
    }
}