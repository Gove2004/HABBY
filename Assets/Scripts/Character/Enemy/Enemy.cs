

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
        // 生成等同于 自身等级的经验球
        GameObject gameObject = SpawnManager.Instance.GetExpBallPrefab();
        int exp = Level;

        while (exp > 0)
        {
            GameObject expBallInstance = PoolCore.Get(gameObject);
            expBallInstance.transform.position = transform.position;
            ExpBall expBall = expBallInstance.GetComponent<ExpBall>();
            
            int cost = Mathf.Min(exp, 10); // 每个经验球最多10点经验
            expBall.Setup(cost);
            exp -= cost;
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


    public void ApplyTimedStatModifier(float attackPowerBonusMultiplier, float moveSpeedBonusMultiplier, float healPerSecondBonus, float duration)
    {
        attackPowerMultiplier += attackPowerBonusMultiplier;
        moveSpeedMultiplier += moveSpeedBonusMultiplier;
        healPerSecond += healPerSecondBonus;

        if (duration <= 0f)
        {
            return;
        }

        timedStatModifiers.Add(new TimedStatModifier
        {
            AttackPowerBonusMultiplier = attackPowerBonusMultiplier,
            MoveSpeedBonusMultiplier = moveSpeedBonusMultiplier,
            HealPerSecondBonus = healPerSecondBonus,
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
            timedStatModifiers.RemoveAt(i);
        }
    }


    private struct TimedStatModifier
    {
        public float AttackPowerBonusMultiplier;
        public float MoveSpeedBonusMultiplier;
        public float HealPerSecondBonus;
        public float RemainingTime;
    }
}