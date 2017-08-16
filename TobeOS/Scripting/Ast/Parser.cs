using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS.Scripting.Ast
{
    class Parser
    {
        private StringList errors = new StringList();
        private int index = -1;
        private Token current = null;
        private Token[] tokens;

        public Parser(Tokenizer t)
        {
            for (int i = 0; i < t.Errors.Length; i++)
            {
                errors.Add(t.Errors[i]);
            }

            tokens = t.Tokens;
        }

        public string[] Errors
        {
            get { return errors.Array; }
        }

        public Command ParseCommand()
        {
            var cmd = ParseSimpleCommand();

            while (cmd != null && Next(TokenType.PIPE))
            {
                var c = ParseSimpleCommand();

                if (c == null)
                {
                    Console.WriteLine("fatal error: expected command after pipe");
                    cmd = null;
                    break;
                }

                cmd = new PipedCommand(cmd, c);
            }

            return cmd;
        }

        Command ParseSimpleCommand()
        {
            var list = new StringList();

            while (Next(TokenType.STRING))
            {
                list.Add(current.Text);
            }

            if (list.Length == 0) return null;
            if (list.Length == 1) return new Command(list.Array[0]);
            return new Command(list.Array[0], list.Skip(1).Array);
        }

        bool Next(TokenType type)
        {
            if (index >= tokens.Length - 1) return false;
            var tok = tokens[index + 1];

            if (tok.Type == type)
            {
                index++;
                current = tok;
                return true;
            }

            return false;
        }
    }
}
