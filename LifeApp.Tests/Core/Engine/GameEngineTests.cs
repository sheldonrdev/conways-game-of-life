using LifeApp.Core.Engine;

namespace LifeApp.Tests.Core.Engine;

public class GameEngineTests
{
    private readonly IGameEngine _gameEngine = new GameEngine();

    #region Centre cell (8 possible neighbours)
    [Fact]
    public void CentreCell_AllNeighboursAlive_ReturnsEight()
    {
        var grid = new bool[,]
        {
            { true, true, true },
            { true, true, true },
            { true, true, true }
        };

        Assert.Equal(8, _gameEngine.GetLiveNeighbours(grid, 1, 1));
    }

    [Fact]
    public void CentreCell_NoNeighboursAlive_ReturnsZero()
    {
        var grid = new bool[,]
        {
            { false, false, false },
            { false, true,  false },
            { false, false, false }
        };

        Assert.Equal(0, _gameEngine.GetLiveNeighbours(grid, 1, 1));
    }

    [Fact]
    public void CentreCell_ThreeNeighboursAlive_ReturnsThree()
    {
        var grid = new bool[,]
        {
            { true,  true,  false },
            { false, true,  false },
            { false, true,  false }
        };

        Assert.Equal(3, _gameEngine.GetLiveNeighbours(grid, 1, 1));
    }
    #endregion

    #region Corner cells (3 possible neighbours)
    [Fact]
    public void TopLeftCorner_AllNeighboursAlive_ReturnsThree()
    {
        var grid = new bool[,]
        {
            { true, true,  false },
            { true, true,  false },
            { false, false, false }
        };

        Assert.Equal(3, _gameEngine.GetLiveNeighbours(grid, 0, 0));
    }

    [Fact]
    public void BottomRightCorner_AllNeighboursAlive_ReturnsThree()
    {
        var grid = new bool[,]
        {
            { false, false, false },
            { false, true,  true },
            { false, true,  true }
        };

        Assert.Equal(3, _gameEngine.GetLiveNeighbours(grid, 2, 2));
    }
    #endregion

    #region Edge cells (5 possible neighbours)
    [Fact]
    public void TopEdge_AllNeighboursAlive_ReturnsFive()
    {
        var grid = new bool[,]
        {
            { true, true,  true },
            { true, true,  true },
            { false, false, false }
        };

        Assert.Equal(5, _gameEngine.GetLiveNeighbours(grid, 0, 1));
    }

    [Fact]
    public void LeftEdge_AllNeighboursAlive_ReturnsFive()
    {
        var grid = new bool[,]
        {
            { true, true,  false },
            { true, true,  false },
            { true, true,  false }
        };
    
        Assert.Equal(5, _gameEngine.GetLiveNeighbours(grid, 1, 0));
    }
    #endregion

    #region Target cell state (Dead) does not affect count
    [Fact]
    public void DeadCentreCell_CountsNeighboursCorrectly()
    {
        var grid = new bool[,]
        {
            { true,  true,  true },
            { true,  false, true },
            { true,  true,  true }
        };

        Assert.Equal(8, _gameEngine.GetLiveNeighbours(grid, 1, 1));
    }
    #endregion
}
