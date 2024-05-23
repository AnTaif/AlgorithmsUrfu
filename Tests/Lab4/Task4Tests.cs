using Labs.Lab4;

namespace Tests.Lab4;

public class Task4Tests
{
    [Theory]
    [InlineData(new[]
    {
        "BCB",
        "ABA",
        "BCB",
        "BAA",
        "BBC",
        "CCB",
        "CBC",
        "CBC"
    }, 3)]
    public void FirstTest(string[] words, int expected)
    {
        var actual = Task4.Solve(words);
        
        Assert.Equal(expected, actual);
    }
}