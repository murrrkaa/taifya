using System;
using System.Collections.Generic;
using System.Linq;

namespace Execution;

public class FakeEnvironment : IEnvironment
{
    private readonly Queue<decimal> inputs = new();
    private readonly List<decimal> results = new();

    public IReadOnlyList<decimal> Results => results.AsReadOnly();

    public void AddInput(decimal value) => inputs.Enqueue(value);

    public decimal ReadNumber()
    {
        if (inputs.Count == 0)
        {
            throw new InvalidOperationException("No more inputs available");
        }

        return inputs.Dequeue();
    }

    public void AddResult(decimal result)
    {
        results.Add(result);
    }
}