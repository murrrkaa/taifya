using System;
using System.Text;

namespace LanguageLexer;

public class Lexer
{
    private static readonly Dictionary<string, TokenType> Keywords = new()
    {
        { "var", TokenType.Var },
        { "const", TokenType.Const },
        { "if", TokenType.If },
        { "else", TokenType.Else },
        { "for", TokenType.For },
        { "while", TokenType.While },
        { "function", TokenType.Function },
        { "return", TokenType.Return },
        { "break", TokenType.Break },
        { "continue", TokenType.Continue },
        { "print", TokenType.Print },
        { "and", TokenType.And },
        { "or", TokenType.Or },
        
         // новые функции
        { "abs", TokenType.Abs },
        { "min", TokenType.Min },
        { "max", TokenType.Max },
        { "round", TokenType.Round },
        { "ceil", TokenType.Ceil },
        { "floor", TokenType.Floor },

        // числовые константы
        { "Pi", TokenType.Pi },
        { "Euler", TokenType.Euler },
    };

    private readonly TestScanner scanner;

    public Lexer(string text)
    {
        scanner = new TestScanner(text);
    }

    public Token ParseToken()
    {
        SkipWhiteSpacesAndComments();

        if(scanner.IsEnd())
        {
            return new Token(TokenType.EndOfFile);
        }

        char ch = scanner.Peek();

        if (char.IsLetter(ch))
        {
            return ParseIdentifierOrKeywords();
        }

        if (char.IsDigit(ch) || ch == '-')
        {
            if (ch == '-' && char.IsDigit(scanner.Peek(1)))
            {
                return ParseNumericLiteral();
            }

            if(char.IsDigit(ch))
            {
                return ParseNumericLiteral();
            }
        }

        if(ch == '\'')
        {
            return ParseStringLiteral();
        }

        switch (ch)
        {
            case ';':
                scanner.Advance();
                return new Token(TokenType.Semicolon);
            case ',':
                scanner.Advance();
                return new Token(TokenType.Comma);
            case ':':
                scanner.Advance();
                return new Token(TokenType.Colon);
            case '{':
                scanner.Advance();
                return new Token(TokenType.OpenBrace);
            case '}':
                scanner.Advance();
                return new Token(TokenType.CloseBrace);
            case '(':
                scanner.Advance();
                return new Token(TokenType.OpenParenthesis);
            case ')':
                scanner.Advance();
                return new Token(TokenType.CloseParenthesis);
            case '[':
                scanner.Advance();
                return new Token(TokenType.OpenBracket);
            case ']':
                scanner.Advance();
                return new Token(TokenType.CloseBracket);
            case '+':
                scanner.Advance();
                return new Token(TokenType.PlusSign);
            case '*':
                scanner.Advance();
                return new Token(TokenType.MultiplySign);
            case '/':
                scanner.Advance();
                return new Token(TokenType.DivideSign);

            case '=':
                scanner.Advance();
                if (scanner.Peek() == '=')
                {
                    scanner.Advance();
                    return new Token(TokenType.EqualThan);
                }

                return new Token(TokenType.AssignThan);

            case '!':
                scanner.Advance();
                if (scanner.Peek() == '=')
                {
                    scanner.Advance();
                    return new Token(TokenType.NotEqualThan);
                }

                return new Token(TokenType.Error);

            case '<':
                scanner.Advance();
                if (scanner.Peek() == '=')
                {
                    scanner.Advance();
                    return new Token(TokenType.LessThanOrEqual);
                }

                return new Token(TokenType.LessThan);

            case '>':
                scanner.Advance();
                if (scanner.Peek() == '=')
                {
                    scanner.Advance();
                    return new Token(TokenType.GreaterThanOrEqual);
                }

                return new Token(TokenType.GreaterThan);

            case '-':
                scanner.Advance();
                return new Token(TokenType.MinusSign);
        }

        scanner.Advance();
        return new Token(TokenType.Error, new TokenValue(ch.ToString()));
    }

