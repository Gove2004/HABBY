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



    public void Update()
    {
        TMPTexts["NowScore"].text = $"金币: {VMContainer.Get<GameViewModel>().NowScore}";
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
        heroInfoItemContainer.DetachChildren();
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
                OnBackBtnClicked();
                break;
            case "Last":
                OnLastOrNextBtnClicked(false);
                break;
            case "Next":
                OnLastOrNextBtnClicked(true);
                break;
            case "Start":
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
        float scoreMultiplier = ViewModel.ScoreMultiplier;
        float multiplier = ViewModel.difficultyMultiplier;
        switch (togName)
        {
            case "0.5":
                ViewModel.UpdateMultiplier(0.5f, val);
                break;
            case "1.0":
                ViewModel.UpdateMultiplier(1.0f, val);
                break;
            case "1.5":
                ViewModel.UpdateMultiplier(1.5f, val);
                break;
            case "2.0":
                ViewModel.UpdateMultiplier(2.0f, val);
                break;
            case "MoreHP":
                ViewModel.SetMoreHP(val);
                break;
            case "MoreDamage":
                ViewModel.SetMoreDamage(val);
                break;
            case "Nothing":
                ViewModel.UpdateMultiplier(0.1f, val);
                break;
            case "T":
                ViewModel.UpdateMultiplier(10f, val);
                break;
        }
    }

    
}



