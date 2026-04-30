


using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using Unity.Collections;


public class SelectViewModel : ViewModel
{
    private int heroCount = System.Enum.GetValues(typeof(HeroType)).Length;

    private HeroType _selectedHero = HeroType.Actor;
    public HeroType SelectedHero
    {
        get => _selectedHero;
        set => SetProperty(ref _selectedHero, value);
    }

    public void LastOrNextHero(bool isNext)
    {
        int currentIndex = (int)SelectedHero;

        if (isNext)
        {
            currentIndex = (currentIndex + 1) % heroCount;
        }
        else
        {
            currentIndex = (currentIndex - 1 + heroCount) % heroCount;
        }

        SelectedHero = (HeroType)currentIndex;
    }

    public void StartBattle()
    {
        SceneCore.Load("Battle", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        SceneCore.UnloadAsync("Select");

        VMContainer.Get<BattleViewModel>().StartBattle(SelectedHero);
    }

    public void BackToMainMenu()
    {
        SceneCore.Load("Home", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        SceneCore.UnloadAsync("Select");
    }
}
