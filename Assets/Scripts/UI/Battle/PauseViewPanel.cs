using System.ComponentModel;
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
                Controller.HidePopup<PauseViewPanel>();
                break;
            case "Quit":
                Controller.HidePopup<PauseViewPanel>();
                VMContainer.Get<BattleViewModel>().ClearBattle();

                SceneCore.Load("Home", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                SceneCore.UnloadAsync("Battle");
                break;
        }
    }
}
