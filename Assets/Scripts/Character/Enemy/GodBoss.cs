

using GoveKits.Runtime.UI;
using UnityEngine;

public class GodBoss : Enemy
{
	private float buffTimer = 0f;

	private const float BuffInterval = 2.5f;
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

		// GodBoss给场上"每个"敌人施加1种随机Buff
		foreach (var enemy in candidates)
		{
			BaseBuff randomBuff = BuffManager.Instance.GetEnemyBuff();
			randomBuff?.Apply(enemy, CurrentAttackPower);
		}
	}
}