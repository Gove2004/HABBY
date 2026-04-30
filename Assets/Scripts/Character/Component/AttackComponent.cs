

public class AttackComponent
{
    private MyAttribute attackPower;
    private MyAttribute attackSpeed;
    private MyAttribute critRate;
    private MyAttribute critDamageMultiplier;

    public int AttackPower => (int)attackPower.Value;
    public float AttackSpeed => attackSpeed.Value;
    public float CritRate => critRate.Value;
    public float CritDamageMultiplier => critDamageMultiplier.Value;

    public AttackComponent(int baseAttackPower, float baseAttackSpeed, float baseCritRate, float baseCritDamageMultiplier)
    {
        attackPower = new MyAttribute(baseAttackPower);
        attackSpeed = new MyAttribute(baseAttackSpeed);
        critRate = new MyAttribute(baseCritRate);
        critDamageMultiplier = new MyAttribute(baseCritDamageMultiplier);
    }

    public bool TryCriticalHit()
    {
        return UnityEngine.Random.value < CritRate;
    }

    public void ApplyAttackBuff(AttributeBuffType buffType, float value)
    {
        switch (buffType)
        {
            case AttributeBuffType.Base:
                attackPower.SetBase(value);
                break;
            case AttributeBuffType.AddFlat:
                attackPower.AddFlat(value);
                break;
            case AttributeBuffType.AddPercent:
                attackPower.AddPercent(value);
                break;
        }
    }


    public void ApplyAttackSpeedBuff(AttributeBuffType buffType, float value)
    {
        switch (buffType)
        {
            case AttributeBuffType.Base:
                attackSpeed.SetBase(value);
                break;
            case AttributeBuffType.AddFlat:
                attackSpeed.AddFlat(value);
                break;
            case AttributeBuffType.AddPercent:
                attackSpeed.AddPercent(value);
                break;
        }
    }


    public void ApplyCritRateBuff(AttributeBuffType buffType, float value)
    {
        switch (buffType)
        {
            case AttributeBuffType.Base:
                critRate.SetBase(value);
                break;
            case AttributeBuffType.AddFlat:
                critRate.AddFlat(value);
                break;
            case AttributeBuffType.AddPercent:
                critRate.AddPercent(value);
                break;
        }
    }


    public void ApplyCritDamageBuff(AttributeBuffType buffType, float value)
    {
        switch (buffType)
        {
            case AttributeBuffType.Base:
                critDamageMultiplier.SetBase(value);
                break;
            case AttributeBuffType.AddFlat:
                critDamageMultiplier.AddFlat(value);
                break;
            case AttributeBuffType.AddPercent:
                critDamageMultiplier.AddPercent(value);
                break;
        }
    }
}