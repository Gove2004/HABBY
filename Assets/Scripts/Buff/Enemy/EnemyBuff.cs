public class Enemy_Recovery_Buff : BaseBuff
{
    public override string Name => "恢复强化";
    public override string Description => "每隔0.5秒回复等同于施法者攻击力的生命，持续2.5秒";

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
            // 每隔0.5秒回复 power 点生命 => 相当于每秒回复 power * 2 点生命
            float healPerSec = power * 2f;
            enemy.ApplyTimedStatModifier(0f, 0f, healPerSec, 0f, 2.5f);
        }
    }
}

public class Enemy_Haste_Buff : BaseBuff
{
    public override string Name => "加速强化";
    public override string Description => "获得50%速度加成，持续2.5秒";

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
            enemy.ApplyTimedStatModifier(0f, 0.50f, 0f, 0f, 2.5f);
        }
    }
}

public class Enemy_Armor_Buff : BaseBuff
{
    public override string Name => "固化强化";
    public override string Description => "获得等同于施法者攻击力的防御，持续1秒";

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
            enemy.ApplyTimedStatModifier(0f, 0f, 0f, power, 1f);
        }
    }
}