
namespace MineSweeper.Class
{
    public interface IGame
    {
        /// <summary>
        /// Start the game
        /// </summary>
        void Start();

        /// <summary>
        /// Flag the cell as a mine
        /// </summary>
        /// <param name="x"> Coordinate starting at 0 </param>
        /// <param name="y"> Coordinate starting at 0 </param>
        void FlagMine(int x, int y);

        /// <summary>
        /// Reveal the cell and its adjacent cells recursively if there are no adjacent mines
        /// </summary>
        /// <param name="x"> Coordinate starting at 0 </param>
        /// <param name="y"> Coordinate starting at 0 </param>
        void RevealCell(int x, int y);

        /// <summary>
        /// Play the game
        /// </summary>
        /// <param name="input"> Input coordinate, e.g A1 </param>
        /// <param name="option"> 1 to flag mine, 2 to reveal cell</param>
        void Play(string input, int option);

        /// <summary>
        /// Set number og moves remaining
        /// </summary>
        /// <param name="movesRemaining">Number of moves remaining</param>
        void SetMovesRemaining(int movesRemaining);

        /// <summary>
        /// Game Over
        /// </summary>
        void GameOver();

        int MovesRemaining { get; }

        bool IsGameOver { get; }
    }

    public class Game : IGame
    {
        private readonly IGrid _grid;
        private readonly IMineGenerator _mineGenerator;
        public int MovesRemaining { get; private set; }
        public bool IsGameOver { get; private set; }

        public Game(IGrid grid, IMineGenerator mineGenerator)
        {
            _grid = grid;
            _mineGenerator = mineGenerator;
        }

        public void Start()
        {
            SetupGrid();
            SetupMines();

            Console.WriteLine();
            Console.WriteLine("Here is your minefield:");

            _grid.Display();

            while (MovesRemaining > 0 && !IsGameOver)
            {
                Console.Write("Type '1' to flag a mine or Type '2' to reveal a cell: ");
                if (int.TryParse(Console.ReadLine(), out int option) && (option == 1 || option == 2))
                {
                    Console.Write("Select a square to reveal (e.g. A1): ");
                    string input = Console.ReadLine().ToUpper();
                    Play(input, option);
                }
                else
                {
                    Console.WriteLine("Invalid input! Please try again.");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Press any key to play again...");
            Console.ReadLine();
        }

        /// <summary>
        /// Setup the grid
        /// </summary>
        private void SetupGrid()
        {
            int gridSize = 0;
            while (gridSize <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("Enter the size of the grid up to a max of 26 (e.g. 4 for a 4x4 grid): ");
                if (int.TryParse(Console.ReadLine(), out gridSize) && gridSize > 1 && gridSize <= 26)
                {
                    _grid.Initialize(gridSize);
                    return;
                }
                else
                    Console.WriteLine("Invalid input! Enter a number greater than 1 and less than or equal to 26");
            }
        }

        /// <summary>
        /// Setup mines
        /// </summary>
        private void SetupMines()
        {
            int totalMineAllowed = (int)Math.Floor(_grid.Size * _grid.Size * 0.35);
            while (MovesRemaining <= 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Enter the number of mines to place on the grid (Maximum {totalMineAllowed}): ");
                if (int.TryParse(Console.ReadLine(), out int mineCount) && mineCount > 0 && mineCount <= totalMineAllowed)
                {
                    SetMovesRemaining(_grid.Size * _grid.Size);
                    _mineGenerator.PlaceMines(_grid, mineCount);
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid input! Enter a number greater than 0 and less than or equal to the maximum allowed.");
                }
            }
        }

        public void Play(string input, int option)
        {
            if (_grid.IsValidInput(input, out int x, out int y))
            {
                Cell cell = _grid.GetCell(x, y);

                if (!cell.IsRevealed)
                {
                    if (option == 1) // Flag mine option
                    {
                        if (!cell.IsMine)
                        {
                            Console.WriteLine("Oh no, this cell is not a mine! Game over.");
                            GameOver();
                            return;
                        }
                        FlagMine(x, y);
                    }
                    else // Reveal cell option
                    {
                        if (cell.IsMine)
                        {
                            Console.WriteLine("Oh no, you detonated a mine! Game over.");
                            GameOver();
                            return;
                        }
                        RevealCell(x, y);
                    }

                    _grid.Display();

                    if (MovesRemaining == 0)
                    {
                        Console.WriteLine("Congratulations, you have won the game!");
                        GameOver();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("This square has already been revealed!");
                    Console.WriteLine();
                    _grid.Display();
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please try again.");
                Console.WriteLine();
            }
        }

        public void FlagMine(int x, int y)
        {
            Cell cell = _grid.GetCell(x, y);
            cell.SetAdjacentMines("X");
            cell.Reveal();
            SetMovesRemaining(MovesRemaining - 1);
        }

        public void RevealCell(int x, int y)
        {
            Cell cell = _grid.GetCell(x, y);
            if (cell.IsRevealed)
                return;

            int adjacentMines = CountAdjacentMines(x, y);
            cell.SetAdjacentMines(adjacentMines.ToString());
            cell.Reveal();
            SetMovesRemaining(MovesRemaining-1);

            if (adjacentMines == 0)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int currentXPosition = x + dx;
                        int currentYPosition = y + dy;

                        if (_grid.IsCellInGrid(currentXPosition, currentYPosition))
                            RevealCell(currentXPosition, currentYPosition);
                    }
                }
            }
        }

        /// <summary>
        /// Count the number of adjacent mines for the given cell
        /// </summary>
        private int CountAdjacentMines(int x, int y)
        {
            int count = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    int currentXPosition = x + dx;
                    int currentYPosition = y + dy;

                    if (_grid.IsCellInGrid(currentXPosition, currentYPosition))
                    {
                        if (_grid.GetCell(currentXPosition, currentYPosition).IsMine)
                            count++;
                    }
                }
            }
            return count;
        }

        public void SetMovesRemaining(int movesRemaining)
        {
            MovesRemaining = movesRemaining;
        }

        public void GameOver()
        {
            IsGameOver = true;
        }
    }
}