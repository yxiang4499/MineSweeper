
using System;

namespace MineSweeper.Class
{
    public interface IGrid
    {
        /// <summary>
        /// Setup the grid with the given size
        /// </summary>
        void Initialize(int size);

        /// <summary>
        /// Display the grid
        /// </summary>
        void Display();

        /// <summary>
        /// Check if the input is valid
        /// </summary>
        /// <param name="input"> Consisting of A-Z and a number, e.g A1 </param>
        /// <param name="x"> Coordinate starting at 0 </param>
        /// <param name="y"> Coordinate starting at 0 </param>
        bool IsValidInput(string input, out int x, out int y);

        /// <summary>
        /// Check that the given cell is within the boundary of the grid
        /// </summary>
        /// <param name="x"> Coordinate starting at 0 </param>
        /// <param name="y"> Coordinate starting at 0 </param>
        bool IsCellInGrid(int x, int y);

        /// <summary>
        /// Get the cell at the given coordinates
        /// </summary>
        /// <param name="x"> Coordinate starting at 0 </param>
        /// <param name="y"> Coordinate starting at 0 </param>
        Cell GetCell(int x, int y);

        int Size { get; }
    }

    public class Grid : IGrid
    {
        private Cell[,] _cells;
        public int Size { get; private set; }

        public void Initialize(int size)
        {
            Size = size;
            _cells = new Cell[size, size];
            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                    _cells[x, y] = new Cell();
        }

        public void Display()
        {
            Console.Write("  ");
            
            for (int x = 0; x < Size; x++)
                Console.Write(x + 1 + " "); // Column headers 
            Console.WriteLine();

            for (int y = 0; y < Size; y++)
            {
                Console.Write((char)('A' + y) + " ");  // Row headers 
                for (int x = 0; x < Size; x++)
                    Console.Write(_cells[x, y].AdjacentMines + " "); // Cell content
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool IsValidInput(string input, out int x, out int y)
        {
            x = -1;
            y = -1;
            if (!string.IsNullOrWhiteSpace(input))
            {
                y = input[0] - 'A'; // Convert letter to index
                if (int.TryParse(input.Substring(1), out x) && IsCellInGrid(--x,y))
                    return true;
            }
            return false;
        }

        public bool IsCellInGrid(int x, int y)
        {
            return x >= 0 && x < Size && y >= 0 && y < Size;
        }

        public Cell GetCell(int x, int y)
        {
            return _cells[x, y];
        }
    }
}