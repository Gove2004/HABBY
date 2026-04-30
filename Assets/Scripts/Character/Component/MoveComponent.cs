

public class MoveComponent
{
    private MyAttribute moveSpeed;

    public float MoveSpeed => moveSpeed.Value;

    public MoveComponent(float baseMoveSpeed)
    {
        moveSpeed = new MyAttribute(baseMoveSpeed);
    }

    public void ApplyMoveSpeedBuff(AttributeBuffType buffType, float value)
    {
        switch (buffType)
        {
            case AttributeBuffType.Base:
                moveSpeed.SetBase(value);
                break;
            case AttributeBuffType.AddFlat:
                moveSpeed.AddFlat(value);
                break;
            case AttributeBuffType.AddPercent:
                moveSpeed.AddPercent(value);
                break;
        }
    }
}