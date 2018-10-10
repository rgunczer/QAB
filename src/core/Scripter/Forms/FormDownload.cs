using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;
using System.IO;

namespace Scripter
{
    public partial class FormServerFolderStructure : Form
    {
        List<string> m_downloadList = new List<string>();

        bool m_bInChecking = false;
        bool m_bToCheck = false;

        public FormServerFolderStructure()
        {
            InitializeComponent();
        }

        private void FormServerFolderStructure_Load(object sender, EventArgs e)
        {
            this.Show();
            this.Update();
            lblPath.Text = Settings.Get("DownloadTo");
            cmdRefresh_Click(null, null);
        }

        private void tvw_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (m_bInChecking)
                return;

            if (!m_bInChecking)
            {
                m_bToCheck = e.Node.Checked;
                
                m_bInChecking = true;

                CheckNodes(e.Node);

                m_bInChecking = false;
            }
        }

        private void CheckNodes(TreeNode parent)
        {
            foreach (TreeNode node in parent.Nodes)
            {
                node.Checked = m_bToCheck;
                CheckNodes(node);
            }
        }

        private void cmdDownload_Click(object sender, EventArgs e)
        {
            string downloadTo = Settings.Get("DownloadTo");

            if ( string.IsNullOrEmpty(downloadTo) )
            {
                UtilSys.MessageBox("Set download to location first");                
                return;
            }
            
            m_downloadList.Clear();
            GetList(tvw.Nodes[0]);

            FrmProgress frmP = new FrmProgress();
            frmP.Text = "Downloading...";
            frmP.SetProgressMax(m_downloadList.Count);

            frmP.Show();
            frmP.Top = Top + Height / 2;
            frmP.Left = (Left + Width / 2) - frmP.Width / 2;

            frmP.Update();

            Scripter.DoEvents();
            
            foreach (string item in m_downloadList)
            {                
                string temp = Settings.Get("ServerIP");

                string[] arr = temp.Split(':');

                if (2 == arr.Length)
                {
                    frmP.Text = item;
                    frmP.UpdateProgress();

                    string ip = arr[0];
                    int port = Convert.ToInt32(arr[1]);

                    NetworkClient nc = new NetworkClient(ip, port);

                    string savePath = Path.Combine(downloadTo, item);
                    nc.DownloadFile(item, savePath);                                                            
                }
            }
            frmP.Close();
        }

        private void GetList(TreeNode parent)
        {
            foreach (TreeNode node in parent.Nodes)
            {
                if (node.Checked)
                {
                    string nodeText = node.Text.ToLower();

                    if (nodeText.EndsWith(".bmp") || nodeText.EndsWith(".scp") || nodeText.EndsWith(".sci") || nodeText.EndsWith(".jpg") || nodeText.EndsWith(".dat") || nodeText.EndsWith(".qpf"))
                    {
                        Stack<string> stackPath = new Stack<string>();
                        TreeNode p = node;

                        while (p != null)
                        {
                            stackPath.Push(p.Text);
                            p = p.Parent;
                        }

                        string path = string.Empty;
                        foreach (string item in stackPath)
                        {
                            path += "\\" + item;
                        }

                        path = path.Substring(1);

                        m_downloadList.Add(path);
                    }
                }
                GetList(node);                
            }
        }

        private void cmdBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            
            DialogResult result = folderDlg.ShowDialog();

            if (DialogResult.OK == result)
            {
                lblPath.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
                Settings.Update("DownloadTo", lblPath.Text);
                Settings.Save();                
            }
        }

        void UpdateSyncFoldersStructureFromServer()
        {
            string temp = Settings.Get("ServerIP");

            string[] arr = temp.Split(':');

            if (2 == arr.Length)
            {
                string ip = arr[0];
                int port = Convert.ToInt32(arr[1]);
                                
                NetworkClient nc = new NetworkClient(ip, port);

                List<string> files = nc.GetSyncFolderStructure();

                ShowFolderStructure(files);                                
            }
            else
                UtilSys.MessageBox("Unable to get the ip address and port number from [" + temp + "]");
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            UpdateSyncFoldersStructureFromServer();
        }
        
        private void ShowFolderStructure(List<string> files)
        {
            TreeView tvw = this.tvw;
            tvw.Nodes.Clear();

            if (null == files)
                return;
            
            TreeNode folder = tvw.Nodes.Add("Invenio");            
            Stack<TreeNode> stack = new Stack<TreeNode>();

            stack.Push(folder);

            int type = -1;

            foreach (string item in files)
            {
                if (string.IsNullOrEmpty(item))
                    continue;

                switch (item)
                {
                    case "Dir":
                        type = 0;
                        continue;

                    case "File":
                        type = 1;
                        continue;

                    case "::UP":
                        type = 2;
                        break;
                }

                switch (type)
                {
                    case 0: // dir
                        {
                            TreeNode nod = stack.Peek().Nodes.Add(item);
                            stack.Push(nod);
                        }
                        break;

                    case 1: // file
                        {
                            stack.Peek().Nodes.Add(item);
                        }
                        break;

                    case 2: // level up
                        {
                            stack.Pop();
                        }
                        break;
                }
            }
            folder.Expand();           
        }



    }
}