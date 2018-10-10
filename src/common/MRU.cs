using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Util;
using System.Diagnostics;


namespace Util
{
    public class MRU
    {
        // variables
        string m_path = string.Empty;
        string m_fileName = string.Empty;
        int m_MaxItemCount = 0;        
        private List<string> m_list = new List<string>();
        
        // properties
        public List<string> lst
        {
            get { return m_list; }
        }

        public int MaxItemCount
        {
            get { return m_MaxItemCount; }
        }

        public int ItemCount
        {
            get { return m_list.Count; }
        }

        public string FileName
        {
            get { return m_fileName; }
        }

        // ctor
        public MRU(string path, int MaxItemCount)
        {
            Debug.Assert(path != null);
            Debug.Assert(MaxItemCount > 0);

            m_path = path;
            m_MaxItemCount = MaxItemCount;

            Load();
        }
        
        public void Load()
        {            
            if (File.Exists(m_path))
            {
                m_list = UtilIO.ReadFile(m_path);

                // truncate if necessary
                while (m_list.Count > m_MaxItemCount)
                {
                    m_list.RemoveAt(m_list.Count - 1);
                }
            }
        }

        public void Save()
        {
            Debug.Assert(m_path != string.Empty);
            
            int i = 0;
                        
            StreamWriter SW = File.CreateText(m_path);
            
            foreach (string str in m_list)
            {
                SW.WriteLine(str);

                if (m_MaxItemCount == i)
                    break;
                ++i;
            }
            SW.Close();                    
        }

        private bool RemoveExistingItem(string item)
        {
            foreach (string itemInList in m_list)
            {
                if (item.ToLower() == itemInList.ToLower())
                {
                    m_list.Remove(itemInList);
                    return true;
                }
            }
            return false;
        }

        public void Add(string item)
        {
            while (RemoveExistingItem(item)) ;                        
            m_list.Insert(0, item);
        }

        public void Remove(string path)
        {
            m_list.Remove(path);
        }

    }
}
