using System.ComponentModel;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class OverViewPanel : ViewPanel<OverViewModel>
{
    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

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
            case "OK":
                Controller.HidePopup<OverViewPanel>();
                VMContainer.Get<BattleViewModel>().ClearBattle();
                
                SceneCore.Load("Home", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                SceneCore.UnloadAsync("Battle");
                break;
        }
    }
}
