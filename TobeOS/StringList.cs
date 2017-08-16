using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS
{
    public class StringList
    {
        private string[] data = new string[0];
        private int length = 0;

        public string[] Array
        {
            get { return data; }
        }

        public int Length
        {
            get { return length; }
        }

        public void Add(String str)
        {
            var newData = new string[length + 1];

            for (int i = 0; i < length; i++)
            {
                newData[i] = data[i];
            }

            newData[length++] = str;
            this.data = newData;
        }
    }
}
