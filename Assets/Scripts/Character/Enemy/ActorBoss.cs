
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class ActorBoss : Enemy
{
	private enum BossState
	{
		Approach,
		MultiFan,
		Rest
	}

	private BossState currentState;
	private float stateTimer;
	private int waveCount = 0;

	private const float ShootDistance = 10f;
	private const float RetreatDistance = 15f;
	private const float RestDuration = 2f;
	private const float MultiFanBulletSpeed = 5f;

	public override void Setup(int level)
    {
		this.level = level;
        MaxHP = level * level;
        CurrentHP = MaxHP;
        AttackPower = Mathf.Max(1, Mathf.RoundToInt(level / 2));
        DefensePower = Mathf.FloorToInt(level / 5);
        MoveSpeed = 1f;

		currentState = BossState.Approach;
        player = VMContainer.Get<BattleViewModel>().playerCharacter;
        OnDeath -= MyDestroy;
        OnDeath += MyDestroy;

        if (hpBar != null) hpBar.SetCharacter(this);
    }


	public override void OnRecycle()
	{
		base.OnRecycle();
		currentState = BossState.Approach;
		stateTimer = 0f;
		waveCount = 0;
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
					currentState = BossState.MultiFan;
					stateTimer = 0f;
					waveCount = 0;
				}
				break;

			case BossState.MultiFan:
				StopMovement();
				if (stateTimer <= 0f)
				{
					if (waveCount < 3)
					{
						FireMultiFan();
						waveCount++;
						stateTimer = 0.25f; // 波次间隔0.5秒
					}
					else
					{
						waveCount = 0;
						currentState = BossState.Rest;
						stateTimer = RestDuration;
					}
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
					currentState = BossState.MultiFan;
					waveCount = 0;
				}
				break;
		}
	}


	private void FireMultiFan()
	{
		// 150度扇形，5颗子弹
		SpawnBullet(7, 120f, 1f, 10f, MultiFanBulletSpeed, 0, 1.5f);
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
				.SetMotion(transform.position, direction, speed)
				.SetColor(Color.red); // Boss子弹颜色为红色
		}
	}
}