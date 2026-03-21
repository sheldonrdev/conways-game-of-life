using LifeApp.Core.Engine;
using LifeApp.Core.Grid;

namespace LifeApp;

class Program
{
    static void Main(string[] args)
    {
        var gameEngine = new GameEngine();
        
        bool[,] grid =
        {
            { false, true,  false },
            { false, false, false  },
            { true,  true,  true  },
            { false, false, false }
        };

        Console.WriteLine("Generation 0:");
        GridDisplay.Output(grid);

        var gridNextGen = gameEngine.GetNextGeneration(grid);

        Console.WriteLine("Generation 1:");
        GridDisplay.Output(gridNextGen);
    }
}