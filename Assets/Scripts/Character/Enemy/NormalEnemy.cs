using System;
using GoveKits.Runtime.UI;
using UnityEngine;

public class NormalEnemy : Enemy
{
    private float attackTimer = 0f;
    private float attackCooldown = 1f;

    public override void Setup(int level)
    {
        this.level = level;
        MaxHP = 3 * level;
        CurrentHP = MaxHP;
        AttackPower = Mathf.Max(1, Mathf.RoundToInt(level / 2));
        DefensePower = Mathf.FloorToInt(level / 2);
        MoveSpeed = 2f;

        player = VMContainer.Get<BattleViewModel>().playerCharacter;
        OnDeath -= MyDestroy;
        OnDeath += MyDestroy;

        if (hpBar != null) hpBar.SetCharacter(this);
    }


    public override void OnRecycle()
    {
        base.OnRecycle();
        attackTimer = 0f;
    }

    protected override void Update()
    {
        base.Update();
        attackTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attackTimer <= 0f && collision.CompareTag("Player"))
        {
            var p = collision.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamageFrom(CurrentAttackPower, transform.position);
                attackTimer = attackCooldown;
            }
        }
    }

    
}
