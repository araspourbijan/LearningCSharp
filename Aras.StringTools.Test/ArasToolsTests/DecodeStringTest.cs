namespace Aras.StringTools.Tests.ArasToolsTests;
public class DecodeStringTest
{
    [Theory]
    [InlineData("a3bfd3")]
    [InlineData("aybf")]
    [InlineData("a2y6b8nf6b")]
    [InlineData("6a2y6b8f1b")]
    [InlineData("a3mb56f9n999d3")]
    public void DecodeString_shouldThrowNotSupportedException(string text)
    {
        Assert.Throws<NotSupportedException>(() => text.DecodeString());
    }

    [Theory]
    [InlineData("a3bfd3g")]
    [InlineData("a3b")]
    [InlineData("12345")]
    public void DecodeString_shouldThrowException(string text)
    {
        Assert.Throws<Exception>(() => text.DecodeString());
    }

    [Theory]
    [InlineData("a3b2c1d3", "aaabbcddd")]
    [InlineData("a1b2c2a3","abbccaaa")]
    [InlineData("a1", "a")]
    [InlineData("", "")]
    [InlineData("a5b4c3d2e1", "aaaaabbbbcccdde")]
    public void DecodeString_shouldCodeString(string text, string expected)
    {
        string actual = text.DecodeString();
        Assert.Equal(expected, actual);
    }
}
