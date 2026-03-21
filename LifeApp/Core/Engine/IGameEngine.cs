namespace LifeApp.Core.Engine;

public interface IGameEngine
{
    /// <summary>
    ///     Gets the number of neighbours within the Moore Neighbourhood of a cell.
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    int GetLiveNeighbours(bool[,] grid, int row, int col);
}
