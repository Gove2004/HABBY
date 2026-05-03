using GoveKits.Runtime.Core;
using GoveKits.Runtime.Storage;
using GoveKits.Runtime.UI;


public class GameViewModel : ViewModel
{
    #region LifeCycle

    public void Initialize()
    {
        MaxScore = PrefsCore.GetInt(MaxScoreKey, 0);
        NowScore = PrefsCore.GetInt(NowScoreKey, 0);

        NowScore = 1000;  // 测试用，正式发布前请删除
        LogCore.Log($"注入测试数据: NowScore = {NowScore}");
    }

    public void ExitGame()
    {
        LogCore.Log("Exit Game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion


    #region MaxScore
    
    public const string MaxScoreKey = "MaxScore";
    public const string NowScoreKey = "NowScore";
    public int MaxScore { get; private set; }
    public int NowScore { get; private set; }
    
    public void UpdateMaxScore(int score)
    {
        NowScore += score;
        PrefsCore.SetInt(NowScoreKey, NowScore);
        if (score > MaxScore)
        {
            MaxScore = score;
            PrefsCore.SetInt(MaxScoreKey, MaxScore);
        }
        PrefsCore.Save();
    }

    public bool TrySpendcore(int score)
    {
        if (NowScore >= score)
        {
            NowScore -= score;
            PrefsCore.SetInt(NowScoreKey, NowScore);
            PrefsCore.Save();
            return true;
        }
        return false;
    }

    #endregion



    #region Save Data

    public float GetNowAttribute(HeroType heroType, string attribute)
    {
        int level = PrefsCore.GetInt($"{heroType}_{attribute}_Level", 0);
        return HeroInfoConfig.ActorInfo[attribute][level].value;
    }

    public int GetNowPrice(HeroType heroType, string attribute)
    {
        int level = PrefsCore.GetInt($"{heroType}_{attribute}_Level", 0);
        return HeroInfoConfig.ActorInfo[attribute][level].price;
    }

    public bool CanUpgradeAttribute(HeroType heroType, string attribute)
    {
        int level = PrefsCore.GetInt($"{heroType}_{attribute}_Level", 0);
        if (level >= HeroInfoConfig.ActorInfo[attribute].Count - 1)
            return false;
        int price = HeroInfoConfig.ActorInfo[attribute][level].price;
        return NowScore >= price;
    }

    public void UpgradeAttribute(HeroType heroType, string attribute)
    {
        if (!CanUpgradeAttribute(heroType, attribute))
            return;

        int level = PrefsCore.GetInt($"{heroType}_{attribute}_Level", 0);
        int price = HeroInfoConfig.ActorInfo[attribute][level].price;
        if (TrySpendcore(price))
        {
            PrefsCore.SetInt($"{heroType}_{attribute}_Level", level + 1);
            PrefsCore.Save();
        }
    }

    #endregion

}
