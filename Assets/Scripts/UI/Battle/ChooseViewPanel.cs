


using GoveKits.Runtime.UI;
using UnityEngine;

public class ChooseViewPanel : ViewPanel<ChooseViewModel>
{
    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

        Refresh();


        Time.timeScale = 0f; // 暂停游戏
    }


    public override void OnHide()
    {
        base.OnHide();

        Time.timeScale = 1f; // 恢复游戏
    }



    protected override void OnDataChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        
    }

    protected override void OnButtonClicked(string btnName)
    {
        switch (btnName)
        {
            case "1":
                onChoose(1);
                break;
            case "2":
                onChoose(2);
                break;
            case "3":
                onChoose(3);
                break;
            case "Refresh":
                Refresh(false);
                break;

        }
    }

    
    private void onChoose(int index)
    {
        ViewModel.Choose(index);
        Controller.HidePopup<ChooseViewPanel>();
    }


    private void Refresh(bool isFree = true)
    {
        if (!isFree)
        {
            if (VMContainer.Get<BattleViewModel>().RefeshCount <= 0)
            {
                return;
            }
            VMContainer.Get<BattleViewModel>().ChangeRefreshCount(-1);
        }
        ViewModel.Refresh();

        TMPTexts["1Name"].text = ViewModel.chooseBuff1.Name;
        TMPTexts["1Description"].text = ViewModel.chooseBuff1.Description;
        TMPTexts["2Name"].text = ViewModel.chooseBuff2.Name;
        TMPTexts["2Description"].text = ViewModel.chooseBuff2.Description;
        TMPTexts["3Name"].text = ViewModel.chooseBuff3.Name;
        TMPTexts["3Description"].text = ViewModel.chooseBuff3.Description;

        TMPTexts["RefreshCost"].text = $"刷新（{VMContainer.Get<BattleViewModel>().RefeshCount}）";
    }
}