using System;
using System.Collections.Generic;
using System.Linq;

namespace Execution;

public class Context
{
    private readonly Stack<Scope> scopes = new();
    private readonly Dictionary<string, decimal> constants = new();
    private readonly IEnvironment environment;

    public Context(IEnvironment environment)
    {
        this.environment = environment;
        PushScope(new Scope());
        DefineConstant("PI", 3.141592653589793m);
        DefineConstant("E", 2.718281828459045m);
    }

    public void PushScope(Scope scope)
    {
        scopes.Push(scope);
    }

    public void PopScope()
    {
        if (scopes.Count > 1)
        {
            scopes.Pop();
        }
    }

    public decimal GetValue(string name)
    {
        foreach (Scope scope in scopes.Reverse())
        {
            if (scope.TryGetVariable(name, out decimal value))
            {
                return value;
            }
        }

        if (constants.TryGetValue(name, out decimal constant))
        {
            return constant;
        }

        throw new InvalidOperationException($"Переменная '{name}' не объявлена.");
    }

    public void AssignVariable(string name, decimal value)
    {
        foreach (Scope scope in scopes.Reverse())
        {
            if (scope.TryAssignVariable(name, value))
            {
                return;
            }
        }

        throw new InvalidOperationException($"Переменная '{name}' не объявлена.");
    }

    public void DefineVariable(string name, decimal value)
    {
        if (!scopes.Peek().TryDefineVariable(name, value))
        {
            throw new InvalidOperationException($"'{name}' уже объявлена в этой области.");
        }
    }

    public void DefineConstant(string name, decimal value)
    {
        if (!constants.TryAdd(name, value))
        {
            throw new InvalidOperationException($"Константа '{name}' уже определена.");
        }
    }
}