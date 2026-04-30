

public class RecoverComponent
{
    private MyAttribute recoverHPPerSecond;
    private float hpRecoverTimer;
    
    public HPComponent HP { get; private set; }

    public RecoverComponent(int baseHP)
    {
        HP = new HPComponent(baseHP);


        recoverHPPerSecond = new MyAttribute(0);
    }


    public void Update(float deltaTime)
    {
        // 恢复HP
        if (recoverHPPerSecond.Value > 0)
        {
            hpRecoverTimer += deltaTime;
            int hpToRecover = (int)(recoverHPPerSecond.Value * hpRecoverTimer);
            if (hpToRecover > 0)
            {
                HP.Heal(hpToRecover);
                hpRecoverTimer = 0f;
            }
        }
    }

    public void SetHPRecovery(float hpPerSecond)
    {
        recoverHPPerSecond.SetBase(hpPerSecond);
    }

    public void ApplyHPRecoveryBuff(AttributeBuffType buffType, float value)
    {
        switch (buffType)
        {
            case AttributeBuffType.Base:
                recoverHPPerSecond.SetBase(value);
                break;
            case AttributeBuffType.AddFlat:
                recoverHPPerSecond.AddFlat(value);
                break;
            case AttributeBuffType.AddPercent:
                recoverHPPerSecond.AddPercent(value);
                break;
        }
    }

}