using System;
using System.Collections.Generic;
using System.Linq;
using LanguageLexer;

namespace LanguageParser;

public static class BuiltinFunctions
{
    private static readonly Dictionary<string, Func<List<decimal>, decimal>> Functions = new()
    {
        { "abs", Abs },
        { "min", Min },
        { "max", Max },
        { "round", Round },
        { "ceil", Ceil },
        { "floor", Floor },
    };

    public static decimal Invoke(string name, List<decimal> arguments)
    {
        if (!Functions.TryGetValue(name, out Func<List<decimal>, decimal>? function))
        {
            throw new ArgumentException($"Unknown builtin function '{name}'");
        }

        return function(arguments);
    }

    private static decimal Abs(List<decimal> arguments)
    {
        if (arguments.Count != 1)
        {
            throw new ArgumentException("Function 'abs' requires exactly one argument");
        }

        return arguments[0] < 0 ? -arguments[0] : arguments[0];
    }

    private static decimal Min(List<decimal> arguments)
    {
        if (arguments.Count == 0)
        {
            throw new ArgumentException("Function 'min' requires at least one argument");
        }

        return arguments.Min();
    }

    private static decimal Max(List<decimal> arguments)
    {
        if (arguments.Count == 0)
        {
            throw new ArgumentException("Function 'max' requires at least one argument");
        }

        return arguments.Max();
    }

    private static decimal Round(List<decimal> arguments)
    {
        if (arguments.Count != 1)
        {
            throw new ArgumentException("Function 'round' requires exactly one argument");
        }

        return (decimal)Math.Round((double)arguments[0]);
    }

    private static decimal Ceil(List<decimal> arguments)
    {
        if (arguments.Count != 1)
        {
            throw new ArgumentException("Function 'ceil' requires exactly one argument");
        }

        return (decimal)Math.Ceiling((double)arguments[0]);
    }

    private static decimal Floor(List<decimal> arguments)
    {
        if (arguments.Count != 1)
        {
            throw new ArgumentException("Function 'floor' requires exactly one argument");
        }

        return (decimal)Math.Floor((double)arguments[0]);
    }
}