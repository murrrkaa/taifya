using System;

using LanguageLexer;

namespace LanguageParser;

#pragma warning disable RCS1194
public class UnexpectedLexemeException : Exception
{
    public UnexpectedLexemeException(TokenType expected, Token actual)
        : base($"Unexpected lexeme {actual} where expected {expected}")
    {
    }
}
#pragma warning restore