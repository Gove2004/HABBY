using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // HP Component Fields
    public int MaxHP;
    public int CurrentHP;
    public float HPPercentage => MaxHP > 0 ? (float)CurrentHP / MaxHP : 0f;
    public event Action<int> OnHPChanged;
    public event Action OnDeath;

    // Attack Component Fields
    public int AttackPower;

    // Defense Component Fields
    public int DefensePower;

    // Move Component Fields
    public float MoveSpeed;

    public Dictionary<string, int> BuffCounters = new Dictionary<string, int>();

    protected SpriteRenderer sr;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        if (sr == null) sr = GetComponent<SpriteRenderer>();
    }

    public virtual void Setup(int initialLevel)
    {
        // HP Init
        MaxHP = 3 * initialLevel;
        CurrentHP = MaxHP;

        // Attack Init
        AttackPower = Mathf.FloorToInt(MathF.Sqrt(initialLevel));

        // Defense Init
        DefensePower = Mathf.FloorToInt(MathF.Sqrt(initialLevel) / 2);

        // Move Init
        MoveSpeed = 2.5f;
    }

    protected virtual void Update()
    {
    }

    public virtual void TakeDamageFrom(int damage, Vector2 fromPosition)
    {
        int actualDamage = Mathf.Max(damage - DefensePower, 0);
        TakeDamage(actualDamage);
        
        // Knockback
        Vector2 knockbackDirection = (transform.position - (Vector3)fromPosition).normalized;
        float knockbackDistance = 0.5f; // Adjust as needed
        transform.DOMove((Vector2)transform.position + knockbackDirection * knockbackDistance, 0.2f);
    }

    // --- Methods ---

    // HP
    public void TakeDamage(int amount, bool isCritical = false)
    {
        CurrentHP -= amount;
        if (CurrentHP < 0) CurrentHP = 0;
        OnHPChanged?.Invoke(CurrentHP);
        
        if (sr != null)
        {
            // 闪红
            Color originalColor = sr.color;
            sr.DOColor(Color.red, 0.1f).OnComplete(() => sr.DOColor(originalColor, 0.1f));
        }

        if (isCritical)
        {
            // 显示暴击文本
            FloatTextManager.Instance.Show(amount.ToString() + "!", transform.position, Color.yellow);
        }
        else
        {
            // 显示伤害文本
            FloatTextManager.Instance.Show(amount.ToString(), transform.position, Color.red);
        }

        if (CurrentHP == 0) OnDeath?.Invoke();
    }

    public void Heal(int amount)
    {
        CurrentHP += amount;
        if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        OnHPChanged?.Invoke(CurrentHP);

        if (sr != null)
        {
            // 闪绿
            Color originalColor = sr.color;
            sr.DOColor(Color.green, 0.1f).OnComplete(() => sr.DOColor(originalColor, 0.1f));
        }

        // 显示治疗文本
        FloatTextManager.Instance.Show(amount.ToString(), transform.position, Color.green);
    }
}

