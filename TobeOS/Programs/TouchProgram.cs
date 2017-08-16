using System;
using System.IO;
using System.Text;

namespace TobeOS.Programs
{
    public class TouchProgram : Program
    {
        public override string GetName()
        {
            return "touch";
        }

        public override int Run(KernelState state, string[] arguments)
        {
            if (arguments.Length < 2)
            {
                state.Io.Err.WriteLine("fatal error: no input path");
                return 1;
            }

            var absolute = FilePaths.Expand(FilePaths.GetAbsolute(FilePaths.Normalize(arguments[1]), state));
            var dir = Path.GetDirectoryName(absolute);

            if (!Directory.Exists(dir))
            {
                state.Io.Err.WriteLine($"fatal error: no such directory \"{dir}\"");
                return (int)ExitCodes.FILESYSTEM_ERROR;
            }

            if (File.Exists(absolute))
            {
                state.Io.Err.WriteLine($"fatal error: file \"{absolute}\" already exists");
                return (int)ExitCodes.FILESYSTEM_ERROR;
            }

            File.Create(absolute).Dispose();
            return (int)ExitCodes.SUCCESS;
        }
    }
}
