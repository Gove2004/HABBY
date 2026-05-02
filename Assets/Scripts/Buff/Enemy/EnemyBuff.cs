public class Enemy_AttackPower_Buff : BaseBuff
{
    public override string Name => "狂暴强化";
    public override string Description => "+50%伤害，持续5秒";

    private const float AttackBonus = 0.5f;
    private const float Duration = 5f;

    public override void Apply(Character character)
    {
        Enemy enemy = character as Enemy;
        if (enemy != null)
        {
            enemy.ApplyTimedStatModifier(AttackBonus, 0f, 0f, Duration);
        }
    }
}


public class Enemy_MoveSpeed_Buff : BaseBuff
{
    public override string Name => "疾行强化";
    public override string Description => "+25%速度，持续5秒";

    private const float SpeedBonus = 0.25f;
    private const float Duration = 5f;

    public override void Apply(Character character)
    {
        Enemy enemy = character as Enemy;
        if (enemy != null)
        {
            enemy.ApplyTimedStatModifier(0f, SpeedBonus, 0f, Duration);
        }
    }
}


public class Enemy_Regen_Buff : BaseBuff
{
    public override string Name => "回复强化";
    public override string Description => "每秒回复1点生命，持续5秒";

    private const float RegenPerSecond = 1f;
    private const float Duration = 5f;

    public override void Apply(Character character)
    {
        Enemy enemy = character as Enemy;
        if (enemy != null)
        {
            enemy.ApplyTimedStatModifier(0f, 0f, RegenPerSecond, Duration);
        }
    }
}