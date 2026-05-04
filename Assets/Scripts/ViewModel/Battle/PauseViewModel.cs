


using GoveKits.Runtime.UI;
using UnityEngine;

public class PauseViewModel : ViewModel
{
    /// <summary>
    /// 获取所有玩家战斗属性，最大生命，攻击力，防御力，暴击率等
    /// </summary>
    /// <returns></returns>
    public string GetPlayerInfo()
    {
        var battleVM = VMContainer.Get<BattleViewModel>();
        if (battleVM == null || battleVM.playerCharacter == null) 
            return "未找到玩家信息";

        var player = battleVM.playerCharacter;
        string info = $"【角色状态】\n";
        info += $"等级: {player.Level}\n";
        info += $"生命值: {player.CurrentHP} / {player.MaxHP}\n";
        info += $"攻击力: {player.AttackPower}\n";
        info += $"防御力: {player.DefensePower}\n";
        info += $"移动速度: {player.MoveSpeed:F1}\n";
        info += $"暴击率: {(player.CritRate * 100):F1}%\n";
        info += $"暴击伤害: {(player.CritDamageMultiplier * 100):F1}%\n";

        // 如果是射手，附加专属属性
        if (player is Actor actor)
        {
            info += $"\n【射手属性】\n";
            info += $"攻击速度: {actor.AttackSpeed:F2}\n";
            info += $"弹幕大小: {actor.BulletSize:F2}\n";
            info += $"子弹飞行速度: {actor.BulletSpeed:F1}\n";
            info += $"子弹存活时间: {actor.BulletLifeTime:F1}秒\n";
            info += $"单次发射数: {actor.BulletFireCount}\n";
            info += $"子弹穿透数: {actor.BulletThroughCount}\n";
        }

        return info;
    }
    
    /// <summary>
    /// 获取所有玩家已经获得的增益效果，果实。
    /// </summary>
    /// <returns></returns>
    public string GetBuffInfo()
    {
        var battleVM = VMContainer.Get<BattleViewModel>();
        if (battleVM == null || battleVM.playerCharacter == null) 
            return "未找到增益信息";

        var player = battleVM.playerCharacter;
        if (player.BuffCounters == null || player.BuffCounters.Count == 0)
        {
            return "暂未获得任何果实增益";
        }

        string info = "【已获果实】\n";
        foreach (var kvp in player.BuffCounters)
        {
            info += $"- {kvp.Key}  [ x{kvp.Value} ]\n";
        }
        return info.TrimEnd('\n');
    }

    /// <summary>
    /// 获取当前关卡和战斗信息，难度倍率，击杀数等
    /// </summary>
    /// <returns></returns>
    public string GetLevelInfo()
    {
        var battleVM = VMContainer.Get<BattleViewModel>();
        if (battleVM == null) 
            return "未找到关卡信息";

        int minutes = Mathf.FloorToInt(battleVM.BattleTime / 60);
        int seconds = Mathf.FloorToInt(battleVM.BattleTime % 60);
        
        string info = $"【战斗统计】\n";
        info += $"存活时间: {minutes:00}:{seconds:00}\n";
        info += $"击杀敌人: {battleVM.KillCount}\n";
        info += $"当前得分: {battleVM.Score}\n";
        info += $"得分倍率: {battleVM.ScoreMultiplier:F1}x\n";
        
        return info;
    }
}