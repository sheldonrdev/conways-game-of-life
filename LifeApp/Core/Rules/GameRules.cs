namespace LifeApp.Core.Rules;

public static class GameRules // Static as its stateless
{
    public static bool ApplyRules(bool isAlive, int liveNeighbours)
    {
        if (isAlive)
        {
            switch (liveNeighbours)
            {
                case < 2: return false; // Underpopulation
                case 2 or 3: return true; // Survival
                default: return false; // Overpopulation
            }
        }
        else
        {
            if (liveNeighbours == 3) return true; // Reproduction
            return false; // Dead remains dead
        }
    }
}
