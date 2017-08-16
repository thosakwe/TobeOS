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
            if (arguments.Length < 2)
            {
                Console.WriteLine("fatal error: not enough arguments");
                return 1;
            }

            List(arguments[1]);
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
