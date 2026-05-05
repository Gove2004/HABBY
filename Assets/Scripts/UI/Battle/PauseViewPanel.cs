using System;
using System.ComponentModel;
using DG.Tweening;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class PauseViewPanel : ViewPanel<PauseViewModel>
{
    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

        TMPTexts["Player"].text = ViewModel.GetPlayerInfo();
        TMPTexts["Buff"].text = ViewModel.GetBuffInfo();
        TMPTexts["Level"].text = ViewModel.GetLevelInfo();
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
            case "Resume":
                AudioManager.Instance.PlayUIClick();
                
                Controller.HidePopup<PauseViewPanel>();
                break;
            case "Quit":
                AudioManager.Instance.PlayUIClick();

                Controller.HidePopup<PauseViewPanel>();
                VMContainer.Get<BattleViewModel>().ClearBattle();

                SceneCore.Load("Home", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                SceneCore.UnloadAsync("Battle");
                break;
        }
    }
}
