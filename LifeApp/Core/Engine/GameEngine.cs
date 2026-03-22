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

        // Compute boundaries upfront - every iteration is guaranteed valid. No checking needed inside.
        for (var mooreRow = Math.Max(0, row - 1); mooreRow <= Math.Min(rows - 1, row + 1); mooreRow++)
        {
            for (var mooreCol = Math.Max(0, col - 1); mooreCol <= Math.Min(cols - 1, col + 1); mooreCol++)
            {
                if (mooreRow == row && mooreCol == col) continue; // Skip the Cell (centre) itself

                if (grid[mooreRow, mooreCol]) // always safe, no edge handling req'd here. Opted for bool[,] to allow for simple conditional checks like this. 
                    count++;
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
