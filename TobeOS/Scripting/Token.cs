using System;
using System.Text;

namespace TobeOS.Scripting
{
    public class Token
    {
        TokenType mType;
        String mText;

        public TokenType Type
        {
            get { return mType; }
        }

        public String Text
        {
            get { return mText; }
        }

        public Token(TokenType type, String text)
        {
            mType = type;
            mText = text;
        }
    }
}
