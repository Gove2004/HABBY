using DG.Tweening;
using GoveKits.Runtime.Core;
using TMPro;
using UnityEngine;

public class FloatText : MonoBehaviour, IPoolable
{
    private TextMeshProUGUI textMesh;
    private RectTransform rectTransform;
    private Sequence floatSequence;

    [Header("动画参数")]
    [SerializeField] private float startScale = 5f;      // 起始缩放
    [SerializeField] private float shrinkDuration = 0.15f; // 快速缩小时间
    [SerializeField] private float floatDuration = 0.8f;   // 上升持续时间
    [SerializeField] private float floatHeight = 1.5f;     // 上升高度
    [SerializeField] private Ease shrinkEase = Ease.OutBack; // 缩小缓动
    [SerializeField] private Ease floatEase = Ease.Linear;   // 上升缓动

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Setup(string text, Vector3 position, Color color)
    {
        // 重置状态
        rectTransform.localScale = Vector3.one * startScale;
        textMesh.color = new Color(color.r, color.g, color.b, 1f);
        transform.position = position + Vector3.up * 0.5f; // 初始位置稍微偏上
        // 额外水平随机偏移，增加一些散乱感
        transform.position += Vector3.right * Random.Range(-0.5f, 0.5f); // 水平随机偏移

        textMesh.text = text;

        // 创建动画序列
        floatSequence = DOTween.Sequence();
        
        // 1. 快速缩小阶段
        floatSequence.Append(
            rectTransform.DOScale(Vector3.one, shrinkDuration)
                .SetEase(shrinkEase)
        );
        
        // 2. 同时开始缓慢上升和淡出
        floatSequence.Join(
            transform.DOMoveY(position.y + floatHeight, floatDuration)
                .SetEase(floatEase)
                .SetDelay(shrinkDuration * 0.5f) // 稍微延迟开始上升
        );
        
        floatSequence.Join(
            textMesh.DOFade(0, floatDuration * 0.7f)
                .SetEase(Ease.InQuad)
                .SetDelay(shrinkDuration * 0.8f) // 稍晚开始淡出
        );

        // 总时长控制
        floatSequence.OnComplete(() => PoolCore.Return(this.gameObject));
    }

    public void OnRecycle()
    {
        if (floatSequence != null)
        {
            floatSequence.Kill();
            floatSequence = null;
        }
        // 重置状态
        rectTransform.localScale = Vector3.one;
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f);
    }
}