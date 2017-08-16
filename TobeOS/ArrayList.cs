using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS
{
    public class ArrayList<T>
    {
        private T[] data = new T[0];
        private int length = 0;

        public T[] Array
        {
            get
            {
                return data;
            }
        }

        public int Length
        {
            get { return length; }
        }

        public void Add(T str)
        {
            var newData = new T[length + 1];

            for (int i = 0; i < length; i++)
            {
                newData[i] = data[i];
            }

            newData[length++] = str;
            this.data = newData;
        }

        public ArrayList<T> Skip(int n)
        {
            var list = new ArrayList<T>();

            if (n < data.Length)
            {
                for (int i = 0; i < n; i++)
                {
                    list.Add(data[i + n]);
                }
            }

            return list;
        }

        public ArrayList<T> Take(int n)
        {
            if (n >= length) return this;
            var list = new ArrayList<T>();

            for (int i = 0; i < n; i++)
            {
                list.Add(data[i]);
            }

            return list;
        }
    }
}
