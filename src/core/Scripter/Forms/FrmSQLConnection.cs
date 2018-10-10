using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Util;

//UtilSys.MessageBox(string.Join(",", lst.ToArray()));
namespace Scripter
{
    public partial class FrmSQLConnection : Form
    {
        private static string serverName = string.Empty;

        public FrmSQLConnection()
        {
            InitializeComponent();
        }

        private void FrmSQLConnection_Load(object sender, EventArgs e)
        {
            txtSQLServer.Text = serverName;
        }

        private void cmdListDatabases_Click(object sender, EventArgs e)
        {
            ListDatabasesOnServer(txtSQLServer.Text);
            serverName = txtSQLServer.Text;
        }

        private void ListDatabasesOnServer(string server)
        {            
            lbDatabases.Items.Clear();

            if (string.IsNullOrEmpty(server))
                return;

            try
            {
                SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder();

                connBuilder.DataSource = server;
                connBuilder.InitialCatalog = "tempdb";
                connBuilder.IntegratedSecurity = true;

                txtConnectionString.Text = connBuilder.ToString();

                using (SqlConnection conn = new SqlConnection(connBuilder.ToString()))
                {
                    conn.Open();                    

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select name from sys.databases order by name";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lbDatabases.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UtilSys.MessageBox(ex.Message);
            }
            finally
            {
                lblDatabases.Text = "Databases [" + lbDatabases.Items.Count.ToString() + "]:";
            }
        }

        private void lbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildConnString();
        }

        private void BuildConnString()
        {
            try
            {
                SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder();
                connBuilder.DataSource = txtSQLServer.Text;
                connBuilder.InitialCatalog = (string)lbDatabases.SelectedItem;
                connBuilder.IntegratedSecurity = chkIntegradedSecurity.Checked;

                if (!chkIntegradedSecurity.Checked)
                {
                    connBuilder.UserID = txtUserName.Text;
                    connBuilder.Password = txtPassword.Text;
                }

                txtConnectionString.Text = connBuilder.ToString();
            }
            catch
            {
                txtConnectionString.Text = string.Empty;                
            }
        }

        private void chkIntegradedSecurity_CheckedChanged(object sender, EventArgs e)
        {                                    
            txtUserName.Enabled = !chkIntegradedSecurity.Checked;
            txtPassword.Enabled = !chkIntegradedSecurity.Checked;

            lblUserName.Enabled = !chkIntegradedSecurity.Checked;
            lblPassword.Enabled = !chkIntegradedSecurity.Checked;

            BuildConnString();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            BuildConnString();
        }
    }
}