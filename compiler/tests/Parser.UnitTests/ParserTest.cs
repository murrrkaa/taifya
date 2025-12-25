using Execution;
using LanguageInterpreter;
using Xunit;

namespace LanguageParser.Tests;

public class ExpressionEvaluationTests
{
    public static TheoryData<string, decimal> GetExpressionTestData()
    {
        return new TheoryData<string, decimal>
        {
            // Числовые литералы
            { "print(42);", 42m },
            { "print(-5);", -5m },
            { "print(3.14);", 3.14m },
            { "print(-2.5);", -2.5m },

            // Константы
            { "print(PI);", 3.141592653589793m },
            { "print(E);", 2.718281828459045m },

            // Унарные операторы
            { "print(+42);", 42m },
            { "print(-(-5));", 5m },

            // Сложение и вычитание
            { "print(1 + 2);", 3m },
            { "print(10 - 3);", 7m },
            { "print(1.128 + 7.5 - 8);", 0.628m },

            // Умножение и деление
            { "print(4 * 7.5 / 5);", 6m },
            { "print(18 * 2);", 36m },

            // Приоритет операторов
            { "print(4 + 10 * 2);", 24m },
            { "print(16 - 7 / 4);", 14.25m },

            // Скобки
            { "print((4 + (5 / 4)) * 2);", 10.5m },
            { "print(2 * (3 + 4));", 14m },

            // Встроенные функции
            { "print(abs(-5));", 5m },
            { "print(min(2, 7, 1));", 1m },
            { "print(max(10, 3.5));", 10m },
            { "print(round(3.6));", 4m },
            { "print(ceil(3.1));", 4m },
            { "print(floor(-2.3));", -3m },

            // Комбинированные выражения
            { "print(abs(-10) + min(1, 2) * 2);", 12m },
            { "print(max(PI, 3) * 2);", 6.283185307179586m },
        };
    }

    [Theory]
    [MemberData(nameof(GetExpressionTestData))]
    public void CanEvaluateExpressionAsProgram(string program, decimal expected)
    {
        FakeEnvironment env = new FakeEnvironment();

        Interpreter interpreter = new Interpreter(env);
        interpreter.Execute(program);

        Assert.Single(env.Results);
        Assert.Equal(expected, env.Results[0]);
    }
}