using BaseUnitTest;
using Xunit;

namespace BinaryTree.Tests;

public sealed class UnitTest1 : YandexTest
{
    protected override IYandexProgram Program { get; } = new Program();

    [Theory]
    [InlineData("1-input", "1-output")]
    [InlineData("2-input", "2-output")]
    public void Test(string input, string output)
    {
        InnerTest(input, output);
    }
}