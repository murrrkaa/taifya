using ExampleLib.UnitTests.Helpers;

using Xunit;

namespace ExampleLib.UnitTests;

public class Lab2Test1
{
    [Fact]
    public void CanFormatEasternArabic()
    {
        int input = 2025;
        string expected = "٢٠٢٥";
        string actual = Lab2.FormatEasternArabic(input);
        Assert.Equal(expected, actual);
        Console.WriteLine("Test CanFormatEasternArabic DONE  ------------------------------------------------");
    }

    [Fact]
    public void CanNegativeFormatEasternArabic()
    {
        int input = -12500;
        string expected = "-١٢٥٠٠";
        string actual = Lab2.FormatEasternArabic(input);
        Assert.Equal(expected, actual);
        Console.WriteLine("Test CanNegativeFormatEasternArabic DONE -------------------------------------");
    }

    [Fact]
    public void CanBorderFormatEasternArabic()
    {
        int input = int.MaxValue;
        string expected = "٢١٤٧٤٨٣٦٤٨";
        string actual = Lab2.FormatEasternArabic(input);
        Assert.Equal(expected, actual);
        Console.WriteLine("Test CanBorderFormatEasternArabic DONE -------------------------------------");
    }

    [Fact]
    public void CanBorderNegativeFormatEasternArabic()
    {
        int input = int.MinValue;
        string expected = "-٢١٤٧٤٨٣٦٤٨";
        string actual = Lab2.FormatEasternArabic(input);
        Assert.Equal(expected, actual);
        Console.WriteLine("Test CanBorderNegativeFormatEasternArabic DONE -------------------------------------");
    }
}
