using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS.Programs
{
    public class ShutdownProgram : Program
    {
        public override string GetName()
        {
            return "shutdown";
        }

        public override int Run(KernelState state, string[] arguments)
        {
            bool help = false, reboot = false;

            for (int i = 1; i < arguments.Length; i++)
            {
                var arg = arguments[i];

                if (arg == "--help" || arg == "-h")
                {
                    help = true;
                    break;
                }

                if (arg == "--reboot" || arg == "-r")
                {
                    reboot = true;
                    break;
                }
            }

            if (help)
            {
                state.Io.Out.WriteLine("usage: shutdown [--help] [-h] [--reboot] [-r]");
                state.Io.Out.WriteLine("Options");
                state.Io.Out.WriteLine("  --help, -h:   Print this help information.");
                state.Io.Out.WriteLine("  --reboot, -r: Reboot the system.");
                return (int)ExitCodes.SUCCESS;
            }

            if (reboot)
            {
                Cosmos.System.Power.Reboot();
                return (int)ExitCodes.SUCCESS;
            }

            state.Io.Err.WriteLine("fatal error: COSMOS does not support ACPI, and thus shutdown is impossible.");
            return (int)ExitCodes.FAILURE;
        }
    }
}
