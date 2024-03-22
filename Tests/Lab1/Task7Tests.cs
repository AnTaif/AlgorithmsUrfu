using Labs.Lab1;

namespace Tests.Lab1;

public class Task7Tests
{
    [Fact]
    public void ValidInput1()
    {
        var n = 3;
        var k = 1;

        var actual = Task7.CountWays(n, k);
        
        Assert.Equal(9, actual);
    }
    
    [Fact]
    public void ValidInput2()
    {
        var n = 4;
        var k = 2;

        var actual = Task7.CountWays(n, k);
        
        Assert.Equal(20, actual);
    }
    
    [Fact]
    public void ValidInput3()
    {
        var n = 5;
        var k = 3;

        var actual = Task7.CountWays(n, k);
        
        Assert.Equal(48, actual);
    }
}