using MineSweeper.Class;
using Xunit;

public class GameTests
{
    [Fact]
    public void RevealCell_ShouldRevealCellAndDecreaseMovesRemaining()
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(2); // 2x2 grid
        var mineGenerator = new MineGenerator();
        mineGenerator.PlaceMines(grid, 1); // Place 1 Mine
        var game = new Game(grid, mineGenerator);
        game.SetMovesRemaining(3);

        // Act
        int yPosition = Convert.ToInt32(grid.GetCell(0, 0).IsMine); // If cell 0,0 contains mine then yPosition set to 1, true = 1, False = 0.
        var cell = grid.GetCell(0, yPosition); // Get Cell that does not contain Mine.
        game.RevealCell(0, yPosition);

        // Assert
        Assert.True(cell.IsRevealed);
        Assert.Equal(grid.Size * grid.Size - 2, game.MovesRemaining); // Total cells - mines - revealed cell
    }

    [Fact]
    public void RevealCell_ShouldNotDecreaseMovesRemainingOnAlreadyRevealedCell()
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(2);
        var mineGenerator = new MineGenerator();
        mineGenerator.PlaceMines(grid, 1);
        var game = new Game(grid, mineGenerator);
        game.SetMovesRemaining(3);

        // Act
        int yPosition = Convert.ToInt32(grid.GetCell(0, 0).IsMine); // If cell 0,0 contains mine then yPosition set to 1, true = 1, False = 0.
        var cell = grid.GetCell(0, yPosition); // Get Cell that does not contain Mine.
        game.RevealCell(0, yPosition);

        var movesRemainingBefore = game.MovesRemaining;
        game.RevealCell(0, yPosition); // Try to reveal the same cell again

        // Assert
        Assert.Equal(movesRemainingBefore, game.MovesRemaining); // Moves remaining should not decrease
    }

    [Fact]
    public void RevealCell_ShouldRevealAllConnectedCellsWhenNoAdjacentMines()
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(3); // 3x3 grid
        var mineGenerator = new MineGenerator();
        mineGenerator.PlaceMines(grid, 0); // No mines
        var game = new Game(grid, mineGenerator);

        // Act
        game.RevealCell(1, 1); // Reveal a cell in the middle

        // Assert
        for (int x = 0; x < grid.Size; x++)
        {
            for (int y = 0; y < grid.Size; y++)
            {
                Assert.True(grid.GetCell(x, y).IsRevealed);
            }
        }
    }

    [Fact]
    public void Play_ShouldTriggerGameOverOnMine()
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(3);
        var mineGenerator = new MineGenerator();
        mineGenerator.PlaceMines(grid, 9); // Place mines on all cells
        var game = new Game(grid, mineGenerator);

        // Act
        game.Play("A1");

        // Assert
        Assert.True(game.IsGameOver);
    }

    [Fact]
    public void Play_ShouldTriggerGameOverOnNoMovesRemaining()
    {
        // Arrange
        var grid = new Grid();
        grid.Initialize(3);
        var mineGenerator = new MineGenerator();
        mineGenerator.PlaceMines(grid, 0); // Place mines on all cells
        var game = new Game(grid, mineGenerator);
        game.SetMovesRemaining(9);

        // Act
        game.Play("A1");

        // Assert
        Assert.Equal(0, game.MovesRemaining);
        Assert.True(game.IsGameOver);

    }
}
