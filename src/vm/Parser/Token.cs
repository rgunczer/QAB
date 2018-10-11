using System;
using System.Collections.Generic;
using System.Text;


namespace Parser
{
    public enum EnumTokenType
    {
        TokenComment,
        TokenCommand,
        TokenSubCommand,
        TokenSubCommandSeparator, // dot
        TokenStringConstant,
        TokenVariable,
        TokenString,
        TokenDefault,
        TokenSeparator,
        TokenWhiteSpace,
    };
    
    class Token
    {
        public EnumTokenType type;
        public string text;

        public Token(string text, EnumTokenType type)
        {
            this.text = text;
            this.type = type;
        }

    }
}
