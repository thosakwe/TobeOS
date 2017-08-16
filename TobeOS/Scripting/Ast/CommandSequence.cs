using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS.Scripting.Ast
{
    public class CommandSequence
    {
        private ArrayList<Command> commands = new ArrayList<Command>();

        public Command[] Commands
        {
            get { return commands.Array; }
        }
    }
}
