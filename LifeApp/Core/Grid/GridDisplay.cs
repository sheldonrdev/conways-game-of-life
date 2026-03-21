namespace LifeApp.Core.Grid;

// NB.
// Static as its stateless
public static class GridDisplay 
{
    public static void Output(bool[,] grid)
    {
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
                Console.Write(grid[row, col] ? " * " : "   "); // * represents alive (true)

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}
