using LifeApp.Core.Grid;

namespace LifeApp.Tests.Core.Grid;

public class GridFactoryTests
{
    #region Grid dimensions
    [Fact]
    public void CreateRandomGrid_ReturnsCorrectRowCount()
    {
        var grid = GridFactory.CreateRandomGrid(5, 10, seed: 69);

        Assert.Equal(5, grid.GetLength(0));
    }

    [Fact]
    public void CreateRandomGrid_ReturnsCorrectColumnCount()
    {
        var grid = GridFactory.CreateRandomGrid(5, 10, seed: 69);

        Assert.Equal(10, grid.GetLength(1));
    }

    [Fact]
    public void CreateRandomGrid_NonSquareGrid_ReturnsCorrectDimensions()
    {
        var grid = GridFactory.CreateRandomGrid(3, 7, seed: 69);

        Assert.Equal(3, grid.GetLength(0));
        Assert.Equal(7, grid.GetLength(1));
    }
    #endregion

    #region Cell values
    [Fact]
    public void CreateRandomGrid_ContainsOnlyBooleanValues()
    {
        var grid = GridFactory.CreateRandomGrid(10, 10, seed: 69);

        for (var row = 0; row < grid.GetLength(0); row++)
        for (var col = 0; col < grid.GetLength(1); col++)
            Assert.IsType<bool>(grid[row, col]);
    }

    [Fact]
    public void CreateRandomGrid_ContainsMixOfAliveAndDeadCells()
    {
        var grid = GridFactory.CreateRandomGrid(10, 10, seed: 69);
        var hasAlive = false;
        var hasDead = false;

        for (var row = 0; row < grid.GetLength(0); row++)
        for (var col = 0; col < grid.GetLength(1); col++)
        {
            if (grid[row, col]) hasAlive = true;
            else hasDead = true;
        }

        Assert.True(hasAlive, "Grid should contain at least one alive cell");
        Assert.True(hasDead, "Grid should contain at least one dead cell");
    }
    #endregion

    #region Deterministic Seeding  
    [Fact]
    public void CreateRandomGrid_SameSeed_ProducesSameGrid()
    {
        var grid1 = GridFactory.CreateRandomGrid(5, 5, seed: 69);
        var grid2 = GridFactory.CreateRandomGrid(5, 5, seed: 69);

        for (var row = 0; row < 5; row++)
        for (var col = 0; col < 5; col++)
            Assert.Equal(grid1[row, col], grid2[row, col]);
    }

    [Fact]
    public void CreateRandomGrid_DifferentSeeds_ProducesDifferentGrid()
    {
        var grid1 = GridFactory.CreateRandomGrid(10, 10, seed: 69);
        var grid2 = GridFactory.CreateRandomGrid(10, 10, seed: 68);
        var hasDifference = false;

        for (var row = 0; row < 10; row++)
        for (var col = 0; col < 10; col++)
        {
            if (grid1[row, col] != grid2[row, col])
            {
                hasDifference = true;
                break;
            }
        }

        Assert.True(hasDifference, "Different seeds should produce different grids");
    }
    #endregion
}
