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
                Console.WriteLine($"fatal error: no such directory \"{dir}\"");
                return 2;
            }

            List(dir);
            return 0;
        }

        private void List(string dir)
        {
            foreach (string path in Directory.GetDirectories(dir))
            {
                Console.WriteLine(path);
            }

            foreach (string path in Directory.GetFiles(dir))
            {
                Console.WriteLine(path);
            }
        }
    }
}
