using System;


public class LevelComponent
{
    private int level;
    private int exp;
    private int nextLevelExpThreshold;

    public event Action<int> OnLevelUp;

    public int Level => level;
    public int Exp => exp;
    public int NextLevelExpThreshold => nextLevelExpThreshold;
    public float ExpProgress => nextLevelExpThreshold > 0 ? (float)exp / nextLevelExpThreshold : 0f;

    public LevelComponent(int initialLevel)
    {
        level = initialLevel;
        exp = 0;
        nextLevelExpThreshold = MyStatic.GetExpThresholdForLevel(level);
    }

    public void AddExp(int amount)
    {
        exp += amount;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        while (exp >= nextLevelExpThreshold)
        {
            exp -= nextLevelExpThreshold;
            level++;
            OnLevelUp(level);
            nextLevelExpThreshold = MyStatic.GetExpThresholdForLevel(level);
        }
    }
}