using System;
using GoveKits.Runtime.UI;
using UnityEngine;

public abstract class Player : Character
{
    public event Action<int> OnExpChanged;

    public float CritRate;
    public float CritDamageMultiplier;

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
            TriggerLevelUp();
            nextLevelExpThreshold = MyStatic.GetExpThresholdForLevel(level);
        }
    }

    // Attack Stats
    public bool TryCriticalHit() => UnityEngine.Random.value < CritRate;
}
