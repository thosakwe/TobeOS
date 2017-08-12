using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS.Scripting.Ast
{
    class Command
    {
        private List<String> mArguments = new List<string>();
        private String mExecutable;

        public List<String> Arguments
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

            for (int i = 1; i < split.Length; i++)
                cmd.Arguments.Add(split[i].Trim());

            return cmd;
        }
    }
}
