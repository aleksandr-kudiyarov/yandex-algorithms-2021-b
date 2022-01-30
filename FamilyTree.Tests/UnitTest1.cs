using BaseUnitTest;
using Xunit;

namespace FamilyTree.Tests;

public class UnitTest1 : YandexTest
{
    [Theory]
    [InlineData("1-input", "1-output")]
    [InlineData("2-input", "2-output")]
    public void Test1(string input, string output)
    {
        InnerTest(input, output);
    }

    protected override string GetActual(string[] input)
    {
        return Program.GetResult(input);
    }
}