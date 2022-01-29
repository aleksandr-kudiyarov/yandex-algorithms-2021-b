using System.IO;
using Xunit;

namespace BaseUnitTest;

public abstract class YandexTest
{
    private readonly string _path;

    protected YandexTest()
    {
        var type = GetType();
        _path = $"D:/Repos/Kudiyarov.YandexAlgorithms/{type.Namespace}/TestCases/";
    }

    protected void InnerTest(string input, string output)
    {
        // arrange
        var inputData = File.ReadAllLines(_path + input);
        var expected = File.ReadAllText(_path + output);
        
        // act
        var actual = GetActual(inputData);
        
        // assert
        Assert.Equal(expected, actual);
    }

    protected abstract string GetActual(string[] input);
}