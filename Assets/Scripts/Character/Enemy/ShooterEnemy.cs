

using GoveKits.Runtime.Core;
using UnityEngine;

public class ShooterEnemy : Enemy
{
	private enum ShooterState
	{
		Approach,
		Shoot,
		Rest
	}

	private ShooterState currentState;
	private float stateTimer;

	private const float ShootDistance = 10f;
	private const float RetreatDistance = 15f;
	private const float RestDuration = 1.2f;
	private const float ShootScatterAngle = 10f;
	private const int ShootCount = 1;
	private const float BulletSpeed = 11f;

	public override void Setup(int level)
    {
        base.Setup(level);
        currentState = ShooterState.Approach;
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
			case ShooterState.Approach:
				if (distanceToPlayer > ShootDistance)
				{
					MoveTowardsPlayer();
				}
				else
				{
					StopMovement();
					currentState = ShooterState.Shoot;
					stateTimer = 0f;
				}
				break;

			case ShooterState.Shoot:
				StopMovement();
				if (stateTimer <= 0f)
				{
					FireBurst();
					currentState = ShooterState.Rest;
					stateTimer = RestDuration;
				}
				break;

			case ShooterState.Rest:
				StopMovement();
				if (distanceToPlayer >= RetreatDistance)
				{
					currentState = ShooterState.Approach;
				}
				else if (stateTimer <= 0f)
				{
					currentState = ShooterState.Shoot;
				}
				break;
		}
	}


	private void FireBurst()
	{
		GameObject bulletPrefab = SpawnManager.Instance.GetBulletPrefab();
		if (bulletPrefab == null || player == null)
		{
			return;
		}

		Vector2 baseDirection = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
		float startAngle = -ShootScatterAngle * 0.5f;
		float angleStep = ShootCount > 1 ? ShootScatterAngle / (ShootCount - 1) : 0f;

		for (int i = 0; i < ShootCount; i++)
		{
			ActorBullet bullet = PoolCore.Get(bulletPrefab).GetComponent<ActorBullet>();
			if (bullet == null)
			{
				continue;
			}

			float currentAngle = startAngle + angleStep * i + Random.Range(-2f, 2f);
			Vector2 direction = Quaternion.Euler(0f, 0f, currentAngle) * baseDirection;
			bullet.SetDamage(CurrentAttackPower)
				.SetLifeTime(2.5f)
				.SetSize(0.9f)
				.SetThroughCount(0)
				.SetMotion(transform.position, direction, BulletSpeed);
		}
	}
}