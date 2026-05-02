using System.ComponentModel;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class HomeViewPanel : ViewPanel<HomeViewModel>
{
    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

        TMPTexts["MaxScore"].text = $"单局最高: {VMContainer.Get<GameViewModel>().MaxScore}\t";
        TMPTexts["NowScore"].text = $"剩余金币: {VMContainer.Get<GameViewModel>().NowScore}\t";
    }






    protected override void OnDataChanged(object sender, PropertyChangedEventArgs e)
    {
        
    }


    protected override void OnButtonClicked(string btnName)
    {
        switch (btnName)
        {
            case "Start":
                OnStartBtnClicked();
                break;
            case "Book":
                OnBookBtnClicked();
                break;
            case "Setting":
                OnSettingBtnClicked();
                break;
            case "Exit":
                OnExitBtnClicked();
                break;
        }
    }


    private void OnStartBtnClicked()
    {
        ViewModel.StartGame();
    }

    private void OnBookBtnClicked()
    {
        Controller.Show<BookViewPanel>();
    }
    
    private void OnSettingBtnClicked()
    {
        LogCore.Log("Setting Btn Clicked");
    }

    private void OnExitBtnClicked()
    {
        VMContainer.Get<GameViewModel>().ExitGame();
    }


}
