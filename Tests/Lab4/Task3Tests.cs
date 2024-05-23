using Labs.Lab4;

namespace Tests.Lab4;

public class Task3Tests
{
    [Theory]
    [InlineData(new[]
    {
        "ADD IVAN 1178927",
        "PRINT PETER",
        "ADD EGOR 123412",
        "PRINT IVAN",
        "EDITPHONE IVAN 112358",
        "PRINT IVAN",
        "PRINT EGOR",
        "DELETE EGOR",
        "EDITPHONE EGOR 123456",
    }, new []
    {
        "ERROR",
        "IVAN 1178927",
        "IVAN 112358",
        "EGOR 123412",
        "ERROR"
    })]
    public void CommonInputs(string[] commands, string[] expected)
    {
        var actual = Task3.Solve(commands);
        
        Assert.Equal(expected, actual);
    }
}