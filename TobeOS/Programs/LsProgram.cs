using System;
using System.IO;

namespace TobeOS.Programs
{
    class LsProgram : Program
    {
        public override string GetName()
        {
            return "ls";
        }

        public override int Run(KernelState state, string[] arguments)
        {
            String dir;

            if (arguments.Length < 2)
            {
                dir = state.WorkingDirectory;
            }
            else
            {
                dir = arguments[1];
            }

            dir = FilePaths.Expand(FilePaths.GetAbsolute(FilePaths.Normalize(dir), state));

            if (!Directory.Exists(dir))
            {
                state.Io.Err.WriteLine($"fatal error: no such directory \"{dir}\"");
                return (int)ExitCodes.FILESYSTEM_ERROR;
            }

            List(state, dir);
            return (int)ExitCodes.SUCCESS;
        }

        private void List(KernelState state, string dir)
        {
            foreach (string path in Directory.GetDirectories(dir))
            {
                state.Io.Out.WriteLine(path);
            }

            foreach (string path in Directory.GetFiles(dir))
            {
                state.Io.Out.WriteLine(path);
            }
        }
    }
}
