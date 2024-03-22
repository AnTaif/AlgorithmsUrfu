using Labs.Lab1;

namespace Tests.Lab1;

public class Task2Tests
{
    [Fact]
    public void ArraysHaveCommonElements()
    {
        var arrA = new[] { 1, 1, 2, 2, 3 };
        var arrB = new[] { 0, 1, 3, 3, 4 };

        var actual = Task2.CountCommonElements(arrA, arrB);
        
        Assert.Equal(2, actual);
    }
    
    [Fact]
    public void ArraysHaveOneCommonElement()
    {
        // Arrange
        int[] arrA = { 1, 2, 3 };
        int[] arrB = { 3, 4, 5 };

        // Act
        var actual = Task2.CountCommonElements(arrA, arrB);

        // Assert
        Assert.Equal(1, actual);
    }
    
    [Fact]
    public void ArraysHaveNoCommonElements()
    {
        // Arrange
        int[] arrA = { 1, 2, 3 };
        int[] arrB = { 4, 5, 6 };

        // Act
        var actual = Task2.CountCommonElements(arrA, arrB);

        // Assert
        Assert.Equal(0, actual);
    }
}