using Cysharp.Threading.Tasks;
using GoveKits.Runtime.Core;
using UnityEngine;

public class ActorBullet : MonoBehaviour, IPoolable
{
    private bool isPlayerBullet;
    private int damage;
    private float lifeTime;
    private int throughCount; // 穿透剩余数，0表示不穿透

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
                enemy.TakeDamageFrom(damage, transform.position);
                throughCount--;
                if (throughCount < 0)
                {
                    isDead = true;
                }
            }
        }
        
        if (isDead)
        {
            MyDestroy();
        }
    }


    private async void MyDestroy()
    {
        // 等待轨迹渲染
        PoolCore.Return(this.gameObject);
    }


    public void OnRecycle()
    {
        // 重置子弹状态
        isPlayerBullet = false;
        damage = 0;
        lifeTime = 0f;
        rb.linearVelocity = Vector2.zero;
        transform.localScale = Vector3.one * 0.25f; // 重置为默认大小
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

    public ActorBullet SetSize(float size)
    {
        transform.localScale *= size;
        return this;
    }

    public ActorBullet SetThroughCount(int count)
    {
        throughCount = count;
        return this;
    }
}
