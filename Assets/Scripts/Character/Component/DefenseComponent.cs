

public class DefenseComponent
{
    private MyAttribute defensePower;

    public int DefensePower => (int)defensePower.Value;

    public DefenseComponent(float baseDefensePower)
    {
        defensePower = new MyAttribute(baseDefensePower);
    }

    public void ApplyDefenseBuff(AttributeBuffType buffType, float value)
    {
        switch (buffType)
        {
            case AttributeBuffType.Base:
                defensePower.SetBase(value);
                break;
            case AttributeBuffType.AddFlat:
                defensePower.AddFlat(value);
                break;
            case AttributeBuffType.AddPercent:
                defensePower.AddPercent(value);
                break;
        }
    }
}