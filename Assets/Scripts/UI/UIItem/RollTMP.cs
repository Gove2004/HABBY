using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RollTMP : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private Tween currentTween;
    
    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }
    
    /// <summary>
    /// 数字滚动效果
    /// </summary>
    /// <param name="before">前缀文本</param>
    /// <param name="from">起始数字</param>
    /// <param name="to">目标数字</param>
    /// <param name="duration">持续时间</param>
    public void RollTMPText(string before, long from, long to, float duration)
    {
        // 停止当前正在进行的动画
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }
        
        long currentValue = from;
        
        // 立即显示起始值
        tmp.text = $"{before}{currentValue}";
        
        // 创建数字滚动动画
        currentTween = DOTween.To(() => currentValue, x =>
        {
            currentValue = x;
            tmp.text = $"{before}{currentValue}";
        }, to, duration)
        .SetEase(Ease.OutExpo) // 使用指数缓动，开始快结束慢
        .OnComplete(() =>
        {
            // 确保最终值正确显示
            tmp.text = $"{before}{to}";
            currentTween = null;
        });
    }
    
    /// <summary>
    /// 重载方法：不带前缀的数字滚动
    /// </summary>
    public void RollTMPText(int from, int to, float duration)
    {
        RollTMPText("", from, to, duration);
    }
    
    /// <summary>
    /// 重载方法：使用默认持续时间
    /// </summary>
    public void RollTMPText(string before, int from, int to)
    {
        RollTMPText(before, from, to, 1.5f);
    }
    
    /// <summary>
    /// 立即停止当前动画并设置最终值
    /// </summary>
    public void StopAndSetFinalValue(string before, int finalValue)
    {
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
            currentTween = null;
        }
        tmp.text = $"{before}{finalValue}";
    }
    
    private void OnDestroy()
    {
        // 清理动画
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }
    }
}