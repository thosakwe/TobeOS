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
                Console.WriteLine("fatal error: no input path");
                return 1;
            }

            var absolute = FilePaths.Expand(FilePaths.GetAbsolute(FilePaths.Normalize(arguments[1]), state));
            var dir = Path.GetDirectoryName(absolute);

            if (!Directory.Exists(dir))
            {
                Console.WriteLine($"fatal error: no such directory \"{dir}\"");
                return 1;
            }

            if (File.Exists(absolute))
            {
                Console.WriteLine($"fatal error: file \"{absolute}\" already exists");
                return 2;
            }

            File.Create(absolute).Dispose();
            return 0;
        }
    }
}
