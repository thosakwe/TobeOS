using System;

namespace TobeOS.Scripting.Ast
{
    class Command
    {
        private string[] mArguments;
        private String mExecutable;

        public string[] Arguments
        {
            get { return mArguments; }
        }

        public String Executable { get { return mExecutable; } }

        public Command(String executable)
        {
            mExecutable = executable;
        }

        public static Command Parse(String str)
        {
            var split = str.Split(' ');
            var cmd = new Command(split[0].Trim());
            cmd.mArguments = new string[split.Length];

            for (int i = 1; i < split.Length; i++)
                cmd.mArguments[i - 1] = split[i];

            return cmd;
        }
    }
}
