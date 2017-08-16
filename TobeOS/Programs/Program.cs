using System;

namespace TobeOS.Programs
{
    public abstract class Program
    {
        public abstract int Run(KernelState state, string[] arguments);

        public abstract string GetName();
    }
}
