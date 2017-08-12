using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS.Programs
{
    public abstract class Program
    {
        public abstract int Run(KernelState state, String[] arguments);

        public abstract String GetName();
    }
}
