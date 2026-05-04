

using System;
using GoveKits.Runtime.UI;
using UnityEngine;

public class LargeBoss : Enemy
{
	private float attackTimer = 0f;
	private float attackCooldown = 1.2f;
	private float chargeTimer = 0f;
	private float chargeCooldownTimer = 2f;
	private bool isCharging;

	private const float ChaseDistance = 10f;
	private const float ChargeCooldown = 2.5f;
	private const float ChargeDuration = 0.5f;
	private const float ChargeSpeedMultiplier = 5f;
	private const float ChargeChance = 0.8f;


	public override void Setup(int level)
    {
		this.level = level;
        MaxHP = level * level;
        CurrentHP = MaxHP;
        AttackPower = Mathf.Max(1, Mathf.RoundToInt(level / 2));
        DefensePower = Mathf.FloorToInt(level / 2);
        MoveSpeed = 1f;

        player = VMContainer.Get<BattleViewModel>().playerCharacter;
        OnDeath -= MyDestroy;
        OnDeath += MyDestroy;

        if (hpBar != null) hpBar.SetCharacter(this);
    }


	public override void OnRecycle()
	{
		base.OnRecycle();
		attackTimer = 0f;
		chargeTimer = 0f;
		chargeCooldownTimer = 2f;
		isCharging = false;
	}

	protected override void Update()
	{
		base.Update();
		attackTimer -= Time.deltaTime;
		chargeCooldownTimer -= Time.deltaTime;

		if (isCharging)
		{
			chargeTimer -= Time.deltaTime;
			if (chargeTimer <= 0f)
			{
				isCharging = false;
				chargeCooldownTimer = ChargeCooldown;
				StopMovement();
			}
		}
	}


	private void FixedUpdate()
	{
		if (player == null)
		{
			StopMovement();
			return;
		}

		if (isCharging)
		{
			MoveTowardsPlayer(ChargeSpeedMultiplier);
			return;
		}

		if (DistanceToPlayer > ChaseDistance)
		{
			MoveTowardsPlayer();
			return;
		}

		StopMovement();

		if (chargeCooldownTimer <= 0f)
		{
			if (UnityEngine.Random.value <= ChargeChance)
			{
				isCharging = true;
				chargeTimer = ChargeDuration;
			}

			chargeCooldownTimer = ChargeCooldown;
		}
	}


	private void OnTriggerStay2D(Collider2D collision)
	{
		if (attackTimer > 0f || !collision.CompareTag("Player"))
		{
			return;
		}

		Player target = collision.GetComponent<Player>();
		if (target == null)
		{
			return;
		}

		int damage = isCharging ? Mathf.RoundToInt(CurrentAttackPower * 1.75f) : CurrentAttackPower;
		target.TakeDamageFrom(damage, transform.position);
		attackTimer = attackCooldown;
	}

	
}