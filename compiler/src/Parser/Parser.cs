using System;
using System.Collections.Generic;
using System.Globalization;
using Execution;
using LanguageLexer;

namespace LanguageParser;

public class Parser
{
    private readonly Context context;
    private readonly IEnvironment environment;
    private readonly TokenStream tokens;

    public Parser(Context context, IEnvironment environment, string sourceCode)
    {
        this.context = context;
        this.environment = environment;
        this.tokens = new TokenStream(sourceCode);
    }

    public void ParseProgram()
    {
        while (true)
        {
            Token current = tokens.Peek();
            if (current.Type == TokenType.EndOfFile)
            {
                break;
            }

            ParseStatement();

            if (tokens.Peek().Type == TokenType.Semicolon)
            {
                tokens.Advance();
            }
            else
            {
                throw new UnexpectedLexemeException(TokenType.Semicolon, tokens.Peek());
            }
        }
    }

    private void ParseStatement()
    {
        switch (tokens.Peek().Type)
        {
            case TokenType.Var:
                Match(TokenType.Var);
                ParseVarDeclaration();
                break;
            case TokenType.Const:
                Match(TokenType.Const);
                ParseConstDeclaration();
                break;
            case TokenType.Print:
                Match(TokenType.Print);
                ParsePrintStatement();
                break;
            case TokenType.Identifier:
                ParseExpression();
                break;
            default:
                throw new Exception($"Неизвестная инструкция: {tokens.Peek().Type}");
        }
    }

    private void ParseVarDeclaration()
    {
        Token nameToken = Match(TokenType.Identifier);
        Match(TokenType.AssignThan);
        decimal value = ParseExpression();
        context.DefineVariable(nameToken.Value!.ToString(), value);
    }

    private void ParseConstDeclaration()
    {
        Token nameToken = Match(TokenType.Identifier);
        Match(TokenType.AssignThan);
        decimal value = ParseExpression();
        context.DefineConstant(nameToken.Value!.ToString(), value);
    }

    private void ParsePrintStatement()
    {
        Match(TokenType.OpenParenthesis);

        if (tokens.Peek().Type != TokenType.CloseParenthesis)
        {
            decimal value = ParseExpression();
            environment.AddResult(value);

            while (tokens.Peek().Type == TokenType.Comma)
            {
                tokens.Advance();
                value = ParseExpression();
                environment.AddResult(value);
            }
        }

        Match(TokenType.CloseParenthesis);
    }

    private decimal ParseExpression()
    {
        return ParseAssignment();
    }

    private decimal ParseAssignment()
    {
        if (tokens.Peek().Type == TokenType.Identifier && tokens.Peek(1).Type == TokenType.AssignThan)
        {
            Token nameToken = tokens.Peek();
            tokens.Advance();
            tokens.Advance();
            decimal right = ParseAssignment();
            context.AssignVariable(nameToken.Value!.ToString(), right);
            return right;
        }

        return ParseAdditive();
    }

    private decimal ParseAdditive()
    {
        decimal value = ParseTermExpression();
        while (true)
        {
            switch (tokens.Peek().Type)
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

    private decimal ParseTermExpression()
    {
        decimal value = ParseFactorExpression();
        while (true)
        {
            switch (tokens.Peek().Type)
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

    private decimal ParseFactorExpression()
    {
        switch (tokens.Peek().Type)
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

    private decimal ParseSimpleExpression()
    {
        Token token = tokens.Peek();
        switch (token.Type)
        {
            case TokenType.Number:
                tokens.Advance();
                return decimal.Parse(token.Value!.ToString(), CultureInfo.InvariantCulture);

            case TokenType.Identifier:
                string name = tokens.Peek().Value!.ToString();
                tokens.Advance();
                return context.GetValue(name);

            case TokenType.Pi:
                tokens.Advance();
                return 3.141592653589793m;

            case TokenType.Euler:
                tokens.Advance();
                return 2.718281828459045m;

            case TokenType.ReadInt:
                tokens.Advance();
                Match(TokenType.OpenParenthesis);
                Match(TokenType.CloseParenthesis);
                return environment.ReadNumber();

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

    private decimal ParseFunctionCall()
    {
        Token functionToken = tokens.Peek();
        tokens.Advance();

        string functionName = functionToken.Type.ToString().ToLowerInvariant();

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

        return BuiltinFunctions.Invoke(functionName, arguments);
    }

    private Token Match(TokenType expected)
    {
        Token actual = tokens.Peek();

        if (actual.Type != expected)
        {
            throw new UnexpectedLexemeException(expected, actual);
        }

        tokens.Advance();
        return actual;
    }
}