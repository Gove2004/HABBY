using DG.Tweening;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [SerializeField] private GameObject minPlayer;
    [SerializeField] private GameObject minBullet;
    // private TrailRenderer bulletTrail;
    [SerializeField] private GameObject minEnemy;
    private SpriteRenderer enemySpriteRenderer;
    private Vector2 enemyMoveDirection = Vector2.left;
    private Vector2 bulletMoveDirection = Vector2.right;
    private float bulletShootTimer = 0f;

    private void Start()
    {
        // bulletTrail = minBullet.GetComponentInChildren<TrailRenderer>();
        enemySpriteRenderer = minEnemy.GetComponent<SpriteRenderer>();
        minBullet.SetActive(false);
    }

    public void Update()
    {
        // 敌人向玩家移动
        minEnemy.transform.position += (Vector3)enemyMoveDirection * Time.deltaTime * 2f;

        // 发射子弹
        bulletShootTimer += Time.deltaTime;
        if (bulletShootTimer >= 1f)
        {
            minBullet.transform.position = minPlayer.transform.position; // 从玩家位置发射
            // bulletTrail.Clear(); // 清除之前的轨迹
            minBullet.SetActive(true);
            bulletShootTimer = 0f;
        }

        // 移动子弹
        if (minBullet.activeSelf)
        {
            minBullet.transform.position += (Vector3)bulletMoveDirection * Time.deltaTime * 4; // 子弹速度
        }

        // 命中
        if (minBullet.activeSelf && Vector2.Distance(minBullet.transform.position, minEnemy.transform.position) < 0.5f)
        {
            minBullet.SetActive(false); // 隐藏子弹

            // 漂字
            if (Random.value < 0.9f)  // 20%概率暴击
            {
                FloatTextManager.Instance.Show("1", minEnemy.transform.position, Color.red);
            }
            else
            {
                FloatTextManager.Instance.Show("2!", minEnemy.transform.position, Color.yellow);
            }

            // 敌人击退
            minEnemy.transform.position += (Vector3)(-enemyMoveDirection) * 2f; // 击退距离
            
            Color originalColor = enemySpriteRenderer.color;
            enemySpriteRenderer.DOColor(new Color(1, 0, 0, 1), 0.15f).OnComplete(() =>
            {
                enemySpriteRenderer.DOColor(originalColor, 0.15f);
            });
        }
    }
}
