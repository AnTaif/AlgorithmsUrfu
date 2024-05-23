using Labs.Lab4;

namespace Tests.Lab4;

public class Task1Tests
{
    [Theory]
    [InlineData(new[]{ 1, 7, 15, 8, 9, 15, 15, 19, 5, 19 },
        new[]
        {
            "1 1 8", 
            "1 6 8", 
            "1 0 6", 
            "2 6 6", 
            "2 1 6", 
            "2 0 9", 
            "1 4 7", 
            "1 3 6"
        }, 
        new[]{ 93, 39, 70, 49, 38 })]
    public void CommonInputs(int[] V, string[] queries, int[] expected)
    {
        var actual = Task1.Solve(V, queries);
        
        Assert.Equal(expected, actual);
    }
}