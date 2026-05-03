


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

        VMContainer.Get<BattleViewModel>().StartBattle(SelectedHero, ScoreMultiplier, difficultyMultiplier, isMoreHP, isMoreDamage);
    }

    public void BackToMainMenu()
    {
        SceneCore.Load("Home", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        SceneCore.UnloadAsync("Select");
    }


    public HeroInfoItemData[] GetHeroInfoItems()
    {
        return new HeroInfoItemData[]
        {
            new HeroInfoItemData("生命"),
            new HeroInfoItemData("攻击"),
            new HeroInfoItemData("防御"),
            new HeroInfoItemData("暴击"),
            new HeroInfoItemData("暴伤"),
            new HeroInfoItemData("攻速"),
        };
    }




    private bool _multiplayerChenged = false;
    public bool MultiplayerChenged
    {
        get => _multiplayerChenged;
        set => SetProperty(ref _multiplayerChenged, value);
    }
    public float ScoreMultiplier { get; private set; } = 1f;
    public float difficultyMultiplier { get; private set; } = 1f;
    public bool isMoreHP { get; private set; } = false;
    public bool isMoreDamage { get; private set; } = false;

    public void UpdateMultiplier(float multiplier, bool val)
    {
        if (val)
        {
            ScoreMultiplier *= multiplier;
            difficultyMultiplier *= multiplier;
        }
        else
        {
            ScoreMultiplier /= multiplier;
            difficultyMultiplier /= multiplier;
        }
        MultiplayerChenged = !MultiplayerChenged;  // 切换状态以触发界面更新
    }

    public void SetMoreHP(bool val)
    {
        isMoreHP = val;
        if (val)
        {
            ScoreMultiplier *= 2f;
        }
        else
        {
            ScoreMultiplier /= 2f;
        }
        MultiplayerChenged = !MultiplayerChenged;  // 切换状态以触发界面更新
    }

    public void SetMoreDamage(bool val)
    {
        isMoreDamage = val;
        if (val)
        {
            ScoreMultiplier *= 2f;
        }
        else
        {
            ScoreMultiplier /= 2f;
        }
        MultiplayerChenged = !MultiplayerChenged;  // 切换状态以触发界面更新
    }
}
