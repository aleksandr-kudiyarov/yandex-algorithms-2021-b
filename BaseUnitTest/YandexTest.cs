using System.IO;
using Xunit;

namespace BaseUnitTest;

public abstract class YandexTest
{
    protected abstract string Path { get; }

    protected void InnerTest(string input, string output)
    {
        input = Path + input;
        output = Path + output;
        var lines = File.ReadAllLines(input);
        var expected = File.ReadAllText(output);
        var actual = GetActual(lines);
        Assert.Equal(expected, actual);
    }

    protected abstract string GetActual(string[] input);
}