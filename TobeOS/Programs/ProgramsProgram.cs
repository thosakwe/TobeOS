using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS.Programs
{
    /// <summary>
    /// So meta!
    /// </summary>
    public class ProgramsProgram : Program
    {
        public override string GetName()
        {
            return "programs";
        }

        public override int Run(KernelState state, string[] arguments)
        {
            foreach (var program in state.Programs)
                state.Io.Out.WriteLine(program.GetName());

            return (int)ExitCodes.SUCCESS;
        }
    }
}
