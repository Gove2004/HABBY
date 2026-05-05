using System.ComponentModel;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class SelectViewPanel : ViewPanel<SelectViewModel>
{
    public GameObject heroInfoItemPrefab;
    public Transform heroInfoItemContainer;


    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

        UpdateHeroDisplay(ViewModel.SelectedHero);
    }



    private long lastScore = 0;
    public void Update()
    {
        long nowScore = VMContainer.Get<GameViewModel>().NowScore;
        if (lastScore != nowScore)
        {
            TMPTexts["NowScore"].GetComponent<RollTMP>().RollTMPText("金币: ", lastScore, nowScore, 3f);
            lastScore = nowScore;
        }
    }



    protected override void OnDataChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.SelectedHero))
        {
            UpdateHeroDisplay(ViewModel.SelectedHero);
        }
        if (e.PropertyName == nameof(ViewModel.MultiplayerChenged))
        {
            TMPTexts["NowMultiplier"].text = $"得分:{ViewModel.ScoreMultiplier:0.0}x    难度:{ViewModel.difficultyMultiplier:0.0}x";
        }
        
    }

    private void UpdateHeroDisplay(HeroType heroType)
    {
        switch (heroType)
        {
            case HeroType.Actor:
                TMPTexts["HeroName"].text = "射手座";
                break;
            case HeroType.Warrior:
                TMPTexts["HeroName"].text = "狂战士";
                break;
            case HeroType.Wizard:
                TMPTexts["HeroName"].text = "法术师";
                break;
        }
        HeroInfoItemData[] heroInfoItems = ViewModel.GetHeroInfoItems();
        foreach (Transform child in heroInfoItemContainer)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < heroInfoItems.Length; i++)
        {
            GameObject item = Instantiate(heroInfoItemPrefab, heroInfoItemContainer);
            HeroInfoItem heroInfoItem = item.GetComponent<HeroInfoItem>();
            heroInfoItem.SetData(heroInfoItems[i]);
            heroInfoItem.OnClick += () => UpdateHeroDisplay(heroType);  // 点击属性项时刷新显示，确保价格和按钮状态更新
        }
    }




    protected override void OnButtonClicked(string btnName)
    {
        switch (btnName)
        {
            case "Back":
                AudioManager.Instance.PlayUIClick();
                OnBackBtnClicked();
                break;
            case "Last":
                AudioManager.Instance.PlayUIClick();
                OnLastOrNextBtnClicked(false);
                break;
            case "Next":
                AudioManager.Instance.PlayUIClick();
                OnLastOrNextBtnClicked(true);
                break;
            case "Start":
                AudioManager.Instance.PlayUIClick();
                OnStartBtnClicked();
                break;
        }
    }

    private void OnBackBtnClicked()
    {
        ViewModel.BackToMainMenu();
    }

    private void OnLastOrNextBtnClicked(bool isNext)
    {
        ViewModel.LastOrNextHero(isNext);
    }

    private void OnStartBtnClicked()
    {
        ViewModel.StartBattle();
    }


    protected override void OnToggleChanged(string togName, bool val)
    {
        switch (togName)
        {
            case "0.5":
                AudioManager.Instance.PlaySelect();
                ViewModel.UpdateMultiplier(0.5f, val);
                break;
            case "1.0":
                AudioManager.Instance.PlaySelect();
                ViewModel.UpdateMultiplier(1.0f, val);
                break;
            case "1.5":
                AudioManager.Instance.PlaySelect();
                ViewModel.UpdateMultiplier(1.5f, val);
                break;
            case "2.0":
                AudioManager.Instance.PlaySelect();
                ViewModel.UpdateMultiplier(2.0f, val);
                break;
            case "MoreHP":
                AudioManager.Instance.PlaySelect();
                ViewModel.SetMoreHP(val);
                break;
            case "MoreDamage":
                AudioManager.Instance.PlaySelect();
                ViewModel.SetMoreDamage(val);
                break;
            case "Nothing":
                AudioManager.Instance.PlaySelect();
                ViewModel.UpdateMultiplier(0.1f, val);
                break;
            case "T":
                AudioManager.Instance.PlaySelect();
                ViewModel.UpdateMultiplier(5f, val);
                break;
        }
    }

    
}



