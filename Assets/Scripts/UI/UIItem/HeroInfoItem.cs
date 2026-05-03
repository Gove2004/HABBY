using System;
using GoveKits.Runtime.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private Button buyBtn;
    [SerializeField] private TextMeshProUGUI priceTMP;
    public event Action OnClick;

    public void SetData(HeroInfoItemData data)
    {
        nameTMP.text = $"{data.Key}: {VMContainer.Get<GameViewModel>().GetNowAttribute(HeroType.Actor, data.Key)}";
        priceTMP.text = $"{VMContainer.Get<GameViewModel>().GetNowPrice(HeroType.Actor, data.Key)}  ";
        buyBtn.interactable = VMContainer.Get<GameViewModel>().CanUpgradeAttribute(HeroType.Actor, data.Key);
        buyBtn.onClick.RemoveAllListeners();
        buyBtn.onClick.AddListener(
            () =>
            {
                VMContainer.Get<GameViewModel>().UpgradeAttribute(HeroType.Actor, data.Key);
                OnClick?.Invoke();  // 触发点击事件，通知外部刷新显示
            }
        );
    }
}




public class HeroInfoItemData
{
    public string Key;


    public HeroInfoItemData(string key)
    {
        Key = key;
    }
}