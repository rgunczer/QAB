using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Util;


namespace Scripter
{
    public partial class FrmFindAndReplace : Form
    {
        private List<string> m_lstSearchHistory = null;
        private List<string> m_lstReplaceHistory = null;
        private static bool _bWholeWord = false;
        private static bool _bMatchCase = false;
        

        public FrmFindAndReplace()
        {
            InitializeComponent();
        }

        private void FrmFindAndReplace_Load(object sender, EventArgs e)
        {
            chkMatchCase.Checked = _bMatchCase;
            chkWholeWord.Checked = _bWholeWord;

            m_lstSearchHistory = Scripter.LoadSearchHistory();

            for (int i = m_lstSearchHistory.Count - 1; i > -1; --i)
            {
                cboTextToFind.Items.Add(m_lstSearchHistory[i]);
            }

            m_lstReplaceHistory = Scripter.LoadReplaceHistory();

            for (int i = m_lstReplaceHistory.Count - 1; i > -1; --i)
            {
                cboReplaceWith.Items.Add(m_lstReplaceHistory[i]);
            }

            cboTextToFind.Text = (string)Tag;            
        }

        private void AddTextToReplaceHistory(string text)
        {
            int index = 0;

            foreach (string item in m_lstReplaceHistory)
            {
                if (item == text)
                {
                    m_lstReplaceHistory.RemoveAt(index);
                    break;
                }
                ++index;
            }

            m_lstReplaceHistory.Add(text);
            Scripter.SaveReplaceHistory(m_lstReplaceHistory);
        }

        private void AddTextToSearchHistory(string text)
        {
            int index = 0;

            foreach (string item in m_lstSearchHistory)
            {
                if (item == text)
                {
                    m_lstSearchHistory.RemoveAt(index);
                    break;
                }
                ++index;
            }

            m_lstSearchHistory.Add(text);
            Scripter.SaveSearchHistory(m_lstSearchHistory);
        }

        private void cmdFind_Click(object sender, EventArgs e)
        {
            if (0 == cboTextToFind.Text.Trim().Length)
                return;

            AddTextToSearchHistory(cboTextToFind.Text);

            _bMatchCase = chkMatchCase.Checked;
            _bWholeWord = chkWholeWord.Checked;

            FindAndReplace.Find(Scripter.ScripterForm.CurrentEditor, cboTextToFind.Text, chkMatchCase.Checked, chkWholeWord.Checked);
        }

        private void cmdReplace_Click(object sender, EventArgs e)
        {
            if (0 == cboTextToFind.Text.Trim().Length)
                return;

            if (0 == cboReplaceWith.Text.Trim().Length)
                return;

            AddTextToSearchHistory(cboTextToFind.Text);
            AddTextToReplaceHistory(cboReplaceWith.Text);

            _bMatchCase = chkMatchCase.Checked;
            _bWholeWord = chkWholeWord.Checked;

            FindAndReplace.Replace(Scripter.ScripterForm.CurrentEditor, cboTextToFind.Text, cboReplaceWith.Text, chkMatchCase.Checked, chkWholeWord.Checked);
        }

        private void cmdReplaceAll_Click(object sender, EventArgs e)
        {
            if (0 == cboTextToFind.Text.Trim().Length)
                return;

            if (0 == cboReplaceWith.Text.Trim().Length)
                return;

            AddTextToSearchHistory(cboTextToFind.Text);
            AddTextToReplaceHistory(cboReplaceWith.Text);

            _bMatchCase = chkMatchCase.Checked;
            _bWholeWord = chkWholeWord.Checked;

            int counter = -1;
            int ret; 

            do
            {
                ++counter;
                ret = FindAndReplace.Replace(Scripter.ScripterForm.CurrentEditor, cboTextToFind.Text, cboReplaceWith.Text, chkMatchCase.Checked, chkWholeWord.Checked);    
            } while (ret != -1);

            string msg = counter.ToString() + " occurences were replaced.";

            UtilSys.MessageBoxInfo(msg);           
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }




    }
}
