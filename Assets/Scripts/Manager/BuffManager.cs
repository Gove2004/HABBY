

using System.Collections.Generic;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class BuffManager : CSharpSingleton<BuffManager>
{
    public List<BaseBuff> EnemyBuffs = new List<BaseBuff>()
    {
        new Enemy_Recovery_Buff(),
        new Enemy_Haste_Buff(),
        new Enemy_Armor_Buff()
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

        for (int i = 0; i < 3; i++)
        {
            PlayerBuffs.Add(new Actor_HP_Buff());
            PlayerBuffs.Add(new Actor_ATK_Buff());
            PlayerBuffs.Add(new Actor_DEF_Buff());
            PlayerBuffs.Add(new Actor_Refresh_Buff());
            
        }

        for (int i = 0; i < 2; i++)
        {
            PlayerBuffs.Add(new Actor_Speed_Buff());
            PlayerBuffs.Add(new Actor_Crit_Buff());
            PlayerBuffs.Add(new Actor_CritDamage_Buff());
            PlayerBuffs.Add(new Actor_ShootSpeed_Buff());
            PlayerBuffs.Add(new Actor_BulletSize_Buff());
            PlayerBuffs.Add(new Actor_BulletSpeed_Buff());
            PlayerBuffs.Add(new Actor_BulletLife_Buff());
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
        var player = VMContainer.Get<BattleViewModel>()?.playerCharacter;
        List<BaseBuff> validBuffs = new List<BaseBuff>();

        // 筛选出还没有达到最大层数上限的 Buff
        foreach (var buff in PlayerBuffs)
        {
            int currentStacks = 0;
            if (player != null && player.BuffCounters.TryGetValue(buff.Name, out currentStacks))
            {
                if (currentStacks >= buff.MaxStack) continue;
            }
            validBuffs.Add(buff);
        }

        if (validBuffs.Count < 3)
        {
            LogCore.Warning(nameof(BuffManager), "玩家可用增益列表不足3个");
            // 退底处理：如果少于3个，补齐不够的空位为null
            List<BaseBuff> fallback = new List<BaseBuff>(validBuffs);
            while (fallback.Count < 3) fallback.Add(null);
            return (fallback[0], fallback[1], fallback[2]);
        }

        List<BaseBuff> selectedBuffs = new List<BaseBuff>();
        List<int> indices = new List<int>();

        while (selectedBuffs.Count < 3)
        {
            int index = Random.Range(0, validBuffs.Count);
            if (!indices.Contains(index))
            {
                indices.Add(index);
                selectedBuffs.Add(validBuffs[index]);
            }
        }

        return (selectedBuffs[0], selectedBuffs[1], selectedBuffs[2]);
    }
}