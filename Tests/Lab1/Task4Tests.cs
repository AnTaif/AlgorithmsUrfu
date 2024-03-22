using Labs.Lab1;

namespace Tests.Lab1;

public class Task4Tests
{
    [Fact]
    public void Test1()
    {
        var arrA = new[] { 1, 5, 4 };
        var arrB = new[] { 0, 1, 2, 3, 4 };
        var mod = 10;

        var actual = Task4.SolveArray(arrA, arrB, mod);

        var expected = new[] { 4, 0, 8, 8, 0 };
        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal(expected, actual);
    }
}