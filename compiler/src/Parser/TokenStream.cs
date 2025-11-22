using LanguageLexer;

namespace LanguageParser;

public class TokenStream
{
    private readonly Lexer lexer;
    private Token nextToken;

    public TokenStream(string sql)
    {
        lexer = new Lexer(sql);
        nextToken = lexer.ParseToken();
    }

    public Token Peek()
    {
        return nextToken;
    }

    public void Advance()
    {
        nextToken = lexer.ParseToken();
    }
}