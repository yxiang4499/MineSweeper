using MineSweeper.Class;
using System.Text;
using Xunit;

public class GridTests
{
    [Fact]
    public void Initialize_ShouldCreateGridWithCorrectSize()
    {
        // Arrange
        var grid = new Grid();
        int size = 5;

        // Act
        grid.Initialize(size);

        // Assert
        Assert.Equal(size, grid.Size);
        Assert.NotNull(grid.GetCell(0, 0));
        Assert.NotNull(grid.GetCell(size - 1, size - 1));
        Assert.IsType<Cell>(grid.GetCell(size - 1, size - 1));
    }

    [Fact]
    public void Display_ShouldOutputCorrectGridRepresentation()
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(3); // 3x3 grid
        grid.GetCell(0, 0).SetAdjacentMines("1");
        grid.GetCell(1, 1).SetAdjacentMines("2");
        grid.GetCell(2, 2).SetAdjacentMines("3");

        StringBuilder expectedOutput = new StringBuilder();

        expectedOutput.AppendLine("  1 2 3 ");
        expectedOutput.AppendLine("A 1 _ _ ");
        expectedOutput.AppendLine("B _ 2 _ ");
        expectedOutput.AppendLine("C _ _ 3 ");
        expectedOutput.AppendLine();

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        grid.Display();

        // Assert
        var actualOutput = stringWriter.ToString();
        Assert.Equal(expectedOutput.ToString(), actualOutput);
    }

    [Theory]
    [InlineData("A1", true, 0, 0)]
    [InlineData("B2", true, 1, 1)]
    [InlineData("Z26", false, -1, -1)]
    [InlineData("", false, -1, -1)]
    [InlineData("Invalid", false, -1, -1)]
    public void IsValidInput_ShouldValidateInputCorrectly(string input, bool expected, int expectedX, int expectedY)
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(5);

        // Act
        bool result = grid.IsValidInput(input, out int x, out int y);

        // Assert
        Assert.Equal(expected, result);
        if (result)
        {
            Assert.Equal(expectedX, x);
            Assert.Equal(expectedY, y);
        }
    }

    [Theory]
    [InlineData(0, 0, true)]  // Top-left corner
    [InlineData(4, 4, true)]  // Bottom-right corner
    [InlineData(2, 3, true)]  // Inside the grid
    [InlineData(-1, 0, false)] // Out of bounds (negative x)
    [InlineData(0, -1, false)] // Out of bounds (negative y)
    [InlineData(5, 0, false)]  // Out of bounds (x exceeds grid size)
    [InlineData(0, 5, false)]  // Out of bounds (y exceeds grid size)
    public void IsCellInGrid_ShouldReturnCorrectResult(int x, int y, bool expected)
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(5); // 5x5 grid

        // Act
        bool result = grid.IsCellInGrid(x, y);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetCell_ShouldThrowExceptionForOutOfBounds()
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(5);

        // Act & Assert
        Assert.Throws<IndexOutOfRangeException>(() => grid.GetCell(5, 5));
    }
}
