using Cosmos.Common;
using System;
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
                new Programs.EditProgram(),
                new Programs.FileProgram(),
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
                Debugger = mDebugger,
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
                    mDebugger.Send($"Input: {input}");
                    var cmd = Command.Parse(input);
                    mDebugger.Send("parsed command");
                    if (cmd != null)
                    {
                        mDebugger.Send("bEFORE IO");
                        var io = state.Io = new ProcessIO
                        {
                            In = new ConsoleStdin(),
                            Out = new StringStdWriter(),
                            Err = new StringStdWriter()
                        };


                        mDebugger.Send("bEFORE RUN");
                        state.LastExitCode = cmd.Run(state, programs);
                        mDebugger.Send("AFTER IO");
                        if (!io.Out.IsEmpty) Console.Write(new string(io.Out.GetContents()));
                        if (!io.Err.IsEmpty) Console.Write(new string(io.Err.GetContents()));
                        mDebugger.Send("bEFORE DISPPOSE");
                        state.Io.Dispose();
                    }
                    else
                    {
                        Console.WriteLine($"Invalid command: \"{input}\"");
                    }
                }
            }
        }
    }
}
