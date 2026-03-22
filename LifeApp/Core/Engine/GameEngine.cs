using LifeApp.Core.Rules;

namespace LifeApp.Core.Engine;

public class GameEngine : IGameEngine
{
    // Leverages Moore Neighbourhood
    // Kept public intentionally (Testability over Accessibility)
    public int GetLiveNeighbours(bool[,] grid, int row, int col)
    {
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);
        var count = 0;

        for (var mooreRow = -1; mooreRow <= 1; mooreRow++)
        {
            for (var mooreCol = -1; mooreCol <= 1; mooreCol++)
            {
                if (mooreRow == 0 && mooreCol == 0) continue; // Skip the Cell (centre) itself

                var r = row + mooreRow;
                var c = col + mooreCol;

                // Handle the edges
                if (r >= 0 && r < rows &&
                    c >= 0 && c < cols && 
                    grid[r, c]) // opted for bool[,] to allow for simple conditional checks like this
                {
                    count++;
                }   
            }
        }

        return count;
    }

    public bool[,] GetNextGeneration(bool[,] grid)
    {
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);
        var next = new bool[rows, cols];

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                var neighbours = GetLiveNeighbours(grid, row, col);
                var alive = grid[row, col];

                next[row, col] = GameRules.ApplyRules(alive, neighbours);
            }
        }

        return next;
    }
}
