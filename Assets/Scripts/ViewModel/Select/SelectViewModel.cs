


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
        // 清理当前选中态
        ScoreMultiplier = 1f;
        difficultyMultiplier = 1f;
        isMoreHP = false;
        isMoreDamage = false;
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
            new HeroInfoItemData(PlayerAttributeType.MaxHP),
            new HeroInfoItemData(PlayerAttributeType.AttackPower),
            new HeroInfoItemData(PlayerAttributeType.DefensePower),
            new HeroInfoItemData(PlayerAttributeType.CritRate, true),
            new HeroInfoItemData(PlayerAttributeType.CritDamageMultiplier, true),
            new HeroInfoItemData(PlayerAttributeType.MoveSpeed, true),
            new HeroInfoItemData(PlayerAttributeType.AttackSpeed, true),

            new HeroInfoItemData(PlayerAttributeType.BulletSpeed, true),
            new HeroInfoItemData(PlayerAttributeType.BulletSize, true),
            new HeroInfoItemData(PlayerAttributeType.BulletLife, true),
            new HeroInfoItemData(PlayerAttributeType.BulletCount),
            new HeroInfoItemData(PlayerAttributeType.BulletPierce),

            new HeroInfoItemData(PlayerAttributeType.ExpRate, true),
            new HeroInfoItemData(PlayerAttributeType.GoldRate, true),
            new HeroInfoItemData(PlayerAttributeType.RefreshTime),
            new HeroInfoItemData(PlayerAttributeType.HealPerFives),

            new HeroInfoItemData(PlayerAttributeType.MoreHPFurits),
            new HeroInfoItemData(PlayerAttributeType.MoreATKFurits),
            new HeroInfoItemData(PlayerAttributeType.MoreDEFFurits),
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
