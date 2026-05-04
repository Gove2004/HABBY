using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class Actor : Player
{
    private Rigidbody2D rb;
    public float AttackSpeed;
    private float shootTimer = 0f;

    public float BulletSpeed;
    public float BulletSize;
    public float BulletLifeTime;
    public int BulletThroughCount = 0; // 穿透数，0表示不穿透
    public int BulletFireCount = 1; // 发射数，1表示单发


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Setup(1);
    }


    protected override void Update()
    {
        base.Update();
        
        shootTimer += Time.deltaTime;

        if (MyInputManager.Instance.IsMouseHeld && shootTimer >= 1f / AttackSpeed)
        {
            Vector2 mousePos = MyInputManager.Instance.MousePosition;
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Shoot(worldMousePos);
            
            shootTimer = 0f; // 重置射击计时器
        }
    }

    private void FixedUpdate()
    {
        Vector2 inputDir = MyInputManager.Instance.InputDirection;
        rb.linearVelocity = inputDir * MoveSpeed;
    }


    public override void Setup(int level = 1)
    {
        this.level = level;
        exp = 0;
        nextLevelExpThreshold = MyStatic.GetExpThresholdForLevel(level);
        
        GameViewModel gameViewModel = VMContainer.Get<GameViewModel>();
        ExpRateMultiplier = (float)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.ExpRate);
        HealPerFiveSeconds = (int)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.HealPerFives);

        MaxHP = (int)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.MaxHP);
        CurrentHP = MaxHP;
        AttackPower = (int)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.AttackPower);
        DefensePower = (int)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.DefensePower);
        AttackSpeed = (float)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.AttackSpeed);
        CritRate = (float)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.CritRate);
        CritDamageMultiplier = (float)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.CritDamageMultiplier);

        MoveSpeed = 2f * (float)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.MoveSpeed);
        BulletFireCount = (int)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.BulletCount);
        BulletThroughCount = (int)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.BulletPierce);
        BulletSpeed = 10.0f * (float)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.BulletSpeed);
        BulletSize = 1.0f * (float)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.BulletSize);
        BulletLifeTime = 1.0f * (float)gameViewModel.GetNowAttribute(HeroType.Actor, PlayerAttributeType.BulletLife);

        if (hpBar != null) hpBar.SetCharacter(this);
    }


    [Header("散射参数")]
    public float baseSpreadAngle = 15f;     // 基础散射角度（单发时）
    public float maxSpreadAngle = 90f;      // 最大散射角度
    public float spreadGrowthRate = 0.5f;   // 散射增长速率
    public bool dynamicSpread = true;       // 是否动态调整散射角度

    private void Shoot(Vector2 targetPos)
    {
        Vector2 baseDirection = (targetPos - (Vector2)transform.position).normalized;
        GameObject bulletObj = SpawnManager.Instance.GetBulletPrefab();
        
        // 动态计算散射角度
        float currentSpreadAngle = dynamicSpread 
            ? CalculateDynamicSpreadAngle() 
            : baseSpreadAngle;
        
        // 计算每颗子弹的角度
        float angleStep = BulletFireCount > 1 ? currentSpreadAngle / (BulletFireCount - 1) : 0f;
        float startAngle = -currentSpreadAngle / 2f;
        
        for (int i = 0; i < BulletFireCount; i++)
        {
            bool isCritical = Random.value < CritRate;
            int damage = isCritical ? Mathf.RoundToInt(AttackPower * CritDamageMultiplier) : AttackPower;

            ActorBullet bullet = PoolCore.Get(bulletObj).GetComponent<ActorBullet>();
            bullet.SetPlayerBullet().SetDamage(damage, isCritical);
            bullet.SetLifeTime(BulletLifeTime);
            bullet.SetSize(BulletSize);
            bullet.SetThroughCount(BulletThroughCount);
            
            // 计算当前子弹的角度
            float currentAngle = startAngle + angleStep * i;
            Vector2 bulletDirection = Quaternion.Euler(0, 0, currentAngle) * baseDirection;
            
            bullet.SetMotion(transform.position, bulletDirection, BulletSpeed);
        }
    }

    // 动态计算散射角度
    private float CalculateDynamicSpreadAngle()
    {
        if (BulletFireCount <= 1) return baseSpreadAngle;
        
        // 根据子弹数量计算散射角度
        float t = Mathf.Clamp01((BulletFireCount - 1) / 10f); // 假设10发子弹达到最大散射
        float spreadAngle = Mathf.Lerp(baseSpreadAngle, maxSpreadAngle, t * spreadGrowthRate);
        
        return spreadAngle;
    }
}
