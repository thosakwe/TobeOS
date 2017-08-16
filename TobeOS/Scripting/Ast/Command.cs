using System;

namespace TobeOS.Scripting.Ast
{
    class Command
    {
        private string[] mArguments;
        private string mExecutable;

        public string[] Arguments
        {
            get { return mArguments; }
        }

        public string Executable { get { return mExecutable; } }

        public Command(string executable)
        {
            mExecutable = executable;
        }

        public static Command Parse(string str)
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

                    if (remaining == 0) throw new FormatException($"Unterminated double-quote as position {scanner.Index}");

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
                        throw new FormatException($"Unterminated double-quote as position {start}");

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
