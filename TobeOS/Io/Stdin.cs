using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS
{
    public abstract class Stdin : IDisposable
    {
        public abstract bool CanRead { get; }
        public abstract int Read();
        public abstract bool ReadInto(byte[] b, int length);
        public abstract ConsoleKeyInfo ReadKey();
        public abstract string ReadLine();
        public abstract void Dispose();
    }
}
