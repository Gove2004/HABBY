using GoveKits.Runtime.UI;
using UnityEngine;

public class NormalEnemy : Enemy
{
    private float attackTimer = 0f;
    private float attackCooldown = 1f;

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

    public override void Setup(int level)
    {
        base.Setup(level);
    }
}
