using System;
using System.ComponentModel;
using DG.Tweening;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

public class BookViewPanel : ViewPanel<BookViewModel>
{
    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

        ShowAnimation();
        ShowEnemy();  // 默认显示敌人
    }

    private void ShowAnimation()
    {
        transform.DOScale(Vector3.one, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack);
    }

    public override void OnHide()
    {
        HideAnimation(base.OnHide);
    }

    private void HideAnimation(Action onComplete)
    {
        // 向左侧飞出
        transform.DOScale(Vector3.zero, 0.5f).From(Vector3.one).SetEase(Ease.InBack).OnComplete(() => {
            onComplete?.Invoke();
        });
    }



    protected override void OnDataChanged(object sender, PropertyChangedEventArgs e)
    {
        
    }


    protected override void OnButtonClicked(string btnName)
    {
        switch (btnName)
        {
            case "Back":
                AudioManager.Instance.PlayUIClick();
                OnBackBtnClicked();
                break;
            case "Enemy":
                AudioManager.Instance.PlayUIClick();
                ShowEnemy();
                break;
            case "Buff":
                AudioManager.Instance.PlayUIClick();
                ShowBuff();
                break;
            case "Hero":
                AudioManager.Instance.PlayUIClick();
                ShowHero();
                break;
        }
    }


    private void OnBackBtnClicked()
    {
        Controller.Back();
    }

    private void ShowEnemy()
    {
        ClearBookItems();
        LoadBookItem(ViewModel.GetAllEnemys());
    }

    private void ShowBuff()
    {
        ClearBookItems();
        LoadBookItem(ViewModel.GetAllBuffs());
    }

    private void ShowHero()
    {
        ClearBookItems();
        LoadBookItem(ViewModel.GetAllHeros());
    }

    public GameObject bookItemPrefab;  //  
    public GameObject RowContainer;
    public Transform bookItemContainer;
    private void LoadBookItem(BookItem[] bookItems)
    {
        // 每4个一行
        int rowCount = Mathf.CeilToInt(bookItems.Length / 4f);
        for (int r = 0; r < rowCount; r++)
        {
            GameObject row = UnityEngine.Object.Instantiate(RowContainer, bookItemContainer);
            row.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                int index = r * 4 + i;
                if (index >= bookItems.Length) break;

                GameObject item = Instantiate(bookItemPrefab, row.transform);
                BookUIItem bookItemUI = item.GetComponent<BookUIItem>();
                bookItemUI.SetData(bookItems[index]);
                bookItemUI.OnClick += OnBookItemClicked;
            }
        }
    }


    private void ClearBookItems()
    {
        foreach (Transform child in bookItemContainer)
        {
            Destroy(child.gameObject);
        }
    }


    private void OnBookItemClicked(BookItem item)
    {
        AudioManager.Instance.PlayUIClick();
        
        Images["Icon"].sprite = item.Icon;
        TMPTexts["Name"].text = item.Name;
        TMPTexts["Description"].text = item.Description;
    }
}
