using BaseUnitTest;
using Xunit;

namespace BinaryTree.Tests;

public class UnitTest1 : YandexTest
{
    protected override string Path => @"D:\Repos\Kudiyarov.YandexAlgorithms\BinaryTree.Tests\TestCases\";
    protected override IYandexProgram Program { get; } = new Program();

    [Theory]
    [InlineData("1-input", "1-output")]
    [InlineData("2-input", "2-output")]
    public void Test(string input, string output)
    {
        InnerTest(input, output);
    }
}