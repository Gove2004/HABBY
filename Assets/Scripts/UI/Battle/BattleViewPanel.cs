

using System.ComponentModel;
using DG.Tweening;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class BattleViewPanel : ViewPanel<BattleViewModel>
{
    public CanvasGroup TipCanvasGroup;


    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

        ViewModel.playerCharacter.OnExpChanged += (v) => OnPlayerLevelExpChanged(false);
        ViewModel.playerCharacter.OnLevelUp += (v) => OnPlayerLevelExpChanged(true);

        UpdateTMPAnimation();

        AudioManager.Instance.PlayBattleBGM();

        CameraManager.Instance.StartBattleCameraEffect();

        // 显示提示文本，并在5秒后淡出
        TipCanvasGroup.gameObject.SetActive(true);
        TipCanvasGroup.DOFade(0f, 5f).From(1f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() => {
            TipCanvasGroup.gameObject.SetActive(false);
        });
    }
    
    private long lastScore = -1;
    private void UpdateTMPAnimation()
    {
        long nowScore = ViewModel.Score;
        if (lastScore != nowScore)
        {
            TMPTexts["Score"].GetComponent<RollTMP>().RollTMPText("得分: ", lastScore, nowScore, 1f);
            lastScore = nowScore;
        }
        TMPTexts["MaxScore"].text = $"最高纪录: {ViewModel.MaxScore}";
    }

    public override void OnHide()
    {
        base.OnHide();
    }

    protected override void OnDataChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ViewModel.BattleTime):
                TMPTexts["BattleTime"].text = $"{FormatTime(ViewModel.BattleTime)}";
                break;
            case nameof(ViewModel.Score):
                UpdateTMPAnimation();
                break;
            case nameof(ViewModel.IsBattleActive):
                if (!ViewModel.IsBattleActive)  // 战斗结束，显示结算界面
                {
                    Controller.Show<OverViewPanel>();
                }
                break;
        }
    }


    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }


    protected override void OnButtonClicked(string btnName)
    {
        switch (btnName)
        {
            case "Pause":
                AudioManager.Instance.PlayUIClick();
                Controller.Show<PauseViewPanel>();
                break;
        }
    }





    private void Update()
    {
        ViewModel.UpdateBattle(Time.deltaTime);
    }


    private void OnPlayerLevelExpChanged(bool isLevelUp)
    {
        if (isLevelUp)
        {
            // 显示升级特效
            TMPTexts["Level"].text = $"Level {ViewModel.playerCharacter.Level}";

            Controller.Show<ChooseViewPanel>();
        }
        TMPTexts["Exp"].text = $"{ViewModel.playerCharacter.Exp } / {ViewModel.playerCharacter.NextLevelExpThreshold} EXP";
        Images["LevelExpBar"].fillAmount = ViewModel.playerCharacter.ExpProgress;
    }


}
