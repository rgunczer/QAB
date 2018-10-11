using System;
using System.Text;
using System.Windows.Forms;


namespace VM
{
    public partial class FrmUserInput : Form
    {
        public string lbl
        {
            set { lblText.Text = value; }
        }

        public string txt
        {
            set { txtData.Text = value; }
            get { return txtData.Text; }
        }

        public FrmUserInput()
        {
            InitializeComponent();
        }

        private void cmdBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofn = new OpenFileDialog();

            if (ofn.ShowDialog() == DialogResult.OK)
            {
                txtData.Text = ofn.FileName;
            }
        }

        private void cmdBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;

            fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;

            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtData.Text = fbd.SelectedPath;
            }

        }



    }
}
