
namespace MineSweeper.Class
{
    // Each cell in the grid
    public class Cell
    {
        public bool IsMine { get; private set; }
        public bool IsRevealed { get; private set; }
        public string AdjacentMines { get; private set; }
        public Cell()
        {
            IsMine = false;
            AdjacentMines = "_";
        }
        public void Reveal()
        {
            IsRevealed = true;
        }
        public void SetMine()
        {
            IsMine = true;
        }
        public void SetAdjacentMines(string adjacentMines)
        {
            AdjacentMines = adjacentMines;
        }
    }
}
