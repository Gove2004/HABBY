

using GoveKits.Runtime.UI;
using UnityEngine;

public class GodBoss : Enemy
{
	private float buffTimer = 0f;

	private const float BuffInterval = 5f;
	private const float BuffRange = 18f;


    public override void Setup(int level)
    {
		this.level = level;
        MaxHP = level * level;
        CurrentHP = MaxHP;
        AttackPower = Mathf.Max(1, Mathf.RoundToInt(level / 3));
        DefensePower = Mathf.FloorToInt(level / 3);
        MoveSpeed = 4f;

		buffTimer = Random.Range(0f, BuffInterval);
        player = VMContainer.Get<BattleViewModel>().playerCharacter;
        OnDeath -= MyDestroy;
        OnDeath += MyDestroy;

        if (hpBar != null) hpBar.SetCharacter(this);
    }


	public override void OnRecycle()
	{
		base.OnRecycle();
		buffTimer = Random.Range(0f, BuffInterval);
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
		Enemy[] all = VMContainer.Get<BattleViewModel>().GetAllEnemies();
		var candidates = new System.Collections.Generic.List<Enemy>();
		foreach (var e in all)
		{
			if (e == null || e == this) continue;
			candidates.Add(e);
		}

		if (candidates.Count == 0) return;

		int n = 5;
		// shuffle candidates (Fisher-Yates)
		for (int i = 0; i < candidates.Count; i++)
		{
			int j = Random.Range(i, candidates.Count);
			var tmp = candidates[i];
			candidates[i] = candidates[j];
			candidates[j] = tmp;
		}

		int take = Mathf.Min(n, candidates.Count);
		for (int k = 0; k < take; k++)
		{
			var enemy = candidates[k];
			BaseBuff attackBuff = BuffManager.Instance.GetEnemyBuff();
			BaseBuff speedBuff = BuffManager.Instance.GetEnemyBuff();
			BaseBuff sustainBuff = BuffManager.Instance.GetEnemyBuff();

			attackBuff?.Apply(enemy, CurrentAttackPower);
			speedBuff?.Apply(enemy, CurrentAttackPower);
			sustainBuff?.Apply(enemy, CurrentAttackPower);

			enemy.Heal(Mathf.Max(1, Level / 2));
		}
	}
}