using UnityEngine;

public class MyAttribute
{
    public float BaseValue { get; private set; }
    public float AddValue { get; private set; }
    public float PercentValue { get; private set; }

    // 计算最终属性值
    public float Value;

    public MyAttribute(float baseValue)
    {
        BaseValue = baseValue;
        AddValue = 0;
        PercentValue = 0;

        UpdateValue();
    }

    public void SetBase(float baseValue)
    {
        BaseValue = baseValue;

        UpdateValue();
    }

    public void AddFlat(float amount)
    {
        AddValue += amount;

        UpdateValue();
    }

    public void AddPercent(float percent)
    {
        PercentValue += percent;

        UpdateValue();
    }

    private void UpdateValue()
    {
        Value = Mathf.RoundToInt((BaseValue + AddValue) * (1 + PercentValue));
    }
}