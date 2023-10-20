namespace SimpleSurvivors.Player
{
    public interface IExpRequirement
    {
        int GetTotalExpRequirement(int currentLevel);
        int GetTotalGainedExp(int currentLevel, int experience);
    }
}