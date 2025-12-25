using Execution;

using Xunit;

namespace LanguageInterpreter.Specs;

public class InterpreterSpecs
{
    [Fact]
    public void SumNumbers_AddsTwoIntegers()
    {
        string code = @"
            var x = readInt();
            var y = readInt();
            var result = x + y;
            print(result);
        ";

        FakeEnvironment env = new FakeEnvironment();
        env.AddInput(12);
        env.AddInput(8);

        Interpreter interpreter = new Interpreter(env);
        interpreter.Execute(code);

        Assert.Equal(new decimal[] { 20m }, env.Results.ToArray());
    }

    [Fact]
    public void CircleSquare_ComputesArea()
    {
        string code = @"
            var radius = readInt();
            var area = PI * radius * radius;
            print(area);
        ";

        FakeEnvironment env = new FakeEnvironment();
        env.AddInput(3);

        Interpreter interpreter = new Interpreter(env);
        interpreter.Execute(code);

        decimal expected = 3.141592653589793m * 3 * 3;
        Assert.Equal(new decimal[] { expected }, env.Results.ToArray());
    }

    [Fact]
    public void FahrenheitToCelsius_ConvertsCorrectly()
    {
        string code = @"
            var fahrenheit = readInt();
            var celsius = (fahrenheit - 32) * 5 / 9;
            print(celsius);
        ";

        FakeEnvironment env = new FakeEnvironment();
        env.AddInput(68);

        Interpreter interpreter = new Interpreter(env);
        interpreter.Execute(code);

        Assert.Equal(new decimal[] { 20m }, env.Results.ToArray());
    }
}