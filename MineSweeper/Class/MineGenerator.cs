
namespace MineSweeper.Class
{
    public interface IMineGenerator
    {
        /// <summary>
        /// Generate a number of mines and place it randomly on the grid
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the mine count exceeds the grid size.</exception>"
        void PlaceMines(IGrid grid, int mineCount);
    }

    public class MineGenerator : IMineGenerator
    {
        public void PlaceMines(IGrid grid, int mineCount)
        {
            if (mineCount > grid.Size * grid.Size)
                throw new ArgumentException("Mine count exceeds grid size.");

            Random random = new Random();
            int placedMines = 0;

            while (placedMines < mineCount)
            {
                int x = random.Next(0, grid.Size);
                int y = random.Next(0, grid.Size);
                Cell cell = grid.GetCell(x, y);

                if (!cell.IsMine)
                {
                    cell.SetMine();
                    placedMines++;

                    //Display mines position
                    //Console.WriteLine("Mine placed at: {0}", (char)('A' + y) + (x+1).ToString());
                }
            }
        }
    }
}