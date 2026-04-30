using GoveKits.Runtime.Core;
using UnityEngine;

public class Actor : Character
{
    private Rigidbody2D rb;
    private float shootRate = 1f;
    private float shootCooldown => shootRate / attackComponent.AttackSpeed;
    private float shootTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Setup(1);

        BattleManager.Instance.RegisterPlayer(this);
    }


    protected override void Update()
    {
        base.Update();
        
        shootTimer += Time.deltaTime;

        if (MyInputManager.Instance.IsMouseHeld && shootTimer >= shootCooldown)
        {
            Vector2 mousePos = MyInputManager.Instance.MousePosition;
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 direction = (worldMousePos - rb.position).normalized;

            Shoot(direction);
            
            shootTimer = 0f; // 重置射击计时器
        }
    }

    private void FixedUpdate()
    {
        Vector2 inputDir = MyInputManager.Instance.InputDirection;
        rb.linearVelocity = inputDir * moveComponent.MoveSpeed;
    }


    public override void Setup(int level)
    {
        base.Setup(level);


    }


    private void Shoot(Vector2 direction)
    {
        GameObject bulletObj = SpawnManager.Instance.GetBulletPrefab();
        
        ActorBullet bullet = PoolCore.Get(bulletObj).GetComponent<ActorBullet>();
        bullet.SetPlayerBullet().SetDamage(attackComponent.AttackPower);
        bullet.SetLifeTime(2.0f); // 设置子弹的生命周期
        bullet.SetMotion(transform.position, direction, 10.0f); // 设置子弹
    }
}
