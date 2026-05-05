using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using Unity.VisualScripting;
using UnityEngine;

public class BattleViewModel : ViewModel
{
    #region Lifecycle

    private bool isBattleActive = false;
    public bool IsBattleActive
    {
        get => isBattleActive;
        set => SetProperty(ref isBattleActive, value);
    }
    private float battleTime;
    public float BattleTime
    {
        get => battleTime;
        set => SetProperty(ref battleTime, value);
    }

    public void StartBattle(HeroType heroType, float scoreMultiplier, float difficultyMultiplier, bool isMoreHP, bool isMoreDamage)
    {
        BattleTime = 0f;
        Score = 0;
        IsBattleActive = true;
        // 初始化战斗数据

        // 根据选择的英雄类型创建角色
        var heroPrefab = SpawnManager.Instance.GetHeroPrefab(heroType);
        var heroInstance = GameObject.Instantiate(heroPrefab);
        heroInstance.transform.position = Vector3.zero; // 设置初始位置
        playerCharacter = heroInstance.GetComponent<Player>();
        playerCharacter.OnDeath += () => EndBattle();
        BuffManager.Instance.ResetPlayerAvailableBuffs(heroType);

        // 系数
        this.ScoreMultiplier = scoreMultiplier;
        this.DifficultyMultiplier = difficultyMultiplier;
        // 加成
        this.isMoreHP = isMoreHP;
        this.isMoreDamage = isMoreDamage;

        // 刷新次数
        RefeshCount = (int)VMContainer.Get<GameViewModel>().GetNowAttribute(HeroType.Actor, PlayerAttributeType.RefreshTime);
    }


    public void UpdateBattle(float deltaTime)
    {
        BattleTime += deltaTime;
        
        UpdateEnemyGeneration(deltaTime);
    }

    public void EndBattle()
    {
        // 处理战斗结束逻辑
        IsBattleActive = false;
        // 上报分数
        VMContainer.Get<GameViewModel>().UpdateMaxScore(Score);
    }

    public void ClearBattle()
    {
        // 清理战斗数据
        BattleTime = 0f;
        Score = 0;
        KillCount = 0;
        RefeshCount = 0;

        // 重置敌人生成状态
        nextWaveTime = 5f;
        currentWave = 0;
        setupLevel = 0;

        // 销毁玩家角色
        if (playerCharacter != null)
        {
            GameObject.Destroy(playerCharacter.gameObject);
            playerCharacter = null;
        }

        // 销毁所有敌人角色
        foreach (var enemy in enemyCharacters)
        {
            if (enemy != null)
            {
                GameObject.Destroy(enemy.gameObject);
            }
        }
        enemyCharacters.Clear();
    }

    #endregion


    #region Enemy Management

    public Player playerCharacter { get; private set; }
    private List<Character> enemyCharacters = new List<Character>();
    public int KillCount = 0;

    #endregion

    #region Generate Manage

    private bool isMoreHP = false;
    private bool isMoreDamage = false;
    public float ScoreMultiplier = 1f; // 分数系数
    public float DifficultyMultiplier = 1f;  // 难度系数
    private float setupLevel = 0;  // 当前敌人等级

    private float nextWaveTime = 5f;  // 下一波敌人出现时间, 默认值为第一波
    private float waveInterval = 20f;   // 每波敌人间隔时间
    private float foreverWaveInterval = 25f; // 超过预设波数后的敌人生成间隔时间
    private float maxWaves = 4 * 6; // 预设的最大波数，超过这个波数后将进入无尽模式

    private int currentWave = 0;  // 当前波数

    private void UpdateEnemyGeneration(float deltaTime)
    {
        // 更新时间
        if (BattleTime >= nextWaveTime)
        {
            currentWave++;
            nextWaveTime = BattleTime + waveInterval;
            if (currentWave > maxWaves)
            {
                nextWaveTime = BattleTime + foreverWaveInterval; // 进入无尽模式，调整生成间隔时间
            }
            setupLevel += 1 * DifficultyMultiplier; // 每波增加1级，乘以难度系数

            // 要生产的敌人
            List<int> enemyToDo = DifficultyConfig.difficultySettings[Mathf.Min(currentWave - 1, DifficultyConfig.difficultySettings.Count - 1)];

            // 生成敌人
            _ = GenerateEnemies(enemyToDo);
        }
    }

    private async UniTask GenerateEnemies(List<int> enemyToDo)
    {
        for (int i = 0; i < enemyToDo.Count; i++)
        {
            int count = enemyToDo[i];
            for (int j = 0; j < count; j++)
            {
                if (playerCharacter == null || !IsBattleActive)
                {
                    return; // 如果玩家角色不存在，停止生成敌人
                }
                SpawnEnemy((EnemyType)i);
                await UniTask.Delay(500); // 每生成一个敌人等待0.5秒
            }
        }
    }


    private void SpawnEnemy(EnemyType enemyType)
    {
        GameObject enemyPrefab = SpawnManager.Instance.GetEnemyPrefab(enemyType);
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject enemyInstance = PoolCore.Get(enemyPrefab);
        Enemy enemyCharacter = enemyInstance.GetComponent<Enemy>();
        enemyCharacters.Add(enemyCharacter);
        enemyCharacter.Setup((int)setupLevel);  // 构建并强化敌人
        enemyCharacter.SetBoost(isMoreHP, isMoreDamage);
        enemyCharacter.OnDeath += () =>
        {
            AddScore(enemyCharacter.Level); // 根据敌人当前等级增加分数
            enemyCharacters.Remove(enemyCharacter);
            KillCount++; // 增加击杀数
        };
        enemyInstance.transform.position = spawnPosition;
    }

    public Enemy[] GetAllEnemies()
    {
        enemyCharacters = enemyCharacters.FindAll(c => c != null); // 清理已死亡的敌人
        return enemyCharacters.Cast<Enemy>().ToArray();
    }


    private Vector2 GetRandomSpawnPosition()
    {
        if (playerCharacter == null)
        {
            return Vector2.zero; // 如果玩家角色未初始化，返回原点
        }
        // 在玩家中心， 半径为25的范围圆上随机生成一个点
        Vector2 playerPos = playerCharacter.transform.position;
        float angle = Random.Range(0f, 360f);
        float radius = 25f;
        Vector2 spawnPos = playerPos + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        return spawnPos;
    }
    #endregion


    #region Score Management

    public long MaxScore => VMContainer.Get<GameViewModel>().MaxScore;
    
    private long score = 0;
    public long Score
    {
        get => score;
        set => SetProperty(ref score, value);
    }


    public void AddScore(int baseScore)
    {
        long finalScore = (long)Mathf.Round(baseScore * ScoreMultiplier);
        Score += finalScore;
    }


    #endregion

    public int RefeshCount = 0;
    public void ChangeRefreshCount(int amount)
    {
        RefeshCount += amount;
        if (RefeshCount < 0) RefeshCount = 0;
    }
}