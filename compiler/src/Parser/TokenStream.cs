using System.Collections.Generic;
using LanguageLexer;

namespace LanguageParser;

public class TokenStream
{
    private readonly Lexer lexer;
    private readonly List<Token> lookupBuffer;
    private Token nextToken;

    public TokenStream(string expression)
    {
        lexer = new Lexer(expression);
        nextToken = lexer.ParseToken();
        lookupBuffer = new List<Token>();
    }

    public Token Peek()
    {
        return nextToken;
    }

    public Token Peek(int n)
    {
        if (n == 0)
        {
            return nextToken;
        }

        while (n > lookupBuffer.Count)
        {
            Token token = lexer.ParseToken();
            lookupBuffer.Add(token);
        }

        return lookupBuffer[n - 1];
    }

    public void Advance()
    {
        if (lookupBuffer.Count > 0)
        {
            nextToken = lookupBuffer[0];
            lookupBuffer.RemoveAt(0);
        }
        else
        {
            nextToken = lexer.ParseToken();
        }
    }
}