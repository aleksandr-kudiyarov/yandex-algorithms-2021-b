using System.IO;
using Xunit;

namespace BaseUnitTest;

public abstract class YandexTest
{
    protected abstract string Path { get; }
    protected abstract IYandexProgram Program { get; }

    protected void InnerTest(string input, string output)
    {
        input = Path + input;
        output = Path + output;
        var lines = File.ReadAllLines(input);
        var expected = File.ReadAllText(output);
        var actual = Program.GetResult(lines);
        Assert.Equal(expected, actual);
    }
}