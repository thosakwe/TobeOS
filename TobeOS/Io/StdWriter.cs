using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS
{
    public abstract class StdWriter : IDisposable
    {
        public abstract bool IsEmpty { get; }

        public abstract void WriteChar(char ch);

        public abstract void WriteChars(char[] chars);

        public abstract void Write(String str);

        public abstract void WriteLine();

        public abstract void WriteLine(String str);

        public abstract char[] GetContents();

        public abstract void Dispose();
    }
}
