


public class Actor_HP_Buff : BaseBuff
{
    public override string Name => "生命果实";
    public override string Description => "+5点最大生命值";

    private int amount = 5;


    public override void Apply(Character character)
    {
        base.Apply(character);
        Actor player = character as Actor;
        if (player != null)
        {
            player.MaxHP += amount;
        }
    }
}


public class Actor_ATK_Buff : BaseBuff
{
    public override string Name => "力量果实";
    public override string Description => "+1点攻击力";

    private int amount = 1;

    public override void Apply(Character character)
    {
        base.Apply(character);
        Actor player = character as Actor;
        if (player != null)
        {
            player.AttackPower += amount;
        }
    }
}


public class Actor_DEF_Buff : BaseBuff
{
    public override string Name => "坚韧果实";
    public override string Description => "+1点防御力";

    private int amount = 1;

    public override void Apply(Character character)
    {
        base.Apply(character);
        Actor player = character as Actor;
        if (player != null)
        {
            player.DefensePower += amount;
        }
    }
}


public class Actor_Speed_Buff : BaseBuff
{
    public override string Name => "敏捷果实";
    public override string Description => "+10%点速度\n最多叠加10层";

    private float amount = 0.1f;
    private const int MaxStacks = 10;

    public override void Apply(Character character)
    {
        base.Apply(character);
        if (character.BuffCounters[Name] > MaxStacks)
        {
            return;
        }
        Actor player = character as Actor;
        if (player != null)
        {
            player.MoveSpeed *= (1 + amount);
        }
    }
}



public class Actor_Crit_Buff : BaseBuff
{
    public override string Name => "暴击果实";
    public override string Description => "+5%点暴击率\n最多叠加20层";

    private float amount = 0.05f;
    private const int MaxStacks = 20;

    public override void Apply(Character character)
    {
        base.Apply(character);
        if (character.BuffCounters[Name] > MaxStacks)
        {
            return;
        }
        Actor player = character as Actor;
        if (player != null)
        {
            player.CritRate += amount;
        }
    }
}



public class Actor_CritDamage_Buff : BaseBuff
{
    public override string Name => "暴伤果实";
    public override string Description => "+25%点暴击伤害";

    private float amount = 0.25f;

    public override void Apply(Character character)
    {
        base.Apply(character);

        Actor player = character as Actor;
        if (player != null)
        {
            player.CritDamageMultiplier += amount;
        }
    }
}


public class Actor_ShootSpeed_Buff : BaseBuff
{
    public override string Name => "攻速果实";
    public override string Description => "+10%点攻速\n最多叠加10层";

    private float amount = 0.1f;
    private const int MaxStacks = 10;

    public override void Apply(Character character)
    {
        base.Apply(character);
        if (character.BuffCounters[Name] > MaxStacks)
        {
            return;
        }
        Actor player = character as Actor;
        if (player != null)
        {
            player.AttackSpeed *= (1 + amount);
        }
    }
}


public class Actor_BulletSpeed_Buff : BaseBuff
{
    public override string Name => "弹速果实";
    public override string Description => "+10%点弹速\n最多叠加10层";

    private float amount = 0.1f;
    private const int MaxStacks = 10;

    public override void Apply(Character character)
    {
        base.Apply(character);
        if (character.BuffCounters[Name] > MaxStacks)
        {
            return;
        }
        Actor player = character as Actor;
        if (player != null)
        {
            player.BulletSpeed *= (1 + amount);
        }
    }
}


public class Actor_BulletThrough_Buff : BaseBuff
{
    public override string Name => "穿透果实";
    public override string Description => "+1点子弹穿透数\n最多叠加5层";

    private int amount = 1;
    private const int MaxStacks = 5;

    public override void Apply(Character character)
    {
        base.Apply(character);
        if (character.BuffCounters[Name] > MaxStacks)
        {
            return;
        }
        Actor player = character as Actor;
        if (player != null)
        {
            player.BulletThroughCount += amount;
        }
    }
}


public class Actor_BulletFire_Buff : BaseBuff
{
    public override string Name => "多重果实";
    public override string Description => "+1点子弹发射数\n最多叠加5层";

    private int amount = 1;
    private const int MaxStacks = 5;

    public override void Apply(Character character)
    {
        base.Apply(character);
        if (character.BuffCounters[Name] > MaxStacks)
        {
            return;
        }
        Actor player = character as Actor;
        if (player != null)
        {
            player.BulletFireCount += amount;
        }
    }
}



public class Actor_BulletSize_Buff : BaseBuff
{
    public override string Name => "弹幕果实";
    public override string Description => "+20%点子弹大小\n最多叠加5层";

    private float amount = 0.1f;
    private const int MaxStacks = 5;

    public override void Apply(Character character)
    {
        base.Apply(character);
        if (character.BuffCounters[Name] > MaxStacks)
        {
            return;
        }
        Actor player = character as Actor;
        if (player != null)
        {
            player.BulletSize *= (1 + amount);
        }
    }
}


public class Actor_BulletLife_Buff : BaseBuff
{
    public override string Name => "持续果实";
    public override string Description => "+0.5秒子弹持续时间\n最多叠加5层";

    private float amount = 0.5f;
    private const int MaxStacks = 5;

    public override void Apply(Character character)
    {
        base.Apply(character);
        if (character.BuffCounters[Name] > MaxStacks)
        {
            return;
        }
        Actor player = character as Actor;
        if (player != null)
        {
            player.BulletLifeTime += amount;
        }
    }
}




