using GoveKits.Runtime.Core;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [Header("Character Prefabs")]
    public GameObject ActorPrefab;
    public GameObject WarriorPrefab;
    public GameObject WizardPrefab;

    [Header("Enemy Prefabs")]
    public GameObject NormalEnemyPrefab;
    public GameObject ActorBossPrefab;
    public GameObject GodBossPrefab;
    public GameObject LargeBossPrefab;
    public GameObject BufferEnemyPrefab;
    public GameObject ShooterEnemyPrefab;

    [Header("Bullet Prefabs")]
    public GameObject ActorBulletPrefab;

    [Header("Other Prefabs")]
    public GameObject FloatTextPrefab;
    public GameObject ExpBallPrefab;

    protected override void Init()
    {
        base.Init();

        ActorPrefab = Resources.Load<GameObject>("Prefabs/Character/Actor");
        WarriorPrefab = Resources.Load<GameObject>("Prefabs/Character/Warrior");
        WizardPrefab = Resources.Load<GameObject>("Prefabs/Character/Wizard");

        NormalEnemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy/NormalEnemy");
        ActorBossPrefab = Resources.Load<GameObject>("Prefabs/Enemy/ActorBoss");
        GodBossPrefab = Resources.Load<GameObject>("Prefabs/Enemy/GodBoss");
        LargeBossPrefab = Resources.Load<GameObject>("Prefabs/Enemy/LargeBoss");
        BufferEnemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy/BufferEnemy");
        ShooterEnemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy/ShooterEnemy");

        ActorBulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet/ActorBullet");

        ExpBallPrefab = Resources.Load<GameObject>("Prefabs/Other/ExpBall");
        FloatTextPrefab = Resources.Load<GameObject>("Prefabs/Other/FloatText");
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


    public GameObject GetEnemyPrefab(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.NormalEnemy:
                return NormalEnemyPrefab;
            case EnemyType.ActorBoss:
                return ActorBossPrefab;
            case EnemyType.GodBoss:
                return GodBossPrefab;
            case EnemyType.LargeBoss:
                return LargeBossPrefab;
            case EnemyType.BufferEnemy:
                return BufferEnemyPrefab;
            case EnemyType.ShooterEnemy:
                return ShooterEnemyPrefab;
            default:
                LogCore.Error(nameof(SpawnManager), $"Unknown EnemyType: {enemyType}");
                return null;
        }
    }

    public GameObject GetBulletPrefab()
    {
        return ActorBulletPrefab;
    }

    public GameObject GetExpBallPrefab()
    {
        return ExpBallPrefab;
    }

    public GameObject GetFloatTextPrefab()
    {
        return FloatTextPrefab;
    }
}
