using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS
{
    public class StringStdWriter : StdWriter
    {
        private StringBuf buf = new StringBuf();

        public override bool IsEmpty => buf.Length == 0;

        public override char[] GetContents()
        {
            return buf.ToCharArray();
        }

        public override void Write(string str)
        {
            buf.AddString(str);
        }

        public override void WriteChar(char ch)
        {
            buf.Add(ch);
        }

        public override void WriteChars(char[] chars)
        {
           for (int i = 0; i < chars.Length; i++)
            {
                buf.Add(chars[i]);
            }
        }

        public override void WriteLine()
        {
            buf.Add('\n');
        }

        public override void WriteLine(string str)
        {
            Write(str);
            WriteLine();
        }

        public override void Dispose()
        {
            buf.Clear();
        }
    }
}
