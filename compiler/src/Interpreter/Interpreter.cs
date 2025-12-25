using System;
using Execution;
using LanguageParser;

namespace LanguageInterpreter;

public class Interpreter
{
    private readonly Context context;
    private readonly IEnvironment environment;

    public Interpreter(IEnvironment? environment = null)
    {
        this.environment = environment ?? new ConsoleEnvironment();
        this.context = new Context(this.environment);
    }

    public void Execute(string sourceCode)
    {
        if (string.IsNullOrEmpty(sourceCode))
        {
            throw new ArgumentException("Source code cannot be null or empty", nameof(sourceCode));
        }

        Parser parser = new Parser(context, environment, sourceCode);
        parser.ParseProgram();
    }
}