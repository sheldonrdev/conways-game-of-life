using LifeApp.Core.Rules;

namespace LifeApp.Tests;

public class GameRulesTests
{
    // NB. Demonstrated AAA pattern only for first Unit test and collapsed for others given simplicity of tests.
    
    #region Underpopulation
    [Fact]
    public void LiveCell_WithZeroLiveNeighbours_Dies()
    {
        // Arrange
        const bool isAlive = true;
        const int liveNeighbours = 0;
        // Act
        var result = GameRules.ApplyRules(isAlive, liveNeighbours);
        // Assert
        Assert.False(result);
    }

    [Fact]
    public void LiveCell_WithOneLiveNeighbour_Dies()
    {
        Assert.False(GameRules.ApplyRules(true, 1));
    }
    #endregion

    #region Survival
    [Fact]
    public void LiveCell_WithTwoLiveNeighbours_Survives()
    {
        Assert.True(GameRules.ApplyRules(true, 2));
    }

    [Fact]
    public void LiveCell_WithThreeLiveNeighbours_Survives()
    {
        Assert.True(GameRules.ApplyRules(true, 3));
    }
    #endregion

    #region Overpopulation
    [Fact]
    public void LiveCell_WithFourLiveNeighbours_Dies()
    {
        Assert.False(GameRules.ApplyRules(true, 4));
    }

    [Fact]
    public void LiveCell_WithEightLiveNeighbours_Dies()
    {
        Assert.False(GameRules.ApplyRules(true, 8));
    }
    #endregion

    #region Reproduction
    [Fact]
    public void DeadCell_WithThreeLiveNeighbours_BecomesAlive()
    {
        Assert.True(GameRules.ApplyRules(false, 3));
    }
    #endregion

    #region Dead remains dead
    [Fact]
    public void DeadCell_WithTwoLiveNeighbours_RemainsDead()
    {
        Assert.False(GameRules.ApplyRules(false, 2));
    }

    [Fact]
    public void DeadCell_WithFourLiveNeighbours_RemainsDead()
    {
        Assert.False(GameRules.ApplyRules(false, 4));
    }

    [Fact]
    public void DeadCell_WithZeroLiveNeighbours_RemainsDead()
    {
        Assert.False(GameRules.ApplyRules(false, 0));
    }
    #endregion
}
