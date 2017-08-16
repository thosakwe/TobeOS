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
                Console.WriteLine("usage: shutdown [--help] [-h] [--reboot] [-r]");
                Console.WriteLine("Options");
                Console.WriteLine("  --help, -h:   Print this help information.");
                Console.WriteLine("  --reboot, -r: Reboot the system.");
                return 0;
            }

            if (reboot)
            {
                Cosmos.System.Power.Reboot();
                return 0;
            }

            Console.WriteLine("fatal error: COSMOS does not support ACPI, and thus shutdown is impossible.");
            return 1;
        }
    }
}
