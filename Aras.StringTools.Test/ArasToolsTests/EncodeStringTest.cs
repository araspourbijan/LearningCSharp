 namespace Aras.StringTools.Test.ArasToolsTests;
using Aras.StringTools;
using Xunit;


public class EncodeStringTest
{
    [Fact]
    public void EncodeString_shouldThrowIndexOutOfRangeException()
    {
        string text = string.Empty;
        Assert.Throws<ArgumentException>("text",() => text.EncodeString());
    }

    [Theory]
    [InlineData("aaabbcddd", "a3b2c1d3")]
    [InlineData("abbccaaa", "a1b2c2a3")]
    [InlineData("a", "a1")]
    public void EncodeString_shouldCodeString(string text, string expected)
    {
        string actual = text.EncodeString();
        Assert.Equal(expected, actual);
    }
}