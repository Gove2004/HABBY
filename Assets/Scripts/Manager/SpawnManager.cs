using GoveKits.Runtime.Core;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [Header("Character Prefabs")]
    public GameObject ActorPrefab;
    public GameObject WarriorPrefab;
    public GameObject WizardPrefab;

    [Header("Enemy Prefabs")]
    public GameObject EnemyPrefab;

    [Header("Bullet Prefabs")]
    public GameObject ActorBulletPrefab;

    protected override void Init()
    {
        base.Init();

        ActorPrefab = Resources.Load<GameObject>("Prefabs/Character/Actor");
        WarriorPrefab = Resources.Load<GameObject>("Prefabs/Character/Warrior");
        WizardPrefab = Resources.Load<GameObject>("Prefabs/Character/Wizard");

        EnemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy/Enemy");

        ActorBulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet/ActorBullet");
    }


    public GameObject GetHeroPrefab(HeroType heroType)
    {
        switch (heroType)
        {
            case HeroType.Actor:
                return ActorPrefab;
            case HeroType.Warrior:
                return WarriorPrefab;
            case HeroType.Wizard:
                return WizardPrefab;
            default:
                LogCore.Error(nameof(SpawnManager), $"Unknown HeroType: {heroType}");
                return null;
        }
    }


    public GameObject GetEnemyPrefab()
    {
        return EnemyPrefab;
    }

    public GameObject GetBulletPrefab()
    {
        return ActorBulletPrefab;
    }
}
