using Labs.Lab4;

namespace Tests.Lab4;

public class Task2Tests
{
    [Theory]
    [InlineData(new[]
        {
            "6 5 1", 
            "7 9 3", 
            "2 3 2", 
            "7 2 9", 
            "9 6 2", 
            "6 6 6", 
            "9 4 1", 
            "8 4 4", 
            "8 3 2", 
            "1 2 6"
        },
        new[]
        {
            "9 7 2", 
            "1 6 5", 
            "3 7 7", 
            "4 4 6", 
            "3 9 7"
        }, 
        new[] { 1, 1, 0, 0, 1 })]
    public void ThreeInputLength(string[] sets, string[] trialSets, int[] expected)
    {
        var actual = Task2.Solve(sets, trialSets);
        
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData(new[]
        { 
            "8 4 0 3 6 9 2",
            "3 5 0 4 3 1 1",
            "7 1 0 3 1 2 4",
            "7 1 5 1 5 5 1",
            "3 4 0 0 3 4 0",
            "3 3 3 6 3 9 3",
            "3 4 1 3 1 8 1",
            "1 1 6 8 6 8 2",
            "5 6 8 1 3 9 3",
            "7 5 7 1 4 0 3"
        },
        new[]
        {
            "1 1 3 3 8 4 1",
            "2 1 1 6 6 8 8",
            "1 1 5 7 5 1 5",
            "3 4 1 3 1 1 8",
            "0 0 1 2 8 2 6"
        }, 
        new[] { 1, 1, 1, 1, 0 })]
    public void SevenInputLength(string[] sets, string[] trialSets, int[] expected)
    {
        var actual = Task2.Solve(sets, trialSets);
        
        Assert.Equal(expected, actual);
    }
}