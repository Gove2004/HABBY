


using System;
using DG.Tweening;
using GoveKits.Runtime.UI;
using UnityEngine;

public class ChooseViewPanel : ViewPanel<ChooseViewModel>
{
    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

        Refresh();
        AudioManager.Instance.PlayLevelUp();

        ShowAnimation();
        Time.timeScale = 0f; // 暂停游戏
    }

    private void ShowAnimation()
    {
        transform.DOScale(Vector3.one, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public override void OnHide()
    {
        HideAnimation(base.OnHide);
    }

    private void HideAnimation(Action onComplete)
    {
        // 向左侧飞出
        transform.DOScale(Vector3.zero, 0.5f).From(Vector3.one).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() => {
            onComplete?.Invoke();
            Time.timeScale = 1f; // 恢复游戏
        });
    }




    protected override void OnDataChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        
    }

    protected override void OnButtonClicked(string btnName)
    {
        switch (btnName)
        {
            case "1":
                AudioManager.Instance.PlaySelect();
                onChoose(1);
                break;
            case "2":
                AudioManager.Instance.PlaySelect();
                onChoose(2);
                break;
            case "3":
                AudioManager.Instance.PlaySelect();
                onChoose(3);
                break;
            case "Refresh":
                AudioManager.Instance.PlayLevelUp();
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