/*
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TobeOS.Scripting
{
    // TODO: Syntax errors
    class Scanner
    {
        private static Regex rgxId = new Regex("[A-Za-z_][A-Za-z0-9_-]*");

        private List<Token> mTokens = new List<Token>();

        public List<Token> Tokens
        {
            get { return mTokens; }
        }

        public void Scan(String text)
        {
            String str = text;

            while (str.Length > 0)
            {
                Match idMatch = rgxId.Match(str);

                if (idMatch != null)
                {
                    mTokens.Add(new Token(TokenType.ID, idMatch.Value));

                    if (str.Length == idMatch.Value.Length) str = "";
                    else
                        str = str.Substring(idMatch.Value.Length);
                }
                else
                {
                    // TODO: Scan string
                    if (str.Length == 1)
                        str = "";
                    else str = str.Substring(1);
                }
            }
        }
    }
}

*/