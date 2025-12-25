using System;
using System.Globalization;

namespace Execution;

public class ConsoleEnvironment : IEnvironment
{
    public decimal ReadNumber()
    {
        string? input = Console.ReadLine();
        return decimal.Parse(input ?? "0", CultureInfo.InvariantCulture);
    }

    public void AddResult(decimal result)
    {
        Console.WriteLine(result.ToString(CultureInfo.InvariantCulture));
    }
}