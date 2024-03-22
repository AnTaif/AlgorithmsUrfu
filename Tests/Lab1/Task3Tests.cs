using System.Diagnostics;
using Labs.Lab1;

namespace Tests.Lab1;

public class Task3Tests
{
    [Theory]
    [InlineData("123", "+", "999", "1122")]
    [InlineData("999", "+", "123", "1122")]
    [InlineData("1000", "-", "1", "999")]
    [InlineData("1", "-", "1000", "-999")]
    [InlineData("1234567890123456789", "+", "9876543210987654321", "11111111101111111110")]
    [InlineData("9876543210987654321", "-", "1234567890123456789", "8641975320864197532")]
    [InlineData("99999999999999999999", "+", "1", "100000000000000000000")]
    public void GivenValidInputs(string num1, string op, string num2, string expectedResult)
    {
        // Arrange
        var i = new BigInt(num1);
        var j = new BigInt(num2);

        // Act
        var result = Task3.Solve(i, j, op[0]);

        // Assert
        Assert.Equal(expectedResult, result.Value);
    }
}