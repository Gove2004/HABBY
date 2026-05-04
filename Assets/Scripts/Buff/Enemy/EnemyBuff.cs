public class Enemy_AttackPower_Buff : BaseBuff
{
    public override string Name => "狂暴强化";
    public override string Description => "+50%伤害，持续5秒";

    private const float Duration = 5f;

    public override void Apply(Character character)
    {
        Apply(character, 0);
    }

    public override void Apply(Character character, int power)
    {
        base.Apply(character);
        Enemy enemy = character as Enemy;
        if (enemy != null)
        {
            float bonus = 0.5f + (power * 0.1f); // 根据强度成长
            enemy.ApplyTimedStatModifier(bonus, 0f, 0f, Duration);
        }
    }
}


public class Enemy_MoveSpeed_Buff : BaseBuff
{
    public override string Name => "疾行强化";
    public override string Description => "+25%速度，持续5秒";

    private const float Duration = 5f;

    public override void Apply(Character character)
    {
        Apply(character, 0);
    }

    public override void Apply(Character character, int power)
    {
        base.Apply(character);
        Enemy enemy = character as Enemy;
        if (enemy != null)
        {
            float bonus = 0.25f + (power * 0.05f); // 根据强度成长
            enemy.ApplyTimedStatModifier(0f, bonus, 0f, Duration);
        }
    }
}


public class Enemy_Regen_Buff : BaseBuff
{
    public override string Name => "回复强化";
    public override string Description => "每秒回复1点生命，持续5秒";

    private const float Duration = 5f;

    public override void Apply(Character character)
    {
        Apply(character, 0);
    }

    public override void Apply(Character character, int power)
    {
        base.Apply(character);
        Enemy enemy = character as Enemy;
        if (enemy != null)
        {
            float regen = 1f + (power * 0.5f); // 根据强度成长
            enemy.ApplyTimedStatModifier(0f, 0f, regen, Duration);
        }
    }
}