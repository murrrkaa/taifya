using ExampleLib.UnitTests.Helpers;

using Xunit;

namespace ExampleLib.UnitTests;

public class LabTest1
{
    [Fact]
    public void CanAddLineNumbers()
    {

        const string unsorted = """
                                ������ ����� � ����� ������,
                                � ����� ������ � �������
                                ���! �� ������� �� ����
                                � �� �� ������� �����!
                                """;
        const string sorted = """
                              1. ������ ����� � ����� ������,
                              2. � ����� ������ � �������
                              3. ���! �� ������� �� ����
                              4. � �� �� ������� �����!
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
        using TempFile file = TempFile.Create("������ ����� � ����� ������,");
        Lab1.AddLineNumbers(file.Path);

        string actual = File.ReadAllText(file.Path);
        Assert.Equal("1. ������ ����� � ����� ������,", actual);
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
