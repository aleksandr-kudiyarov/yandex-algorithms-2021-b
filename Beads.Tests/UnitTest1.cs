using BaseUnitTest;
using Xunit;

namespace Beads.Tests;

public class UnitTest1 : YandexTest
{
    [Theory]
    [InlineData("1-input", "1-output")]
    [InlineData("2-input", "2-output")]
    [InlineData("3-input", "3-output")]
    [InlineData("5-input", "5-output")]
    public void Test1(string input, string output)
    {
        InnerTest(input, output);
    }

    protected override string GetActual(string[] input)
    {
        return Program.GetResult(input);
    }
}