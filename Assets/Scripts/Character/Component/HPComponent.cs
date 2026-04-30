


using System;

public class HPComponent
{
    private MyAttribute maxHP;
    private int currentHP;

    public int MaxHP => (int)maxHP.Value;
    public int CurrentHP => currentHP;
    public float HPPercentage => MaxHP > 0 ? (float)currentHP / MaxHP : 0f;

    public event Action<int> OnHPChanged;
    public event Action OnDeath;

    public HPComponent(int baseMaxHP)
    {
        maxHP = new MyAttribute(baseMaxHP);
        currentHP = MaxHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;
        OnHPChanged?.Invoke(currentHP);
        if (currentHP == 0) OnDeath?.Invoke();
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > MaxHP) currentHP = MaxHP;
        OnHPChanged?.Invoke(currentHP);
    }

    
    public void ApplyBuff(AttributeBuffType buffType, float value)
    {
        switch (buffType)
        {
            case AttributeBuffType.Base:
                maxHP.SetBase(value);
                break;
            case AttributeBuffType.AddFlat:
                maxHP.AddFlat(value);
                break;
            case AttributeBuffType.AddPercent:
                maxHP.AddPercent(value);
                break;
        }
        if (currentHP > MaxHP) currentHP = MaxHP;
        OnHPChanged?.Invoke(currentHP);
    }
}