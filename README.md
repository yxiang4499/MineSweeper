A Windows console application built with C# .NET Core 8.0 and xUnit test library in VS 2022.

**Solution includes 2 projects**

	MineSweeper
	MineSweeper.Tests


**Instructions**

	Unzip Minesweeper folder and inside the folder click on MineSweeper.sln

	Build the whole solution.

	Under Test tab in VS2022 click on test explorer and run all test and view the test results.

	After which you may run the minesweeper application.

**Assumption**

	Y axis has range from A to Z.
 
	X axis has range from 1 to 26.


**Design consideration**

	4 Classes : 
 
	Game -> Contains the main logic of the game, has a IGame interface implement.
 
	MineGenerator -> Handles only the generation of mines and putting mines in a grid, has a IMineGenerator interface implement.
 
	Grid -> Contains many cell and handles grid creation and other grid related logics, has a IGrid interface implement.
 
	Cell -> The cell objects in the grid, holds data for each grid cell.
