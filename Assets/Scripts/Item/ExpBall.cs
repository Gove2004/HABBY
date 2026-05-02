using DG.Tweening;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class ExpBall : MonoBehaviour, IPoolable
{
    private Character player;
    private SpriteRenderer spriteRenderer;
    private int expValue = 1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void Setup(int expValue)
    {
        // 重置颜色，防止因为预制体上的绿色导致和蓝色贴图正片叠底变成黑色
        spriteRenderer.color = Color.white;
        this.expValue = expValue;
        transform.localScale = Vector3.one * 0.5f;
        
        // 随机选择一个纹理
        int randomIndex = Random.Range(0, 3); // 假设有3种不同的纹理
        switch (randomIndex)
        {
            case 0:
                spriteRenderer.sprite = MyStatic.GenerateSquareTexture(32, Color.green);
                break;
            case 1:
                spriteRenderer.sprite = MyStatic.GenerateCircleTexture(32, Color.green);
                break;
            case 2:
                spriteRenderer.sprite = MyStatic.GenerateTriangleTexture(32, Color.green);
                break;
        }

        // 随机位移1小段距离并且随机旋转
        transform.DOMove(transform.position + (Vector3)(Random.insideUnitCircle), 0.5f).SetEase(Ease.OutQuad);
        transform.DORotate(new Vector3(0, 0, Random.Range(0f, 360f)), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutQuad);
    
        player = VMContainer.Get<BattleViewModel>().playerCharacter;
    }


    private float speed = 0f;
    private float acceleration = 1f;
    public void FixedUpdate()
    {
        // 飞向玩家
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            speed += acceleration * Time.fixedDeltaTime;
            transform.position += (Vector3)(direction * speed * Time.fixedDeltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 获取经验
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.AddExp(expValue);
            }

            PoolCore.Return(this.gameObject);
        }
    }

    public void OnRecycle()
    {
        expValue = 1;
        speed = 0f;
        transform.localScale = Vector3.one * 0.5f;
    }
}
