using System;
using System.IO;
using System.Text;

namespace TobeOS.Programs
{
    public class MkdirProgram : Program
    {
        public override string GetName()
        {
            return "mkdir";
        }

        public override int Run(KernelState state, string[] arguments)
        {
            var list = new StringList();
            bool p = false;

            for (int i = 1; i < arguments.Length; i++)
            {
                if (arguments[i] == "-p")
                {
                    p = true;
                }
                else
                {
                    list.Add(arguments[i]);
                }
            }

            if (list.Length == 0)
            {
                Console.WriteLine("fatal error: no input path");
                return 1;
            }

            foreach (var path in list.Array)
            {
                var absolute = FilePaths.Expand(FilePaths.GetAbsolute(FilePaths.Normalize(path), state));
                var dir = Path.GetDirectoryName(absolute);

                if (Directory.Exists(absolute))
                {
                    if (!p)
                    {
                        Console.WriteLine($"fatal error: directory \"{absolute}\" already exists");
                        return 2;
                    }
                }
                else Directory.CreateDirectory(absolute);
            }

            return 0;
        }
    }
}
