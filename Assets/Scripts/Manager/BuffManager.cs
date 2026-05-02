

using System.Collections.Generic;
using GoveKits.Runtime.Core;
using UnityEngine;

public class BuffManager : CSharpSingleton<BuffManager>
{
    public List<BaseBuff> EnemyBuffs = new List<BaseBuff>()
    {
        new Enemy_AttackPower_Buff(),
        new Enemy_MoveSpeed_Buff(),
        new Enemy_Regen_Buff(),
        new Enemy_AttackPower_Buff(),
        new Enemy_MoveSpeed_Buff(),
        new Enemy_Regen_Buff(),
    };


    public List<BaseBuff> PlayerBuffs = new List<BaseBuff>()
    {
        
    };



    public void ResetPlayerAvailableBuffs(HeroType heroType)
    {
        switch (heroType)
        {
            case HeroType.Actor:
                ResetActorBuffs();
                break;
            // 暂时只有射手，后续可以添加更多英雄类型
            default:
                LogCore.Error(nameof(BuffManager), $"未知的英雄类型: {heroType}");
                break;
        }
    }


    private void ResetActorBuffs()
    {
        PlayerBuffs.Clear();

        for (int i = 0; i < 5; i++)
        {
            PlayerBuffs.Add(new Actor_HP_Buff());
            PlayerBuffs.Add(new Actor_ATK_Buff());
            PlayerBuffs.Add(new Actor_DEF_Buff());
            PlayerBuffs.Add(new Actor_Speed_Buff());
        }

        for (int i = 0; i < 3; i++)
        {
            PlayerBuffs.Add(new Actor_Crit_Buff());
            PlayerBuffs.Add(new Actor_CritDamage_Buff());
            PlayerBuffs.Add(new Actor_ShootSpeed_Buff());
            PlayerBuffs.Add(new Actor_BulletSize_Buff());
            PlayerBuffs.Add(new Actor_BulletSpeed_Buff());
        }

        for (int i = 0; i < 1; i++)
        {
            PlayerBuffs.Add(new Actor_BulletFire_Buff());
            PlayerBuffs.Add(new Actor_BulletThrough_Buff());
        }
    }






    public BaseBuff GetEnemyBuff()
    {
        if (EnemyBuffs.Count == 0)
        {
            LogCore.Warning(nameof(BuffManager), "敌人增益列表为空");
            return null;
        }

        int index = Random.Range(0, EnemyBuffs.Count);
        return EnemyBuffs[index];
    }


    public (BaseBuff, BaseBuff, BaseBuff) GetThreePlayerBuffs()
    {
        if (PlayerBuffs.Count < 3)
        {
            LogCore.Warning(nameof(BuffManager), "玩家增益列表不足3个");
            return (null, null, null);
        }

        List<BaseBuff> selectedBuffs = new List<BaseBuff>();
        List<int> indices = new List<int>();

        while (selectedBuffs.Count < 3)
        {
            int index = Random.Range(0, PlayerBuffs.Count);
            if (!indices.Contains(index))
            {
                indices.Add(index);
                selectedBuffs.Add(PlayerBuffs[index]);
            }
        }

        return (selectedBuffs[0], selectedBuffs[1], selectedBuffs[2]);
    }
}