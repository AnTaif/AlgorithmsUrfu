using Labs.Lab1;

namespace Tests.Lab1;

public class Task6Tests
{
    [Fact]
    public void ValidInput1()
    {
        var arr = new[] { 8, 9, 6, 9, 8 };

        var actual = Task6.FindMinDifference(arr);
        
        Assert.Equal(4, actual);
    }

    [Fact]
    public void ValidInput2()
    {
        var arr = new[] { 14, 2, 12, 9, 9, 8 };

        var actual = Task6.FindMinDifference(arr);
        
        Assert.Equal(2, actual);
    }
    
    [Fact]
    public void WhenZeroDifference()
    {
        int[] arr = { 3, 1, 2, 4 };

        var result = Task6.FindMinDifference(arr);

        Assert.Equal(0, result);
    }
}