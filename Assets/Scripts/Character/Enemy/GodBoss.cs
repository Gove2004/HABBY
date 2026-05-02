

using UnityEngine;

public class GodBoss : Enemy
{
	private float buffTimer = 0f;

	private const float BuffInterval = 5f;
	private const float BuffRange = 18f;


    public override void Setup(int level)
    {
        base.Setup(level);
        buffTimer = Random.Range(2f, 3f);
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

		if (DistanceToPlayer > 10f)
		{
			MoveTowardsPlayer(1.1f);
		}
		else
		{
			StopMovement();
		}

		if (buffTimer <= 0f)
		{
			ApplyDivineBuffs();
			buffTimer = BuffInterval;
		}
	}


	private void ApplyDivineBuffs()
	{
		Enemy[] enemies = Object.FindObjectsOfType<Enemy>();
		foreach (Enemy enemy in enemies)
		{
			if (enemy == null)
			{
				continue;
			}

			if (Vector2.Distance(transform.position, enemy.transform.position) > BuffRange)
			{
				continue;
			}

			BaseBuff attackBuff = BuffManager.Instance.GetEnemyBuff();
			BaseBuff speedBuff = BuffManager.Instance.GetEnemyBuff();
			BaseBuff sustainBuff = BuffManager.Instance.GetEnemyBuff();

			attackBuff?.Apply(enemy);
			speedBuff?.Apply(enemy);
			sustainBuff?.Apply(enemy);

			enemy.Heal(Mathf.Max(1, Level / 2));
		}
	}
}