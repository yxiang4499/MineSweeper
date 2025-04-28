using MineSweeper.Class;
using Xunit;

public class MineGeneratorTests
{
    [Fact]
    public void PlaceMines_ShouldPlaceCorrectNumberOfMines()
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(5);
        var mineGenerator = new MineGenerator();
        int mineCount = 5;

        // Act
        mineGenerator.PlaceMines(grid, mineCount);

        // Assert
        int actualMineCount = 0;
        for (int x = 0; x < grid.Size; x++)
        {
            for (int y = 0; y < grid.Size; y++)
            {
                if (grid.GetCell(x, y).IsMine)
                    actualMineCount++;
            }
        }
        Assert.Equal(mineCount, actualMineCount);
    }

    [Fact]
    public void PlaceMines_WithZeroMines_ShouldNotPlaceAnyMines()
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(5);
        var mineGenerator = new MineGenerator();
        int mineCount = 0;

        // Act
        mineGenerator.PlaceMines(grid, mineCount);

        // Assert
        int actualMineCount = 0;
        for (int x = 0; x < grid.Size; x++)
        {
            for (int y = 0; y < grid.Size; y++)
            {
                if (grid.GetCell(x, y).IsMine)
                    actualMineCount++;
            }
        }
        Assert.Equal(mineCount, actualMineCount);
    }

    [Fact]
    public void PlaceMines_WithMaximumMines_ShouldFillAllCells()
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(3); // 3x3 grid
        var mineGenerator = new MineGenerator();
        int mineCount = 9; // Maximum mines for a 3x3 grid

        // Act
        mineGenerator.PlaceMines(grid, mineCount);

        // Assert
        int actualMineCount = 0;
        for (int x = 0; x < grid.Size; x++)
        {
            for (int y = 0; y < grid.Size; y++)
            {
                if (grid.GetCell(x, y).IsMine)
                    actualMineCount++;
            }
        }
        Assert.Equal(mineCount, actualMineCount);
    }

    [Fact]
    public void PlaceMines_WithExceedingMines_ShouldThrowException()
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(3); // 3x3 grid
        var mineGenerator = new MineGenerator();
        int mineCount = 10; // Exceeds grid capacity

        // Act & Assert
        Assert.Throws<ArgumentException>(() => mineGenerator.PlaceMines(grid, mineCount));
    }

    [Fact]
    public void PlaceMines_ShouldPlaceMinesRandomly()
    {
        // Arrange
        var grid1 = new Grid();
        var grid2 = new Grid();
        grid1.Initialize(5);
        grid2.Initialize(5);
        var mineGenerator = new MineGenerator();
        int mineCount = 5;

        // Act
        mineGenerator.PlaceMines(grid1, mineCount);
        mineGenerator.PlaceMines(grid2, mineCount);

        // Assert
        bool areGridsIdentical = true;
        for (int x = 0; x < grid1.Size; x++)
        {
            for (int y = 0; y < grid1.Size; y++)
            {
                if (grid1.GetCell(x, y).IsMine != grid2.GetCell(x, y).IsMine)
                {
                    areGridsIdentical = false;
                    break;
                }
            }
        }
        Assert.False(areGridsIdentical); // Mines should not be placed identically
    }
}
