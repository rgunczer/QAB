using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Scripter
{
    public partial class FrmProgress : Form
    {
        public FrmProgress()
        {
            InitializeComponent();
        }

        public void SetProgressMax(int value)
        {
            progress.Maximum = value;
        }

        public void UpdateProgress()
        {
            ++progress.Value;
        }
    }
}
