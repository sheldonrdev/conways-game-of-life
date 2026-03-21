namespace LifeApp.Core.Grid;

// NB.
// Static as its stateless
// Named factory due to creational nature, NOT GOF Factory pattern
// Seed for reproducible randomness (testing purposes)
public static class GridFactory  
{
    public static bool[,] CreateRandomGrid(int rows, int cols, int? seed = null) 
    {
        var random = seed.HasValue ? 
            new Random(seed.Value) : 
            new Random();
        
        var grid = new bool[rows, cols];

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                grid[row, col] = random.Next(2) == 1; // randomly generates either 0 or 1 then converts to False (Dead) or True (Alive) via the == 1
            }
        }
            
        return grid;
    }
}
