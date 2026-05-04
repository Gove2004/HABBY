using System;
using GoveKits.Runtime.UI;
using UnityEngine;

public abstract class Player : Character
{
    public event Action<int> OnExpChanged;

    public float CritRate;
    public float CritDamageMultiplier;
    public float ExpRateMultiplier = 1f;

    public int HealPerFiveSeconds = 0;
    private float healTimer = 0f;

    protected override void Update()
    {
        base.Update();

        // 每5秒回血
        healTimer += Time.deltaTime;
        if (healTimer >= 5f)
        {
            healTimer = 0f;
            if (HealPerFiveSeconds > 0)
            {
                Heal(HealPerFiveSeconds);
            }
        }
    }

    // Level
    public void AddExp(int amount)
    {
        exp += Mathf.RoundToInt(amount * ExpRateMultiplier);
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
