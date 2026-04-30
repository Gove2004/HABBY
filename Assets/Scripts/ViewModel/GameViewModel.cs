using GoveKits.Runtime.Core;
using GoveKits.Runtime.Storage;
using GoveKits.Runtime.UI;


public class GameViewModel : ViewModel
{
    #region LifeCycle

    public void Initialize()
    {
        LogCore.Log("GameViewModel Initialized");

        MaxScore = PrefsCore.GetInt(MaxScoreKey, 0);
        NowScore = PrefsCore.GetInt(NowScoreKey, 0);
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

}
