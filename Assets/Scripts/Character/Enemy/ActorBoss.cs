
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class ActorBoss : Enemy
{
	private enum BossState
	{
		Approach,
		Laser,
		Fan,
		Rest
	}

	private BossState currentState;
	private float stateTimer;

	private const float ShootDistance = 10f;
	private const float RetreatDistance = 15f;
	private const float RestDuration = 2f;
	private const float LaserBulletSpeed = 5f;
	private const float FanBulletSpeed = 5f;

	public override void Setup(int level)
    {
		this.level = level;
        MaxHP = 5 * level;
        CurrentHP = MaxHP;
        AttackPower = Mathf.Max(1, Mathf.RoundToInt(level / 2));
        DefensePower = Mathf.FloorToInt(level / 2);
        MoveSpeed = 1f;

		currentState = BossState.Approach;
        player = VMContainer.Get<BattleViewModel>().playerCharacter;
        OnDeath -= MyDestroy;
        OnDeath += MyDestroy;

        if (hpBar != null) hpBar.SetCharacter(this);
    }


	protected override void Update()
	{
		base.Update();
		stateTimer -= Time.deltaTime;
	}


	private void FixedUpdate()
	{
		if (player == null)
		{
			StopMovement();
			return;
		}

		float distanceToPlayer = DistanceToPlayer;

		switch (currentState)
		{
			case BossState.Approach:
				if (distanceToPlayer > ShootDistance)
				{
					MoveTowardsPlayer();
				}
				else
				{
					StopMovement();
					currentState = Random.value < 0.4f ? BossState.Laser : BossState.Fan;
					stateTimer = 0f;
				}
				break;

			case BossState.Laser:
				StopMovement();
				if (stateTimer <= 0f)
				{
					FireLaser();
					currentState = BossState.Rest;
					stateTimer = RestDuration;
				}
				break;

			case BossState.Fan:
				StopMovement();
				if (stateTimer <= 0f)
				{
					FireFan();
					currentState = BossState.Rest;
					stateTimer = RestDuration;
				}
				break;

			case BossState.Rest:
				StopMovement();
				if (distanceToPlayer >= RetreatDistance)
				{
					currentState = BossState.Approach;
				}
				else if (stateTimer <= 0f)
				{
					currentState = Random.value < 0.4f ? BossState.Laser : BossState.Fan;
				}
				break;
		}
	}


	private void FireLaser()
	{
		SpawnBullet(3, 30f, 1.6f, 3f, LaserBulletSpeed, 3, 3f);
	}


	private void FireFan()
	{
		SpawnBullet(7, 55f, 0.8f, 2.2f, FanBulletSpeed, 0, 1f);
	}


	private void SpawnBullet(int count, float spreadAngle, float size, float lifeTime, float speed, int throughCount, float damageMultiplier)
	{
		GameObject bulletPrefab = SpawnManager.Instance.GetBulletPrefab();
		if (bulletPrefab == null || player == null)
		{
			return;
		}

		Vector2 baseDirection = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
		float startAngle = -spreadAngle * 0.5f;
		float angleStep = count > 1 ? spreadAngle / (count - 1) : 0f;

		for (int i = 0; i < count; i++)
		{
			ActorBullet bullet = PoolCore.Get(bulletPrefab).GetComponent<ActorBullet>();
			if (bullet == null)
			{
				continue;
			}

			float currentAngle = startAngle + angleStep * i;
			Vector2 direction = Quaternion.Euler(0f, 0f, currentAngle) * baseDirection;
			bullet.SetDamage(Mathf.RoundToInt(CurrentAttackPower * damageMultiplier))
				.SetLifeTime(lifeTime)
				.SetSize(size)
				.SetThroughCount(throughCount)
				.SetMotion(transform.position, direction, speed);
		}
	}
}