﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS.Programs
{
    public class EchoProgram : Program
    {
        public override string GetName()
        {
            return "echo";
        }

        public override int Run(KernelState state, string[] arguments)
        {
            for (int i = 1; i < arguments.Length; i++)
                Console.WriteLine(arguments[i]);

            return 0;
        }
    }
}
