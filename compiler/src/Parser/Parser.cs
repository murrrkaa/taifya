using System;
using System.Collections.Generic;
using System.Globalization;
using LanguageLexer;

namespace LanguageParser;

/// <summary>
/// Выполняет синтаксический разбор выражений.
/// Грамматика описана в файле `docs/specification/expressions-grammar.md`.
/// </summary>
public class Parser
{
    private readonly TokenStream tokens;

    private Parser(string code)
    {
        tokens = new TokenStream(code);
    }

    /// <summary>
    /// Вычисляет значение выражения и возвращает результат.
    /// </summary>
    public static decimal EvaluateExpression(string code)
    {
        Parser parser = new Parser(code);
        return parser.ParseExpression();
    }

    // expression = term_expression, { ("+" | "-"), term_expression } ;
    private decimal ParseExpression()
    {
        decimal value = ParseTermExpression();
        while (true)
        {
            Token token = tokens.Peek();
            switch (token.Type)
            {
                case TokenType.PlusSign:
                    tokens.Advance();
                    value += ParseTermExpression();
                    break;
                case TokenType.MinusSign:
                    tokens.Advance();
                    value -= ParseTermExpression();
                    break;
                default:
                    return value;
            }
        }
    }

    // term_expression = factor_expression, { ("*" | "/"), factor_expression } ;
    private decimal ParseTermExpression()
    {
        decimal value = ParseFactorExpression();
        while (true)
        {
            Token token = tokens.Peek();
            switch (token.Type)
            {
                case TokenType.MultiplySign:
                    tokens.Advance();
                    value *= ParseFactorExpression();
                    break;
                case TokenType.DivideSign:
                    tokens.Advance();
                    value /= ParseFactorExpression();
                    break;
                default:
                    return value;
            }
        }
    }

    // factor_expression = [ "+" | "-" ], simple_expression ;
    private decimal ParseFactorExpression()
    {
        Token token = tokens.Peek();
        switch (token.Type)
        {
            case TokenType.PlusSign:
                tokens.Advance();
                return ParseSimpleExpression();

            case TokenType.MinusSign:
                tokens.Advance();
                return -ParseSimpleExpression();

            default:
                return ParseSimpleExpression();
        }
    }

    // simple_expression = number | identifier | constant | function_call | "(", expression, ")" ;
    private decimal ParseSimpleExpression()
    {
        Token token = tokens.Peek();
        switch (token.Type)
        {
            case TokenType.Number:
                tokens.Advance();
                return decimal.Parse(token.Value!.ToString(), CultureInfo.InvariantCulture);
            case TokenType.Identifier:
                throw new NotImplementedException("Variables are not supported yet");

            case TokenType.Pi:
                tokens.Advance();
                return 3.141592653589793m;

            case TokenType.Euler:
                tokens.Advance();
                return 2.718281828459045m;

            case TokenType.Abs:
            case TokenType.Min:
            case TokenType.Max:
            case TokenType.Round:
            case TokenType.Ceil:
            case TokenType.Floor:
                return ParseFunctionCall();

            case TokenType.OpenParenthesis:
                tokens.Advance();
                decimal expr = ParseExpression();
                Match(TokenType.CloseParenthesis);
                return expr;

            default:
                throw new UnexpectedLexemeException(TokenType.Number, token);
        }
    }

    // function_call = function_name, "(", [ expression_list ], ")" ;
    private decimal ParseFunctionCall()
    {
        Token functionToken = tokens.Peek();
        tokens.Advance();

        Match(TokenType.OpenParenthesis);

        List<decimal> arguments = new List<decimal>();
        if (tokens.Peek().Type != TokenType.CloseParenthesis)
        {
            arguments.Add(ParseExpression());
            while (tokens.Peek().Type == TokenType.Comma)
            {
                tokens.Advance();
                arguments.Add(ParseExpression());
            }
        }

        Match(TokenType.CloseParenthesis);

        string functionName = functionToken.Type.ToString().ToLower();
        return BuiltinFunctions.Invoke(functionName, arguments);
    }

    /// <summary>
    /// Пропускает ожидаемую лексему либо бросает исключение.
    /// </summary>
    private void Match(TokenType expected)
    {
        Token actual = tokens.Peek();
        if (actual.Type != expected)
        {
            throw new UnexpectedLexemeException(expected, actual);
        }

        tokens.Advance();
    }
}