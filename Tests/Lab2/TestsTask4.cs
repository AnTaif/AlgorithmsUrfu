using Labs.Lab2;

namespace Tests.Lab2;

public class TestsTask4
{
    [Fact]
    public void Test1()
    {
        var n = 5;
        var k = 7;
        var m = 13;
        var l = 100;

        var actualGeneratedArray = Task4.GenerateArray(n, k, m, l);
        var expectedGeneratedArray = new[] { 7, 91, 83, 79, 27 };
        
        Assert.Equal(expectedGeneratedArray, actualGeneratedArray);

        var actualSortedArray = Task4.CountingSort(actualGeneratedArray);
        var expectedSortedArray = new[] { 7, 27, 79, 83, 91 };
        
        Assert.Equal(expectedSortedArray, actualSortedArray);

        var actualSum = Task4.OddSum(actualSortedArray, l);
        var expectedSum = 77;
        
        Assert.Equal(expectedSum, actualSum);
    }
}