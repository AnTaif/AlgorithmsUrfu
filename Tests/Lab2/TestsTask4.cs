using Labs.Lab2;

namespace Tests.Lab2;

public class TestsTask4
{
    [Theory]
    [InlineData(5, 7, 13, 100, 77)]
    // [InlineData(4, 5, 2, 10, 0)]
    // [InlineData(6, 10, 3, 20, 10)]
    public void Test1(int n, int k, int m, int l, int expectedSum)
    {
        var actualSum = Task4.Solve(n, k, m, l);
        
        Assert.Equal(expectedSum, actualSum);
    }
}