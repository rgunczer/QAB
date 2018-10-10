using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Scripter
{
    public partial class FrmHotList : Form
    {
        public FrmHotList()
        {
            InitializeComponent();
        }

        private void FrmHotList_Load(object sender, EventArgs e)
        {
            LoadHotList();
        }
        
        private void LoadHotList()
        {
            lbHotListItems.Items.Clear();

            string[] HotList = new string[10];

            for(int j = 0; j < HotList.Length; ++j)
            {
                string path = Settings.Get("HotSlot" + j);

                if (path.Length > 0)
                    lbHotListItems.Items.Add(path);
                else
                    lbHotListItems.Items.Add("[empty]");
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (lbHotListItems.SelectedIndex == -1)
                return;

            Settings.Update("HotSlot" + lbHotListItems.SelectedIndex.ToString(), string.Empty);

            Settings.Save();

            LoadHotList();
        }

        private void FrmHotList_FormClosed(object sender, FormClosedEventArgs e)
        {
            Scripter.ScripterForm.LoadHotList();
        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            if (lbHotListItems.SelectedIndex == -1)
                return;

            if (lbHotListItems.SelectedIndex == 0)
                return;

            int curIndex = lbHotListItems.SelectedIndex;
            int prevIndex = curIndex - 1;

            string curPath = Settings.Get("HotSlot" + curIndex.ToString());
            string prevPath = Settings.Get("HotSlot" + prevIndex.ToString());

            Settings.Update("HotSlot" + curIndex.ToString(), prevPath);
            Settings.Update("HotSlot" + prevIndex.ToString(), curPath);

            Settings.Save();
            LoadHotList();

            lbHotListItems.SelectedIndex = prevIndex;
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            if (lbHotListItems.SelectedIndex == -1)
                return;

            if (lbHotListItems.SelectedIndex == lbHotListItems.Items.Count - 1)
                return;

            int curIndex = lbHotListItems.SelectedIndex;
            int nextIndex = curIndex + 1;

            string curPath = Settings.Get("HotSlot" + curIndex.ToString());
            string nextPath = Settings.Get("HotSlot" + nextIndex.ToString());

            Settings.Update("HotSlot" + curIndex.ToString(), nextPath);
            Settings.Update("HotSlot" + nextIndex.ToString(), curPath);

            Settings.Save();
            LoadHotList();

            lbHotListItems.SelectedIndex = nextIndex;
        }


    }
}