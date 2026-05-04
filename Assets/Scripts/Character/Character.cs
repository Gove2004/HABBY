using System;
using System.Collections.Generic;
using DG.Tweening;
using GoveKits.Runtime.Core;
using UnityEngine;

public abstract class Character : MonoBehaviour, IPoolable
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
    protected Color baseColor = Color.white;
    private Tween colorTween;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        if (sr != null) baseColor = sr.color;
        hpBar = GetComponentInChildren<HPBar>();
        
    }

    public abstract void Setup(int initialLevel);

    protected virtual void Update()
    {
    }

    public virtual void TakeDamageFrom(int damage, Vector2 fromPosition, bool isCritical = false)
    {
        int actualDamage = Mathf.Max(damage - DefensePower, 0);
        TakeDamage(actualDamage, isCritical);
        
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
            // 闪红前停止旧的补间并重置颜色
            colorTween?.Kill();
            sr.color = baseColor;
            colorTween = sr.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo);
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
            // 闪绿前停止旧的补间并重置颜色
            colorTween?.Kill();
            sr.color = baseColor;
            colorTween = sr.DOColor(Color.green, 0.1f).SetLoops(2, LoopType.Yoyo);
        }

        // 显示治疗文本
        FloatTextManager.Instance.Show(amount.ToString(), transform.position, Color.green);
    }

    public virtual void OnRecycle()
    {
        // 重置HP
        CurrentHP = MaxHP;
        OnHPChanged?.Invoke(CurrentHP);
        
        // 重置颜色
        if (sr != null)
        {
            colorTween?.Kill();
            sr.color = baseColor;
        }

        // 其他重置逻辑（如Buff等）可以在子类中实现
    }

}

