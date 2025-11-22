using System;

using LanguageLexer;

namespace LanguageParser;

#pragma warning disable RCS1194 // Конструкторы исключения не нужны, т.к. это не класс общего назначения.
public class UnexpectedLexemeException : Exception
{
    public UnexpectedLexemeException(TokenType expected, Token actual)
        : base($"Unexpected lexeme {actual} where expected {expected}")
    {
    }
}
#pragma warning restore RCS1194 для чего этот класс