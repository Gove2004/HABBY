
using UnityEngine;

public class BufferEnemy : Enemy
{
	private float buffTimer = 0f;

	private const float BuffInterval = 4f;
	private const float BuffRange = 12f;

	public override void Setup(int level)
    {
        base.Setup(level);
        buffTimer = Random.Range(1f, 2f);
    }

	protected override void Update()
	{
		base.Update();
		buffTimer -= Time.deltaTime;
	}


	private void FixedUpdate()
	{
		if (player == null)
		{
			StopMovement();
			return;
		}

		if (DistanceToPlayer > BuffRange)
		{
			MoveTowardsPlayer(0.9f);
		}
		else
		{
			StopMovement();
		}

		if (buffTimer <= 0f)
		{
			ApplyBuffToNearbyEnemies();
			buffTimer = BuffInterval;
		}
	}


	private void ApplyBuffToNearbyEnemies()
	{
		BaseBuff buff = BuffManager.Instance.GetEnemyBuff();
		if (buff == null)
		{
			return;
		}

		Enemy[] enemies = Object.FindObjectsOfType<Enemy>();
		foreach (Enemy enemy in enemies)
		{
			if (enemy == null || enemy == this)
			{
				continue;
			}

			if (Vector2.Distance(transform.position, enemy.transform.position) > BuffRange)
			{
				continue;
			}

			buff.Apply(enemy);
		}
	}
}