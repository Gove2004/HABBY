
using GoveKits.Runtime.UI;
using UnityEngine;

public class BufferEnemy : Enemy
{
	private float buffTimer = 0f;

	private const float BuffInterval = 4f;
	private const float BuffRange = 12f;

	public override void Setup(int level)
    {
		this.level = level;
        MaxHP = level;
        CurrentHP = MaxHP;
        AttackPower = Mathf.Max(1, Mathf.RoundToInt(level / 3));
        DefensePower = Mathf.FloorToInt(level / 2);
        MoveSpeed = 2f;

		buffTimer = Random.Range(0f, BuffInterval);
        player = VMContainer.Get<BattleViewModel>().playerCharacter;
        OnDeath -= MyDestroy;
        OnDeath += MyDestroy;

        if (hpBar != null) hpBar.SetCharacter(this);
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

		Enemy[] all = VMContainer.Get<BattleViewModel>().GetAllEnemies();
		var candidates = new System.Collections.Generic.List<Enemy>();
		foreach (var e in all)
		{
			if (e == null || e == this) continue;
			candidates.Add(e);
		}

		if (candidates.Count == 0) return;

		// pick one random enemy on the field
		int idx = Random.Range(0, candidates.Count);
		buff.Apply(candidates[idx]);
	}
}