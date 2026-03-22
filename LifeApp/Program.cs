using LifeApp.Core.Engine;
using LifeApp.Core.Grid;

namespace LifeApp;

class Program
{
    static void Main(string[] args)
    {
        var gameEngine = new GameEngine();

        do
        {
            // Input validation
            var rows = GetValidInput("Enter number of rows: ");
            var cols = GetValidInput("Enter number of columns: ");
            var generations = GetValidInput("Enter number of generations: ");

            var grid = GridFactory.CreateRandomGrid(rows, cols);

            for (var gen = 0; gen < generations; gen++)
            {
                Console.Clear();
                Console.WriteLine($"Generation {gen}:");
                GridDisplay.Output(grid);
                Thread.Sleep(500);
                grid = gameEngine.GetNextGeneration(grid);
            }

            Console.WriteLine("Continue game play? (y/n): ");
        } 
        while (Console.ReadLine()?.Trim().ToLower() == "y");
        
        Console.WriteLine("Thanks for playing :)");
        Thread.Sleep(500);
    }

    // Non-Numeric, Zero/Negative checks
    private static int GetValidInput(string prompt)
    {
        while (true) // Continue until valid input is received
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            
            if (int.TryParse(input, out var value) && value > 0) 
                return value;
            
            Console.WriteLine("Please enter a positive whole number.");
        }
    }
}