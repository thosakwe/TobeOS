using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS
{
    public class StringBuf
    {
        private char[] data = new char[0];
        private int length = 0;

        public int Length
        {
            get { return length; }
        }

        public void Add(char ch)
        {
            var newData = new char[length + 1];

            for (int i = 0; i < length; i++)
            {
                newData[i] = data[i];
            }

            newData[length++] = ch;
            this.data = newData;
        }

        public void AddString(string str)
        {
            for (int i = 0; i < str.Length; i++)
                Add(str[i]);
        }

        public void Clear()
        {
            data = new char[0];
            length = 0;
        }

        public string Take()
        {
            var s = ToString();
            Clear();
            return s;
        }

        public override string ToString()
        {
            return new string(data);
        }
    }
}
