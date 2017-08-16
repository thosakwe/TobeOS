using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS
{
    public class PipedStdin : Stdin
    {
        private char[] contents;
        private int index = -1, length;
        private bool open = true;
        private StdWriter writer;

        public PipedStdin(StdWriter writer)
        {
            contents = writer.GetContents();
            length = contents.Length;
            this.writer = writer;
        }

        public override bool CanRead => open && index >= length - 1;

        public override void Dispose()
        {
            writer.Dispose();
            open = false;
        }

        public override int Read()
        {
            if (!CanRead) return -1;
            return contents[++index];
        }

        public override bool ReadInto(byte[] b, int length)
        {
            if ((length - index - 1) < length) return false;

            for (int i = 0; i < length; i++)
            {
                b[i] = (byte)Read();
            }

            return true;
        }

        public override ConsoleKeyInfo ReadKey()
        {
            var ch = Read();
            if (ch == -1) ch = 0;
            // TODO: Check alt, shift, etc.
            return new ConsoleKeyInfo((char)ch, (ConsoleKey)ch, false, false, false);
        }

        public override string ReadLine()
        {
            if (!CanRead) throw new IndexOutOfRangeException("Cannot read a line from a closed output.");

            using (var buf = new StringBuf())
            {
                while (CanRead)
                {
                    var ch = (char)Read();
                    if (ch == '\n') break;
                    else buf.Add(ch);
                }

                return buf.ToString();
            }
        }
    }
}
