using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

using Util;
using Controls;



namespace Scripter
{
    public static class FindAndReplace
    {
        static int m_searchIndex = 0;

        public static int SearchIndex
        {
            get { return m_searchIndex; }
            set { m_searchIndex = value; }
        }

        public static int Replace(Editor editor, string textToFind, string textToReplaceWith, bool matchCase, bool wholeWord)
        {
            if (null == editor)
                return -1;

            int pos = m_searchIndex;

            int index = -1;

            if (matchCase && wholeWord)
                index = editor.Find(textToFind, pos, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.WholeWord | RichTextBoxFinds.MatchCase);
            else
            {
                if (matchCase)
                    index = editor.Find(textToFind, pos, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase);
                else if (wholeWord)
                    index = editor.Find(textToFind, pos, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.WholeWord);
                else
                    index = editor.Find(textToFind, pos, RichTextBoxFinds.NoHighlight);
            }

            if (index >= 0)
            {
                editor.SelectionStart = index;
                editor.SelectionLength = textToFind.Length;

                editor.SelectedText = textToReplaceWith;

                m_searchIndex = index + textToReplaceWith.Length;
                return m_searchIndex;
            }

            m_searchIndex = 0;
            return -1;
        }

        public static void Find(Editor editor, string textToFind, bool matchCase, bool wholeWord)
        {
            if (null == editor)
                return;

            int pos = m_searchIndex;

            int index = -1;

            if (matchCase && wholeWord)
                index = editor.Find(textToFind, pos, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.WholeWord | RichTextBoxFinds.MatchCase);
            else
            {
                if (matchCase)
                    index = editor.Find(textToFind, pos, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase);
                else if (wholeWord)
                    index = editor.Find(textToFind, pos, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.WholeWord);
                else
                    index = editor.Find(textToFind, pos, RichTextBoxFinds.NoHighlight);
            }

            if (index >= 0)
            {
                editor.SelectionStart = index;
                editor.SelectionLength = textToFind.Length;
                m_searchIndex = index + textToFind.Length;                
            }
            else // no result
            {
                UtilSys.MessageBoxInfo("The following text was not found:\n\n" + textToFind + "\n");
                m_searchIndex = 0;
            }
        }


    }
}