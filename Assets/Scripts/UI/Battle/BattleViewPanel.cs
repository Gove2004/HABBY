

using System.ComponentModel;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class BattleViewPanel : ViewPanel<BattleViewModel>
{
    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

        ViewModel.playerCharacter.OnExpChanged += (v) => OnPlayerLevelExpChanged(false);
        ViewModel.playerCharacter.OnLevelUp += (v) => OnPlayerLevelExpChanged(true);
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
        else
        {
            TMPTexts["Exp"].text = $"{ViewModel.playerCharacter.Exp } / {ViewModel.playerCharacter.NextLevelExpThreshold} EXP";
            Images["LevelExpBar"].fillAmount = ViewModel.playerCharacter.ExpProgress;
        }
        
    }


}
