using Labs.Lab7;

namespace Tests.Lab9;

public class Task9Tests
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 10)]
    [InlineData(2, 45)]
    [InlineData(3, 165)]
    [InlineData(10, 43749)]
    [InlineData(40, 45433800)]
    [InlineData(80, 9)]
    [InlineData(81, 1)]
    public void Testing(int s, int expected)
    {
        var actual = Task9.Solve(s);
        
        Assert.Equal(expected, actual);
    }
}