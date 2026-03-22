using LifeApp.Core.Engine;
using LifeApp.Core.Grid;

namespace LifeApp;

class Program
{
    static void Main(string[] args)
    {
        var rows = 10;
        var cols = 10;
        var generations = 10;
        var gameEngine = new GameEngine();
        var grid = GridFactory.CreateRandomGrid(rows, cols);

        for (var gen = 0; gen < generations; gen++)
        {
            Console.WriteLine($"Generation {gen}:");
            GridDisplay.Output(grid);
            grid = gameEngine.GetNextGeneration(grid);
        }
    }
}