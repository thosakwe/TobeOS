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
            // TODO: Read input from stdin...
            if (arguments.Length < 2)
            {
                Console.WriteLine("fatal error: no input file");
                return 1;
            }

            for (int i = 1; i < arguments.Length; i++)
            {
                var absolute = FilePaths.Expand(FilePaths.GetAbsolute(FilePaths.Normalize(arguments[i]), state));

                if (!File.Exists(absolute))
                {

                    Console.WriteLine($"fatal error: no file \"{absolute}\" exists");

                    // TODO: Constant exit codes
                    return 2;
                }
            }

            for (int i = 1; i < arguments.Length; i++)
            {
                var absolute = FilePaths.Expand(FilePaths.GetAbsolute(FilePaths.Normalize(arguments[i]), state));
                Console.Write(File.ReadAllText(absolute));
            }

            Console.WriteLine();
            return 0;
        }
    }
}
