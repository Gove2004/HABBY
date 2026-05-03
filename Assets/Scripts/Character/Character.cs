using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // Level Component Fields
    protected int level;
    protected int exp;
    protected int nextLevelExpThreshold;
    public int Level => level;
    public int Exp => exp;
    public int NextLevelExpThreshold => nextLevelExpThreshold;
    public float ExpProgress => nextLevelExpThreshold > 0 ? (float)exp / nextLevelExpThreshold : 0f;
    public event Action<int> OnLevelUp;
    protected void TriggerLevelUp() => OnLevelUp?.Invoke(level);
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
    protected HPBar hpBar;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        hpBar = GetComponentInChildren<HPBar>();
        
    }

    public abstract void Setup(int initialLevel);

    protected virtual void Update()
    {
    }

    public virtual void TakeDamageFrom(int damage, Vector2 fromPosition)
    {
        int actualDamage = Mathf.Max(damage - DefensePower, 0);
        TakeDamage(actualDamage);
        
        // Knockback
        Vector2 knockbackDirection = (transform.position - (Vector3)fromPosition).normalized;
        float knockbackDistance = 1f; // Adjust as needed
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

