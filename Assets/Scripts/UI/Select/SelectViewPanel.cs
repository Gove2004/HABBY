using System.ComponentModel;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class SelectViewPanel : ViewPanel<SelectViewModel>
{
    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

        TMPTexts["NowScore"].text = $"金币: {VMContainer.Get<GameViewModel>().NowScore}";

        UpdateHeroDisplay(ViewModel.SelectedHero);
    }




    protected override void OnDataChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.SelectedHero))
        {
            UpdateHeroDisplay(ViewModel.SelectedHero);
        }
        
    }

    private void UpdateHeroDisplay(HeroType heroType)
    {
        switch (heroType)
        {
            case HeroType.Actor:
                TMPTexts["HeroName"].text = "射手";
                break;
            case HeroType.Warrior:
                TMPTexts["HeroName"].text = "战士";
                break;
            case HeroType.Wizard:
                TMPTexts["HeroName"].text = "法师";
                break;
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
}



