using System.Collections.Generic;

namespace Execution;

public class Scope
{
    private readonly Dictionary<string, decimal> variables = new();

    public bool TryGetVariable(string name, out decimal value)
    {
        if (variables.TryGetValue(name, out decimal v))
        {
            value = v;
            return true;
        }

        value = 0.0m;
        return false;
    }

    public bool TryAssignVariable(string name, decimal value)
    {
        if (variables.ContainsKey(name))
        {
            variables[name] = value;
            return true;
        }

        return false;
    }

    public bool TryDefineVariable(string name, decimal value)
    {
        return variables.TryAdd(name, value);
    }
}