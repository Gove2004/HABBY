using System;
using System.ComponentModel;
using DG.Tweening;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class OverViewPanel : ViewPanel<OverViewModel>
{
    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

        TMPTexts["Context"].text = GetFinalContext();

        Time.timeScale = 0f; // 暂停游戏
        ShowAnimation();
    }


    private void ShowAnimation()
    {
        transform.DOScale(Vector3.one, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public override void OnHide()
    {
        base.OnHide();

        Time.timeScale = 1f; // 恢复游戏
    }


    protected override void OnDataChanged(object sender, PropertyChangedEventArgs e)
    {
        
    }


    protected override void OnButtonClicked(string btnName)
    {
        switch (btnName)
        {
            case "OK":
                AudioManager.Instance.PlayUIClick();
                Controller.HidePopup<OverViewPanel>();
                VMContainer.Get<BattleViewModel>().ClearBattle();
                
                SceneCore.Load("Home", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                SceneCore.UnloadAsync("Battle");
                break;
        }
    }


    private string GetFinalContext()
    {
        var battleVM = VMContainer.Get<BattleViewModel>();
        return $"你迷失在黑暗中...\n\n" +
               $"在刚才的 <color=#00ffff>{FormatTime(battleVM.BattleTime)}</color> 时间里\n" +
               $"你击败了 <color=red>{battleVM.KillCount}</color> 个敌人\n" +
               $"升到了 <color=green>{battleVM.playerCharacter.Level}</color> 级\n" +
               $"最终得分: <color=blue>{battleVM.Score}</color>\n" + 
               $"获得金币: <color=yellow>{battleVM.Score}</color> 金币";
    }


    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }
}
