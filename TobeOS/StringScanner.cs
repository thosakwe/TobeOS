using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS
{
    public class StringScanner
    {
        private string str;
        private int index = -1, length;

        public int Index
        {
            get { return index; }
        }

        public StringScanner(String str)
        {
            this.str = str;
            length = str.Length;
        }

        public int Remaining()
        {
            if (IsDone()) return 0;
            return length - index - 1;
        }

        public bool IsDone()
        {
            return index >= length - 1;
        }

        public char Read()
        {
            return str[++index];
        }

        public char Peek()
        {
            return str[index + 1];
        }

        public bool Scan(String needle)
        {
            if ((length - index - 1) < length)
                return false;

            for (int i = 0; i < needle.Length; i++)
            {
                if (str[index + i + 1] != needle[i])
                    return false;
            }

            index += needle.Length;
            return true;
        }
    }
}
