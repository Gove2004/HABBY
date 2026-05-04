using GoveKits.Runtime.Core;
using GoveKits.Runtime.Storage;
using GoveKits.Runtime.UI;


public class GameViewModel : ViewModel
{
    #region LifeCycle

    public void Initialize()
    {
        long.TryParse(PrefsCore.GetString(MaxScoreKey, "0"), out long maxScore);
        MaxScore = maxScore;

        long.TryParse(PrefsCore.GetString(NowScoreKey, "0"), out long nowScore);
        NowScore = nowScore;

        // NowScore = 1000;  // 测试用，正式发布前请删除
        // LogCore.Log($"注入测试数据: NowScore = {NowScore}");
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
    public long MaxScore { get; private set; }
    public long NowScore { get; private set; }
    
    public void UpdateMaxScore(long score)
    {
        NowScore += score;
        PrefsCore.SetString(NowScoreKey, NowScore.ToString());
        if (score > MaxScore)
        {
            MaxScore = score;
            PrefsCore.SetString(MaxScoreKey, MaxScore.ToString());
        }
        PrefsCore.Save();
    }

    public bool TrySpendcore(long score)
    {
        if (NowScore >= score)
        {
            NowScore -= score;
            PrefsCore.SetString(NowScoreKey, NowScore.ToString());
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

    public long GetNowPrice(HeroType heroType, string attribute)
    {
        int level = PrefsCore.GetInt($"{heroType}_{attribute}_Level", 0);
        return HeroInfoConfig.ActorInfo[attribute][level].price;
    }

    public bool CanUpgradeAttribute(HeroType heroType, string attribute)
    {
        int level = PrefsCore.GetInt($"{heroType}_{attribute}_Level", 0);
        if (level >= HeroInfoConfig.ActorInfo[attribute].Count - 1)
            return false;
        long price = HeroInfoConfig.ActorInfo[attribute][level].price;
        return NowScore >= price;
    }

    public void UpgradeAttribute(HeroType heroType, string attribute)
    {
        if (!CanUpgradeAttribute(heroType, attribute))
            return;

        int level = PrefsCore.GetInt($"{heroType}_{attribute}_Level", 0);
        long price = HeroInfoConfig.ActorInfo[attribute][level].price;
        if (TrySpendcore(price))
        {
            PrefsCore.SetInt($"{heroType}_{attribute}_Level", level + 1);
            PrefsCore.Save();
        }
    }

    #endregion

}
