using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS.Programs
{
    public class FileProgram : Program
    {
        public override string GetName()
        {
            return "file";
        }

        public override int Run(KernelState state, string[] arguments)
        {
            string path = null;
            bool append = false, help = false;


            state.Debug("hello help!!!");
            for (int i = 1; i < arguments.Length; i++)
            {
                var arg = arguments[i];

                state.Debug($"arg: {arg}");

                if (arg == "--append" || arg == "-a")
                {
                    append = true;
                }
                else if (arg == "--help" || arg == "-h")
                {
                    help = true;
                    break;
                }
                else if (arg.Trim().Length > 0)
                {
                    path = arg;
                    break;
                }
            }

            if (help)
            {
                state.Debug("Help!!!");
                state.Io.Out.WriteLine("file: Write to file");
                state.Io.Out.WriteLine("usage: file [--append] [-a] [--help] [-h] <path>");
                state.Io.Out.WriteLine("Options");
                state.Io.Out.WriteLine("  --append, -a: Append to the file, if it exists..");
                state.Io.Out.WriteLine("  --help, -h:   Print this help information.");
                return (int)ExitCodes.SUCCESS;
            }

            else if (path == null)
            {
                state.Debug("No path!!!");
                state.Io.Err.WriteLine("fatal error: no input path");
                return (int)ExitCodes.FAILURE;
            }

            else
            {
                state.Debug("Reading stdin!!!");
                var absolute = FilePaths.Expand(FilePaths.GetAbsolute(FilePaths.Normalize(arguments[1]), state));

                using (var buf = new StringBuf())
                {
                    int i = 0;

                    while (state.Io.In.CanRead)
                    {
                        if (i++ > 0)
                            buf.Add('\n');
                        buf.AddString(state.Io.In.ReadLine());
                    }

                    try
                    {
                        // TODO: Use StreamReader???
                        var s = buf.ToString();

                        if (append && System.IO.File.Exists(absolute))
                        {
                            s = System.IO.File.ReadAllText(absolute) + '\n' + s;
                        }

                        System.IO.File.WriteAllText(absolute, s);
                        return (int)ExitCodes.SUCCESS;
                    }
                    catch (Exception exc)
                    {
                        state.Io.Err.WriteLine(exc.Message);
                        return (int)ExitCodes.FILESYSTEM_ERROR;
                    }
                }
            }
        }
    }
}
