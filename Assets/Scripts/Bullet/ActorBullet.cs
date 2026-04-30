using GoveKits.Runtime.Core;
using UnityEngine;

public class ActorBullet : MonoBehaviour, IPoolable
{
    private bool isPlayerBullet;
    private int damage;
    private float lifeTime;

    private Rigidbody2D rb;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            MyDestroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isDead = false;
        if (isPlayerBullet && collision.CompareTag("Enemy") 
            || !isPlayerBullet && collision.CompareTag("Player"))
        {
            Character enemy = collision.GetComponent<Character>();
            if (enemy != null)
            {
                enemy.hpComponent.TakeDamage(damage);
                isDead = true;
            }
        }
        
        if (isDead)
        {
            MyDestroy();
        }
    }


    private void MyDestroy()
    {
        PoolCore.Return(this.gameObject);
    }


    public void OnRecycle()
    {
        // 重置子弹状态
        isPlayerBullet = false;
        damage = 0;
        lifeTime = 0f;
        rb.linearVelocity = Vector2.zero;
    }


    public ActorBullet SetPlayerBullet()
    {
        isPlayerBullet = true;
        return this;
    }

    public ActorBullet SetDamage(int damage)
    {
        this.damage = damage;
        return this;
    }

    public ActorBullet SetLifeTime(float lifeTime)
    {
        this.lifeTime = lifeTime;
        return this;
    }

    public ActorBullet SetMotion(Vector2 start, Vector2 direction, float speed)
    {
        transform.position = start;
        if (trailRenderer != null)
        {
            trailRenderer.Clear();
        }
        rb.linearVelocity = direction.normalized * speed;
        return this;
    }
}
