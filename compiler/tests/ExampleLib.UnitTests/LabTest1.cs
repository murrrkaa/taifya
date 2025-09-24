using ExampleLib.UnitTests.Helpers;

using Xunit;

namespace ExampleLib.UnitTests;

public class LabTest1
{
    [Fact]
    public void CanAddLineNumbers()
    {

        const string unsorted = """
                                Играют волны — ветер свищет,
                                И мачта гнется и скрыпит…
                                Увы! он счастия не ищет
                                И не от счастия бежит!
                                """;
        const string sorted = """
                              1. Играют волны — ветер свищет,
                              2. И мачта гнется и скрыпит…
                              3. Увы! он счастия не ищет
                              4. И не от счастия бежит!
                              """;

        using TempFile file = TempFile.Create(unsorted);
        Lab1.AddLineNumbers(file.Path);

        string actual = File.ReadAllText(file.Path);
        Assert.Equal(sorted.Replace("\r\n", "\n"), actual);
        Console.WriteLine("Test CanAddLineNumbers DONE =================================================");
    }

    [Fact]
    public void CanOneLineFile()
    {
        using TempFile file = TempFile.Create("Играют волны — ветер свищет,");
        Lab1.AddLineNumbers(file.Path);

        string actual = File.ReadAllText(file.Path);
        Assert.Equal("1. Играют волны — ветер свищет,", actual);
        Console.WriteLine("Test CanOneLineFile DONE ====================================================");
    }

    [Fact]
    public void CanEmptyFile()
    {
        using TempFile file = TempFile.Create("");
        Lab1.AddLineNumbers(file.Path);

        string actual = File.ReadAllText(file.Path);
        Assert.Equal("", actual);
        Console.WriteLine("Test CanEmptyFile DONE =======================================================");
    }
}
