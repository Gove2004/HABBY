

using System.Collections.Generic;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public abstract class Enemy : Character
{
    protected Player player;
    protected Rigidbody2D rb;

    private readonly List<TimedStatModifier> timedStatModifiers = new List<TimedStatModifier>();
    private float attackPowerMultiplier = 1f;
    private float moveSpeedMultiplier = 1f;
    private float healPerSecond = 0f;
    private float healAccumulator = 0f;

    protected int CurrentAttackPower => Mathf.Max(1, Mathf.RoundToInt(AttackPower * attackPowerMultiplier));
    protected float CurrentMoveSpeed => MoveSpeed * moveSpeedMultiplier;
    protected float DistanceToPlayer => player == null ? float.PositiveInfinity : Vector2.Distance(transform.position, player.transform.position);

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }


    protected override void Update()
    {
        base.Update();
        TickTimedStatModifiers();
    }


    public override void OnRecycle()
    {
        base.OnRecycle();
        
        timedStatModifiers.Clear();
        attackPowerMultiplier = 1f;
        moveSpeedMultiplier = 1f;
        healPerSecond = 0f;
        healAccumulator = 0f;
        BuffCounters.Clear();
        
        StopMovement();
        OnDeath -= MyDestroy;
    }


    public void SetBoost(bool isMoreHP, bool isMoreDamage)
    {
        if (isMoreHP)
        {
            MaxHP = Mathf.RoundToInt(MaxHP * 2f);
            CurrentHP = MaxHP;
        }

        if (isMoreDamage)
        {
            AttackPower = Mathf.RoundToInt(AttackPower * 2f);
        }
    }


    protected virtual void MyDestroy()
    {
        // 将总经验划分为 3~5 个经验球掉落（保证前期有爆率爽感，后期也不会因为太多模型而卡顿）
        GameObject gameObject = SpawnManager.Instance.GetExpBallPrefab();
        int totalExp = Level;

        if (totalExp > 0)
        {
            int ballCount = Mathf.Min(totalExp, Random.Range(3, 6)); // 数量限制 3 到 5 个（若总经验不足则以实际经验为准）
            int baseExp = totalExp / ballCount;
            int remainder = totalExp % ballCount;

            for (int i = 0; i < ballCount; i++)
            {
                GameObject expBallInstance = PoolCore.Get(gameObject);
                
                // 给经验球稍微增加一点随机的散落偏移，防止完全重叠在一起看不出来
                Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
                expBallInstance.transform.position = (Vector2)transform.position + randomOffset;
                
                ExpBall expBall = expBallInstance.GetComponent<ExpBall>();
                int expForThisBall = baseExp + (i < remainder ? 1 : 0);
                expBall.Setup(expForThisBall);
            }
        }

        PoolCore.Return(this.gameObject);
    }


    protected void MoveTowardsPlayer(float speedMultiplier = 1f)
    {
        if (player == null)
        {
            return;
        }

        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        MoveByDirection(direction, speedMultiplier);
    }


    protected void MoveByDirection(Vector2 direction, float speedMultiplier = 1f)
    {
        float speed = CurrentMoveSpeed * speedMultiplier;
        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * speed;
            return;
        }

        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }


    protected void StopMovement()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }


    protected bool IsPlayerWithin(float distance)
    {
        return player != null && DistanceToPlayer <= distance;
    }


    protected bool IsPlayerBeyond(float distance)
    {
        return player != null && DistanceToPlayer >= distance;
    }


    private float defensePowerBonus = 0f;
    public override int GetDefensePower() => Mathf.FloorToInt(DefensePower + defensePowerBonus);

    public void ApplyTimedStatModifier(float attackPowerBonusMultiplier, float moveSpeedBonusMultiplier, float healPerSecondBonus, float defensePowerBonus, float duration)
    {
        attackPowerMultiplier += attackPowerBonusMultiplier;
        moveSpeedMultiplier += moveSpeedBonusMultiplier;
        healPerSecond += healPerSecondBonus;
        this.defensePowerBonus += defensePowerBonus;

        if (duration <= 0f)
        {
            return;
        }

        timedStatModifiers.Add(new TimedStatModifier
        {
            AttackPowerBonusMultiplier = attackPowerBonusMultiplier,
            MoveSpeedBonusMultiplier = moveSpeedBonusMultiplier,
            HealPerSecondBonus = healPerSecondBonus,
            DefensePowerBonus = defensePowerBonus,
            RemainingTime = duration
        });
    }


    private void TickTimedStatModifiers()
    {
        float deltaTime = Time.deltaTime;

        if (!Mathf.Approximately(healPerSecond, 0f))
        {
            healAccumulator += healPerSecond * deltaTime;
            int healAmount = Mathf.FloorToInt(healAccumulator);
            if (healAmount > 0)
            {
                Heal(healAmount);
                healAccumulator -= healAmount;
            }
        }

        for (int i = timedStatModifiers.Count - 1; i >= 0; i--)
        {
            TimedStatModifier modifier = timedStatModifiers[i];
            modifier.RemainingTime -= deltaTime;
            if (modifier.RemainingTime > 0f)
            {
                timedStatModifiers[i] = modifier;
                continue;
            }

            attackPowerMultiplier -= modifier.AttackPowerBonusMultiplier;
            moveSpeedMultiplier -= modifier.MoveSpeedBonusMultiplier;
            healPerSecond -= modifier.HealPerSecondBonus;
            this.defensePowerBonus -= modifier.DefensePowerBonus;
            timedStatModifiers.RemoveAt(i);
        }
    }


    private struct TimedStatModifier
    {
        public float AttackPowerBonusMultiplier;
        public float MoveSpeedBonusMultiplier;
        public float HealPerSecondBonus;
        public float DefensePowerBonus;
        public float RemainingTime;
    }
}