using LearningCSharp.CQRS.Application.Test;
using Xunit;

namespace LearningCSharp.CQRS.Test;
public class CalculateTest
{
    //public static int Add(int a, int b) => a + b;
    [Fact]
    public void Add_ShouldReturnSum()
    {
        // Arrange
        int a = 10;
        int b = 20;
        int returnSum = 30;

        // Act
        var result = Calculate.Add(a, b);

        // Assert
        Assert.Equal(returnSum, result);
    }
}
