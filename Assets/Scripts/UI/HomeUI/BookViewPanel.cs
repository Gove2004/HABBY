using System.ComponentModel;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

public class BookViewPanel : ViewPanel<BookViewModel>
{
    public override void OnShow(object payload = null)
    {
        base.OnShow(payload);

        ShowEnemy();  // 默认显示敌人
    }



    protected override void OnDataChanged(object sender, PropertyChangedEventArgs e)
    {
        
    }


    protected override void OnButtonClicked(string btnName)
    {
        switch (btnName)
        {
            case "Back":
                OnBackBtnClicked();
                break;
            case "Enemy":
                ShowEnemy();
                break;
            case "Buff":
                ShowBuff();
                break;
            case "Hero":
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
            GameObject row = Object.Instantiate(RowContainer, bookItemContainer);
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
        Images["Icon"].sprite = item.Icon;
        TMPTexts["Name"].text = item.Name;
        TMPTexts["Description"].text = item.Description;
    }
}
