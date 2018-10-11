using System;
using System.Collections.Generic;
using System.Text;


namespace Parser
{
    static class Tokenizer
    {
        public static List<Token> GetTokens(string line)
        {
            List<Token> tokens = new List<Token>();

            if (null == line || line == string.Empty)
                return tokens;

            string input = line;
            bool inside = false;

            if (input.StartsWith("//"))
            {
                Token tok = new Token(input, EnumTokenType.TokenComment);
                tokens.Add(tok);
                return tokens;
            }

            string token = string.Empty;

            for (int i = 0; i < input.Length; ++i)
            {
                char ch = input[i];

                if (inside)
                {
                    if ('"' == ch)
                    {
                        inside = false;
                        token += ch;

                        Token tok = new Token(token, EnumTokenType.TokenString);
                        tokens.Add(tok);
                        token = string.Empty;
                    }
                    else
                        token += ch;

                    continue;
                }
                else
                {
                    switch (ch)
                    {
                        case '"':
                            inside = true;
                            token += ch;
                        continue;

                        case '\t':
                        case ' ': 
                        case '.':
                        case ';':
                        case ',':
                        case '(':
                        case ')':
                        {
                            Token tok = null;

                            if (token.Length > 0)
                            {
                                tok = new Token(token, EnumTokenType.TokenDefault);

                                if ('@' == token[0])
                                    tok.type = EnumTokenType.TokenVariable;

                                tokens.Add(tok);
                            }

                            tok = new Token(ch.ToString(), EnumTokenType.TokenSeparator);
                            tokens.Add(tok);
                            token = string.Empty;
                        }
                        break;

                        default:
                            token += ch;
                        break;
                    }
                }
            }

            if (token.Length > 0)
            {
                Token tok = new Token(token, EnumTokenType.TokenDefault);
                tokens.Add(tok);
            }

            return tokens;
        }


        public static List<string> Init(string line)
        {
            int charIndex = 0;

            bool quoteInside = false;

            List<string> tokens = new List<string>();

            if (null == line || line == string.Empty)
                return tokens;

            string input = line.Trim().Replace('\t', ' ');
            bool inside = false;

            StringBuilder token = new StringBuilder();
                        
            foreach (char ch in input)
            {
                switch (ch)
                {
                    case '\\':
                    {
                        if (charIndex + 1 == input.Length - 1)
                        {
                            if ('"' == input[charIndex + 1])
                            {
                                token.Append(ch);
                                break;
                            }
                        }

                        if (charIndex + 1 < input.Length)
                        {
                            if (inside && input[charIndex + 1] == '"')
                            {
                                quoteInside = true;
                                break;
                            }
                        }

                        token.Append(ch);
                    }
                    break;

                    case ')':
                    case '(':
                    case ',':
                    case ';':
                        if (inside)
                        {
                            token.Append(ch);
                        }
                        else
                        {
                            if (token.Length > 0)
                                tokens.Add(token.ToString());

                            tokens.Add(ch.ToString());
                            token.Remove(0, token.Length);
                        }
                    break;

                    case '"':
                        if (inside)
                        {
                            if (quoteInside)
                            {
                                token.Append(ch);
                                quoteInside = false;
                            }
                            else
                            {
                                inside = false;
                                tokens.Add(token.ToString());
                                token.Remove(0, token.Length);
                            }
                        }
                        else
                        {
                            if (token.Length > 0)
                                tokens.Add(token.ToString());
                            
                            inside = true;
                            token.Remove(0, token.Length);                            
                        }
                    break;

                    case ' ':
                    {
                        if (inside)
                            token.Append(ch);
                        else
                        {
                            if (token.Length > 0)
                            {
                                tokens.Add(token.ToString());
                                token.Remove(0, token.Length);    
                            }
                        }
                    }
                    break;

                    default:
                        token.Append(ch);
                    break;
                }
                ++charIndex;
            }

            if (token.Length > 0)
            {
                tokens.Add(token.ToString());
                token.Remove(0, token.Length);
            }

            return tokens;
        }


    }
}