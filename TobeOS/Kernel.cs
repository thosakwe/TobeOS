﻿using System;
using TobeOS.Scripting.Ast;
using Sys = Cosmos.System;

namespace TobeOS
{
    public class Kernel : Sys.Kernel
    {
        private Sys.FileSystem.CosmosVFS fileSystem;
        private Programs.Program[] programs;

        protected override void BeforeRun()
        {
            var fs = fileSystem = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            programs = new Programs.Program[] {
                new Programs.CatProgram(),
                new Programs.CdProgram(),
                new Programs.EchoProgram(),
                new Programs.LsProgram(),
                new Programs.MkdirProgram(),
                new Programs.ShutdownProgram(),
                new Programs.TouchProgram(),
                new Programs.ProgramsProgram(),
                new Programs.VolumesProgram()
            };
        }

        protected override void Run()
        {
            var state = new KernelState
            {
                FileSystem = fileSystem,
                LastExitCode = 0,
                Programs = programs,
                WorkingDirectory = "0:\\"
            };

            while (true)
            {
                Console.Write($"{state.WorkingDirectory}> ");
                var input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    var cmd = Command.Parse(input);
                    Programs.Program p = null;

                    foreach (var program in programs)
                    {
                        if (program.GetName() == cmd.Executable)
                        {
                            p = program;
                            break;
                        }
                    }

                    if (p == null)
                    {
                        Console.WriteLine($"Unrecognized program: \"{cmd.Executable}\"");
                    }
                    else
                    {
                        var args = new string[cmd.Arguments.Length + 1];
                        args[0] = cmd.Executable;

                        for (int i = 0; i < cmd.Arguments.Length; i++)
                            args[i + 1] = cmd.Arguments[i];
                        

                        int exitCode;

                        try
                        {
                            exitCode = p.Run(state, args);
                        }
                        catch (Exception exc)
                        {
                            exitCode = 1;
                            Console.WriteLine(exc.Message);
                        }

                        state.LastExitCode = exitCode;
                    }
                }
            }
        }
    }
}
