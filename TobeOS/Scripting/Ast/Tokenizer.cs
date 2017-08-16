using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS.Scripting.Ast
{
    class Tokenizer
    {
        private StringList errors = new StringList();
        private ArrayList<Token> tokens = new ArrayList<Token>();
        private StringScanner scanner;

        public string[] Errors
        {
            get { return errors.Array; }
        }

        public Token[] Tokens
        {
            get { return tokens.Array; }
        }

        public Tokenizer(String str)
        {
            scanner = new StringScanner(str);
        }

        public void Scan()
        {
            using (var buf = new StringBuf())
            {

                while (!scanner.IsDone())
                {
                    var ch = scanner.Read();

                    if (ch == '|')
                    {
                        if (buf.Length != 0)
                        {
                            tokens.Add(new Token(TokenType.STRING, buf.Take()));
                        }

                        tokens.Add(new Token(TokenType.PIPE, "|"));
                    }
                    else if (ch == ' ')
                    {
                        if (buf.Length != 0)
                        {
                            tokens.Add(new Token(TokenType.STRING, buf.Take()));
                        }
                    }
                    else if (ch == '"')
                    {
                        if (buf.Length != 0)
                        {
                            tokens.Add(new Token(TokenType.STRING, buf.Take()));
                        }

                        int start = scanner.Index;
                        int remaining = scanner.Remaining();

                        if (remaining == 0) errors.Add($"Unterminated double-quote as position {scanner.Index}");

                        else
                        {
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
                                errors.Add($"Unterminated double-quote as position {start}");

                            tokens.Add(new Token(TokenType.STRING, b.Take()));
                        }
                    }
                    else
                    {
                        buf.Add(ch);
                    }
                }

                if (buf.Length != 0)
                {
                    tokens.Add(new Token(TokenType.STRING, buf.Take()));
                }
            }
        }
    }
}
