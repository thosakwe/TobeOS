using System;
using System.IO;

namespace TobeOS.Programs
{
    public class CatProgram : Program
    {
        public override string GetName()
        {
            return "cat";
        }

        public override int Run(KernelState state, string[] arguments)
        {
            if (arguments.Length < 2)
            {
                return new EditProgram().Run(state, arguments);
            }

            for (int i = 1; i < arguments.Length; i++)
            {
                var absolute = FilePaths.Expand(FilePaths.GetAbsolute(FilePaths.Normalize(arguments[i]), state));

                if (!File.Exists(absolute))
                {
                    state.Io.Err.WriteLine($"fatal error: no file \"{absolute}\" exists");
                    return (int) ExitCodes.FILESYSTEM_ERROR;
                }
            }

            for (int i = 1; i < arguments.Length; i++)
            {
                var absolute = FilePaths.Expand(FilePaths.GetAbsolute(FilePaths.Normalize(arguments[i]), state));
                state.Io.Out.Write(File.ReadAllText(absolute));
            }

            state.Io.Out.WriteLine();
            return (int)ExitCodes.SUCCESS;
        }
    }
}
