using System;
using UnityEngine;

public abstract class Player : Character
{
    // Level Component Fields
    private int level;
    private int exp;
    private int nextLevelExpThreshold;
    public event Action<int> OnLevelUp;
    public event Action<int> OnExpChanged;
    public int Level => level;
    public int Exp => exp;
    public int NextLevelExpThreshold => nextLevelExpThreshold;
    public float ExpProgress => nextLevelExpThreshold > 0 ? (float)exp / nextLevelExpThreshold : 0f;

    public float CritRate;
    public float CritDamageMultiplier;

    public override void Setup(int initialLevel)
    {
        base.Setup(initialLevel);

        level = initialLevel;
        exp = 0;
        nextLevelExpThreshold = MyStatic.GetExpThresholdForLevel(level);

        CritRate = 0.0f;
        CritDamageMultiplier = 2.0f;
    }

    protected override void Update()
    {
        base.Update();
    }

    // Level
    public void AddExp(int amount)
    {
        exp += amount;
        OnExpChanged?.Invoke(exp);
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        while (exp >= nextLevelExpThreshold)
        {
            exp -= nextLevelExpThreshold;
            OnExpChanged?.Invoke(exp);
            level++;
            OnLevelUp?.Invoke(level);
            nextLevelExpThreshold = MyStatic.GetExpThresholdForLevel(level);
        }
    }

    // Attack Stats
    public bool TryCriticalHit() => UnityEngine.Random.value < CritRate;
}
