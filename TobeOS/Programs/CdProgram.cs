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
                state.Io.Err.WriteLine("fatal error: no input path");
                return (int)ExitCodes.FAILURE;
            }

            var absolute = FilePaths.Expand(FilePaths.GetAbsolute(FilePaths.Normalize(arguments[1]), state));

            if (!Directory.Exists(absolute))
            {
                state.Io.Err.WriteLine($"fatal error: no such directory \"{absolute}\"");
                return (int)ExitCodes.FILESYSTEM_ERROR;
            }

            state.WorkingDirectory = absolute;
            return (int)ExitCodes.SUCCESS;
        }
    }
}
