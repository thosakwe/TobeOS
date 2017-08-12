using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS.Scripting.Ast
{
    class Command
    {
        private List<String> mArguments = new List<string>();

        public List<String> Arguments
        {
            get { return mArguments; }
        }

        public String Executable { get; set; }
    }
}
