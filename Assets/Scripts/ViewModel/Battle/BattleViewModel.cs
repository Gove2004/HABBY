using GoveKits.Runtime.UI;
using UnityEngine;

public class BattleViewModel : ViewModel
{
    private float battleTime;
    public float BattleTime
    {
        get => battleTime;
        set => SetProperty(ref battleTime, value);
    }



    public void StartBattle(HeroType heroType)
    {
        // 初始化战斗数据

        // 根据选择的英雄类型创建角色
        var heroPrefab = SpawnManager.Instance.GetHeroPrefab(heroType);
        var heroInstance = GameObject.Instantiate(heroPrefab);
        heroInstance.transform.position = Vector3.zero; // 设置初始位置
        
    }



    public void UpdateBattle(float deltaTime)
    {
        BattleTime += deltaTime;
        
    }
}