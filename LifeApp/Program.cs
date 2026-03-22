using LifeApp.Core.Engine;
using LifeApp.Core.Grid;

namespace LifeApp;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter number of rows: ");
        var rows = int.Parse(Console.ReadLine()!);
        Console.Write("Enter number of columns: ");
        var cols = int.Parse(Console.ReadLine()!);
        Console.Write("Enter number of generations: ");
        var generations = int.Parse(Console.ReadLine()!);

        var gameEngine = new GameEngine();
        var grid = GridFactory.CreateRandomGrid(rows, cols);

        for (var gen = 0; gen < generations; gen++)
        {
            Console.Clear();
            Console.WriteLine($"Generation {gen}:");
            GridDisplay.Output(grid);
            Thread.Sleep(500);
            grid = gameEngine.GetNextGeneration(grid);
        }
    }
}