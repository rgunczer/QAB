using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Util
{
    static class UtilIO
    {
        public static bool SkipLine(string line)
        {
            if (string.IsNullOrEmpty(line))
                return (true);

            if (0 == line.Trim().Length) // empty line
                return (true);

            if ('/' == line[0] && '/' == line[1]) // comment
                return (true);

            return (false);
        }

        public static string[] ReadFile2Array(string path2file)
        {
            string[] buffer = null ;                       
            buffer = File.ReadAllLines(path2file);
            return (buffer);
        }

        public static List<string> ReadFile(string path2file)
        {
            List<string> buffer = new List<string>();

            try
            {
                using (FileStream fileStream = new FileStream(path2file, FileMode.Open, FileAccess.Read))
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    while (streamReader.Peek() >= 0)
                    {
                        buffer.Add(streamReader.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {
                UtilSys.MessageBox(ex.Message);
            }
            return (buffer);
        }


    }
}
