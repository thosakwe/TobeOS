using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS.Io
{
    public class CombinedStdin : Stdin
    {
        private Stdin a, b;

        public CombinedStdin(Stdin a, Stdin b)
        {
            this.a = a;
            this.b = b;
        }

        public override bool CanRead => a.CanRead || b.CanRead;

        public override void Dispose()
        {
            a.Dispose();
            b.Dispose();
        }

        public override int Read()
        {
            var ch = a.Read();
            return ch != -1 ? ch : b.Read();
        }

        public override bool ReadInto(byte[] b, int length)
        {
            return a.ReadInto(b, length) || this.b.ReadInto(b, length);
        }

        public override ConsoleKeyInfo ReadKey()
        {
            var key = a.ReadKey();
            return key.KeyChar != 0 ? key : b.ReadKey();
        }

        public override string ReadLine()
        {
            if (a.CanRead)
                return a.ReadLine();
            else if (b.CanRead)
                return b.ReadLine();
            throw new IndexOutOfRangeException("Cannot read a line from a closed output.");
        }
    }
}
