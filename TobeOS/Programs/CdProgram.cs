using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TobeOS.Programs
{
    public class CdProgram : Program
    {
        public override string GetName()
        {
            return "cd";
        }

        public override int Run(KernelState state, string[] arguments)
        {
            if (arguments.Length < 2)
            {
                Console.WriteLine("fatal error: no input path");
                return 1;
            }

            var absolute = FilePaths.Expand(FilePaths.GetAbsolute(FilePaths.Normalize(arguments[1]), state));

            if (!Directory.Exists(absolute))
            {
                Console.WriteLine($"fatal error: no such directory \"{absolute}\"");
                return 2;
            }

            state.WorkingDirectory = absolute;
            return 0;
        }
    }
}
