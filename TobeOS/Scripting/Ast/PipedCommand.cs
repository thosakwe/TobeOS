using System;
using System.Collections.Generic;
using System.Text;
using TobeOS.Programs;

namespace TobeOS.Scripting.Ast
{
    public class PipedCommand : Command
    {
        private Command a, b;

        public PipedCommand(Command a, Command b)
        {
            this.a = a;
            this.b = b;
        }

        public override int Run(KernelState state, Program[] programs)
        {
            state.Debug("run a");
            state.LastExitCode = a.Run(state, programs);

            state.Debug("disposing stdin");
            state.Io.In.Dispose();
            state.Debug("disposed!");

            state.Io = new ProcessIO
            {
                In = new PipedStdin(state.Io.Out),
                Out = new StringStdWriter(),
                Err = state.Io.Err,
            };

            state.Debug("run b");
            return b.Run(state, programs);
        }
    }
}
