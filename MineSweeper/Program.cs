using MineSweeper.Class;

IGrid grid = new Grid(); 
IMineGenerator mineGenerator = new MineGenerator();

Console.WriteLine("Welcome to Minesweeper!");

while (true)
{
    Game newGame = new Game(grid, mineGenerator);
    newGame.Start();
}
