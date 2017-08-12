using System;
using System.Collections.Generic;
using TobeOS.Scripting;
using Sys = Cosmos.System;

namespace TobeOS
{
    public class Kernel: Sys.Kernel
    {
        
        protected override void Run()
        {
            while (true)
            {
                Console.Write("> ");
                String input = Console.ReadLine();

                Scanner scanner = new Scanner();
                scanner.Scan(input);

                Console.WriteLine($"{scanner.Tokens.Count} token(s)");
            }
        }
    }
}