    private Token ParseIdentifierOrKeywords()
    {
        StringBuilder value = new StringBuilder();
        value.Append(scanner.Peek());
        scanner.Advance();

        while (!scanner.IsEnd())
        {
            char ch = scanner.Peek();
            if(char.IsLetterOrDigit(ch))
            {
                value.Append(ch);
                scanner.Advance();
            }
            else
            {
                break;
            }
        }

        string text = value.ToString();
        if(Keywords.TryGetValue(text, out TokenType type))
        {
            return new Token(type);
        }

        return new Token(TokenType.Identifier, new TokenValue(text));
    }

    private Token ParseStringLiteral()
    {
        scanner.Advance();
        StringBuilder contents = new StringBuilder();

        while (!scanner.IsEnd() && scanner.Peek() != '\'')
        {
            if (scanner.Peek() == '\\')
            {
                scanner.Advance();
                if (scanner.IsEnd())
                {
                    return new Token(TokenType.Error);
                }

                char next = scanner.Peek();

                switch (next)
                {
                    case '\'':
                        contents.Append('\'');
                        scanner.Advance();
                        break;
                    case '\\':
                        contents.Append('\\');
                        scanner.Advance();
                        break;
                    case 'n':
                        contents.Append('\n');
                        scanner.Advance();
                        break;
                    default:
                        return new Token(TokenType.Error);
                }
            }
            else
            {
                contents.Append(scanner.Peek());
                scanner.Advance();
            }
        }

        if (scanner.IsEnd())
        {
            return new Token(TokenType.Error);
        }

        scanner.Advance();
        return new Token(TokenType.String, new TokenValue(contents.ToString()));
    }

    private Token ParseNumericLiteral()
    {
        StringBuilder value = new StringBuilder();
        if (scanner.Peek() == '-')
        {
            value.Append('-');
            scanner.Advance();
        }

        while (!scanner.IsEnd() && char.IsDigit(scanner.Peek()))
        {
            value.Append(scanner.Peek());
            scanner.Advance();
        }

        if (value.Length == 0 || value.ToString() == "-")
        {
            return new Token(TokenType.Error, new TokenValue(value.ToString()));
        }

        if (!scanner.IsEnd() && scanner.Peek() == '.')
        {
            value.Append('.');
            scanner.Advance();

            int fractionStart = value.Length;

            while (!scanner.IsEnd() && char.IsDigit(scanner.Peek()))
            {
                value.Append(scanner.Peek());
                scanner.Advance();
            }

            if (value.Length == fractionStart)
            {
                return new Token(TokenType.Error, new TokenValue(value.ToString()));
            }
        }

        string numberText = value.ToString();
        return new Token(TokenType.Number, new TokenValue(numberText));
    }

    private void SkipWhiteSpacesAndComments()
    {
        do
        {
            SkipWhiteSpaces();
        }
        while (TryParseMultilineComment() || TryParseSingleLineComment());
    }

    private void SkipWhiteSpaces()
    {
        while (!scanner.IsEnd() && char.IsWhiteSpace(scanner.Peek()))
        {
            scanner.Advance();
        }
    }

    private bool TryParseMultilineComment()
    {
        if(scanner.Peek() == '/' && scanner.Peek(1) == '*')
        {
            scanner.Advance();
            scanner.Advance();
            while (!scanner.IsEnd())
            {
                if(scanner.Peek() == '*' && scanner.Peek(1) == '/')
                {
                    scanner.Advance();
                    scanner.Advance();
                    return true;
                }

                scanner.Advance();
            }

            return true;
        }

        return false;
    }

    private bool TryParseSingleLineComment()
    {
        if(scanner.Peek() == '#')
        {
            while(!scanner.IsEnd() && scanner.Peek() != '\n')
            {
                scanner.Advance();
            }

            return true;
        }

        return false;
    }
}
