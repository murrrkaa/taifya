using System;
using LanguageParser;
using Xunit;

namespace LanguageParser.Tests
{
    public class ParserTest
    {
        public static TheoryData<string, decimal> GetExpressionTestData()
        {
            return new TheoryData<string, decimal>
            {
                // Числовые литералы
                { "42", 42m },
                { "-5", -5m },
                { "3.14", 3.14m },
                { "-2.5", -2.5m },

                // Константы
                { "Pi", 3.141592653589793m },
                { "Euler", 2.718281828459045m },

                // Унарные операторы
                { "+42", 42m },
                { "-(-5)", 5m },

                // Сложение и вычитание
                { "1 + 2", 3m },
                { "10 - 3", 7m },
                { "1.128 + 7.5 - 8", 0.628m },

                // Умножение и деление
                { "4 * 7.5 / 5", 6m },
                { "18 * 2", 36m },

                // Приоритет операторов
                { "4 + 10 * 2", 24m },
                { "16 - 7 / 4", 14.25m },

                // Скобки
                { "(4 + (5 / 4)) * 2", 10.5m },
                { "2 * (3 + 4)", 14m },

                // Встроенные функции
                { "abs(-5)", 5m },
                { "min(2, 7, 1)", 1m },
                { "max(10, 3.5)", 10m },
                { "round(3.6)", 4m },
                { "ceil(3.1)", 4m },
                { "floor(-2.3)", -3m },

                // Комбинированные выражения
                { "abs(-10) + min(1, 2) * 2", 12m },
                { "max(Pi, 3) * 2", 6.283185307179586m },
            };
        }

        [Theory]
        [MemberData(nameof(GetExpressionTestData))]
        public void Can_evaluate_expressions(string expression, decimal expected)
        {
            decimal actual = Parser.EvaluateExpression(expression);
            Assert.Equal(expected, actual);
        }
    }
}