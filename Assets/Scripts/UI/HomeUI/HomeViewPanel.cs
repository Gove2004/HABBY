using System.ComponentModel;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.Storage;
using GoveKits.Runtime.UI;
using UnityEngine;

public class HomeViewPanel : ViewPanel<HomeViewModel>
{
    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

        
        // 复位摄像机
        CameraManager.Instance.Reset();

        // 将 BGM 音量设置为0.5
        AudioCore.SetVolume(AudioChannel.BGM, 0.5f);

        AudioManager.Instance.PlayMainBGM();

        PlayShowAnimation();
    }


    private void PlayShowAnimation()
    {
        TMPTexts["MaxScore"].GetComponent<RollTMP>().RollTMPText("最高纪录: ", 0, int.Parse(VMContainer.Get<GameViewModel>().MaxScore.ToString()), 5f);
        TMPTexts["NowScore"].GetComponent<RollTMP>().RollTMPText("金币: ", 0, int.Parse(VMContainer.Get<GameViewModel>().NowScore.ToString()), 5f);

    }



    protected override void OnDataChanged(object sender, PropertyChangedEventArgs e)
    {
        
    }


    protected override void OnButtonClicked(string btnName)
    {
        switch (btnName)
        {
            case "Start":
                AudioManager.Instance.PlayUIClick();
                OnStartBtnClicked();
                break;
            case "Book":
                AudioManager.Instance.PlayUIClick();
                OnBookBtnClicked();
                break;
            // case "Setting":
            //     AudioManager.Instance.PlayUIClick();
            //     OnSettingBtnClicked();
            //     break;
            case "Exit":
                AudioManager.Instance.PlayUIClick();
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
    
    // private void OnSettingBtnClicked()
    // {
    //     LogCore.Log("Setting Btn Clicked");
    // }

    private void OnExitBtnClicked()
    {
        VMContainer.Get<GameViewModel>().ExitGame();
    }


}
