using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using Unity.VisualScripting;
using UnityEngine;

public class BattleViewModel : ViewModel
{
    #region Lifecycle

    private bool isBattleActive = false;
    private float battleTime;
    public float BattleTime
    {
        get => battleTime;
        set => SetProperty(ref battleTime, value);
    }

    public void StartBattle(HeroType heroType)
    {
        isBattleActive = true;
        // 初始化战斗数据

        // 根据选择的英雄类型创建角色
        var heroPrefab = SpawnManager.Instance.GetHeroPrefab(heroType);
        var heroInstance = GameObject.Instantiate(heroPrefab);
        heroInstance.transform.position = Vector3.zero; // 设置初始位置
        playerCharacter = heroInstance.GetComponent<Player>();

        BuffManager.Instance.ResetPlayerAvailableBuffs(heroType);
    }


    public void UpdateBattle(float deltaTime)
    {
        BattleTime += deltaTime;
        
        UpdateEnemyGeneration(deltaTime);
    }

    public void EndBattle()
    {
        // 处理战斗结束逻辑
        isBattleActive = false;
        BattleTime = 0f;
        playerCharacter = null;
        enemyCharacters.Clear();
        setupLevel = 0;
        currentWave = 0;
    }

    #endregion


    #region Enemy Management

    public Player playerCharacter { get; private set; }
    private List<Character> enemyCharacters = new List<Character>();

    #endregion

    #region Generate Manage

    private float difficultyMultiplier = 1f;  // 难度系数
    private int setupLevel = 0;  // 当前敌人等级

    private float nextWaveTime = 5f;  // 下一波敌人出现时间, 默认值为第一波
    private float waveInterval = 15f;   // 每波敌人间隔时间
    private float foreverWaveInterval = 30f; // 超过预设波数后的敌人生成间隔时间
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
            setupLevel += (int)(1 * difficultyMultiplier); // 每波增加1级，乘以难度系数

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
                if (playerCharacter == null || !isBattleActive)
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
        enemyInstance.transform.position = spawnPosition;
        Character enemyCharacter = enemyInstance.GetComponent<Character>();
        enemyCharacters.Add(enemyCharacter);
        enemyCharacter.Setup(setupLevel); // 根据当前波数设置敌人属性
        enemyCharacter.OnDeath += () => enemyCharacters.Remove(enemyCharacter);
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
}