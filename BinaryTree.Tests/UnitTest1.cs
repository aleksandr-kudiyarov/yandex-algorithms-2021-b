using BaseUnitTest;
using Xunit;

namespace BinaryTree.Tests;

public class UnitTest1 : YandexTest
{
    [Theory]
    [InlineData("1-input", "1-output")]
    public void Test(string input, string output)
    {
        InnerTest(input, output);
    }

    protected override string Path => @"D:\Repos\Kudiyarov.YandexAlgorithms\BinaryTree.Tests\TestCases\";
    protected override string GetActual(string[] input)
    {
        return Program.GetResult(input);
    }
}