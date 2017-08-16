using System;

namespace TobeOS.Scripting.Ast
{
    public class Command
    {
        private string[] mArguments;
        private string mExecutable;

        public string[] Arguments
        {
            get { return mArguments; }
        }

        public string Executable { get { return mExecutable; } }

        public Command()
        {
            mExecutable = "<none>";
            mArguments = new string[0];
        }

        public Command(string executable)
        {
            mExecutable = executable;
            mArguments = new string[0];
        }

        public Command(string executable, string[] arguments)
        {
            mExecutable = executable;
            mArguments = arguments;
        }

        public virtual int Run(KernelState state, Programs.Program[] programs)
        {
            Programs.Program p = null;

            foreach (var program in programs)
            {
                state.Debug($"Is program {program.GetName()} {Executable}...?");
                if (program.GetName() == Executable)
                {
                    p = program;
                    break;
                }
            }

            if (p == null)
            {
                Console.WriteLine($"Unrecognized program: \"{Executable}\"");
                return (int) ExitCodes.FAILURE;
            }
            else
            {
                state.Debug($"Running {Executable}...");
                var args = new string[mArguments.Length + 1];
                args[0] = Executable;

                for (int i = 0; i < mArguments.Length; i++)
                {
                    args[i + 1] = mArguments[i];
                    state.Debug($"  * {mArguments[i]}");
                }
                
                try
                {
                   var code = p.Run(state, args);
                    state.Debug($"Exited with {code}");
                    return code;
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    return (int)ExitCodes.FAILURE;
                }
            }
        }

        public static Command Parse(String str)
        {
            var tok = new Tokenizer(str);
            tok.Scan();
            var p = new Parser(tok);

            if (p.Errors.Length != 0)
            {
                for (int i = 0; i < p.Errors.Length; i++)
                    Console.WriteLine(p.Errors[i]);
                return null;
            }

            return p.ParseCommand();
        }

        private static Command ParseOld(string str)
        {
            var split = ParseArguments(str);
            var cmd = new Command(split[0].Trim());

            int nonEmptyLength = 0;


            for (int i = 1; i < split.Length; i++)
            {
                if (split[i].Trim().Length > 0)
                    nonEmptyLength++;
            }

            cmd.mArguments = new string[nonEmptyLength];

            int index = 0;


            for (int i = 1; i < split.Length; i++)
            {
                if (index >= nonEmptyLength) break;
                var trim = split[i].Trim();

                if (trim.Length > 0)
                {
                    cmd.mArguments[index++] = trim;
                }
            }

            while (index < nonEmptyLength)
            {

            }


            return cmd;
        }

        public static string[] ParseArguments(string str)
        {
            var list = new StringList();
            var scanner = new StringScanner(str);
            StringBuf buf = null;

            while (!scanner.IsDone())
            {
                var ch = scanner.Read();

                if (ch == ' ')
                {
                    if (buf != null)
                    {
                        list.Add(buf.Take());
                        buf = null;
                    }
                }
                if (ch != '"')
                {
                    if (buf == null) buf = new StringBuf();
                    buf.Add(ch);
                }
                else
                {
                    if (buf != null)
                    {
                        list.Add(buf.Take());
                        buf = null;
                    }

                    int start = scanner.Index;
                    int remaining = scanner.Remaining();

                    if (remaining == 0) throw new FormatException($"Unterminated double-quote at position {scanner.Index}");

                    var b = new StringBuf();
                    bool terminated = false;

                    for (int i = 0; i < remaining; i++)
                    {
                        var c = scanner.Read();

                        if (c != '"')
                            b.Add(c);
                        else
                        {
                            terminated = true;
                            break;
                        }
                    }

                    if (!terminated)
                        throw new FormatException($"Unterminated double-quote at position {start}");

                    list.Add(b.Take());
                }
            }

            if (buf != null)
            {
                list.Add(buf.Take());
            }

            return list.Array;
        }
    }
}
