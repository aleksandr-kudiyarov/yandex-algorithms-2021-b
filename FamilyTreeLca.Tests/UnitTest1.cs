using BaseUnitTest;
using Xunit;

namespace FamilyTreeLca.Tests;

public class UnitTest : YandexTest
{
    [Theory]
    [InlineData("1-input", "1-output")]
    public void Test1(string input, string output)
    {
        InnerTest(input, output);
    }

    protected override string GetActual(string[] input)
    {
        return Program.GetResult(input);
    }
}